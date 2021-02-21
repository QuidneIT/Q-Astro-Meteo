//tabs=4
// --------------------------------------------------------------------------------
// TODO fill in this information for your driver, then remove this line!
//
// ASCOM ObservingConditions driver for QAstro
//
// Description:	Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam 
//				nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
//				erat, sed diam voluptua. At vero eos et accusam et justo duo 
//				dolores et ea rebum. Stet clita kasd gubergren, no sea takimata 
//				sanctus est Lorem ipsum dolor sit amet.
//
// Implements:	ASCOM ObservingConditions interface version: <To be completed by driver developer>
// Author:		(XXX) Your N. Here <your@email.here>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// dd-mmm-yyyy	XXX	6.0.0	Initial edit, created from ASCOM driver template
// --------------------------------------------------------------------------------
//


// This is used to define code in the template that is specific to one class implementation
// unused code canbe deleted and this definition removed.
#define ObservingConditions

using ASCOM;
using ASCOM.Astrometry;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;

namespace ASCOM.QAstroMeteo
{
    //
    // Your driver's DeviceID is ASCOM.QAstro.ObservingConditions
    //
    // The Guid attribute sets the CLSID for ASCOM.QAstro.ObservingConditions
    // The ClassInterface/None addribute prevents an empty interface called
    // _QAstro from being created and used as the [default] interface
    //
    // TODO Replace the not implemented exceptions with code to implement the function or
    // throw the appropriate ASCOM exception.
    //

    /// <summary>
    /// ASCOM ObservingConditions Driver for QAstro.
    /// </summary>
    [Guid("f63c6a4a-cf06-4086-af9b-60d0d09e19f5")]
    [ProgId("ASCOM.QAstroMeteo.ObservingConditions")]
    [ServedClassName("Q-Astro Meteo")]
    [ClassInterface(ClassInterfaceType.None)]
    public class ObservingConditions : ReferenceCountedObjectBase, IObservingConditions
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.QAstroMeteo.ObservingConditions";
        // TODO Change the descriptive string for your driver then remove this line
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "ASCOM ObservingConditions Driver for Q-Astro Meteo.";
        private static string driverShortName = "Q-Astro Meteo";
        private static int interfaceVersion = 2;

        /// <summary>
        /// Private variable to hold the connected state
        /// </summary>
//        private bool connectedState;

        /// <summary>
        /// Initializes a new instance of the <see cref="QAstro"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public ObservingConditions()
        {
            SharedResources.tl.LogMessage(driverShortName, "Starting initialisation");
            driverID = Marshal.GenerateProgIdForType(this.GetType());
//            connectedState = false; // Initialise connected to false
            SharedResources.SetLoggingConnectionType();
            SharedResources.tl.LogMessage(driverShortName, "Completed initialisation");
        }

