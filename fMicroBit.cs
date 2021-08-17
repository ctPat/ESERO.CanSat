using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ESERO.CanSat.Utils;

namespace ESERO.CanSat
{
    public partial class fMicroBit : Form
    {
        public static fMicroBit instance;

        public List<ChannelsRecord> records = new List<ChannelsRecord>();
        public DataTable dt;
        public DataRow dr;

        public List<DataRow> lastRows;
        public int iLastRows = 50;

        public SerialPort sp;

        public int iBaudRate;        
        public bool bConnected;
        public string sVersion = "";
        public string sPortName = "";

        public bool bRecording = false;
        public string sExportCSV = "";

        private System.Timers.Timer tmr;

        private bool bDataReceived;

        public fMicroBit()
        {
            InitializeComponent();
            instance = this;
            this.dt = CreateDataTable();

            this.StartMicroBit();

            tmr = new System.Timers.Timer();
            tmr.Interval = 2000;
            tmr.Elapsed += tmr_Elapsed;
            tmr.Start();

            try
            {
                this.iBaudRate = Int32.Parse(ConfigurationManager.AppSettings["baudrate"]);
            }
            catch (Exception)
            {
                iBaudRate = 9600;
            }
        }

        private delegate void SerialDataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e);

        public void StartMicroBit()
        {
            try
            {
                sp = new SerialPort();
                this.CloseAllPorts();

                sp.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(sp_DataReceived);

                sp.BaudRate = this.iBaudRate;

                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    if (sp.IsOpen) sp.Close();
                    sp.PortName = port;
                    try
                    {
                        sp.Open();
                    }
                    catch (System.Exception)
                    { }
                }
            }
            catch (System.ArgumentOutOfRangeException)
            { }
            catch (Exception ex)
            {
                string sError = ex.ToString();
                Utils.AppendToExceptionsFile(("Exceptions_" + DateTime.Now.Date.ToString("dd.MM.yyyy") + ".txt"), sError);
            }
        }

        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                this.bDataReceived = true;
                this.bConnected = true;

                SerialPort _sp = (SerialPort)sender;
                sPortName = _sp.PortName;

                string sIn = _sp.ReadLine();
                if (!sIn.Contains("DAP"))
                {
                    sIn.Replace("false", "0");
                    sIn.Replace("true", "1");

                    string[] recList = sIn.Split(";");

                    if (recList.Count() == 14)
                    {
                        try
                        {
                            fMain.instance.Invoke(new System.Action(() =>
                            {
                                fMain.instance.Controls["pnlA"].Controls["txtRawData"].Text = sIn;
                                fMain.instance.Controls["pnlA"].Controls["txtVersion"].Text = "Version: " + this.sVersion;
                                try
                                {
                                    fMain.instance.Controls["pnlA"].Controls["txtPortAttributes"].Text = Utils.GetPortAttributes()[this.sPortName];
                                }
                                catch (System.ArgumentException)
                                {
                                    // NOOP
                                }                                
                                fMain.instance.Controls["pnlA"].Controls["txtSpeed"].Text = iBaudRate.ToString();
                                fMain.instance.Controls["pnlA"].Controls["txtConnected"].Text = "CONNECTED";
                                fMain.instance.Controls["pnlA"].Controls["txtConnected"].BackColor = Color.LightBlue;
                                fMain.instance.Controls["pnlA"].Controls["lbl2"].Visible = true;
                                fMain.instance.Controls["pnlA"].Controls["lbl3"].Visible = true;
                                fMain.instance.Controls["pnlA"].Controls["lbl4"].Visible = true;
                                fMain.instance.Controls["pnlA"].Controls["lbl5"].Visible = true;
                                fMain.instance.Controls["pnlMenu"].Controls["btnSelectChannels"].Enabled = true;
                                fMain.instance.Controls["pnlMenu"].Controls["btnGraphs"].Enabled = true;
                                fMain.instance.Controls["pnlMenu"].Controls["btnExport"].Enabled = true;
                            }));
                        }
                        catch (Exception ex)
                        {
                            string sError = ex.ToString();
                            Utils.AppendToExceptionsFile(("Exceptions_" + DateTime.Now.Date.ToString("dd.MM.yyyy") + ".txt"), sError);
                        }

                        ChannelsRecord record;
                        record.TimeStamp = DateTime.Now;
                        record.TempExt = ParseDouble(recList[0]);
                        record.PressureExt = ParseDouble(recList[1]);
                        record.Height = Utils.ConvertPressureToAltitude(ParseDouble(recList[2]));
                        record.TempInt = ParseDouble(recList[3]);
                        record.AccelerationInt_XDir = ParseDouble(recList[4]);
                        record.AccelerationInt_YDir = ParseDouble(recList[5]);
                        record.AccelerationInt_ZDir = ParseDouble(recList[6]);
                        record.AccelerationCalc = ParseDouble(recList[7]);
                        record.LightLevel = ParseDouble(recList[8]);
                        record.RotationInt_Pitch = ParseDouble(recList[9]);
                        record.RotationInt_Roll = ParseDouble(recList[10]);
                        record.MagneticForceInt = ParseDouble(recList[11]);
                        if (recList[12].Contains("V1"))
                        {
                            this.sVersion = "V1";
                            record.Touch = 0;
                            record.Sound = 0;
                        }
                        else
                        {
                            this.sVersion = "V2";
                            record.Touch = recList[12] == "false" ? 0 : 1;
                            record.Sound = ParseDouble(recList[13]);
                        }

                        // Add records to data table
                        records.Add(record);
                        AddDataTableRecord(ref dt, record);

                        fMain.instance.Invoke(new System.Action(() =>
                        {
                            fMain.instance.Controls["pnlB"].Controls["glbl1"].Text = record.TempExt.ToString();
                            fMain.instance.Controls["pnlB"].Controls["glbl2"].Text = record.PressureExt.ToString();
                            fMain.instance.Controls["pnlB"].Controls["glbl3"].Text = record.Height.ToString();
                            fMain.instance.Controls["pnlB"].Controls["glbl4"].Text = record.TempInt.ToString();
                            fMain.instance.Controls["pnlB"].Controls["glbl5"].Text = record.AccelerationInt_XDir.ToString();
                            fMain.instance.Controls["pnlB"].Controls["glbl6"].Text = record.AccelerationInt_YDir.ToString();
                            fMain.instance.Controls["pnlB"].Controls["glbl7"].Text = record.AccelerationInt_ZDir.ToString();
                            fMain.instance.Controls["pnlB"].Controls["glbl8"].Text = record.AccelerationCalc.ToString();
                            fMain.instance.Controls["pnlB"].Controls["glbl9"].Text = record.LightLevel.ToString();
                            fMain.instance.Controls["pnlB"].Controls["glbl10"].Text = record.RotationInt_Pitch.ToString();
                            fMain.instance.Controls["pnlB"].Controls["glbl11"].Text = record.RotationInt_Roll.ToString();
                            fMain.instance.Controls["pnlB"].Controls["glbl12"].Text = record.MagneticForceInt.ToString();
                            fMain.instance.Controls["pnlB"].Controls["grpData5"].Controls["glbl13"].Text = record.Touch.ToString();
                            fMain.instance.Controls["pnlB"].Controls["grpData5"].Controls["glbl14"].Text = record.Sound.ToString();

                            // Timestamp
                            fMain.instance.Controls["pnlB"].Controls["glbl15"].Text = record.TimeStamp.ToString();

                            if (this.sVersion == "V1")
                            {
                                fMain.instance.Controls["pnlB"].Controls["grpData4"].Visible = false;
                                fMain.instance.Controls["pnlB"].Controls["grpData5"].Visible = false;
                            }
                            else if (this.sVersion == "V2")
                            {
                                fMain.instance.Controls["pnlB"].Controls["grpData4"].Visible = true;
                                fMain.instance.Controls["pnlB"].Controls["grpData5"].Visible = true;
                            }
                        }));

                        if (bRecording)                        
                            Utils.ExportCSV(this.sVersion, this.sExportCSV, record);

                        if (dt!=null)
                        {
                            lastRows = dt.AsEnumerable().Skip(dt.Rows.Count - iLastRows).Take(iLastRows).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string sError = ex.ToString();
                Utils.AppendToExceptionsFile(("Exceptions_" + DateTime.Now.Date.ToString("dd.MM.yyyy") + ".txt"), sError);
            }
        }

        private double ParseDouble(string recVal)
        {
            try
            {
                return Double.Parse(recVal);
            }
            catch (Exception ex)
            {
                string sError = ex.ToString();                
                return 0D;
            }
        }

        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable("ChannelRecords");
            dt.Columns.Add(new DataColumn(string.Format("TimeStamp")));
            dt.Columns.Add(new DataColumn(string.Format("Temp-Ext")));
            dt.Columns.Add(new DataColumn(string.Format("Pressure-Ext")));
            dt.Columns.Add(new DataColumn(string.Format("Altitude")));
            dt.Columns.Add(new DataColumn(string.Format("Temp-Int")));
            dt.Columns.Add(new DataColumn(string.Format("Accel-X")));
            dt.Columns.Add(new DataColumn(string.Format("Accel-Y")));
            dt.Columns.Add(new DataColumn(string.Format("Accel-Z")));
            dt.Columns.Add(new DataColumn(string.Format("Accel-T")));
            dt.Columns.Add(new DataColumn(string.Format("LightLevel")));
            dt.Columns.Add(new DataColumn(string.Format("Rotation-Pitch")));
            dt.Columns.Add(new DataColumn(string.Format("Rotation-Roll")));
            dt.Columns.Add(new DataColumn(string.Format("Magnetic-Force-T")));
            dt.Columns.Add(new DataColumn(string.Format("Touch")));
            dt.Columns.Add(new DataColumn(string.Format("Sound")));

            return dt;
        }

        private void AddDataTableRecord(ref DataTable dt, ChannelsRecord rec)
        {
            dr = dt.NewRow();

            dr[0] = rec.TimeStamp;
            dr[1] = rec.TempExt;
            dr[2] = rec.PressureExt;
            dr[3] = rec.Height;
            dr[4] = rec.TempInt;
            dr[5] = rec.AccelerationInt_XDir;
            dr[6] = rec.AccelerationInt_YDir;
            dr[7] = rec.AccelerationInt_ZDir;
            dr[8] = rec.AccelerationCalc;
            dr[9] = rec.LightLevel;
            dr[10] = rec.RotationInt_Pitch;
            dr[11] = rec.RotationInt_Roll;
            dr[12] = rec.MagneticForceInt;
            dr[13] = rec.Touch;
            dr[14] = rec.Sound;

            dt.Rows.Add(dr);
        }
        
        public void CloseAllPorts()
        {
            try
            {
                foreach (string port in SerialPort.GetPortNames())
                {
                    sp.PortName = port;
                    sp.Close();
                }
            }
            catch (Exception ex)
            {
                string sError = ex.ToString();
                Utils.AppendToExceptionsFile(("Exceptions_" + DateTime.Now.Date.ToString("dd.MM.yyyy") + ".txt"), sError);
            }
        }

        // Watchdog timer
        private void tmr_Elapsed(object sender, EventArgs e)
        {
            try
            {
                if (!this.bDataReceived)
                {
                    this.bConnected = false;
                    fMain.instance.Invoke(new System.Action(() =>
                    {
                        fMain.instance.Controls["pnlA"].Controls["txtConnected"].Text = "DISCONNECTED";
                        fMain.instance.Controls["pnlA"].Controls["txtConnected"].BackColor = Color.LightPink;
                        fMain.instance.Controls["pnlA"].Controls["txtRawData"].Text = "";
                        fMain.instance.Controls["pnlA"].Controls["txtVersion"].Text = "";
                        fMain.instance.Controls["pnlA"].Controls["txtSpeed"].Text = "";
                        fMain.instance.Controls["pnlA"].Controls["txtPortAttributes"].Text = "";
                        fMain.instance.Controls["pnlA"].Controls["lbl2"].Visible = false;
                        fMain.instance.Controls["pnlA"].Controls["lbl3"].Visible = false;
                        fMain.instance.Controls["pnlA"].Controls["lbl4"].Visible = false;
                        fMain.instance.Controls["pnlA"].Controls["lbl5"].Visible = false;
                        fMain.instance.Controls["pnlMenu"].Controls["btnSelectChannels"].Enabled = false;
                        fMain.instance.Controls["pnlMenu"].Controls["btnGraphs"].Enabled = false;
                        fMain.instance.Controls["pnlMenu"].Controls["btnExport"].Enabled = false;
                        fMain.instance.btnSerialPort_Click(this, null);
                    }));
                    this.StartMicroBit();
                }

                this.bDataReceived = false;
            }
            catch (Exception ex)
            {
                string sError = ex.ToString();
                Utils.AppendToExceptionsFile(("Exceptions_" + DateTime.Now.Date.ToString("dd.MM.yyyy") + ".txt"), sError);
            }
        }
    }
}