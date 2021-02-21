//
// ================
// Shared Resources
// ================
//
// This class is a container for all shared resources that may be needed
// by the drivers served by the Local Server. 
//
// NOTES:
//
//	* ALL DECLARATIONS MUST BE STATIC HERE!! INSTANCES OF THIS CLASS MUST NEVER BE CREATED!
//
// Written by:	Bob Denny	29-May-2007
// Modified by Chris Rowland and Peter Simpson to hamdle multiple hardware devices March 2011
//
using System;
using System.Collections.Generic;
using System.Text;
using ASCOM;
using ASCOM.Utilities;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Timers;

namespace ASCOM.QAstroMeteo
{
    /// <summary>
    /// The resources shared by all drivers and devices, in this example it's a serial port with a shared SendMessage method
    /// an idea for locking the message and handling connecting is given.
    /// In reality extensive changes will probably be needed.
    /// Multiple drivers means that several applications connect to the same hardware device, aka a hub.
    /// Multiple devices means that there are more than one instance of the hardware, such as two focusers.
    /// In this case there needs to be multiple instances of the hardware connector, each with it's own connection count.
    /// </summary>
    public static class SharedResources
    {
        // object used for locking to prevent multiple drivers accessing common code at the same time
        private static readonly object lockObject = new object();

        // Shared serial port. This will allow multiple drivers to use one single serial port.
        private static ASCOM.Utilities.Serial s_sharedSerial = new ASCOM.Utilities.Serial();        // Shared serial port
        private static int s_z = 0;     // counter for the number of connections to the serial port

        private static string ASCOMfunction = "m";     //Define that communicate ObservingConditions to Arduino
        private static int SEND_RECEIVE_WAIT = 1000;   //Number Miliseconds to wait after sending data to collect a response.
        private static int SERIAL_CONNECTION_TIMEOUT = 15000;

        private static bool serverConnected = false;
        private static string telnetServer;

        private static TelnetConnection telnetConnection;
        private static TraceLogger traceLogger;
        private static MeteoCSVData csvData;
        private static int noDataReceived = 0; 


        private static System.Timers.Timer timerAstro;
        private static int iTimerInterval = 9000;          //Milliseconds = 9 seconds

        private static string pObsTemp = "";
        private static string pAltitude = "";
        private static string pDewPoint = "";
        private static string pHumidity = "";
        private static string pPressure = "";
        private static string pSkyState = "";
        private static string pRaining = "";
        private static string pRainRate = "";
        private static string pCloudCover = "";
        private static string pSkyTemp = "";
        private static string pSkyQuality = "";
        private static string pLux = "";

        private static string pConnectionType = "";

        public static string ObsTemp
        { get { return pObsTemp; } }

        public static string Altitude
        { get { return pAltitude; } }

        public static string DewPoint
        { get { return pDewPoint; } }

        public static string Humidity
        { get { return pHumidity; } }

        public static string Pressure
        { get { return pPressure; } }

        public static string SkyState
        { get { return pSkyState; } }

        public static string Raining
        { get { return pRaining; } }

        public static string RainRate
        { get { return pRainRate; } }

        public static string CloudCover
        { get { return pCloudCover; } }

        public static string SkyTemp
        { get { return pSkyTemp; } }

        public static string SkyQuality
        { get { return pSkyQuality; } }

        public static string Lux
        { get { return pLux; } }

        public static string connectionType
        { get { return pConnectionType; } }

        //
        // Public access to shared resources
        //
        public static TraceLogger tl
        {
            get
            {
                if (traceLogger == null)
                {
                    traceLogger = new TraceLogger("", "Q-Astro Meteo");
                    traceLogger.Enabled = ASCOM.QAstroMeteo.Properties.Settings.Default.TraceLevel;
                }
                return traceLogger;
            }
        }

        public static void SetLoggingConnectionType()
        {
            if (ASCOM.QAstroMeteo.Properties.Settings.Default.SerialConnection)
                pConnectionType = "Serial: " + ASCOM.QAstroMeteo.Properties.Settings.Default.COMPort;
            else
                pConnectionType = "Server: " + ASCOM.QAstroMeteo.Properties.Settings.Default.Server;
        }