        //
        // PUBLIC COM INTERFACE IObservingConditions IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected
            if (SharedResources.Connected)
                System.Windows.Forms.MessageBox.Show("Already connected, just press OK");
            else
            {
                using (ServerSetupDialog setupNetwork = new ServerSetupDialog())
                    setupNetwork.ShowDialog();
            }
        }

        public ArrayList SupportedActions
        {
            get
            {
                SharedResources.tl.LogMessage(driverShortName + " SupportedActions Get", "Returning empty arraylist");
                return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            throw new ASCOM.ActionNotImplementedException(driverShortName + " Action " + actionName + " is not implemented by this driver");
        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            this.CommandString(command, raw);
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            return !this.CommandString(command, raw).Equals("0");
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            return SharedResources.rawCommand(ASCOMfunction, command, raw);
        }

        public void Dispose()
        {
        }

        public bool Connected
        {
            get { return IsConnected; }
            set
            {
                {
                    SharedResources.tl.LogMessage(driverShortName + " Connected Set", value.ToString());
                    if (value == IsConnected)
                        return;

                    if (value)
                    {
                        if (IsConnected) return;
                        SharedResources.tl.LogMessage(driverShortName + " Connected Set", "Connecting to port " + SharedResources.connectionType);
                        SharedResources.Connected = true;
  //                      connectedState = SharedResources.Connected;
                    }
                    else
                    {
 //                       connectedState = false;
                        SharedResources.Connected = false;
                        SharedResources.tl.LogMessage(driverShortName + " Connected Set", "Disconnected, " + SharedResources.connections + " connections left");
                    }
                }
            }
        }

        public string Description
        {
            get
            {
                SharedResources.tl.LogMessage(driverShortName + " Description Get", driverDescription);
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverInfo = "Information about the driver itself. Version: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                SharedResources.tl.LogMessage(driverShortName + " DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                SharedResources.tl.LogMessage(driverShortName + " DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            get
            {
                SharedResources.tl.LogMessage(driverShortName + " InterfaceVersion Get", interfaceVersion.ToString());
                return Convert.ToInt16(interfaceVersion);
            }
        }

        public string Name
        {
            get
            {
                SharedResources.tl.LogMessage(driverShortName + " Name Get", driverShortName);
                return driverShortName;
            }
        }


        #endregion

        #region IObservingConditions Implementation

        private string ASCOMfunction = "m";     //Define that communicate ObservingConditions to Arduino

        /// <summary>
        /// Gets and sets the time period over which observations wil be averaged
        /// </summary>
        /// <remarks>
        /// Get must be implemented, if it can't be changed it must return 0
        /// Time period (hours) over which the property values will be averaged 0.0 =
        /// current value, 0.5= average for the last 30 minutes, 1.0 = average for the
        /// last hour
        /// </remarks>
        public double AveragePeriod
        {
            get
            {
                SharedResources.tl.LogMessage("AveragePeriod", "get - 0");
                return 0;
            }
            set
            {
                SharedResources.tl.LogMessage("AveragePeriod", "set - " + value);
                if (value != 0)
                    throw new InvalidValueException("AveragePeriod", value.ToString(), "0 only");
            }
        }

        /// <summary>
        /// Amount of sky obscured by cloud
        /// </summary>
        /// <remarks>0%= clear sky, 100% = 100% cloud coverage</remarks>
        public double CloudCover
        {
            get
            {
                //string recTemp = SendCommand("c");
                SharedResources.tl.LogMessage(driverShortName + " Cloudcover", SharedResources.CloudCover);
                return Convert.ToDouble(SharedResources.CloudCover);
            }
        }

        /// <summary>
        /// Atmospheric dew point at the observatory in deg C
        /// </summary>
        /// <remarks>
        /// Normally optional but mandatory if <see cref=" ASCOM.DeviceInterface.IObservingConditions.Humidity"/>
        /// Is provided
        /// </remarks>
        public double DewPoint
        {
            get
            {
                //string recDewPoint = SendCommand("d");
                SharedResources.tl.LogMessage(driverShortName + " DewPoint", SharedResources.DewPoint);
                return Convert.ToDouble(SharedResources.DewPoint);
            }
        }

        /// <summary>
        /// Atmospheric relative humidity at the observatory in percent
        /// </summary>
        /// <remarks>
        /// Normally optional but mandatory if <see cref="ASCOM.DeviceInterface.IObservingConditions.DewPoint"/> 
        /// Is provided
        /// </remarks>
        public double Humidity
        {
            get
            {
                //string recTemp = SendCommand("h");
                SharedResources.tl.LogMessage(driverShortName + " Humidity", SharedResources.Humidity);
                return Convert.ToDouble(SharedResources.Humidity);
            }
        }

        /// <summary>
        /// Atmospheric pressure at the observatory in hectoPascals (mB)
        /// </summary>
        /// <remarks>
        /// This must be the pressure at the observatory and not the "reduced" pressure
        /// at sea level. Please check whether your pressure sensor delivers local pressure
        /// or sea level pressure and adjust if required to observatory pressure.
        /// </remarks>
        public double Pressure
        {
            get
            {
                //string recTemp = SendCommand("p");
                SharedResources.tl.LogMessage(driverShortName + " Pressure", SharedResources.Pressure);
                return Convert.ToDouble(SharedResources.Pressure);
            }
        }

        /// <summary>
        /// Rain rate at the observatory
        /// </summary>
        /// <remarks>
        /// This property can be interpreted as 0.0 = Dry any positive nonzero value = wet.
        /// </remarks>
        public double RainRate
        {
            get
            {
                //string recTemp = SendCommand("v");
                SharedResources.tl.LogMessage(driverShortName + " RainRate", SharedResources.RainRate);
                return Convert.ToDouble(SharedResources.RainRate);
            }
        }

        /// <summary>
        /// Forces the driver to immediatley query its attached hardware to refresh sensor
        /// values
        /// </summary>
        public void Refresh()
        {
            throw new ASCOM.MethodNotImplementedException();
        }

        /// <summary>
        /// Provides a description of the sensor providing the requested property
        /// </summary>
        /// <param name="PropertyName">Name of the property whose sensor description is required</param>
        /// <returns>The sensor description string</returns>
        /// <remarks>
        /// PropertyName must be one of the sensor properties, 
        /// properties that are not implemented must throw the MethodNotImplementedException
        /// </remarks>
        public string SensorDescription(string PropertyName)
        {
            switch (PropertyName.Trim().ToLowerInvariant())
            {
                case "averageperiod":
                    return "Average period in hours, immediate values are only available";
                case "dewpoint":
                    return "Current Dew Point in Celsius, immediate values are only available";
                case "humidity":
                    return "Current Humidity in %, immediate values are only available";
                case "pressure":
                    return "Current Atmospheric Pressure based on basic Sea Level of 1013.25 mbar, immediate values are only available";
                case "rainrate":
                    return "Current Rain rate (0 Dry, 1.5 Light Rain, 5.0 Moderate Rain, 25 Heavy Rain, 60 Violent Rain), immediate values are only available";
                case "skybrightness":
                    return "Current sky brightness in Lux, immediate values are only available";
                case "skyquality":
                    return "Current sky quality in magnitudes per square arc second, immediate values are only available";
                case "skytemperature":
                    return "Current sky temperature in Celsius, immediate values are only available";
                case "temperature":
                    return "Current temperature of the Observatory or Environment directly around the Q-Astro Meteo unit in Celsius, immediate values are only available";
                case "starfwhm":
                case "winddirection":
                case "windgust":
                case "windspeed":
                    SharedResources.tl.LogMessage("SensorDescription", PropertyName + " - not implemented");
                    throw new MethodNotImplementedException("SensorDescription(" + PropertyName + ")");
                default:
                    SharedResources.tl.LogMessage("SensorDescription", PropertyName + " - unrecognised");
                    throw new ASCOM.InvalidValueException("SensorDescription(" + PropertyName + ")");
            }
        }

        /// <summary>
        /// Sky brightness at the observatory
        /// </summary>
        public double SkyBrightness
        {
            get
            {
                //string recTemp = SendCommand("b");
                SharedResources.tl.LogMessage(driverShortName + " SkyBrightness", SharedResources.Lux);
                return Convert.ToDouble(SharedResources.Lux);
            }
        }

        /// <summary>
        /// Sky quality at the observatory
        /// </summary>
        public double SkyQuality
        {
            get
            {
                //string recTemp = SendCommand("q");
                SharedResources.tl.LogMessage(driverShortName + " SkyQuality", SharedResources.SkyQuality);
                return Convert.ToDouble(SharedResources.SkyQuality);
            }
        }

        /// <summary>
        /// Seeing at the observatory
        /// </summary>
        public double StarFWHM
        {
            get
            {
                SharedResources.tl.LogMessage("StarFWHM", "get - not implemented");
                throw new PropertyNotImplementedException("StarFWHM", false);
            }
        }

        /// <summary>
        /// Sky temperature at the observatory in deg C
        /// </summary>
        public double SkyTemperature
        {
            get
            {
                //string recTemp = SendCommand("t");
                SharedResources.tl.LogMessage(driverShortName + " SkyTemperature", SharedResources.SkyTemp);
                return Convert.ToDouble(SharedResources.SkyTemp);
            }
        }

        /// <summary>
        /// Temperature at the observatory in deg C
        /// </summary>
        public double Temperature
        {
            get
            {
                //string recTemp = SendCommand("o");
                SharedResources.tl.LogMessage(driverShortName + " Temperature", SharedResources.ObsTemp);
                return Convert.ToDouble(SharedResources.ObsTemp);
            }
        }

        /// <summary>
        /// Provides the time since the sensor value was last updated
        /// </summary>
        /// <param name="PropertyName">Name of the property whose time since last update Is required</param>
        /// <returns>Time in seconds since the last sensor update for this property</returns>
        /// <remarks>
        /// PropertyName should be one of the sensor properties Or empty string to get
        /// the last update of any parameter. A negative value indicates no valid value
        /// ever received.
        /// </remarks>
        public double TimeSinceLastUpdate(string PropertyName)
        {
            // the checks can be removed if all properties have the same time.
            if (!string.IsNullOrEmpty(PropertyName))
            {
                switch (PropertyName.Trim().ToLowerInvariant())
                {
                    // break or return the time on the properties that are implemented
                    case "dewpoint":
                    case "humidity":
                    case "skytemperature":
                    case "temperature":
                    case "averageperiod":
                    case "pressure":
                    case "rainrate":
                    case "skybrightness":
                    case "skyquality":
                    case "starfwhm":
                    case "winddirection":
                    case "windgust":
                    case "windspeed":
                        // throw an exception on the properties that are not implemented
                        SharedResources.tl.LogMessage("TimeSinceLastUpdate", PropertyName + " - not implemented");
                        throw new MethodNotImplementedException("SensorDescription(" + PropertyName + ")");
                    default:
                        SharedResources.tl.LogMessage("TimeSinceLastUpdate", PropertyName + " - unrecognised");
                        throw new ASCOM.InvalidValueException("SensorDescription(" + PropertyName + ")");
                }
            }
            // return the time
            SharedResources.tl.LogMessage("TimeSinceLastUpdate", PropertyName + " - not implemented");
            throw new MethodNotImplementedException("TimeSinceLastUpdate(" + PropertyName + ")");
        }

        /// <summary>
        /// Wind direction at the observatory in degrees
        /// </summary>
        /// <remarks>
        /// 0..360.0, 360=N, 180=S, 90=E, 270=W. When there Is no wind the driver will
        /// return a value of 0 for wind direction
        /// </remarks>
        public double WindDirection
        {
            get
            {
                SharedResources.tl.LogMessage("WindDirection", "get - not implemented");
                throw new PropertyNotImplementedException("WindDirection", false);
            }
        }

        /// <summary>
        /// Peak 3 second wind gust at the observatory over the last 2 minutes in m/s
        /// </summary>
        public double WindGust
        {
            get
            {
                SharedResources.tl.LogMessage("WindGust", "get - not implemented");
                throw new PropertyNotImplementedException("WindGust", false);
            }
        }

        /// <summary>
        /// Wind speed at the observatory in m/s
        /// </summary>
        public double WindSpeed
        {
            get
            {
                SharedResources.tl.LogMessage("WindSpeed", "get - not implemented");
                throw new PropertyNotImplementedException("WindSpeed", false);
            }
        }

        #endregion

        #region private methods

        #region calculate the gust strength as the largest wind recorded over the last two minutes

        // save the time and wind speed values
        private Dictionary<DateTime, double> winds = new Dictionary<DateTime, double>();

        private double gustStrength;

        private void UpdateGusts(double speed)
        {
            Dictionary<DateTime, double> newWinds = new Dictionary<DateTime, double>();
            var last = DateTime.Now - TimeSpan.FromMinutes(2);
            winds.Add(DateTime.Now, speed);
            var gust = 0.0;
            foreach (var item in winds)
            {
                if (item.Key > last)
                {
                    newWinds.Add(item.Key, item.Value);
                    if (item.Value > gust)
                        gust = item.Value;
                }
            }
            gustStrength = gust;
            winds = newWinds;
        }

        #endregion

        #endregion

        #region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private bool IsConnected
        {
            get
            {
                // TODO check that the driver hardware connection exists and is connected to the hardware
                return SharedResources.Connected;
//                connectedState;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new ASCOM.NotConnectedException(message);
            }
        }
        #endregion
    }
}