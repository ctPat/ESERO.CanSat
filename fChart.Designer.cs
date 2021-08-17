
namespace ESERO.CanSat
{
    partial class fChart
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
            this.crtMain = new Syncfusion.Windows.Forms.Chart.ChartControl();
            this.cboChannels = new System.Windows.Forms.ComboBox();
            this.gradientLabel1 = new Syncfusion.Windows.Forms.Tools.GradientLabel();
            this.SuspendLayout();
            // 
            // crtMain
            // 
            this.crtMain.ChartArea.CursorLocation = new System.Drawing.Point(0, 0);
            this.crtMain.ChartArea.CursorReDraw = false;
            this.crtMain.IsWindowLess = false;
            // 
            // 
            // 
            this.crtMain.Legend.Location = new System.Drawing.Point(729, 31);
            this.crtMain.Localize = null;
            this.crtMain.Location = new System.Drawing.Point(12, 12);
            this.crtMain.Name = "crtMain";
            this.crtMain.PrimaryXAxis.LogLabelsDisplayMode = Syncfusion.Windows.Forms.Chart.LogLabelsDisplayMode.Default;
            this.crtMain.PrimaryXAxis.Margin = true;
            this.crtMain.PrimaryYAxis.LogLabelsDisplayMode = Syncfusion.Windows.Forms.Chart.LogLabelsDisplayMode.Default;
            this.crtMain.PrimaryYAxis.Margin = true;
            this.crtMain.Size = new System.Drawing.Size(838, 411);
            this.crtMain.TabIndex = 0;
            // 
            // 
            // 
            this.crtMain.Title.Name = "Default";
            this.crtMain.VisualTheme = "";
            // 
            // cboChannels
            // 
            this.cboChannels.BackColor = System.Drawing.Color.White;
            this.cboChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChannels.FormattingEnabled = true;
            this.cboChannels.Location = new System.Drawing.Point(196, 441);
            this.cboChannels.Name = "cboChannels";
            this.cboChannels.Size = new System.Drawing.Size(189, 23);
            this.cboChannels.TabIndex = 1;
            this.cboChannels.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);
            // 
            // gradientLabel1
            // 
            this.gradientLabel1.BeforeTouchSize = new System.Drawing.Size(132, 23);
            this.gradientLabel1.BorderAppearance = System.Windows.Forms.BorderStyle.None;
            this.gradientLabel1.BorderColor = System.Drawing.Color.Transparent;
            this.gradientLabel1.BorderSides = ((System.Windows.Forms.Border3DSide)((((System.Windows.Forms.Border3DSide.Left | System.Windows.Forms.Border3DSide.Top) 
            | System.Windows.Forms.Border3DSide.Right) 
            | System.Windows.Forms.Border3DSide.Bottom)));
            this.gradientLabel1.Location = new System.Drawing.Point(58, 441);
            this.gradientLabel1.Name = "gradientLabel1";
            this.gradientLabel1.Size = new System.Drawing.Size(132, 23);
            this.gradientLabel1.TabIndex = 2;
            this.gradientLabel1.Text = "Chart Channel values:";
            this.gradientLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(862, 510);
            this.Controls.Add(this.gradientLabel1);
            this.Controls.Add(this.cboChannels);
            this.Controls.Add(this.crtMain);
            this.Name = "fChart";
            this.Text = "ESERO.CanSat Micro:bit Data";
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Chart.ChartControl crtMain;
        private System.Windows.Forms.ComboBox cboChannels;
        private Syncfusion.Windows.Forms.Tools.GradientLabel gradientLabel1;
    }
}