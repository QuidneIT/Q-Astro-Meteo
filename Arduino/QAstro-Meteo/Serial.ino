/* ---------------------------------------------------------------------------------------------------------------------------- */
/* Start of Serial Commands */

void initSerial()
{
    Serial.flush();
    Serial.begin(9600);  // Baud rate, make sure this is the same as ASCOM driver
    ASCOMcmd = "";
    ASCOMcmdComplete = false;  
}

void serialEvent() 
{
  while (Serial.available()) 
  {
    // get the new byte:
    char inChar = (char)Serial.read();
    // add it to the ASCOMcmd:
    ASCOMcmd += inChar;
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == '#')
        ASCOMcmdComplete = true;
  }
}

void SendSerialCommand(char function, String sendCmd)
{
  SendSerialCommand(String(function) + sendCmd);
}

void SendSerialCommand(String sendCmd)
{
  Serial.print(sendCmd); 
  Serial.println("#");  // Similarly, so ASCOM knows
}

/* ---------------------------------------------------------------------------------------------------------------------------- */
/* End of Serial Commands */
