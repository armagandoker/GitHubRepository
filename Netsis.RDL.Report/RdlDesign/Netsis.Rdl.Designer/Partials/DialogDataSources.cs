using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign
{
    internal partial class DialogDataSources
    {

        #region [_PRIVATES_]

        private void ChangeControlsPropertiesOnLoad()
        {
            this.Text = "Rapor Data Source'ları";
            bAdd.Text = "Ekle";
            bRemove.Text = "Çıkar";
            label1.Text = "Data Source Adı";
            chkSharedDataSource.Visible = false;
            tbFilename.Visible = false;
            bGetFilename.Visible = false;

            lDataProvider.Text = "Veritabanı Tipi";
            string text = cbDataProvider.Text;
            cbDataProvider.Items.Clear();
            cbDataProvider.Items.Add("SQL");
            cbDataProvider.Items.Add("Oracle");
            cbDataProvider.Text = text;

            ckbIntSecurity.Text = "Integrated Security";
            lPrompt.Visible = false;
            tbPrompt.Visible = false;
            bTestConnection.Text = "Bağlantı Sına";
            bOK.Text = "Tamam";
            bCancel.Text = "İptal";
            bExprConnect.Visible = false;

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
