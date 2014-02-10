using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign
{
    internal partial class DialogNewChart
    {

        #region [_PRIVATES_]

        private void ChangeControlsPropertiesOnLoad()
        {
            this.Text = "Yeni Grafik";
            label7.Text = "Grafik Tipi";
            label6.Text = "Alt Tip";
            label2.Text = "Dataset Alanları";
            label3.Text = "Grafik (X) Grup Kategorileri";
            label4.Text = "Grafik Serileri";
            bCategoryUp.Text = "Yukarı";
            bCategoryDown.Text = "Aşağı";
            bCategoryDelete.Text = "Sil";
            lChartData2.Text = "Y Koordinatı";
            lChartData3.Text = "Balon Boyutu";

            bSeriesUp.Text = "Yukarı";
            bSeriesDown.Text = "Aşağı";
            bSeriesDelete.Text = "Sil";

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
