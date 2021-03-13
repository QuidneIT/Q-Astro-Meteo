/* ----------------------------------------------------------------------------------------------------------------------------*/ 
/*  Start of Meteo Commands */

#include "MeteoData.h"

//Cloud Model Variables. In function initMeteo, these will be populated with the default valies defines in defines.h
int K1 = 0;
int K2 = 0;
int K3 = 0;
int K4 = 0;
int K5 = 0;
int K6 = 0;
int K7 = 0;
double CLOUD_TEMP_OVERCAST = 0;
double CLOUD_TEMP_CLEAR = 0;
double CLOUD_FLAG_PERCENT = 0;
double TLSCORRECTIONFACTOR = 0;

void DoMeteoAction(String cmd)
{
    switch ((char)cmd[0])
    {
    case 'a':   //Altitude
        SendResponse(meteoId, String(Altitude));
        break;
    case 'b':   //Sky Brightness
        SendResponse(meteoId, String(averagedluxreading));
        break;
    case 'c':   //CloudCover
        SendResponse(meteoId, String(cloudcover));
        break;
    case 'd':   //DewPoint
        SendResponse(meteoId, String(DewPoint));
        break;
    case 'h':   //Humidity
        SendResponse(meteoId, String(Humidity));
        break;
    case 'k':   //Receive or Send the Cloud Model values
        processCloudModelData(cmd.substring(1, (cmd.length() - 1))); //pass the full command received but without the closing #
        break;
    case 'l':   //Receive or Send the Cloud Temp values
        processCloudTempSettings(cmd.substring(1, (cmd.length() - 1))); //pass the full command received but without the closing #
        break;
    case 'm':  //All Data
        returnAllData();
        break;
    case 'n':   //Diagnostics Data
        returnDiagnosticsData();
        break;
    case 'o':   //Observatory Temp
        SendResponse(meteoId, String(ObsTemp));
        break;
    case 'p':   //Pressure
        SendResponse(meteoId, String(Pressure));
        break;
    case 'q':   //Sky Quality
        SendResponse(meteoId, String(mySQMreading));
        break;
    case 'r':   //Raining
        SendResponse(meteoId, String(raining));
        break;
    case 's':   //SkyState
        SendResponse(meteoId, String(skystate));
        break;
    case 't':   //Sky Temp
        SendResponse(meteoId, String(mlx90614object));
        break;
    case 'u':   //Receive or Send the Cloud Model values
        processLuxSettings(cmd.substring(1, (cmd.length() - 1))); //pass the full command received but without the closing #
        break;
    case 'v':   //RainRate
        SendResponse(meteoId, String(rainRate));
        break;
    }
}

void processCloudModelData(String cmd)
{
    switch ((char)cmd[0])
    {
    case 's':   //Set newly received Cloud Model Values from ASCOM
        setCloudModelData(cmd.substring(1));
        break;
    case 'g':   //Send current Cloud Model Values to ASCOM
        returnAllCloudModelData();
        break;
    }
}

void processCloudTempSettings(String cmd)
{
    switch ((char)cmd[0])
    {
    case 's':   //Set newly received Cloud Temp Values from ASCOM
        setCloudTempSettings(cmd.substring(1));
        break;
    case 'g':   //Send current Cloud Temp Values to ASCOM
        returnCloudTempSettings();
        break;
    }
}

void processLuxSettings(String cmd)
{
    switch ((char)cmd[0])
    {
    case 's':
        setLuxSettings(cmd.substring(1));
        break;
    case 'g':
        returnLuxMultiplier();
        break;
    }
}

void setCloudTempSettings(String cmd)
{
    int cValues = 3;
    for (int i = 0; i < cValues; i++)
    {
        String item = "";

        if (i < (cValues - 1))
        {
            int pos = cmd.indexOf("_");
            item = cmd.substring(0, (pos));
            cmd = cmd.substring(pos + 1);
        }
        else
            item = cmd;

        String cItem = item.substring(0, 3);
        int cValue = item.substring(3).toInt();

#ifdef DEBUG
        Serial.println(String(i) + "- " + item + " : " + cItem + " : " + String(cValue));
#endif

        switch ((char)cItem[2])
        {
        case 'O': //CTO
            CLOUD_TEMP_OVERCAST = cValue;
            break;
        case 'C': //CTC
            CLOUD_TEMP_CLEAR = cValue;
            break;
        case 'P': //CFP
            CLOUD_FLAG_PERCENT = cValue;
            break;
        }
    }
    returnCloudTempSettings();
}

void returnCloudTempSettings()
{
    String returnData = "";
    returnData += "CTO" + String(CLOUD_TEMP_OVERCAST) + "_";
    returnData += "CTC" + String(CLOUD_TEMP_CLEAR) + "_";
    returnData += "CFP" + String(CLOUD_FLAG_PERCENT);

    SendResponse(meteoId, returnData);
}

