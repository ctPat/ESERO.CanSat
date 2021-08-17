using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESERO.CanSat
{
    public partial class fAbout : MetroForm
    {
        public fAbout()
        {
            InitializeComponent();

            // Timer parameters
            tmr = new System.Timers.Timer();
            tmr.Interval = 5000;
            tmr.Elapsed += tmr_Elapsed;
        }

        // Display timer
        private System.Timers.Timer tmr;

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            // Escape key pressed
            if ((int)e.KeyChar == 27)
                this.Hide();
            
            e.Handled = true;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            // Left mouse button clicked
            if (e.Button == MouseButtons.Left)
                this.Hide();
        }

        private void OnVisibleChanged(object sender, EventArgs e)
        {
            // Detect show/hide
            if (this.Visible == true)
                tmr.Start();
            else
                tmr.Stop();
        }

        // Event sink
        private void tmr_Elapsed(object sender, EventArgs e)
        {
            // On different thread
            this.Invoke(new System.Action(() =>
            {
                this.Hide();
            }));          
        }
    }
}
