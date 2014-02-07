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

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer
{
    public partial class Splash : Form
    {
        #region ..Constructors..

        public Splash()
        {
            InitializeComponent();
            //
            this.Progress.Value = 0;
            this.Progress.Visible = false;
            this.lblInfo.Text = String.Empty;
            this.lblInfo.Visible = false;
            this.StepFactor = 1;
            this.TopMost = !Debugger.IsAttached;
        }

        #endregion

        public void SetProgress()
        {
            // set progress
            if (!this.Progress.Visible) this.Progress.Visible = true;
            this.Progress.Value += 100 / StepFactor;
        }

        public void SetProgressInfo(string infoTxt)
        {
            // set info label
            if (!this.lblInfo.Visible) this.lblInfo.Visible = true;
            this.lblInfo.Text = infoTxt;
        }

        #region ..Properties..

        public int StepFactor { get; set; }

        #endregion
    }
}