void setCloudModelData(String cmd)
{
    int kValues = 7;
    for (int i = 0; i < kValues; i++)
    {
        String item = "";

        if (i < (kValues - 1))
        {
            int pos = cmd.indexOf("_");
            item = cmd.substring(0, (pos));
            cmd = cmd.substring(pos + 1);
        }
        else
            item = cmd;

        String kNr = item.substring(0, 2);
        int kValue = item.substring(2).toInt();

#ifdef DEBUG
        Serial.println(String(i) + "- " + item + " : " + kNr + " : " + String(kValue));
#endif

        switch ((char)kNr[1])
        {
        case '1':
            K1 = kValue;
            break;
        case '2':
            K2 = kValue;
            break;
        case '3':
            K3 = kValue;
            break;
        case '4':
            K4 = kValue;
            break;
        case '5':
            K5 = kValue;
            break;
        case '6':
            K6 = kValue;
            break;
        case '7':
            K7 = kValue;
            break;
        }
    }
    returnAllCloudModelData();
}

void returnAllCloudModelData()
{
    String returnData = "";
    returnData += "K1" + String(K1) + "_";
    returnData += "K2" + String(K2) + "_";
    returnData += "K3" + String(K3) + "_";
    returnData += "K4" + String(K4) + "_";
    returnData += "K5" + String(K5) + "_";
    returnData += "K6" + String(K6) + "_";
    returnData += "K7" + String(K7);

    SendResponse(meteoId, returnData);
}

void returnLuxMultiplier()
{
    String returnData = "";
    returnData += "LUX" + String(TLSCORRECTIONFACTOR);

    SendResponse(meteoId, returnData);
}

void setLuxSettings(String cmd)
{
    if (cmd.substring(0, 3) == "LUX")
        TLSCORRECTIONFACTOR = cmd.substring(3).toDouble();
}

void returnAllData()
{
    String returnData = "";
    returnData += "o" + String(ObsTemp) + "_";
    returnData += "a" + String(Altitude) + "_";
    returnData += "d" + String(DewPoint) + "_";
    returnData += "h" + String(Humidity) + "_";
    returnData += "p" + String(Pressure) + "_";
    returnData += "s" + String(skystate) + "_";
    returnData += "r" + String(raining) + "_";
    returnData += "v" + String(rainRate) + "_";
    returnData += "c" + String(cloudcover) + "_";
    returnData += "t" + String(mlx90614object) + "_";
    returnData += "q" + String(mySQMreading) + "_";
    returnData += "b" + String(averagedluxreading);

    SendResponse(meteoId, returnData);
}

void returnDiagnosticsData()
{
    String returnData = "";
    returnData += "o" + String(ObsTemp) + "_";
    returnData += "a" + String(Altitude) + "_";
    returnData += "d" + String(DewPoint) + "_";
    returnData += "h" + String(Humidity) + "_";
    returnData += "p" + String(Pressure) + "_";
    returnData += "s" + String(skystate) + "_";
    returnData += "r" + String(raining) + "_";
    returnData += "v" + String(rainRate) + "_";
    returnData += "c" + String(cloudcover) + "_";
    returnData += "t" + String(mlx90614object) + "_";
    returnData += "q" + String(mySQMreading) + "_";
    returnData += "b" + String(averagedluxreading);
    returnData += "x" + String(sensorTemperature) + "_";
    returnData += "u" + String(sensorPower);

    SendResponse(meteoId, returnData);
}


/* Start of ObservingConditions functions */
/* ---------------------------------------------------------------------------------------------------------------------------- */

void initMeteo()
{
    K1 = defK1;
    K2 = defK2;
    K3 = defK3;
    K4 = defK4;
    K5 = defK5;
    K6 = defK6;
    K7 = defK7;

    CLOUD_TEMP_OVERCAST = defCLOUD_TEMP_OVERCAST;
    CLOUD_TEMP_CLEAR = defCLOUD_TEMP_CLEAR;
    CLOUD_FLAG_PERCENT = defCLOUD_FLAG_PERCENT;

    TLSCORRECTIONFACTOR = defTLSCORRECTIONFACTOR;

    bme.begin();
}

void initRainSensor()
{
    raining = 'I';                        //Initialising
    pinMode(RAINSENSOR_OUT_PIN, OUTPUT);
    pinMode(RAINSENSOR_IN_PIN, OUTPUT);
    pinMode(RAINSENSOR_HEATER_PIN, OUTPUT);
}

void updateMeteoData()
{
    updateBMEData();
    updateRainSensor();
    updateHeaterPower();
        
    readmlx90614sensor();
    getskystate();
    getlux();
}

void updateMeteoDataTimer()
{
    if (millis() - timeOfLastMeteoUpdate > METEO_UPDATE_INTERVAL)
    {
        updateMeteoData();
        timeOfLastMeteoUpdate = millis();
    }
}

