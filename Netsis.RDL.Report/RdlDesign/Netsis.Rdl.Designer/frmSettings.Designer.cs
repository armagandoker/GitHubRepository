namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer
{
    partial class frmSettings
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
            this.rchkTurkishConversion = new Telerik.WinControls.UI.RadCheckBox();
            this.rbOk = new Telerik.WinControls.UI.RadButton();
            this.rtbAJMReportDownload = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.rchkTurkishConversion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbAJMReportDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // rchkTurkishConversion
            // 
            this.rchkTurkishConversion.Location = new System.Drawing.Point(12, 41);
            this.rchkTurkishConversion.Name = "rchkTurkishConversion";
            this.rchkTurkishConversion.Size = new System.Drawing.Size(179, 18);
            this.rchkTurkishConversion.TabIndex = 0;
            this.rchkTurkishConversion.Text = "Türkçe karakter çevrimi yapılsın.";
            // 
            // rbOk
            // 
            this.rbOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.rbOk.Location = new System.Drawing.Point(260, 70);
            this.rbOk.Name = "rbOk";
            this.rbOk.Size = new System.Drawing.Size(84, 24);
            this.rbOk.TabIndex = 1;
            this.rbOk.Text = "Kaydet";
            this.rbOk.Click += new System.EventHandler(this.rbOk_Click);
            // 
            // rtbAJMReportDownload
            // 
            this.rtbAJMReportDownload.Location = new System.Drawing.Point(116, 10);
            this.rtbAJMReportDownload.Name = "rtbAJMReportDownload";
            this.rtbAJMReportDownload.Size = new System.Drawing.Size(225, 20);
            this.rtbAJMReportDownload.TabIndex = 2;
            this.rtbAJMReportDownload.TabStop = false;
            this.rtbAJMReportDownload.Text = "ReportDownloadHandler.ashx";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(12, 12);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(98, 18);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "AJM Rapor Sayfası";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 101);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.rtbAJMReportDownload);
            this.Controls.Add(this.rbOk);
            this.Controls.Add(this.rchkTurkishConversion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayarlar";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rchkTurkishConversion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbAJMReportDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadCheckBox rchkTurkishConversion;
        private Telerik.WinControls.UI.RadButton rbOk;
        private Telerik.WinControls.UI.RadTextBox rtbAJMReportDownload;
        private Telerik.WinControls.UI.RadLabel radLabel1;
    }
}
