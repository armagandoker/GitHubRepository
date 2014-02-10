using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign
{
    internal partial class SQLCtl
    {

        #region [_PRIVATES_]

        private void ChangeControlsPropertiesOnLoad()
        {
            bOK.Text = "Tamam";
            bCancel.Text = "İptal";
            this.Text = "SQL Komutu";

            for (int i = 0; i < tvTablesColumns.Nodes.Count; i++)
            {
                if (tvTablesColumns.Nodes[i].Text == "Tables")
                {
                    tvTablesColumns.Nodes[i].Text = "Tablolar";
                    tvTablesColumns.Nodes[i].Name = "Tables";
                }
                else if (tvTablesColumns.Nodes[i].Text == "Views")
                {
                    tvTablesColumns.Nodes[i].Text = "View'lar";
                    tvTablesColumns.Nodes[i].Name = "Views";
                }
            }

            tvTablesColumns.NodeMouseDoubleClick += tvTablesColumns_NodeMouseDoubleClick;
            UserControlPropertiesHelper.AddSQLTableViewFilterToTreeView(tvTablesColumns);
            UserControlPropertiesHelper.AddNodesToTreeViewTag(tvTablesColumns);
        }

        void tvTablesColumns_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSQL.Text.Trim()) && e.Node.Parent != null && e.Node.Parent.Parent != null && tbSQL.Text.ToUpper().Contains(" FROM "))
            {
                int index = tbSQL.Text.ToUpper().IndexOf(" FROM ");
                tbSQL.Select(index, 0);
                tbSQL.SelectedText = ", ";
                tbSQL.Select(index + 2, 0);
            }      
            bMove.PerformClick();
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
