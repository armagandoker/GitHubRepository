using fyiReporting.RdlDesign.Netsis.Rdl.Designer;
using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign
{
    partial class PropertyDialog
    {

        #region [_PRIVATES_]

        private void ChangeControlsPropertiesOnLoad()
        {

            tcProps.DrawItem += tcProps_DrawItem;
            tcProps.Appearance = TabAppearance.Normal;
            tcProps.DrawMode = TabDrawMode.OwnerDrawFixed;

            foreach (TabPage tp in tcProps.TabPages)
            {
                tp.BackColor = this.BackColor;
                tp.BorderStyle = BorderStyle.None;
            }

            bOK.Text = "Tamam";
            bCancel.Text = "İptal";
            bApply.Text = "Uygula";
            bDelete.Text = "Sil";

            if (tcProps.TabPages[tcProps.TabPages.Count - 1].Controls[0] is InteractivityCtl)
                tcProps.TabPages.RemoveAt(tcProps.TabPages.Count - 1);

            if (tcProps.TabPages[0].Controls[0] is ListCtl || tcProps.TabPages[0].Controls[0] is TableCtl)
            {
                tcProps.TabPages.RemoveAt(tcProps.TabPages.Count - 1);
                tcProps.TabPages.RemoveAt(tcProps.TabPages.Count - 1);               
            }

            foreach (UserControl ctl in this._TabPanels)
            {               
                UserControlPropertiesHelper.ChangeUserControlsPropertiesOnLoad(ctl);
            }            

        }

        void tcProps_DrawItem(object sender, DrawItemEventArgs e)
        {
            FakeTelericThemeHelper.ChangeTabColor(e, tcProps);
        }

        #endregion

        #region [_PROTECTEDS_]

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Size = new System.Drawing.Size(530, this.Height);
            FakeTelericThemeHelper.ApplyToolBar(this);
            ChangeControlsPropertiesOnLoad();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            FakeTelericThemeHelper.PaintToolBar(e, this);
        }

        protected override void OnClosed(EventArgs e)
        {
            frmNetsisRdlDeginer.ShowWaitDialog();
            base.OnClosed(e);
        }

        #endregion

    }
}
