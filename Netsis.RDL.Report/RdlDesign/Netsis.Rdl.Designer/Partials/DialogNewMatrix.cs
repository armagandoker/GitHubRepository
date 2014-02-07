using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign
{
    internal partial class DialogNewMatrix
    {

        #region [_PRIVATES_]

        private void ChangeControlsPropertiesOnLoad()
        {
            this.Text = "Yeni Matris";            
            label5.Text = "Matris Hücre Formülü";
            label2.Text = "Dataset Alanları";
            label3.Text = "Matris Kolonları";
            label4.Text = "Matris Satırları";
            bColumnUp.Text = "Yukarı";
            bColumnDown.Text = "Aşağı";
            bColumnDelete.Text = "Sil";

            bRowUp.Text = "Yukarı";
            bRowDown.Text = "Aşağı";
            bRowDelete.Text = "Sil";            

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
