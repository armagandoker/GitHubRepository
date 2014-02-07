using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign
{
    internal partial class DrillParametersDialog
    {

        #region [_PRIVATES_]

        private void ChangeControlsPropertiesOnLoad()
        {
            this.Text = "Rapor ve Parametreler";
            label1.Text = "Rapor Adı";
            bRefreshParms.Text = "Güncelle";
            bRefreshParms.Width = 60;
            bOK.Text = "Tamam";
            bCancel.Text = "İptal";

            dgtbName.HeaderText = "Parametre Adı";
            dgtbValue.HeaderText = "Değeri";
            dgtbOmit.HeaderText = "Atla";

            foreach (Control ctl in this.Controls)
            {
                ctl.Location = new Point(ctl.Location.X, ctl.Location.Y + FakeTelericThemeHelper.BORDER_SIZE);
            }
        }

        #endregion

        #region [_PROTECTEDS_]

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);            
            ChangeControlsPropertiesOnLoad();
            FakeTelericThemeHelper.ApplyToolBar(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            FakeTelericThemeHelper.PaintToolBar(e, this);
        }

        #endregion

    }
}
