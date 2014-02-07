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
    internal partial class FindTab
    {        

        #region [_PRIVATES_]

        private void ChangeControlsPropertiesOnLoad()
        {
            this.Text = "Ara";

            tcFRG.Location = new Point(tcFRG.Location.X, tcFRG.Location.Y + FakeTelericThemeHelper.BORDER_SIZE);

            tcFRG.DrawItem += tcFRG_DrawItem;
            tcFRG.Appearance = TabAppearance.Normal;
            tcFRG.DrawMode = TabDrawMode.OwnerDrawFixed;

            foreach (TabPage tp in tcFRG.TabPages)
            {
                tp.BackColor = this.BackColor;
                tp.BorderStyle = BorderStyle.None;
            }

            tcFRG.TabPages[0].Text = "Ara";
            tcFRG.TabPages[1].Text = "Değiştir";
            tcFRG.TabPages[2].Text = "Git";

            btnNext.Text = "Sonraki..";
            groupBox1.Text = "Arama Yönü";
            label1.Text = "Ara";
            radioUp.Text = "Yukarı";
            radioDown.Text = "Aşağı";
            chkCase.Text = "Büyük/Küçük harf Duyarlı";
            chkCase.Width = 180;
            btnCancel.Text = "Kapat";
            label3.Text = "Ara";
            label2.Text = "Değiştir";
            chkMatchCase.Text = "Büyük/Küçük harf Duyarlı";
            chkMatchCase.Width = 180;
            btnFindNext.Text = "Sonrakini Bul";
            btnReplace.Text = "Değiştir";
            btnReplaceAll.Text = "Tümünü Değiştir";
            bCloseReplace.Text = "Kapat";
            label4.Text = "Satır Numarası";
            btnGoto.Text = "Git";
            bCloseGoto.Text = "Kapat";            
        }

        #endregion

        #region [_PROTECTEDS_]

        void tcFRG_DrawItem(object sender, DrawItemEventArgs e)
        {
            FakeTelericThemeHelper.ChangeTabColor(e, tcFRG);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FakeTelericThemeHelper.ApplyToolBar(this);
            ChangeControlsPropertiesOnLoad();            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            FakeTelericThemeHelper.PaintToolBar(e, this);
        }

        #endregion

    }
}
