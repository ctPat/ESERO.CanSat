
namespace ESERO.CanSat
{
    partial class fAbout
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
            this.picAbout = new System.Windows.Forms.PictureBox();
            this.lblTM1 = new System.Windows.Forms.Label();
            this.lblTM2 = new System.Windows.Forms.Label();
            this.lblTM3 = new System.Windows.Forms.Label();
            this.lblTM4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picAbout)).BeginInit();
            this.SuspendLayout();
            // 
            // picAbout
            // 
            this.picAbout.Image = global::ESERO.CanSat.Properties.Resources.Satellite_2;
            this.picAbout.Location = new System.Drawing.Point(12, 12);
            this.picAbout.Name = "picAbout";
            this.picAbout.Size = new System.Drawing.Size(282, 219);
            this.picAbout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAbout.TabIndex = 0;
            this.picAbout.TabStop = false;
            // 
            // lblTM1
            // 
            this.lblTM1.AutoSize = true;
            this.lblTM1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTM1.ForeColor = System.Drawing.Color.Maroon;
            this.lblTM1.Location = new System.Drawing.Point(311, 57);
            this.lblTM1.Name = "lblTM1";
            this.lblTM1.Size = new System.Drawing.Size(93, 17);
            this.lblTM1.TabIndex = 1;
            this.lblTM1.Text = "Dr Keith Quille";
            // 
            // lblTM2
            // 
            this.lblTM2.AutoSize = true;
            this.lblTM2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTM2.ForeColor = System.Drawing.Color.Maroon;
            this.lblTM2.Location = new System.Drawing.Point(311, 90);
            this.lblTM2.Name = "lblTM2";
            this.lblTM2.Size = new System.Drawing.Size(82, 17);
            this.lblTM2.TabIndex = 1;
            this.lblTM2.Text = "Patrick Cahill";
            // 
            // lblTM3
            // 
            this.lblTM3.AutoSize = true;
            this.lblTM3.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTM3.ForeColor = System.Drawing.Color.Maroon;
            this.lblTM3.Location = new System.Drawing.Point(311, 123);
            this.lblTM3.Name = "lblTM3";
            this.lblTM3.Size = new System.Drawing.Size(83, 17);
            this.lblTM3.TabIndex = 1;
            this.lblTM3.Text = "Kev Mooney";
            // 
            // lblTM4
            // 
            this.lblTM4.AutoSize = true;
            this.lblTM4.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTM4.ForeColor = System.Drawing.Color.Maroon;
            this.lblTM4.Location = new System.Drawing.Point(311, 156);
            this.lblTM4.Name = "lblTM4";
            this.lblTM4.Size = new System.Drawing.Size(88, 17);
            this.lblTM4.TabIndex = 1;
            this.lblTM4.Text = "Milan Murray\r\n";
            // 
            // fAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CaptionForeColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(416, 239);
            this.ControlBox = false;
            this.Controls.Add(this.lblTM4);
            this.Controls.Add(this.lblTM3);
            this.Controls.Add(this.lblTM2);
            this.Controls.Add(this.lblTM1);
            this.Controls.Add(this.picAbout);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.White;
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Name = "fAbout";
            this.ShowInTaskbar = false;
            this.ShowMaximizeBox = false;
            this.ShowMinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About the Team";
            this.VisibleChanged += new System.EventHandler(this.OnVisibleChanged);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.picAbout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picAbout;
        private System.Windows.Forms.Label lblTM1;
        private System.Windows.Forms.Label lblTM2;
        private System.Windows.Forms.Label lblTM3;
        private System.Windows.Forms.Label lblTM4;
    }
}