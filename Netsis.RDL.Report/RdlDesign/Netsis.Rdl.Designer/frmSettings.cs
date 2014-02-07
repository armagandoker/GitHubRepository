using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using Netsis.Rdl.Contracts.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer
{
    public partial class frmSettings : Telerik.WinControls.UI.RadForm
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            rchkTurkishConversion.Checked = NReportConfig.Instance().TurkishConvertion;
            rtbAJMReportDownload.Text = NReportConfig.Instance().AJMReportDownloadPage;
        }

        private void rbOk_Click(object sender, EventArgs e)
        {
            NReportConfig.Instance().TurkishConvertion = rchkTurkishConversion.Checked;
            NReportConfig.Instance().AJMReportDownloadPage = rtbAJMReportDownload.Text;
            NReportConfig.Instance().SaveSettings();
        }

    }
}
