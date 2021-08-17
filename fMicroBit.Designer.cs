
namespace ESERO.CanSat
{
    partial class fMicroBit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Text.DecoderReplacementFallback decoderReplacementFallback1 = new System.Text.DecoderReplacementFallback();
            System.Text.EncoderReplacementFallback encoderReplacementFallback1 = new System.Text.EncoderReplacementFallback();
            this.spMain = new System.IO.Ports.SerialPort(this.components);
            this.SuspendLayout();
            // 
            // spMain
            // 
            this.spMain.BaudRate = 9600;
            this.spMain.DataBits = 8;
            this.spMain.DiscardNull = false;
            this.spMain.DtrEnable = false;            
            this.spMain.Handshake = System.IO.Ports.Handshake.None;
            this.spMain.NewLine = "\n";
            this.spMain.Parity = System.IO.Ports.Parity.None;
            this.spMain.ParityReplace = ((byte)(63));
            this.spMain.PortName = "COM1";
            this.spMain.ReadBufferSize = 4096;
            this.spMain.ReadTimeout = -1;
            this.spMain.ReceivedBytesThreshold = 1;
            this.spMain.RtsEnable = false;
            this.spMain.StopBits = System.IO.Ports.StopBits.One;
            this.spMain.WriteBufferSize = 2048;
            this.spMain.WriteTimeout = -1;
            // 
            // frmMicroBit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "frmMicroBit";
            this.Text = "frmMicroBit";
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort spMain;
    }
}