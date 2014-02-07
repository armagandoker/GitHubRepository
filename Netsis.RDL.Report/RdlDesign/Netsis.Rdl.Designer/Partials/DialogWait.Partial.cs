using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RdlViewer.Resources;

namespace fyiReporting.RdlViewer
{
    public partial class DialogWait
    {

        protected override void OnLoad(EventArgs e)
        {            
            base.OnLoad(e);
            this.BackColor =  Color.FromArgb(191, 219, 255);
            this.Text = "Rapor Hazýrlanýyor...";
            this.label1.Text = "Rapor Hazýrlanýrken Lütfen bekleyiniz";
            this.label2.Text = "Geçen Zaman";
        }

    }
}