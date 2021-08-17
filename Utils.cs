using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESERO.CanSat
{
    public static class Utils
    {
        public struct ChannelsRecord
        {
            public DateTime TimeStamp;
            public double TempExt;
            public double PressureExt;
            public double Height;
            public double TempInt;
            public double AccelerationInt_XDir;
            public double AccelerationInt_YDir;
            public double AccelerationInt_ZDir;
            public double AccelerationCalc;
            public double LightLevel;
            public double RotationInt_Pitch;
            public double RotationInt_Roll;
            public double MagneticForceInt;
            public double Touch;
            public double Sound;
        }

        // Magic number
        private static double DONT_INCLUDE = -54321D;

        public static void AppendToExceptionsFile(string filename, string str)
        {
            using (FileStream fs = File.Open(CreateDocsFolder() + @"\" + filename, FileMode.Append))
            using (StreamWriter sw = new(fs))
            {
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(str + "\n");
            }
        }

        private static string CreateDocsFolder()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string folder = path + @"\CanSat";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }

        // LINQ recursion
        public static IEnumerable<Control> GetAllRecursive(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAllRecursive(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        public static double ConvertPressureToAltitude(double dPressureExt)
        {
            try
            {
                double dOut = dPressureExt * 100;
                dOut = dOut / 101325;
                dOut = Math.Log(dOut) / Math.Log(10);
                dOut = dOut / 5.25588;
                dOut = Math.Pow(10, dOut) - 1;

                dOut = dOut / (0 - 0.0000225577);

                // Above calculation can generate NaN values
                if (Double.IsNaN(dOut))
                    return -1D;
                else
                    return Math.Round(dOut, 0);
            }
            catch (Exception ex)
            {
                string sError = ex.ToString();
                Utils.AppendToExceptionsFile(("Exceptions_" + DateTime.Now.Date.ToString("dd.MM.yyyy") + ".txt"), sError);
                return -1D;
            }
        }

        #region " CSV export "
        public static void ExportCSV(string sVersion, string sFileName, ChannelsRecord record)
        {
            string sOut = record.TimeStamp.ToString();

            var tgls = Utils.GetAllRecursive(fMain.instance.Controls["pnlB"], typeof(ToggleButton));

            foreach (ToggleButton tgl in tgls)
            {
                if (tgl.ToggleState == ToggleButtonState.Inactive)
                {
                    switch (tgl.Name)
                    {
                        case "tgl1":
                            record.TempExt = DONT_INCLUDE;
                            break;
                        case "tgl2":
                            record.PressureExt = DONT_INCLUDE;
                            break;
                        case "tgl3":
                            record.Height = DONT_INCLUDE;
                            break;
                        case "tgl4":
                            record.TempInt = DONT_INCLUDE;
                            break;
                        case "tgl5":
                            record.AccelerationInt_XDir = DONT_INCLUDE;
                            break;
                        case "tgl6":
                            record.AccelerationInt_YDir = DONT_INCLUDE;
                            break;
                        case "tgl7":
                            record.AccelerationInt_ZDir = DONT_INCLUDE;
                            break;
                        case "tgl8":
                            record.AccelerationCalc = DONT_INCLUDE;
                            break;
                        case "tgl9":
                            record.LightLevel = DONT_INCLUDE;
                            break;
                        case "tgl10":
                            record.RotationInt_Pitch = DONT_INCLUDE;
                            break;
                        case "tgl11":
                            record.RotationInt_Roll = DONT_INCLUDE;
                            break;
                        case "tgl12":
                            record.MagneticForceInt = DONT_INCLUDE;
                            break;
                        case "tgl13":
                            if (sVersion == "V2")
                                record.Touch = DONT_INCLUDE;
                            break;
                        case "tgl14":
                            if (sVersion == "V2")
                                record.Sound = DONT_INCLUDE;
                            break;
                        default:
                            break;
                    }
                }
            }

            sOut += ChannelValue(record.TempExt);
            sOut += ChannelValue(record.PressureExt);
            sOut += ChannelValue(record.Height);
            sOut += ChannelValue(record.TempInt);
            sOut += ChannelValue(record.AccelerationInt_XDir);
            sOut += ChannelValue(record.AccelerationInt_YDir);
            sOut += ChannelValue(record.AccelerationInt_ZDir);
            sOut += ChannelValue(record.AccelerationCalc);
            sOut += ChannelValue(record.LightLevel);
            sOut += ChannelValue(record.RotationInt_Pitch);
            sOut += ChannelValue(record.RotationInt_Roll);
            sOut += ChannelValue(record.MagneticForceInt);

            if (sVersion == "V2")
                sOut += ChannelValue(record.Touch);
            if (sVersion == "V2")
                sOut += ChannelValue(record.Sound);

            AppendToExportFile(sFileName, sOut);
        }
        #endregion

        private static string ChannelValue(double value)
        {
            if (value == DONT_INCLUDE)
                return "";
            else
                return "," + value.ToString();
        }

        private static void AppendToExportFile(string filename, string str)
        {
            using (FileStream fs = File.Open(CreateDocsFolder() + @"\" + filename, FileMode.Append))
            using (StreamWriter sw = new(fs))
            {
                sw.WriteLine(str);
            }
        }

        // Get port attributes
        public static Dictionary<string, string> GetPortAttributes()
        {
            var dictPorts = new Dictionary<string, string>();
            
            try
            {
                using (var searcher = new ManagementObjectSearcher("select * from WIN32_SerialPort"))
                {
                    string[] portNames = SerialPort.GetPortNames();
                    var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();
                    var portsList = (from n in portNames
                                     join p in ports on n equals p["DeviceID"].ToString()
                                     select n + " - " + p["Caption"]).ToList();

                    string[] aOut;
                    foreach (var port in portsList)
                    {
                        aOut = port.Split(" - ");
                        dictPorts.Add(aOut[0], aOut[1]);
                    }
                }
                return dictPorts;
            }
            catch (System.ArgumentException)
            {
                // NOOP
                return dictPorts;
            }
            catch (Exception ex)
            {
                string sError = ex.ToString();
                Utils.AppendToExceptionsFile(("Exceptions_" + DateTime.Now.Date.ToString("dd.MM.yyyy") + ".txt"), sError);
                return dictPorts;
            }
        }
    }
}

