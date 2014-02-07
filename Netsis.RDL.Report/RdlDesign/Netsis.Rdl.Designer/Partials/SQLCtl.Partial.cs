﻿using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
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

            //tvTablesColumns.NodeMouseDoubleClick += tvTablesColumns_NodeMouseDoubleClick;
        }

        void tvTablesColumns_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
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