void updateBMEData()
{
    float logEx;

    Humidity = bme.readHumidity();
    // Read temperature as Celsius (the default)
    ObsTemp = bme.readTemperature();
    Altitude = bme.readAltitude(SEA_LEVEL_PRESSURE_HPA);
    Pressure = round((bme.readPressure() / 100.0F));

    // Check if Temp or Humidity data from BME280 has an error
    if (isnan(ObsTemp) || isnan(Humidity))
    {
        // if error reading BME280 set all to 0
        ObsTemp = 0;
        Humidity = 0;
        DewPoint = 0;
        Pressure = 0;
    }
    else {
        // if no error reading BME280 calc dew point
        // more complex dew point calculation
        logEx = 0.66077 + 7.5 * ObsTemp / (237.3 + ObsTemp) + (log10(Humidity) - 2);
        DewPoint = (logEx - 0.66077) * 237.3 / (0.66077 + 7.5 - logEx);

        if (isnan(DewPoint))
            DewPoint = 0;
    }
}

void updateRainSensor()
{
    pinMode(RAINSENSOR_IN_PIN, INPUT);
    digitalWrite(RAINSENSOR_OUT_PIN, HIGH);
    int val = analogRead(RAINSENSOR_IN_PIN);
    digitalWrite(RAINSENSOR_OUT_PIN, LOW);

    pinMode(RAINSENSOR_IN_PIN, OUTPUT);
    rainCapacitance = (float)val * IN_CAP_TO_GND / (float)(MAX_ADC_VALUE - val);

    if (rainCapacitance < 100)      //Capacitance ~ 88 - 89pF
    {
        raining = 'D';
        rainRate = RATE_DRY;
    }
    else if ((rainCapacitance > (CAP_DRY - 1)) && (rainCapacitance < CAP_LIGHT))   //Light Rain
    {
        raining = 'L';
        rainRate = RATE_LIGHT_RAIN;
    }
    else if ((rainCapacitance > (CAP_LIGHT - 1)) && (rainCapacitance < CAP_MODERATE))  //Moderate Rain
    {
        raining = 'M';
        rainRate = RATE_MODERATE_RAIN;
    }
    else if ((rainCapacitance > (CAP_MODERATE - 1)) && (rainCapacitance < CAP_HEAVY))  //Heavy Rain
    {
        raining = 'R';
        rainRate = RATE_HEAVY_RAIN;
    }
    else if (rainCapacitance > (CAP_HEAVY - 1))   //Violent Rain
    {
        raining = 'H';
        rainRate = RATE_VIOLENT_RAIN;
    }
}

// If the device is above MIN_DEVICE_TEMP use the environment Dew Point to keep the device dry by heating it to DEWPOINT_THRESHOLD above Dew Point.
// Else if the device temp is below MIN_DEVICE_TEMP then heat the device to DEWPOINT_THRESHOLD above MIN_DEVICE_TEMP. This is to ensures that the device never freezes over.
// Both DEWPOINT_THRESHOLD and MIN_DEVICE_TEMP can be changed in definitions.h 
void updateHeaterPower()
{
    int heaterPower = 0;
    double sensorTemperature = getThermistorSensorTemp();

    if (sensorTemperature != -273.15)
    {
        if (sensorTemperature > MIN_DEVICE_TEMP)
            heaterPower = calcHeaterPowerSetting(sensorTemperature, DewPoint);
        else
            heaterPower = calcHeaterPowerSetting(sensorTemperature, MIN_DEVICE_TEMP);
    }

    analogWrite(RAINSENSOR_HEATER_PIN, heaterPower);   // set the PWM value to be 0-254    
}

double getThermistorSensorTemp()
{
    float VRT;
    float VR;
    float RT;
    float ln;

    VRT = analogRead(RAINSENSOR_THERMISTOR_PIN);                  //Acquisition of analog value of VRT
    VRT = (5.00 / MAX_ADC_VALUE) * VRT;                           //Conversion to voltage
    VR = VCC - VRT;
    RT = VRT * (R / VR);                                          //Resistance of RT
    ln = log(RT / RT0);
    sensorTemperature = ((1 / ((ln / B) + (1 / VT0))) - 273.15);  //Calculate Temperature from thermistor and conversion to Celsius

    return sensorTemperature;
}

// Provide device temp and min temp (or Dew Point). 
// Then this will calculate the power needed to keep the device atleast 5c above the min temp.
int calcHeaterPowerSetting(double SensorTemp, double minTemp)
{
    double tempDiff = 0;                                              // set output duty cycle on temp diff between Rain Sensor Temp and ambient dew point 
    double requiredSensorPower = 0;

    tempDiff = (minTemp + DEWPOINT_THRESHOLD) - SensorTemp;           // Heater ON if  temp Diff  >  SensorTemp - (Dew Point(C) + Threshold(C))
    tempDiff = constrain(tempDiff, 0.0, DEWPOINT_THRESHOLD);       // restrict between 0 & threshold
    sensorPower = MAX_DEWPOWER * (tempDiff / DEWPOINT_THRESHOLD);   // PWM 0 - 100% duty cycle EQUIV TO analog 0 - 254

    return sensorPower;
}

/* ---------------------------------------------------------------------------------------------------------------------------- */
/* End of Meteo functions */