        //
        // Public access to shared resources
        //

        #region single serial port connector
        //
        // this region shows a way that a single serial port could be connected to by multiple 
        // drivers.
        //
        // Connected is used to handle the connections to the port.
        //
        // SendMessage is a way that messages could be sent to the hardware without
        // conflicts between different drivers.
        //
        // All this is for a single connection, multiple connections would need multiple ports
        // and a way to handle connecting and disconnection from them - see the
        // multi driver handling section for ideas.
        //

        /// <summary>
        /// Shared serial port
        /// </summary>
        public static ASCOM.Utilities.Serial SharedSerial { get { return s_sharedSerial; } }

        /// <summary>
        /// number of connections to the shared serial port
        /// </summary>
        public static int connections { get { return s_z; } set { s_z = value; } }

        /// <summary>
        /// Example of a shared SendMessage method, the lock
        /// prevents different drivers tripping over one another.
        /// It needs error handling and assumes that the message will be sent unchanged
        /// and that the reply will always be terminated by a "#" character.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// 

        public static string SendMessage(string function, string message)
        {
            string response = "";

            lock (lockObject)
            {
                tl.LogMessage("Q-Astro Meteo", "Lock Object");

                if (serverConnected && !String.IsNullOrEmpty(message))
                {
                    string msg = function + message + "#";

                    tl.LogMessage("Q-Astro Meteo", "Send Msg: " + msg);

                    if (ASCOM.QAstroMeteo.Properties.Settings.Default.SerialConnection)
                        response = SendMessageSerial(msg);
                    else
                        response = SendMessageTelnet(msg);

                    tl.LogMessage("Q-Astro Meteo", "Response Msg: " + response);
                }
                else
                {
                    tl.LogMessage("Q-Astro Meteo", "Not Connected or Empty Send Msg: " + message);
                    return "";
                }
            }

            return response;
        }

        /// <summary>
        /// Example of handling connecting to and disconnection from the
        /// shared serial port.
        /// Needs error handling
        /// the port name etc. needs to be set up first, this could be done by the driver
        /// checking Connected and if it's false setting up the port before setting connected to true.
        /// It could also be put here.
        /// </summary>
        public static bool Connected
        {
            set
            {
                lock (lockObject)
                {
                    if (value)
                    {
                        if (s_z == 0)
                        {
                            if (ASCOM.QAstroMeteo.Properties.Settings.Default.SerialConnection)
                                ConnectSerial();
                            else
                                ConnectTelnet();

                            if (serverConnected)
                                setConnection();                        }
                        s_z++;
                    }
                    else
                    {
                        s_z--;
                        if (s_z <= 0)
                            setDisconnection();                    }
                }
            }
            get { return serverConnected; }
        }

        private static void setConnection()
        {
            setKValues();
            setCloudValues();
            if (ASCOM.QAstroMeteo.Properties.Settings.Default.CSVData)
                csvData = new MeteoCSVData();

            GetData();
            InitialiseTimer();
            timerAstro.Start();
        }

        private static void setDisconnection()
        {
            DisposeTimer();

            if (ASCOM.QAstroMeteo.Properties.Settings.Default.SerialConnection)
                SharedSerial.Connected = false;

            serverConnected = false;
            traceLogger.Enabled = false;
            traceLogger.Dispose();
            traceLogger = null;
            s_z = 0;
        }

        #endregion

        #region Multi Driver handling
        // this section illustrates how multiple drivers could be handled,
        // it's for drivers where multiple connections to the hardware can be made and ensures that the
        // hardware is only disconnected from when all the connected devices have disconnected.

        // It is NOT a complete solution!  This is to give ideas of what can - or should be done.
        //
        // An alternative would be to move the hardware control here, handle connecting and disconnecting,
        // and provide the device with a suitable connection to the hardware.
        //
        /// <summary>
        /// dictionary carrying device connections.
        /// The Key is the connection number that identifies the device, it could be the COM port name,
        /// USB ID or IP Address, the Value is the DeviceHardware class
        /// </summary>
        private static Dictionary<string, DeviceHardware> connectedDevices = new Dictionary<string, DeviceHardware>();

