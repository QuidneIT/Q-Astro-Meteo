#include <Arduino.h>

//I2C device found at address 0x29  !
//I2C device found at address 0x5A  ! MLX90614
//I2C device found at address 0x60  !
//I2C device found at address 0x6A  !
//I2C device found at address 0x76  ! BME280

/* Capacitive Rain Sensor

This property can be interpreted as 0.0 = Dry any positive non-zero value = wet.

Rainfall intensity is classified according to the rate of precipitation:

Light rain — when the precipitation rate is < 2.5 mm (0.098 in) per hour
Moderate rain — when the precipitation rate is between 2.5 mm (0.098 in) - 7.6 mm (0.30 in) or 10 mm (0.39 in) per hour
Heavy rain — when the precipitation rate is > 7.6 mm (0.30 in) per hour, or between 10 mm (0.39 in) and 50 mm (2.0 in) per hour
Violent rain — when the precipitation rate is > 50 mm (2.0 in) per hour

  Sensitivity     Capacitance   Ratio                     Percipitation
% DRY   % WATER     pF            %       Name              Rate 
  100     0         100           0       Dry               0.0
  75      25        180           80      Light Rain        1.5
  60      40        280           170     Moderate Rain     5.0
  50      50        390           250     Heavy Rain        25
  0       100       >550          >370    Violent Rain      60 
*/

//RainSensor Pins
#define RAINSENSOR_IN_PIN         A0
#define RAINSENSOR_OUT_PIN        A2
#define RAINSENSOR_HEATER_PIN     6
#define RAINSENSOR_THERMISTOR_PIN A1

//Rain Constants based on table above
#define CAP_DRY       100
#define CAP_LIGHT     180
#define CAP_MODERATE  280
#define CAP_HEAVY     390
#define CAP_VIOLENT   550

#define RATE_DRY           0
#define RATE_LIGHT_RAIN    1.5
#define RATE_MODERATE_RAIN 5.0
#define RATE_HEAVY_RAIN    25
#define RATE_VIOLENT_RAIN  60

//Thermistor Constants
#define VT0   293.15          // 20 + 273.15
#define RT0   1218            // R Nominal at 20c (or 293.15K) - Table 2 here: https://radiocontrolli.eu/Capacitive-Rain-Sensor-High-Sensibility-RC-SPC1KA-p242943472
#define R     10000           // R=10K Pullup Resistor
#define B     3480   //3977   // B (Beta) is the constant which describes manner in which the resistance of a thermistor decreases. Beta is measured in degrees Kelvin (K). See XLS provided with B calcs.
                              // https://www.ametherm.com/blog/thermistors/thermistor-beta-calculations.
#define VCC   5               // Supply voltage
#define IN_CAP_TO_GND 24.48
#define MAX_ADC_VALUE 1023
#define SAMPLE_NUMBER 10

//Meteo Constants
#define METEO_UPDATE_INTERVAL 5000      // in milliseconds
#define MIN_DEVICE_TEMP       10      // This is the min temp that needs the device to be kept at. 
#define DEWPOINT_THRESHOLD    5
#define MAX_DEWPOWER          254
#define SEA_LEVEL_PRESSURE_HPA (1013.25)

//Rain Sensor Variables
char raining = '-';     // Undetermined
int rainRate = 0;
float rainCapacitance = 0;
float heaterPower = 0;
double sensorTemperature = 0;
double sensorPower = 0;

// mySQM+ Variable Declarations used by Meteo

//From https://indiduino.wordpress.com/2013/02/02/meteostation/
//Cloudy sky is warmer then clear sky. Thus sky temperature meassure by IR sensor
//is a good indicator to estimate cloud cover. However IR really meassure the
//temperatura of all the air column above increassing with ambient temperature.
//So it is important include some correction factor:
//From AAG Cloudwatcher formula. Need to improve futher.
//http://www.aagware.eu/aag/cloudwatcher700/WebHelp/index.htm#page=Operational%20Aspects/23-TemperatureFactor-.htm
//Sky temp correction factor. Tsky=Tsky_meassure – Tcorrection
//Formula Tcorrection = (K1 / 100) * (Thr – K2 / 10) + (K3 / 100) * pow((exp (K4 / 1000* Thr)) , (K5 / 100));
// Reading material: https://lunatico.es/aagcw/TechInfo/SkyTemperatureModel.pdf
// More: https://lunatico.es/aagcw/enhelp/
// And more: https://indiduino.wordpress.com/2013/02/02/meteostation/

// cloud model defaults
#define defK1  33
#define defK2  0
#define defK3  4
#define defK4  100
#define defK5  100
#define defK6  0
#define defK7  0

//Clear sky corrected temperature (temp below means 0% clouds)
#define defCLOUD_TEMP_CLEAR -8 
//Totally cover sky corrected temperature (temp above means 100% clouds)
#define defCLOUD_TEMP_OVERCAST 0 
//Activation treshold for cloudFlag (%)
#define defCLOUD_FLAG_PERCENT 30

//Lux Adjustment Multiplier
#define defTLSCORRECTIONFACTOR 1.0

int     skystate;                               // skystate
float   averagedluxreading;                     // averaged luminosity reading
float   mySQMreading;                           // the SQM value, sky magnitude
double  cloudcover;                             // % cloud cover
double  tskycorrected;                          // corrected temperature of sky
double  scalefactor = 0.0008056640625;          // analog pins have values from 0-3.3V at 0-4095, each value is 3.3/4096 = 0.0008056640625 V
float   mlx90614object;                         // ir sky object

// Meteo Variable Declarations
double  ObsTemp;                            // ASCOM reading = o
double  Altitude;                           // ASCOM reading = a
double  DewPoint;                           // ASCOM reading = d
int     Humidity;                           // ASCOM reading = h
int     Pressure;                           // ASCOM reading = p

unsigned long timeOfLastMeteoUpdate; //time in milliseconds of last meteo update

bool BME280Error = false;

QAstro_BME280 bme; // I2C
