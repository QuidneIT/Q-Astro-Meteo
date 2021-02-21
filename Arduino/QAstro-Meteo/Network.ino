//Credit for part of this code (Telnet Connection Management) goes to Tomas Salazar: https://gist.github.com/atomsfat/1813823

#if defined(ARDUINO_SAMD_NANO_33_IOT)

#include "Network.h"

char ssid[] = SECRET_SSID;        // your network SSID (name)
char pass[] = SECRET_PASS;    // your network password (use for WPA, or use as key for WEP)

int status = WL_IDLE_STATUS;

bool alreadyConnected = false; //we'll use a flag separate from client.connected so we can recognize when a new connection has been created
         
unsigned long timeOfLastActivity; //time in milliseconds of last activity

WiFiServer telnetServer(TELNET_PORT);
WiFiClient serverClient;

void initNetwork() 
{
   pinMode(LED_BUILTIN, OUTPUT);
  
  if (STATICIP == 1)
    WiFi.config(myIP,myDNS,myGW,mySN);
    
  // attempt to connect to Wifi network:
  while (status != WL_CONNECTED) 
  {
    Serial.print("Attempting to connect to SSID: ");
    Serial.println(ssid);

    // Connect to WPA/WPA2 network. Change this line if using open or WEP network:
    status = WiFi.begin(ssid, pass);
    // wait 5 seconds for connection:
    digitalWrite(LED_BUILTIN, LOW);   // turn the LED on (HIGH is the voltage level)
    delay(10000);
  }
  digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)

  // start the server:
  telnetServer.begin();
}

void networkEvent()
{
  serverClient = telnetServer.available();
  
  if (serverClient)
  {
    if (!alreadyConnected)
    {
      flushConnection();
//      SendNetworkCommand("Connected to " + String(DEVICE_RESPONSE));
      alreadyConnected = true;
#ifdef DEBUG
      Serial.println("Client Connection Established");
#endif
    }
    checkIncommingRequests();
  }
//  if (alreadyConnected)
//    checkConnectionTimeout();
}

void flushConnection()
{
  timeOfLastActivity = millis();
  serverClient.flush();
  telnetServer.flush();
}

void checkIncommingRequests()
{
    ASCOMcmdComplete = false;
    ASCOMcmd = "";

  while (serverClient.available())
  {
    char inChar = serverClient.read();
    ASCOMcmd += inChar;

    if (inChar == '#')
    {
      ASCOMcmdComplete = true;
      dataSource = SOURCENETWORK;
      flushConnection();
    }
  }
}

void checkConnectionTimeout()
{
  if(millis() - timeOfLastActivity > ALLOWED_CONNECTION_TIME)
  {
    closeConnection();
#ifdef DEBUG
    Serial.println("Timeout");
#endif
  }
}

void closeConnection()
{
  SendNetworkCommand("Bye");
  serverClient.stop();
  alreadyConnected = false;
#ifdef DEBUG
  Serial.println("Network Connection Closed!");
#endif
}

void SendNetworkCommand(char function, String sendCmd)
{
    SendNetworkCommand(String(function) + sendCmd);
}

void SendNetworkCommand(String sendCmd)
{
  if (alreadyConnected)
  {
    flushConnection();
    serverClient.print(sendCmd); 
    serverClient.println("#");  // Similarly, so ASCOM knows
#ifdef DEBUG
    Serial.println(sendCmd + "#");
#endif
  }
}

void printWifiStatus() 
{
  // print the SSID of the network you're attached to:
  Serial.print("SSID: ");
  Serial.println(WiFi.SSID());

  // print your board's IP address:
  IPAddress ip = WiFi.localIP();
  Serial.print("IP Address: ");
  Serial.println(ip);

  // print the received signal strength:
  long rssi = WiFi.RSSI();
  Serial.print("signal strength (RSSI):");
  Serial.print(rssi);
  Serial.println(" dBm");
}
#endif