        /// <summary>
        /// This is called in the driver Connect(true) property,
        /// it add the device id to the list of devices if it's not there and increments the device count.
        /// </summary>
        /// <param name="deviceId"></param>
        public static void Connect(string deviceId)
        {
            lock (lockObject)
            {
                if (!connectedDevices.ContainsKey(deviceId))
                    connectedDevices.Add(deviceId, new DeviceHardware());
                connectedDevices[deviceId].count++;       // increment the value
            }
        }

        public static void Disconnect(string deviceId)
        {
            lock (lockObject)
            {
                if (connectedDevices.ContainsKey(deviceId))
                {
                    connectedDevices[deviceId].count--;
                    if (connectedDevices[deviceId].count <= 0)
                        connectedDevices.Remove(deviceId);
                }
            }
        }

        public static bool IsConnected(string deviceId)
        {
            if (connectedDevices.ContainsKey(deviceId))
                return (connectedDevices[deviceId].count > 0);
            else
                return false;
        }
        #endregion

        public static string rawCommand(string function, string command)
        {
            return rawCommand(function, command, false);
        }

        public static string rawCommand(string function, string command, bool raw)
        {
            try
            {
                string answer = SharedResources.SendMessage(function, command);
                if (raw)
                    return answer.Trim();
                else
                    return answer.Substring(2).Trim();

            }
            catch (System.TimeoutException e)
            {
                tl.LogMessage("Q-Astro Meteo Timeout exception", e.Message);
            }
            catch (ASCOM.NotConnectedException e)
            {
                tl.LogMessage("Q-Astro Meteo Not connected exception", e.Message);
            }
            catch (ASCOM.DriverException e)
            {
                tl.LogMessage("Q-Astro Meteo Driver exception", e.Message);
            }

            return String.Empty;
        }

        #region Private Serial Methods

