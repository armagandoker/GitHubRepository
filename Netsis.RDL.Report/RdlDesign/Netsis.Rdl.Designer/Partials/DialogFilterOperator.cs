using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign
{
    public partial class DialogFilterOperator
    {

        #region [_PRIVATES_]

        private void ChangeControlsPropertiesOnLoad()
        {
            this.Text = "Filitre Operatörü Seçim";
            lOp.Text = "Filitre operatörü seç";
            lOp.Location = new System.Drawing.Point(lOp.Location.X, lOp.Location.Y + FakeTelericThemeHelper.BORDER_SIZE);
            cbOperator.Location = new System.Drawing.Point(cbOperator.Location.X, cbOperator.Location.Y + FakeTelericThemeHelper.BORDER_SIZE);

            bOK.Text = "Tamam";
            bCancel.Text = "İptal";            
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
