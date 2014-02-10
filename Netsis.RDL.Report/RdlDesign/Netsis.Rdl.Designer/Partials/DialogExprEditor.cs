using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign
{
    public partial class DialogExprEditor
    {

        #region [_PRIVATES_]

        private void ChangeControlsPropertiesOnLoad()
        {
            this.Text = "Formül Düzenle";
            lOp.Text = "Seç ve tıkla >>";
            lExpr.Text = "Formüller = ile başlar";
            bOK.Text = "Tamam";
            bCancel.Text = "İptal";

            tvOp.NodeMouseDoubleClick += tvOp_NodeMouseDoubleClick;
        }

        void tvOp_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbExpr.Text.Trim()) && e.Node.Parent != null && e.Node.Parent.Parent != null && tbExpr.Text.ToUpper().Contains(" FROM "))
            {
                int index = tbExpr.Text.ToUpper().IndexOf(" FROM ");
                tbExpr.Select(index, 0);
                tbExpr.SelectedText = ", ";
                tbExpr.Select(index + 2, 0);
            }        
            bCopy.PerformClick();
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
