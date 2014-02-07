/* ====================================================================
   Copyright (C) 2004-2008  fyiReporting Software, LLC
   Copyright (C) 2011  Peter Gill <peter@majorsilence.com>

   This file is part of the fyiReporting RDL project.
	
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.


   For additional information, email info@fyireporting.com or visit
   the website www.fyiReporting.com.
*/
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Text;
using RdlViewer.Resources;
using fyiReporting.RDL;

namespace fyiReporting.RdlViewer
{
	/// <summary>
	/// RdlViewerFind finds text inside of the RdlViewer control
	/// </summary>
	public class RdlViewerFind : System.Windows.Forms.UserControl
    {
        private Button bClose;
        private Button bFindNext;
        private Button bFindPrevious;
        private CheckBox ckHighlightAll;
        private CheckBox ckMatchCase;
        private Label lFind;
        private Label lStatus;
        private TextBox tbFind;
        private PageItem position = null;

        private RdlViewer _Viewer;

        public RdlViewer Viewer
        {
            get { return _Viewer; }
            set { _Viewer = value; }
        }

        public RdlViewerFind()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RdlViewerFind));
			this.bClose = new System.Windows.Forms.Button();
			this.tbFind = new System.Windows.Forms.TextBox();
			this.bFindNext = new System.Windows.Forms.Button();
			this.bFindPrevious = new System.Windows.Forms.Button();
			this.ckHighlightAll = new System.Windows.Forms.CheckBox();
			this.ckMatchCase = new System.Windows.Forms.CheckBox();
			this.lFind = new System.Windows.Forms.Label();
			this.lStatus = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// bClose
			// 
			resources.ApplyResources(this.bClose, "bClose");
			this.bClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.bClose.FlatAppearance.BorderSize = 0;
			this.bClose.Name = "bClose";
			this.bClose.UseVisualStyleBackColor = false;
			this.bClose.Click += new System.EventHandler(this.bClose_Click);
			// 
			// tbFind
			// 
			resources.ApplyResources(this.tbFind, "tbFind");
			this.tbFind.Name = "tbFind";
			this.tbFind.TextChanged += new System.EventHandler(this.tbFind_TextChanged);
			// 
			// bFindNext
			// 
			resources.ApplyResources(this.bFindNext, "bFindNext");
			this.bFindNext.Name = "bFindNext";
			this.bFindNext.UseVisualStyleBackColor = true;
			this.bFindNext.Click += new System.EventHandler(this.bFindNext_Click);
			// 
			// bFindPrevious
			// 
			resources.ApplyResources(this.bFindPrevious, "bFindPrevious");
			this.bFindPrevious.Name = "bFindPrevious";
			this.bFindPrevious.UseVisualStyleBackColor = true;
			this.bFindPrevious.Click += new System.EventHandler(this.bFindPrevious_Click);
			// 
			// ckHighlightAll
			// 
			resources.ApplyResources(this.ckHighlightAll, "ckHighlightAll");
			this.ckHighlightAll.Name = "ckHighlightAll";
			this.ckHighlightAll.UseVisualStyleBackColor = true;
			this.ckHighlightAll.CheckedChanged += new System.EventHandler(this.ckHighlightAll_CheckedChanged);
			// 
			// ckMatchCase
			// 
			resources.ApplyResources(this.ckMatchCase, "ckMatchCase");
			this.ckMatchCase.Name = "ckMatchCase";
			this.ckMatchCase.UseVisualStyleBackColor = true;
			this.ckMatchCase.CheckedChanged += new System.EventHandler(this.ckMatchCase_CheckedChanged);
			// 
			// lFind
			// 
			resources.ApplyResources(this.lFind, "lFind");
			this.lFind.Name = "lFind";
			// 
			// lStatus
			// 
			resources.ApplyResources(this.lStatus, "lStatus");
			this.lStatus.ForeColor = System.Drawing.Color.Salmon;
			this.lStatus.Name = "lStatus";
			// 
			// RdlViewerFind
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.lStatus);
			this.Controls.Add(this.lFind);
			this.Controls.Add(this.ckMatchCase);
			this.Controls.Add(this.ckHighlightAll);
			this.Controls.Add(this.bFindPrevious);
			this.Controls.Add(this.bFindNext);
			this.Controls.Add(this.tbFind);
			this.Controls.Add(this.bClose);
			this.Name = "RdlViewerFind";
			this.VisibleChanged += new System.EventHandler(this.RdlViewerFind_VisibleChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        private void bClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void bFindNext_Click(object sender, EventArgs e)
        {
            FindNext();
        }

        public void FindNext()
        {
            if (_Viewer == null)
                throw new ApplicationException(Strings.RdlViewerFind_ErrorA_PropertyMustSetPriorFindNext);

            if (tbFind.Text.Length == 0)    // must have something to find
                return;

            RdlViewerFinds findOptions =
                ckMatchCase.Checked ?
                RdlViewerFinds.MatchCase :
                RdlViewerFinds.None;

            bool begin = position == null;
            position = _Viewer.Find(tbFind.Text, position, findOptions);
            if (position == null)
            {   
                if (!begin)     // if we didn't start from beginning already; try from beginning
                    position = _Viewer.Find(tbFind.Text, position, findOptions);

                lStatus.Text = position == null ? 
                    Strings.RdlViewerFind_FindNext_Phrase_not_found : Strings.RdlViewerFind_FindNext_Reached_end_of_report;

                _Viewer.HighlightPageItem = position;
                if (position != null)
                    _Viewer.ScrollToPageItem(position);
            }
            else
            {
                lStatus.Text = "";
                _Viewer.HighlightPageItem = position;
                _Viewer.ScrollToPageItem(position);
            }
        }

        private void bFindPrevious_Click(object sender, EventArgs e)
        {
            FindPrevious();
        }

        public void FindPrevious()
        {
            if (_Viewer == null)
                throw new ApplicationException(Strings.RdlViewerFind_ErrorA_PropertyMustSetPriorFindPrevious);

            if (tbFind.Text.Length == 0)    // must have something to find
                return;

            RdlViewerFinds findOptions = RdlViewerFinds.Backward |
                (ckMatchCase.Checked ? RdlViewerFinds.MatchCase : RdlViewerFinds.None);

            bool begin = position == null;
            position = _Viewer.Find(tbFind.Text, position, findOptions);
            if (position == null)
            {
                if (!begin)     // if we didn't start from beginning already; try from bottom
                    position = _Viewer.Find(tbFind.Text, position, findOptions);

                lStatus.Text = position == null ?
					Strings.RdlViewerFind_FindNext_Phrase_not_found : Strings.RdlViewerFind_FindPrevious_Reached_top_of_report;

                _Viewer.HighlightPageItem = position;
                if (position != null)
                    _Viewer.ScrollToPageItem(position);
            }
            else
            {
                lStatus.Text = "";
                _Viewer.HighlightPageItem = position;
                _Viewer.ScrollToPageItem(position);
            }
        }

        private void RdlViewerFind_VisibleChanged(object sender, EventArgs e)
        {
            lStatus.Text = "";
            if (this.Visible)
            {
                _Viewer.HighlightText = tbFind.Text;
                tbFind.Focus();
                FindNext();         // and go find the contents of the textbox
            }
            else
            {   // turn off any highlighting when find control not visible
                _Viewer.HighlightPageItem = position = null;
                _Viewer.HighlightText = null;
                _Viewer.HighlightAll = false;
                ckHighlightAll.Checked = false;
            }
        }

        private void tbFind_TextChanged(object sender, EventArgs e)
        {
            lStatus.Text = "";
            position = null;        // reset position when edit changes?? todo not really
            _Viewer.HighlightText = tbFind.Text;
            ckHighlightAll.Enabled = bFindNext.Enabled = bFindPrevious.Enabled =
                    tbFind.Text.Length > 0;
            if (tbFind.Text.Length > 0)
                FindNext();
        }

        private void ckHighlightAll_CheckedChanged(object sender, EventArgs e)
        {
            _Viewer.HighlightAll = ckHighlightAll.Checked;
        }

        private void ckMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            _Viewer.HighlightCaseSensitive = ckMatchCase.Checked;
        }
    }
}
