//The design of Q-Astro Meteo uses mySQM+ by Robert Brown as a bases.
//The details for mySQM+ can be found here: https://sourceforge.net/projects/mysqmproesp32/

//This file contains the functions used by Q-Astro Meteo. The ones not used have been removed from the file.

// ------------------------------------------------------------------------
// mySQM+ A DIY WEATHER STATION + SKY QUALITY METER Project using ESP32
// OFFICIAL FIRMWARE RELEASE 106
// ------------------------------------------------------------------------
// Cloud Sensor, Rain Sensor, Barometric Sensor, GPS, Lux Sensor, SQM,
// Wind Speed, Wind Direction, Rain Bucket Gauge, RTC, NTP, Webserver,
// MQTT, Weather Underground,
// Latest PCB rev 11
// Supports myESP32 Daughter Board with remote sensors via JSON/ESPNOW/MQTT
//
// ------------------------------------------------------------------------
// With special thanks to
// ------------------------------------------------------------------------
// Paul Porters
// Carlos Muñoz
// Tommy Lee
// Pete
// Andrew
// for their support, help, advice, testing, suggestions and code which
// really improved this firmware

// ------------------------------------------------------------------------
// COPYRIGHT
// ------------------------------------------------------------------------
// (c) Copyright Robert Brown 2020 - All Rights Reserved. Main author.
// (c) Copyright Holger M, 2020    - SPIFFS peristent data
// (c) Copyright Pieter P          - SPIFFs file handling examples
// (c) Copyright Nacho Mas         - SkyModel (INDIDUINOMETEO)
// (c) Copyright Paul Porters      - Enhancements to SkyModel
//
// ------------------------------------------------------------------------
// SPECIAL LICENSE
// ------------------------------------------------------------------------
// This code is released under license. If you copy or write new code based
// on the code in these files you MUST include to link to these files AND
// you MUST include references to the author(s) of this code.

// ------------------------------------------------------------------------
// CONTRIBUTIONS
// ------------------------------------------------------------------------
// Please donate a contribution in thanks for this project, use PayPal and
// send your contribution to user rbb1brown@gmail.com (Robert Brown). All
// contributions are gratefully accepted and used to further these projects.

float   mlx90614ambient;                        // ir sky ambient - uses optical filter from 550nm - 1400nm
//float   mlx90614object;                         // ir sky object
//int     rainVout;                               // millivolts rain out, 4320
//double  volts;                                  // voltage of rain vout, 4.32
//double  scalefactor = 0.0008056640625;          // analog pins have values from 0-3.3V at 0-4095, each value is 3.3/4096 = 0.0008056640625 V
double  tsky;                                   // corrected temperature of sky
float   nelm;                                   // nelm reading (Naked Eye Limit Measurement)
float   IR_630_970nm;                           // IR reading from TSL2591 sensor 630nm - 970nm
byte    gain;                                   // TSL2591 gain setting
byte    itime;                                  // TSL2591 integration time setting

boolean tsl2591found = true;
boolean mlx90614found = true;
boolean rtcpresent;

unsigned long luxtimer;                         // timer used to get lux value
unsigned long bme280timer;                      // timer used to get bme280 values
unsigned long mlx90614timer;                    // timer used to get mlx90614 values

AsyncTSL2591 myTSL2591;    
Adafruit_MLX90614 mymlx90614 = Adafruit_MLX90614();         // IR sensor, used by cloud model

//Q-Astro Meteo mySQL+ Initialise Function
void initSQM()
{
  // initialize all values to known state
  averagedluxreading = 1000.0;
  mySQMreading = 5.08355;
  nelm = -8.56753;

  mlx90614ambient = 20.0;
  mlx90614object = 20.0;

  skystate = SKYCLEAR;
  cloudcover = 0.0;

  tsl2591found = false;                                         // start TSL2591 sensor
  gain = TSL2591_GAIN_MAX;
  itime = TSL2591_TIME_500MS;
  if ( myTSL2591.begin((TSL2591_GAIN) gain, (TSL2591_TIME) itime) == true)
  {
    tsl2591found = true;
    luxtimer = millis();
  }

  // mlx90614.begin() does not return a status so check for it manually
  Wire.beginTransmission(MLX90614_I2CADDR);                     // start MLX90614 sensor
  if (Wire.endTransmission() != 0)
    mlx90614found = false;
  else
  {
    mlx90614found = true;
    mymlx90614.begin();
    mlx90614timer = millis();
  }
}

// ------------------------------------------------------------------------
// READ MLX90614 SENSOR
// ------------------------------------------------------------------------
void readmlx90614sensor(void)
{
  unsigned long timenow = millis();

  // check if time has expired, get mlx90614 values every 6s
  if (((timenow - mlx90614timer) > MLX90614UPDATETIME) || (timenow < mlx90614timer))
  {
    mlx90614timer = millis();
    if ( mlx90614found == true )
    {
      mlx90614ambient = mymlx90614.readAmbientTempC();          //Ambient Temp is the temp of the MLX90614 itself
      mlx90614object = mymlx90614.readObjectTempC();            //Object Temp is the temp we use to determine cloudy or not.
    }
  }
}

