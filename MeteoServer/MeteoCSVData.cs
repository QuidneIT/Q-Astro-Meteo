using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using ASCOM.Utilities;
using System.Windows.Forms;

namespace ASCOM.QAstroMeteo
{
    class MeteoCSVData
    {
        private string fileName = "";

        public MeteoCSVData()
        {
            fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\QAstro-Meteo-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".csv";
            WriteHeader();
        }

        private void WriteHeader()
        {
            string dataLine = "";
            dataLine += "Date" + ",";
            dataLine += "Time" + ",";
            dataLine += "ObsTemp" + ",";
            dataLine += "Altitude" + ",";
            dataLine += "DewPoint" + ",";
            dataLine += "Humidity" + ",";
            dataLine += "Pressure" + ",";
            dataLine += "SkyState" + ",";
            dataLine += "Raining" + ",";
            dataLine += "RainRate" + ",";
            dataLine += "CloudCover" + ",";
            dataLine += "SkyTemp" + ",";
            dataLine += "SkyQuality" + ",";
            dataLine += "Lux";

            WriteData(dataLine);
        }

        public void WriteDataLine(string ObsTemp, string Altitude, string DewPoint, string Humidity, 
            string Pressure,string SkyState, string Raining, string RainRate, 
            string CloudCover, string SkyTemp, string SkyQuality, string Lux)
        {
            string dataLine = "";
            dataLine += DateTime.Now.ToString("yyyy/MM/dd") + ",";
            dataLine += DateTime.Now.ToString("HH:mm:ss") + ",";
            dataLine += ObsTemp + ",";
            dataLine += Altitude + ",";
            dataLine += DewPoint + ",";
            dataLine += Humidity +",";
            dataLine += Pressure +",";
            dataLine += SkyState +",";
            dataLine += Raining +",";
            dataLine += RainRate +",";
            dataLine += CloudCover +",";
            dataLine += SkyTemp +",";
            dataLine += SkyQuality +",";
            dataLine += Lux;

            WriteData(dataLine);
        }

        private void WriteData(string line)
        {
            try
            {

                using (StreamWriter csvDatafile = File.AppendText(fileName))
                    csvDatafile.WriteLine(line);
            }
            catch(Exception error)
            {
            }
        }

    }

}
