using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign
{
    public partial class DialogListOfStrings
    {

        #region [_PRIVATES_]

        private void ChangeControlsPropertiesOnLoad()
        {                        
            label1.Text = "Tüm değerleri ayrı satırlar halinde giriniz";

            bOK.Text = "Tamam";
            bCancel.Text = "İptal";

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