// ------------------------------------------------------------------------
// GET SIGN OF A NUMBER, used by getskystate()
// ------------------------------------------------------------------------
double getsign(double d)
{
  if (d < 0) return -1.;
  if (d == 0) return 0.;
  return 1.;
}

// ------------------------------------------------------------------------
// CLOUD MODEL MLS90614 SENSOR IS REQUIRED
// ------------------------------------------------------------------------
void getskystate()
{
  // Readings are only valid at night when dark and sensor is pointed to sky
  // During the day readings are meaningless

  // cloud model
  double t67;
  if ( abs((K2 / 10. - mlx90614ambient)) < 1 )
  {
    // t67 = Math.Sign(k6) * Math.Sign(ambient - k2 / 10) * Math.Abs((k2 / 10 - ambient));
    t67 = getsign(K6) * getsign(mlx90614ambient - K2 / 10.) * abs((K2 / 10. - mlx90614ambient));
  }
  else
  {
    // t67 = k6 / 10 * Math.Sign(ambient - k2 / 10) * (Math.Log(Math.Abs((k2 / 10 - ambient))) / Math.Log(10) + k7 / 100);
    t67 = K6 / 10. * getsign(mlx90614ambient - K2 / 10.) * (log(abs((K2 / 10. - mlx90614ambient))) / log(10) + K7 / 100);
  }

  // double Td = (K1 / 100.) * (T - K2 / 10) + (K3 / 100.) * pow((exp (K4 / 1000.* T)) , (K5 / 100.)) + t67;
  double td = (K1 / 100.) * (mlx90614ambient - K2 / 10.) + (K3 / 100.) * pow((exp(K4 / 1000. * mlx90614ambient)), (K5 / 100.) + t67);

  // the corrected sky temperature (Tsky) is given by:
  tsky = mlx90614object - td;
  tskycorrected = tsky;
  // now calculate cloud index
  double tcloudy = CLOUD_TEMP_OVERCAST;
  double tclear  = CLOUD_TEMP_CLEAR;
  if ( tsky < tclear )
  {
    tsky = tclear;
  }
  else if ( tsky > tcloudy )
  {
    tsky = tcloudy;
  }
  cloudcover = ((tsky - tclear) * 100.) / (tcloudy - tclear);
  if ( cloudcover > CLOUD_FLAG_PERCENT )
  {
    skystate = SKYCLOUDY;
  }
  else
  {
    skystate = SKYCLEAR;
  }
}

// ------------------------------------------------------------------------
// LUX SENSOR: DO NOT CHANGE ANY OF THIS CODE
// ------------------------------------------------------------------------
void configuresensor( byte newgain, byte inttime )
{
  if ( tsl2591found == true )
  {
    // It is recommended that the ADC_EN be disabled before changing the analog
    // (GAIN) in the Analog register (0x07) because the change will affect the
    // integration in progress, yielding an indeterminate result.
    myTSL2591.poweroff();
    myTSL2591.setGain((TSL2591_GAIN) newgain);
    // The integration time (ITIME) in the Timing register (0x01) can be changed
    // at any time; however, the change will take effect only upon completion of
    // the current ALS cycle
    myTSL2591.setTiming((TSL2591_TIME) inttime);
  }
}