        private static void ConnectSerial()
        {
            try
            {
                SharedSerial.PortName = ASCOM.QAstroMeteo.Properties.Settings.Default.COMPort;
                //                                SharedSerial.ReceiveTimeoutMs = 2000;
                SharedSerial.Speed = ASCOM.Utilities.SerialSpeed.ps9600;
                //                                SharedSerial.Handshake = ASCOM.Utilities.SerialHandshake.None;
                SharedSerial.Connected = true;
               Thread.Sleep(SERIAL_CONNECTION_TIMEOUT);    //Stupid Arduino restarts when opening port - needs to wait
                string answer = SharedResources.SendMessageSerial("i#");
                if (answer.Contains("Q-Astro Meteo"))
                    SharedResources.serverConnected = true;
                else
                {
                    MessageBox.Show("Q-Astro Meteo device not detected at port " + SharedResources.SharedSerial.PortName, "Device not detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SharedResources.tl.LogMessage("Connected answer", "Wrong answer " + answer);
                }
            }
            catch (System.IO.IOException exception)
            {
                MessageBox.Show("Q-Astro Meteo Serial port not opened for " + SharedResources.SharedSerial.PortName, "Invalid port state", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SharedResources.tl.LogMessage("Serial port not opened", exception.Message);
            }
            catch (System.UnauthorizedAccessException exception)
            {
                MessageBox.Show("Q-Astro Meteo Access denied to serial port " + SharedResources.SharedSerial.PortName, "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SharedResources.tl.LogMessage("Access denied to serial port", exception.Message);
            }
            catch (ASCOM.DriverAccessCOMException exception)
            {
                MessageBox.Show("Q-Astro Meteo ASCOM driver exception: " + exception.Message, "ASCOM driver exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Runtime.InteropServices.COMException exception)
            {
                MessageBox.Show("Q-Astro Meteo Serial port read timeout for port " + SharedResources.SharedSerial.PortName, "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SharedResources.tl.LogMessage("Q-Astro Meteo Serial port read timeout", exception.Message);
            }
        }

        private static string SendMessageSerial(string msg)
        {
            SharedSerial.ClearBuffers();
            SharedSerial.Transmit(msg);
            string strRec = SharedSerial.ReceiveTerminated("#");
            SharedSerial.ClearBuffers();

            return strRec;
        }

        #endregion

        #region Private Telnet Methods

        private static void ConnectTelnet()
        {
            try
            {
                if (StartTelnetConnection())
                    SharedResources.serverConnected = true;
                else
                {
                    MessageBox.Show("Q-Astro Meteo device not detected on IP " + SharedResources.telnetServer, "Device not detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SharedResources.tl.LogMessage("Connected answer", "Wrong answer ");
                    SharedResources.serverConnected = false;
                }
            }
            catch (SocketException exception)
            {
                MessageBox.Show("Q-Astro Meteo Telnet connection not established for " + SharedResources.telnetServer, "Invalid server state", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SharedResources.tl.LogMessage("Server connection not estalished", exception.Message);

            }
            catch (System.IO.IOException exception)
            {
                MessageBox.Show("Q-Astro Meteo Telnet port not opened for " + SharedResources.telnetServer, "Invalid server state", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SharedResources.tl.LogMessage("Server connection not estalished", exception.Message);
            }
            catch (System.UnauthorizedAccessException exception)
            {
                MessageBox.Show("Q-Astro Meteo Access denied to server " + SharedResources.telnetServer, "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SharedResources.tl.LogMessage("Access denied to server", exception.Message);
            }
            catch (ASCOM.DriverAccessCOMException exception)
            {
                MessageBox.Show("Q-Astro Meteo ASCOM driver exception: " + exception.Message, "ASCOM driver exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Runtime.InteropServices.COMException exception)
            {
                MessageBox.Show("Q-Astro Meteo Connection read timeout for server " + SharedResources.telnetServer, "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SharedResources.tl.LogMessage("QAstro Connection to server read timeout", exception.Message);
            }
        }

        private static bool StartTelnetConnection()
        {
            telnetServer = ASCOM.QAstroMeteo.Properties.Settings.Default.Server;
            telnetConnection = new TelnetConnection(telnetServer, 23);

            return telnetConnection.CheckConnected();
        }

        private static void CloseTelnetConnection()
        {
            telnetConnection.WriteLine("cl#");
            Thread.Sleep(SEND_RECEIVE_WAIT);
            telnetConnection.CloseConnection();
            telnetConnection = null;
        }

        private static string SendMessageTelnet(string msg)
        {
            string strRec = "";

            if (StartTelnetConnection())
            {
                telnetConnection.WriteLine(msg);
                Thread.Sleep(SEND_RECEIVE_WAIT);
                strRec = telnetConnection.Read();
                CloseTelnetConnection();

                if (strRec.Contains("#"))
                    noDataReceived = 0;
                else
                    noDataReceived++;

                if (noDataReceived > 2)
                {
                    MessageBox.Show("No Connection!!");
                    throw new ASCOM.NotConnectedException("No Data received for last 3 data requests");
                }
            }
            return strRec;
        }

        #endregion

        #region Timer Methods

        private static void InitialiseTimer()
        {
            timerAstro = new System.Timers.Timer(iTimerInterval);
            timerAstro.Elapsed += timerAstro_Tick;
            timerAstro.AutoReset = true;
            timerAstro.Enabled = true;
        }

        private static void DisposeTimer()
        {
            timerAstro.Stop();
            timerAstro.Enabled = false;
            timerAstro.Dispose();
        }

        private static void timerAstro_Tick(Object source, ElapsedEventArgs e)
        {
            GetData();
        }

        #endregion

        #region Private Data Methods

        private static void setKValues()
        {
            string kValues = "ks";
            kValues += "K1" + ASCOM.QAstroMeteo.Properties.Settings.Default.K1 + "_";
            kValues += "K2" + ASCOM.QAstroMeteo.Properties.Settings.Default.K2 + "_";
            kValues += "K3" + ASCOM.QAstroMeteo.Properties.Settings.Default.K3 + "_";
            kValues += "K4" + ASCOM.QAstroMeteo.Properties.Settings.Default.K4 + "_";
            kValues += "K5" + ASCOM.QAstroMeteo.Properties.Settings.Default.K5 + "_";
            kValues += "K6" + ASCOM.QAstroMeteo.Properties.Settings.Default.K6 + "_";
            kValues += "K7" + ASCOM.QAstroMeteo.Properties.Settings.Default.K7;

            SendMessage(ASCOMfunction, kValues);
        }

        private static void setCloudValues()
        {
            string cValues = "ls";
            cValues += "CTO" + ASCOM.QAstroMeteo.Properties.Settings.Default.CTO + "_";
            cValues += "CTC" + ASCOM.QAstroMeteo.Properties.Settings.Default.CTC + "_";
            cValues += "CFP" + ASCOM.QAstroMeteo.Properties.Settings.Default.CFP;

            SendMessage(ASCOMfunction, cValues);
        }

        private static void GetData()
        {
            if (serverConnected)
            {
                string response = rawCommand(ASCOMfunction, "m", true);

                if (response.Length > 0)
                {
                    int iPos = response.IndexOf('#');
                    if (iPos > 0)
                        response = response.Substring(1, iPos - 1); //Start at 1 as 0 contains the Function which will be w.

                    DeconstructData(response);
                    if (ASCOM.QAstroMeteo.Properties.Settings.Default.CSVData)
                        csvData.WriteDataLine(pObsTemp, pAltitude, pDewPoint, pHumidity, pPressure, pSkyState, pRaining, pRainRate, pCloudCover, pSkyTemp, pSkyQuality, pLux);

                }

            }
        }

        private static void DeconstructData(string response)
        {
            string[] meteoItems = response.Split('_');

            foreach (var meteo in meteoItems)
            {
                string item = meteo.Substring(1);

                switch (meteo[0])
                {
                    case 'o':       //Observatory Temp
                        pObsTemp = item;
                        break;
                    case 'a':       //Altitude
                        pAltitude = item;
                        break;
                    case 'd':       //Dew Point
                        pDewPoint = item;
                        break;
                    case 'h':       //Humidity
                        pHumidity = item;
                        break;
                    case 'p':       //Presure
                        pPressure = item;
                        break;
                    case 's':       //Sky state
                        pSkyState = item;
                        break;
                    case 'r':       //Rain
                        pRaining = item;
                        break;
                    case 'v':       //Rain rate
                        pRainRate = item;
                        break;
                    case 'c':       //Cloud cover
                        pCloudCover = item;
                        break;
                    case 't':       //Sky Temperature
                        pSkyTemp = item;
                        break;
                    case 'q':       //Sky Quality
                        pSkyQuality = item;
                        break;
                    case 'b':       //Lux Reading
                        pLux = item;
                        break;
                }
            }
        }

        #endregion

    }
    /// <summary>
    /// Skeleton of a hardware class, all this does is hold a count of the connections,
    /// in reality extra code will be needed to handle the hardware in some way
    /// </summary>
    public class DeviceHardware
    {
        internal int count { set; get; }

        internal DeviceHardware()
        {
            count = 0;
        }
    }

    //#region ServedClassName attribute
    ///// <summary>
    ///// This is only needed if the driver is targeted at  platform 5.5, it is included with Platform 6
    ///// </summary>
    //[global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    //public sealed class ServedClassNameAttribute : Attribute
    //{
    //    // See the attribute guidelines at 
    //    //  http://go.microsoft.com/fwlink/?LinkId=85236

    //    /// <summary>
    //    /// Gets or sets the 'friendly name' of the served class, as registered with the ASCOM Chooser.
    //    /// </summary>
    //    /// <value>The 'friendly name' of the served class.</value>
    //    public string DisplayName { get; private set; }
    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="ServedClassNameAttribute"/> class.
    //    /// </summary>
    //    /// <param name="servedClassName">The 'friendly name' of the served class.</param>
    //    public ServedClassNameAttribute(string servedClassName)
    //    {
    //        DisplayName = servedClassName;
    //    }
    //}
    //#endregion
}
