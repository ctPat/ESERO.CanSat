using ESERO.CanSat.Properties;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESERO.CanSat
{
    public partial class fMain : MetroForm
    {
        public static fMain instance;

        fChart ofChart;
        fMicroBit ofMicroBit;
        fAbout ofAbout = new fAbout();

        // Constructor
        public fMain()
        {
            InitializeComponent();
            instance = this;            
        }

        #region " Load  event "

        private void frmMockup_Load(object sender, EventArgs e)
        {            
            //****************************
            // Format menu button controls
            //****************************          
            this.btnSerialPort.TabStop = false;
            this.btnSerialPort.FlatStyle = FlatStyle.Flat;
            this.btnSerialPort.FlatAppearance.BorderSize = 0;

            this.btnSelectChannels.TabStop = false;
            this.btnSelectChannels.FlatStyle = FlatStyle.Flat;
            this.btnSelectChannels.FlatAppearance.BorderSize = 0;

            this.btnAbout.TabStop = false;
            this.btnAbout.FlatStyle = FlatStyle.Flat;
            this.btnAbout.FlatAppearance.BorderSize = 0;

            this.btnGraphs.TabStop = false;
            this.btnGraphs.FlatStyle = FlatStyle.Flat;
            this.btnGraphs.FlatAppearance.BorderSize = 0;

            this.btnExport.TabStop = false;
            this.btnExport.FlatStyle = FlatStyle.Flat;
            this.btnExport.FlatAppearance.BorderSize = 0;

            //******************
            // Initialise panels
            //******************                        
            ofChart = new fChart();
            this.SetPanel(ofChart, this.pnlC);

            ofMicroBit = new fMicroBit();
            ofMicroBit.StartMicroBit();
            this.ConnectionStatus();

            // Initial show/hide
            this.OnToggleStateChanged(this, null);
        }

        #endregion

        #region " Helpers "

        private void SetPanel(Form frm, Panel pnl)
        {
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            pnl.Controls.Clear();
            pnl.Controls.Add(frm);
        }

        private void HidePanels()
        {
            this.pnlA.Visible = false;
            this.pnlB.Visible = false;
            this.pnlC.Visible = false;
        }

        private void ConnectionStatus()
        {
            this.HidePanels();
            this.pnlA.Dock = DockStyle.Left;
            this.pnlA.Visible = true;
            this.pnlA.Width = this.Width - this.pnlMenu.Width;
        }

        #endregion

        #region " Button events "

        public void btnSerialPort_Click(object sender, EventArgs e)
        {
            this.ConnectionStatus();
        }

        private void btnSelectChannels_Click(object sender, EventArgs e)
        {
            this.HidePanels();
            this.pnlB.Dock = DockStyle.Left;
            this.pnlB.Visible = true;
            this.pnlB.Width = this.Width - this.pnlMenu.Width;
        }

        private void btnGraphs_Click(object sender, EventArgs e)
        {
            this.HidePanels();
            this.pnlC.Dock = DockStyle.Left;
            this.pnlC.Visible = true;
            this.pnlC.Width = this.Width - this.pnlMenu.Width;
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            ofAbout.Show(this);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.HidePanels();
            this.dlgOpenFile.Title = "Open CanSat Data in Excel (\"CanSat\" folder)";
            this.dlgOpenFile.DefaultExt = "csv";
            this.dlgOpenFile.Filter = "csv files (*.csv)|*.csv";
            this.dlgOpenFile.ShowDialog();

            if (File.Exists(dlgOpenFile.FileName))
                Process.Start(new ProcessStartInfo(dlgOpenFile.FileName) { UseShellExecute = true });
        }

        #endregion

        #region " Controls - manage states "
        private void OnToggleStateChanged(object sender, ToggleStateChangedEventArgs e)
        {
            var tgls = Utils.GetAllRecursive(this.pnlB, typeof(ToggleButton));

            foreach (ToggleButton tgl in tgls)
            {
                //Debug.Print(tgl.Name);
                this.CheckActiveState(tgl);
            }
        }

        private void CheckActiveState(ToggleButton tgl)
        {
            switch (tgl.Name)
            {
                case "tgl1":
                    this.ShowHideLabel(tgl, this.glbl1);
                    this.ShowHideLabel(tgl, this.lblCh1);
                    this.ShowHideLabel(tgl, this.lblDim1);
                    break;
                case "tgl2":
                    this.ShowHideLabel(tgl, this.glbl2);
                    this.ShowHideLabel(tgl, this.lblCh2);
                    this.ShowHideLabel(tgl, this.lblDim2);
                    break;
                case "tgl3":
                    this.ShowHideLabel(tgl, this.glbl3);
                    this.ShowHideLabel(tgl, this.lblCh3);
                    this.ShowHideLabel(tgl, this.lblDim3);
                    break;
                case "tgl4":
                    this.ShowHideLabel(tgl, this.glbl4);
                    this.ShowHideLabel(tgl, this.lblCh4);
                    this.ShowHideLabel(tgl, this.lblDim4);
                    break;
                case "tgl5":
                    this.ShowHideLabel(tgl, this.glbl5);
                    this.ShowHideLabel(tgl, this.lblCh5);
                    this.ShowHideLabel(tgl, this.lblDim5);
                    break;
                case "tgl6":
                    this.ShowHideLabel(tgl, this.glbl6);
                    this.ShowHideLabel(tgl, this.lblCh6);
                    this.ShowHideLabel(tgl, this.lblDim6);
                    break;
                case "tgl7":
                    this.ShowHideLabel(tgl, this.glbl7);
                    this.ShowHideLabel(tgl, this.lblCh7);
                    this.ShowHideLabel(tgl, this.lblDim7);
                    break;
                case "tgl8":
                    this.ShowHideLabel(tgl, this.glbl8);
                    this.ShowHideLabel(tgl, this.lblCh8);
                    this.ShowHideLabel(tgl, this.lblDim8);
                    break;
                case "tgl9":
                    this.ShowHideLabel(tgl, this.glbl9);
                    this.ShowHideLabel(tgl, this.lblCh9);
                    this.ShowHideLabel(tgl, this.lblDim9);
                    break;
                case "tgl10":
                    this.ShowHideLabel(tgl, this.glbl10);
                    this.ShowHideLabel(tgl, this.lblCh10);
                    this.ShowHideLabel(tgl, this.lblDim10);
                    break;
                case "tgl11":
                    this.ShowHideLabel(tgl, this.glbl11);
                    this.ShowHideLabel(tgl, this.lblCh11);
                    this.ShowHideLabel(tgl, this.lblDim11);
                    break;
                case "tgl12":
                    this.ShowHideLabel(tgl, this.glbl12);
                    this.ShowHideLabel(tgl, this.lblCh12);
                    this.ShowHideLabel(tgl, this.lblDim12);
                    break;
                case "tgl13":
                    this.ShowHideLabel(tgl, this.glbl13);
                    this.ShowHideLabel(tgl, this.lblCh13);
                    this.ShowHideLabel(tgl, this.lblDim13);
                    break;
                case "tgl14":
                    this.ShowHideLabel(tgl, this.glbl14);
                    this.ShowHideLabel(tgl, this.lblCh14);
                    this.ShowHideLabel(tgl, this.lblDim14);
                    break;
                default:
                    break;
            }
        }
        private void ShowHideLabel(ToggleButton tgl, Control ctl)
        {
            switch (tgl.ToggleState)
            {
                case ToggleButtonState.Active:
                    ctl.Visible = true;
                    break;
                case ToggleButtonState.Inactive:
                    ctl.Visible = false;
                    break;
                default:
                    break;
            }
        }

        private void chkAll_CheckStateChanged(object sender, EventArgs e)
        {
            switch (this.chkAll.CheckState)
            {
                case CheckState.Unchecked:
                    this.CheckUncheck(ChannelState.OFF);
                    break;
                case CheckState.Checked:
                    this.CheckUncheck(ChannelState.ON);
                    break;
                case CheckState.Indeterminate:
                    break;
                default:
                    break;
            }

            this.btnSerialPort.Focus();
        }

        private enum ChannelState { ON, OFF };

        private void CheckUncheck(ChannelState flag)
        {
            var tgls = Utils.GetAllRecursive(this.pnlB, typeof(ToggleButton));

            switch (flag)
            {
                case ChannelState.ON:
                    foreach (ToggleButton tgl in tgls)
                    {
                        tgl.ToggleState = ToggleButtonState.Active;
                        this.CheckActiveState(tgl);
                    }
                    break;
                case ChannelState.OFF:
                    foreach (ToggleButton tgl in tgls)
                    {
                        tgl.ToggleState = ToggleButtonState.Inactive;
                        this.CheckActiveState(tgl);
                    }
                    break;
                default:
                    break;
            }
        }

        private bool bOnOff = false;

        private void picRecord_Click(object sender, EventArgs e)
        {
            bOnOff = bOnOff ? false : true;
            
            switch (bOnOff)
            {
                case true:
                    this.picRecord.Image = Resources.Recording2;
                    this.lblRecording.Text = "RECORDING";
                    this.lblRecording.BackColor = Color.LightCoral;
                    EnableDisableToggles(false);
                    fMicroBit.instance.bRecording = true;
                    fMicroBit.instance.sExportCSV = "Export_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".csv";
                    break;
                case false:
                    this.picRecord.Image = Resources.Recording;
                    this.lblRecording.Text = "";
                    this.lblRecording.BackColor = Color.White;
                    EnableDisableToggles(true);
                    fMicroBit.instance.bRecording = false;
                    fMicroBit.instance.sExportCSV = "";
                    break;
            }            
        }

        private void EnableDisableToggles(bool state)
        {
            var tgls = Utils.GetAllRecursive(this.pnlB, typeof(ToggleButton));

            foreach (ToggleButton tgl in tgls)
                tgl.Enabled = state;            
        }

        #endregion
    }
}


