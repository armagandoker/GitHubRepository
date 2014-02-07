using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RdlViewer.Resources;
using fyiReporting.RDL;

namespace fyiReporting.RdlViewer
{
    public partial class RdlViewer
    {

        public void SetParameters()
        {
            foreach (Control ctl in _ParameterPanel.Controls)
            {
                if (ctl.Tag is UserReportParameter)
                {
                    UserReportParameter up = (UserReportParameter)ctl.Tag ;
                    if (ctl is TextBox)
                    {
                        this.ParametersTextValidated(ctl, new EventArgs());
                    }
                    else if (ctl is ComboBox)
                    {
                        this.ParametersLeave(ctl, new EventArgs());
                    }


                    if (_Parameters.Contains(up.Name))
                        _Parameters[up.Name] = up.Value;
                    else
                        _Parameters.Add(up.Name, up.Value);
                }
            }
        }

    }
}