using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESERO.CanSat
{
    public partial class fChart : MetroForm
    {
        public static fChart instance;
        ChartSeries series;

        private System.Timers.Timer tmr = new System.Timers.Timer();
        
        public fChart()
        {
            InitializeComponent();
            instance = this;            

            this.crtMain.Series.Clear();
            series = new ChartSeries("", ChartSeriesType.Line);
            this.crtMain.Series.Add(series);

            this.PopulateChannelsCombo();

            tmr.Interval = 1000;
            tmr.Elapsed += tmr_Elapsed;
        }

        public void LoadChart(int iIndex)
        {
            try
            {
                this.crtMain.Series.Clear();
                ChartSeries series = new ChartSeries(fMicroBit.instance.dt.Columns[iIndex].ColumnName, ChartSeriesType.Line);

                for (int i = 0; i < fMicroBit.instance.lastRows.Count(); i++)
                {                    
                    series.Points.Add(i, Double.Parse(fMicroBit.instance.lastRows[i][iIndex].ToString()));                  
                }

                this.crtMain.PrimaryXAxis.DrawGrid = false;
                this.crtMain.PrimaryYAxis.DrawGrid = false;                

                series.Style.Interior = new Syncfusion.Drawing.BrushInfo(Color.Red);
                series.Style.TextColor = Color.Maroon;

                series.Style.Symbol.Color = Color.LightGreen;

                series.Style.Symbol.Shape = ChartSymbolShape.Circle;

                ChartBordersInfo border = new ChartBordersInfo();
                border.Outer = new ChartBorder(ChartBorderStyle.Solid, Color.White);
                border.Inner = new ChartBorder(ChartBorderStyle.DashDot, Color.Cyan);       
                series.Style.ElementBorders = border;

                this.crtMain.Series.Add(series);
            }
            catch (Exception ex)
            {
                string sError = ex.ToString();
                Utils.AppendToExceptionsFile(("Exceptions_" + DateTime.Now.Date.ToString("dd.MM.yyyy") + ".txt"), sError);
            }            
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.tmr.Start();
            Cursor.Current = Cursors.Default;
        }

        private void PopulateChannelsCombo()
        {
            this.cboChannels.Items.Clear();

            this.cboChannels.Items.Add("Temp - External");
            this.cboChannels.Items.Add("Pressure - External");
            this.cboChannels.Items.Add("Altitude");
            this.cboChannels.Items.Add("Temp - Internal");
            this.cboChannels.Items.Add("Acceleration - X Direction");
            this.cboChannels.Items.Add("Acceleration - Y Direction");
            this.cboChannels.Items.Add("Acceleration - Z Direction");
            this.cboChannels.Items.Add("Acceleration - Calculated");
            this.cboChannels.Items.Add("Light Level");
            this.cboChannels.Items.Add("Rotation - Pitch");
            this.cboChannels.Items.Add("Rotation - Roll");
            this.cboChannels.Items.Add("Magnetic Force");

            // Ignore V2 for now
            //if (fMicroBit.instance.sVersion == "V2")
            //{
            //    this.cboChannels.Items.Add("Touch");
            //    this.cboChannels.Items.Add("Sound");
            //}
        }
        
        private void tmr_Elapsed(object sender, EventArgs e)
        {
            fChart.instance.Invoke(new System.Action(() =>
            {
                this.LoadChart(this.cboChannels.SelectedIndex + 1);
            }));
        }

    }
}