void getlux()                                               // get lux reading
{
  static float    luxreadingovertime = 1000.;               // used for kickstart
  static int      kickcount = 0;                            // counter used by kickstart
  static boolean  sensorenabled = false;                    // used to trigger a reading
  unsigned long   newupdate;                                // timer to do periodic updates of lux
  static int      skipcount = 0;

  newupdate = millis();
  if ( tsl2591found == true )                               // check if there is a sensor
  {
    // check if time has expired, get lux light level every 2s
    if ( ((newupdate - luxtimer) > TSL2591UPDATETIME ) || (newupdate < luxtimer))
    {
      luxtimer = newupdate;
      gain = myTSL2591.getGain();
      itime = myTSL2591.getTiming();

      // Get lux
      if ( sensorenabled == false )
      {
        sensorenabled = true;
        myTSL2591.startLuminosityMeasurement();             // powers on sensor, start sensor measurement
      }
      else
      {
        if ( myTSL2591.isMeasurementReady() != 0 )          // check if measurement ready
        {
          uint16_t ch1_ir;                                  // ch1 infra-red spectrum
          uint16_t ch0_full;                                // ch0 full spectrum - both visible and infrared light
          float    tlux;                                    // sensor lux reading
          uint32_t lum;                                     // luminosity

          sensorenabled = false;
          lum = myTSL2591.getFullLuminosity();              // power off device and get full luminosity reading
          
          ch1_ir = lum >> 16;                               // get ir count ch1 from lum
          IR_630_970nm = ch1_ir;                            // this is an uncalibrated IR count
          ch0_full = lum & 0xFFFF;                          // Get visible count ch0 from lum
          tlux = myTSL2591.calculateLux(ch0_full, ch1_ir) * TLSCORRECTIONFACTOR;  // calculate lux value

          // if ( ch0_full == 65535 )                       // check for saturation
          if ( ch0_full >= 50000 )
          {
            skipcount++;
            if ( skipcount > 3)                             // avoid changing gain if just a single blip
            {
              skipcount = 0;
              switch ( gain )
              {
                case TSL_GAIN_LOW:
                  break;
                case TSL_GAIN_MED:
                  gain = TSL_GAIN_LOW;
                  break;
                case TSL_GAIN_HIGH:
                  gain = TSL_GAIN_MED;
                  break;
                case TSL_GAIN_MAX:
                  gain = TSL_GAIN_HIGH;
                  break;
              }
              configuresensor( gain, itime );
              // ignore this value;
              return;
            }
            else
            {
              return;
            }
          }

          // check for saturation
          // if ( ch1_ir == 65535 )
          if ( ch1_ir >= 50000 )
          {
            skipcount++;
            if ( skipcount > 3)                             // avoid changing gain if just a single blip
            {
              skipcount = 0;
              switch ( itime )
              {
                case TSL_INTEGRATIONTIME_LOW:
                  break;
                case TSL_INTEGRATIONTIME_200MS:
                  itime = TSL_INTEGRATIONTIME_LOW;
                  break;
                case TSL_INTEGRATIONTIME_MED:
                  itime = TSL_INTEGRATIONTIME_200MS;
                  break;
                case TSL_INTEGRATIONTIME_400MS:
                  itime = TSL_INTEGRATIONTIME_MED;
                  break;
                case TSL_INTEGRATIONTIME_HIGH:
                  itime = TSL_INTEGRATIONTIME_400MS;
                  break;
                case TSL_INTEGRATIONTIME_MAX:
                  itime = TSL_INTEGRATIONTIME_HIGH;
                  break;
              }
              configuresensor( gain, itime );
              // ignore this value;
              return;
            }
            else
            {
              return;
            }
          }

          if ( ch0_full == 0 )                              // check for not sensitive enough
          {
            skipcount++;
            if ( skipcount > 3)                             // avoid changing gain if just a single blip
            {
              skipcount = 0;
              switch ( gain )
              {
                case TSL_GAIN_LOW:
                  gain = TSL_GAIN_MED;
                  break;
                case TSL_GAIN_MED:
                  gain = TSL_GAIN_HIGH;
                  break;
                case TSL_GAIN_HIGH:
                  gain = TSL_INTEGRATIONTIME_MAX;
                  break;
                case TSL_GAIN_MAX:
                  break;
              }
              configuresensor( gain, itime );
              // ignore this value;
              return;
            }
            else
            {
              return;
            }
          }

          if ( ch1_ir == 0 )                                // check for not sensitive enough
          {
            skipcount++;
            if ( skipcount > 3)                             // avoid changing gain if just a single blip
            {
              skipcount = 0;
              switch ( itime )
              {
                case TSL_INTEGRATIONTIME_LOW:
                  itime = TSL_INTEGRATIONTIME_200MS;
                  break;
                case TSL_INTEGRATIONTIME_200MS:
                  itime = TSL_INTEGRATIONTIME_MED;
                  break;
                case TSL_INTEGRATIONTIME_MED:
                  itime = TSL_INTEGRATIONTIME_400MS;
                  break;
                case TSL_INTEGRATIONTIME_400MS:
                  itime = TSL_INTEGRATIONTIME_HIGH;
                  break;
                case TSL_INTEGRATIONTIME_HIGH:
                  itime = TSL_INTEGRATIONTIME_MAX;
                  break;
                case TSL_INTEGRATIONTIME_MAX:
                  break;
              }
              configuresensor( gain, itime );
              // ignore this value;
              return;
            }
            else
            {
              return;
            }
          }

          if ( tlux == (float) .0 )                                   // avoid 0
          {
            tlux = (float) 0.0001;                                    // sqm = 20.08355939
          }
          if ( tlux < (float) 0.00001 )                               // sqm = 25.08355939
          {
            tlux = (float) 0.00001;
          }
          averagedluxreading = tlux;

          if ( luxreadingovertime == averagedluxreading )             // begin kickstart
          {
            kickcount++;
            // if number of readings that are the same are greater than threshold then kickstart
            if ( kickcount > KICKSTARTTHRESHHOLD )
            {
              kickcount = 0;
              gain = TSL_GAIN_MAX;
              configuresensor( gain, itime );
            }
          }

          luxreadingovertime = averagedluxreading;                    // save previous reading
          mySQMreading = log10(averagedluxreading / 108000) / -0.4;   // calculate SQM
          nelm = (float) 7.93 - 5.0 * log10((pow(10, (4.316 - (mySQMreading / 5.0))) + (float)1.0));
        } // if ( myTSL2591.isMeasurementReady() == 0 )
      } // if ( sensorenabled == false )
    }
  } // if ( timecheck(readluxtimer, TSL2591UPDATETIME))
}
