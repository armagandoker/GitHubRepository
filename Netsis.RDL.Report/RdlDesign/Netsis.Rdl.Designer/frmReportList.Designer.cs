namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer
{
    partial class frmReportList
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn14 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn7 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn8 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.rbExecute = new Telerik.WinControls.UI.RadButton();
            this.rbOK = new Telerik.WinControls.UI.RadButton();
            this.rlw = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbExecute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlw.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.rbExecute);
            this.radPanel1.Controls.Add(this.rbOK);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel1.Location = new System.Drawing.Point(0, 305);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(830, 33);
            this.radPanel1.TabIndex = 0;
            // 
            // rbExecute
            // 
            this.rbExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbExecute.Location = new System.Drawing.Point(651, 5);
            this.rbExecute.Name = "rbExecute";
            this.rbExecute.Size = new System.Drawing.Size(84, 24);
            this.rbExecute.TabIndex = 1;
            this.rbExecute.Text = "radButton1";
            this.rbExecute.Click += new System.EventHandler(this.rbExecute_Click);
            // 
            // rbOK
            // 
            this.rbOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rbOK.Location = new System.Drawing.Point(738, 5);
            this.rbOK.Name = "rbOK";
            this.rbOK.Size = new System.Drawing.Size(84, 24);
            this.rbOK.TabIndex = 0;
            this.rbOK.Text = "Aç";
            this.rbOK.Click += new System.EventHandler(this.rbOk_Click);
            // 
            // rlw
            // 
            this.rlw.AutoSizeRows = true;
            this.rlw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rlw.Location = new System.Drawing.Point(0, 0);
            // 
            // rlw
            // 
            this.rlw.MasterTemplate.AllowAddNewRow = false;
            this.rlw.MasterTemplate.AllowDeleteRow = false;
            this.rlw.MasterTemplate.AllowDragToGroup = false;
            this.rlw.MasterTemplate.AllowEditRow = false;
            this.rlw.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn13.FieldName = "Id";
            gridViewTextBoxColumn13.HeaderText = "Kod";
            gridViewTextBoxColumn13.Name = "Id";
            gridViewTextBoxColumn13.Width = 75;
            gridViewTextBoxColumn14.FieldName = "Name";
            gridViewTextBoxColumn14.HeaderText = "Ad";
            gridViewTextBoxColumn14.Name = "Name";
            gridViewTextBoxColumn14.Width = 150;
            gridViewTextBoxColumn15.FieldName = "Description";
            gridViewTextBoxColumn15.HeaderText = "Açıklama";
            gridViewTextBoxColumn15.Name = "Description";
            gridViewTextBoxColumn15.Width = 201;
            gridViewTextBoxColumn16.FieldName = "Owner";
            gridViewTextBoxColumn16.HeaderText = "Sahip";
            gridViewTextBoxColumn16.Name = "Owner";
            gridViewTextBoxColumn16.Width = 100;
            gridViewDateTimeColumn7.FieldName = "Creation";
            gridViewDateTimeColumn7.HeaderText = "Yaratılma Tarihi";
            gridViewDateTimeColumn7.Name = "Creation";
            gridViewDateTimeColumn7.Width = 100;
            gridViewDateTimeColumn8.FieldName = "LastUpdate";
            gridViewDateTimeColumn8.HeaderText = "Güncelleme Tarihi";
            gridViewDateTimeColumn8.Name = "LastUpdate";
            gridViewDateTimeColumn8.Width = 101;
            this.rlw.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn13,
            gridViewTextBoxColumn14,
            gridViewTextBoxColumn15,
            gridViewTextBoxColumn16,
            gridViewDateTimeColumn7,
            gridViewDateTimeColumn8});
            this.rlw.MasterTemplate.EnableFiltering = true;
            this.rlw.MasterTemplate.MultiSelect = true;
            this.rlw.Name = "rlw";
            this.rlw.ShowGroupPanel = false;
            this.rlw.Size = new System.Drawing.Size(830, 305);
            this.rlw.TabIndex = 1;
            this.rlw.Text = "radGridView1";
            this.rlw.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.rlw_CellDoubleClick);
            // 
            // frmReportList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 338);
            this.Controls.Add(this.rlw);
            this.Controls.Add(this.radPanel1);
            this.Name = "frmReportList";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Raporlar";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmReportList_Load);
            this.Shown += new System.EventHandler(this.frmReportList_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rbExecute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlw.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton rbOK;
        private Telerik.WinControls.UI.RadButton rbExecute;
        private Telerik.WinControls.UI.RadGridView rlw;

    }
}
