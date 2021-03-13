/*
 * Q-Astro Meteo Monitor
 *
 * Q-Astro Meteor Code.
 * Version: 1.6.0
 * 
 * Copyright (c)2021 Quidne IT Ltd.
 * 
 * This Arduino Code is designed for either the Arduino Nano or Arduino Nano 33 IoT
 * The compiler is able to determine which one is used and include or exclude code
 * if needed.
 * 
 */ 

#define DEVICE_RESPONSE "Q-Astro Meteo ver 1.6.1"
 
#include <Arduino.h>
#include <math.h>
#include <QAstro_BME280.h>

#if defined(ARDUINO_SAMD_NANO_33_IOT)
	#include <WiFiNINA.h>
#endif

//mySQM+ Libraries
#include "mySQMDefines.h"
#include <myMLX90614AF.h>
#include <myAsyncTSL2591.h>                     // Adafruit, modified

#define qastroId 'i'
#define meteoId 'm'

#define SOURCESERIAL 0
#define SOURCENETWORK 1

#define DEBUG 0

String ASCOMcmd = "";
bool ASCOMcmdComplete = false;
int dataSource = SOURCESERIAL;

void setup()
{
    Serial.println("Init Serial..");
    initSerial();

#if defined(ARDUINO_SAMD_NANO_33_IOT)
    Serial.println("Init Network..");
    initNetwork();
    printWifiStatus();
#endif

    Serial.println("Init SQM..");
    initSQM();

    Serial.println("Init Meteo..");
    initMeteo();

    Serial.println("Init Rain Sensor..");
    initRainSensor();

    Serial.println("Collect Initial Data..");
    updateMeteoData();

    Serial.println("Start Main Loop..");
}

void loop() 
{
    dataSource = SOURCESERIAL;

#if defined(ARDUINO_SAMD_NANO_33_IOT)
    networkEvent();
#endif
    serialEvent();

  if (ASCOMcmdComplete) {

    switch((char)ASCOMcmd[0]) {
      case qastroId:
        SendResponse(DEVICE_RESPONSE);
      break;

#if defined(ARDUINO_SAMD_NANO_33_IOT)
      case 'c':
        checkCloseConnection();
      break;
#endif
      
      case meteoId: //Case fhe function is for the Environmentals
        DoMeteoAction(ASCOMcmd.substring(1)); //Remove first char from string as this is the function type.
      break;
    }
    ASCOMcmdComplete = false;
    ASCOMcmd = "";
  }
  updateMeteoDataTimer();
}

void SendResponse(char function, String sendCmd)
{
  SendResponse(String(function) + sendCmd);
}

void SendResponse(String sendCmd)
{
  switch(dataSource) {
    case SOURCESERIAL:       // Request Source from Serial
      SendSerialCommand(sendCmd);
    break;
#if defined(ARDUINO_SAMD_NANO_33_IOT)   
    case SOURCENETWORK:       // Request Source from Network
      SendNetworkCommand(sendCmd);
    break;
#endif
  }
}

// if we got here, ASCOMcmd[0] = 'c', check the next 
// character to make sure the command is valid
#if defined(ARDUINO_SAMD_NANO_33_IOT)
void checkCloseConnection()
{
  if (ASCOMcmd[1] == 'l')
    closeConnection();
}
#endif
