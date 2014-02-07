using fyiReporting.RDL;
using fyiReporting.RdlDesign;
using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Components;
using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using fyiReporting.RdlDesign.Resources;
using fyiReporting.RdlEngine;
using Netsis.Framework.Service;
using Netsis.Framework.Service.Client;
using Netsis.Rdl.Contracts.Entities;
using Netsis.Rdl.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls.UI.Localization;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer
{
    public partial class frmNetsisRdlDeginer : Telerik.WinControls.UI.RadForm
    {        

        #region [_FIELDS_]

        frmMDIChild mc = null;
        static frmNetsisRdlDeginer _designer = null;

        #endregion

        #region [_CONSTRUCTORS_]

        public frmNetsisRdlDeginer()
        {
            InitializeComponent();          
            PrepareScreen();     
            _designer = this;
        }

        #endregion

        #region [_PRIVATES_]

        private frmMDIChild CreateMDIChild(NReportInfo reportInfo)
        {
            try
            {
                foreach (DocumentWindow doc in radDock1.DockWindows.DocumentWindows)
                {
                    if (!(doc.Controls[0] is frmMDIChild))
                        continue;
                    frmMDIChild tmpChild = (frmMDIChild)doc.Controls[0];
                    if (tmpChild.ReportInfo.Id != null && reportInfo.Id != null && tmpChild.ReportInfo.Id == reportInfo.Id.Value)
                    {
                        radDock1.ActivateWindow(doc);
                        return (frmMDIChild)doc.Controls[0];
                    }
                }
                mc = new frmMDIChild(this.ClientRectangle.Width * 3 / 5, this.ClientRectangle.Height * 3 / 5);
                mc.OnSelectionChanged += new MDIChild.RdlChangeHandler(SelectionChanged);
                mc.OnReportItemInserted += new MDIChild.RdlChangeHandler(ReportItemInserted);
                mc.OnDesignTabChanged += new MDIChild.RdlChangeHandler(DesignTabChanged);
                mc.OnSelectionMoved += new MDIChild.RdlChangeHandler(SelectionMoved);
                mc.OnOpenSubreport += new DesignCtl.OpenSubreportEventHandler(OpenSubReportEvent);
                mc.OnHeightChanged += new DesignCtl.HeightEventHandler(HeightChanged);
                mc.Hit += mc_Hit;

                mc.MdiParent = this;
                mc.Tab = new TabPage();
                mc.ReportInfo = reportInfo;

                SetLocalMDIChildWindow(mc);

                DocumentWindow dw = new DocumentWindow(reportInfo.Name);
                dw.SizeChanged += dw_SizeChanged;
                dw.Controls.Add(mc);
                radDock1.AddDocument(dw);
                frmNetsisRdlDeginer.ShowWaitDialog();
                mc.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (mc != null)
                    mc.Close();
                return null;
            }
            return mc;
        }

        void dw_SizeChanged(object sender, EventArgs e)
        {
            DocumentWindow dw = (DocumentWindow)sender;
            ((Form)dw.Controls[0]).WindowState = FormWindowState.Normal;
            ((Form)dw.Controls[0]).WindowState = FormWindowState.Maximized;
        }

        void mc_Hit(object sender, Point hitPoint)
        {
            this.rssHitLocation.Text = String.Format("X : {0}, Y: {1}", hitPoint.X.ToString(), hitPoint.Y.ToString());
        }

        private void SetSelectedComponentStyle(string name, string value)
        {
            if (mc == null)
                return;

            mc.ApplyStyleToSelected(name, value);
            mainProperties.ResetSelection(mc.RdlEditor.DrawCtl, mc.RdlEditor.DesignCtl);
            ReportItemSelectedChanged();

            mc.Focus();
        }

        private int InitToolbarFont()
        {
            foreach (FontFamily ff in FontFamily.Families)
            {
                tscFontFamilies.Items.Add(ff.Name);
            }

            for (int i = 0; i < 10; i++)
            {
                tsmFontFamilies.DropDownItems.Add((string)tscFontFamilies.Items[i]);
            }
            tsmFontFamilies.DropDownItems.Add("...Tümünü Yükle...");
            return tscFontFamilies.Width;
        }

        private int InitToolbarFontSize()
        {
            string[] sizes = new string[] { "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72" };
            tscFontSize.Items.AddRange(sizes);
            foreach (string ff in sizes)
            {
                tsmFontSize.DropDownItems.Add(ff);
            }
            return tscFontSize.Width;
        }

        private void InitToolbarColors()
        {
            tsmBackgroundColor.DropDown = NColorToolStriptItem.GetDropDownColorItems();
            tsmFontColor.DropDown = NColorToolStriptItem.GetDropDownColorItems();
        }

        private bool CloseDesignToolWindow()
        {
            if (mc != null && !mc.CloseOk())
                return false;
            if (mc != null)
                mc.Dispose();
            SetLocalMDIChildWindow(null);
            mainProperties.ResetSelection(null, null);
            return true;
        }

        private void SelectionMoved(object sender, System.EventArgs e)
        {
            if (mc != null)
                SetStatusNameAndPosition();
        }

        private void SetStatusNameAndPosition()
        {
            if (mc == null)
                return;
            mainProperties.ResetSelection(mc.RdlEditor.DrawCtl, mc.RdlEditor.DesignCtl);
            if (mc.DesignTab == "design")
                SetStatusNameAndPositionDesign(mc);
            else if (mc.DesignTab == "edit")
                SetStatusNameAndPositionEdit(mc);
            else
                rssHitLocation.Text = "";
        }

        private void HeightChanged(object sender, HeightEventArgs e)
        {
            if (e.Height == null)
            {
                mainProperties.ResetSelection(mc.RdlEditor.DrawCtl, mc.RdlEditor.DesignCtl);
                rssHitLocation.Text = "";
                return;
            }
            var rinfo = new RegionInfo(CultureInfo.CurrentCulture.LCID);
            var unit = rinfo.IsMetric ? Strings.RdlDesigner_Status_cm : Strings.RdlDesigner_Status_in;
            var h = DesignXmlDraw.GetSize(e.Height) / DesignXmlDraw.POINTSIZED;
            if (rinfo.IsMetric)
                h *= 2.54f;
            rssHitLocation.Text = string.Format("   {1}={0:0.00}{2}        ", h, Strings.RdlDesigner_Status_Height, unit);
        }

        private void OpenSubReportEvent(object sender, SubReportEventArgs e)
        {
            NReportInfo subReport = NReportInfoHelper.GetNSubReport(e.SubReportName);
            if (subReport != null)
                CreateMDIChild(subReport);
        }

        private void SetStatusNameAndPositionDesign(MDIChild mc)
        {
            if (mc.DrawCtl.SelectedCount <= 0)
            {
                rssHitLocation.Text = "";
                return;
            }

            // Handle position
            var pos = mc.SelectionPosition;
            var sz = mc.SelectionSize;
            string spos;

            if (pos.X == float.MinValue) // no item selected is probable cause
            {
                spos = "";
            }
            else
            {
                var rinfo = new RegionInfo(CultureInfo.CurrentCulture.LCID);
                double m72 = DesignXmlDraw.POINTSIZED;

                var x = pos.X / m72;
                var y = pos.Y / m72;
                var unit = rinfo.IsMetric ? Strings.RdlDesigner_Status_cm : Strings.RdlDesigner_Status_in;

                if (rinfo.IsMetric)
                {
                    x *= 2.54d;
                    y *= 2.54d;
                }

                if (sz.Width == float.MinValue) // item is in a table/matrix is probably cause
                {
                    spos = string.Format("   x={0:0.00}{2}, y={1:0.00}{2}        ",
                                         x, y, unit);
                }
                else
                {
                    var w = sz.Width / m72;
                    var h = sz.Height / m72;

                    if (rinfo.IsMetric)
                    {
                        w *= 2.54d;
                        h *= 2.54d;
                    }

                    spos = string.Format("   x={0:0.00}{4}, y={1:0.00}{4}, w={2:0.00}{4}, h={3:0.00}{4}        ",
                                         x, y, w, h, unit);
                }
            }
            rssHitLocation.Text = mc.SelectionName + " - " + spos;
        }

        private void SetStatusNameAndPositionEdit(MDIChild mc)
        {
            rssHitLocation.Text = string.Format("{2} {0}  {3} {1}", mc.CurrentLine, mc.CurrentCh, Strings.RdlDesigner_Status_Ln, Strings.RdlDesigner_Status_Ch);
        }

        private void dataSetsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            OpenDataSets((string)menu.Tag);
        }

        private void SetVisibilityOfXMLMenus(bool visibility)
        {
            tsmFindNext.Enabled = visibility;
            tsmReplace.Enabled = visibility;
            acFormatRDL.Enabled = visibility;
            tsmGoTo.Enabled = visibility;
        }

        private void SetVisibilityOfPreviewMenus(bool visibility)
        {
            tsbHTML.Visible = visibility;
            tsbWord.Visible = visibility;
            tsbPdf.Visible = visibility;
            tsbToExcel.Visible = visibility;
        }

        private void SetVisibilityOfDesignMenus(bool visibility)
        {
            acSave.Enabled = visibility;
            tsmClose.Enabled = visibility;
            acCut.Enabled = visibility;
            acCopy.Enabled = visibility;
            acPaste.Enabled = visibility;
            acItemDelete.Enabled = visibility;
            tsmSelectAll.Enabled = visibility;
            acUndo.Enabled = visibility;
            acRedo.Enabled = visibility;
            tsmDataSet.Enabled = visibility;
            tsmDataSource.Enabled = visibility;
            tsmImages.Enabled = visibility;
            acBold.Enabled = visibility;
            acItalic.Enabled = visibility;
            acUnderline.Enabled = visibility;
            acCentre.Enabled = visibility;
            acLeftAlign.Enabled = visibility;
            acRightAlign.Enabled = visibility;
            tscBackColor.Enabled = visibility;
            tscFontFamilies.Enabled = visibility;
            tscFontSize.Enabled = visibility;
            tscForeColor.Enabled = visibility;
            tsmFormat.Visible = visibility;
            tvDatas.Visible = visibility;
            rmiAddNewDsDt.Enabled = visibility;

            FillDataSourceSets();
        }

        private void SetVisibilityOfMDIFormMenus(bool visibility)
        {
            acShowDesigner.Enabled = visibility;
            acShowRDL.Enabled = visibility;
            acShowPreview.Enabled = visibility;
        }

        private void TabChange()
        {
            bool xmlMenuVisibility = false;
            bool designMenuVisibility = false;
            bool mdiFormMenuVisibility = false;
            bool PreviewMenuVisibility = false;
            if (mc != null)
            {
                designMenuVisibility = true;
                mdiFormMenuVisibility = true;
                switch (mc.DesignTab)
                {
                    case "edit":
                        xmlMenuVisibility = true;
                        acFind.Enabled = true;
                        break;
                    case "design":
                        acFind.Enabled = false;
                        break;
                    case "preview":
                        mc.RdlEditor.ShowPreviewWaitDialog(false);
                        acFind.Enabled = true;
                        designMenuVisibility = false;
                        PreviewMenuVisibility = true;
                        break;
                    default:
                        break;
                }
            }
            SetVisibilityOfXMLMenus(xmlMenuVisibility);
            SetVisibilityOfDesignMenus(designMenuVisibility);
            SetVisibilityOfMDIFormMenus(mdiFormMenuVisibility);
            SetVisibilityOfPreviewMenus(PreviewMenuVisibility);
            SetStatusNameAndPosition();
        }

        private void SetTab(string tabName)
        {
            if (mc == null)
                return;
            mc.RdlEditor.DesignTab = tabName;
        }

        private void SetLocalMDIChildWindow(frmMDIChild form)
        {
            mc = form;
            string formName = (mc == null ? string.Empty : form.ReportInfo.Name + " - ");
            this.Text = String.Format("{0}Rapor Dizayn", formName);
            TabChange();
        }

        private void ReportItemSelectedChanged()
        {
            if (mc == null)
                return;
            mainProperties.ResetSelection(mc.RdlEditor.DrawCtl, mc.RdlEditor.DesignCtl);

            StyleInfo si = mc.SelectedStyle;
            if (si == null)
                return;

            acCentre.Checked = si.TextAlign == TextAlignEnum.Center ? true : false;
            acLeftAlign.Checked = si.TextAlign == TextAlignEnum.Left ? true : false;
            acRightAlign.Checked = si.TextAlign == TextAlignEnum.Right ? true : false;
            acBold.Checked = si.IsFontBold() ? true : false;
            acItalic.Checked = si.FontStyle == FontStyleEnum.Italic ? true : false;
            acUnderline.Checked = si.TextDecoration == TextDecorationEnum.Underline ? true : false;
            tscFontFamilies.Text = si.FontFamily;

            string rs = string.Format(NumberFormatInfo.InvariantInfo, "{0:0.#}", si.FontSize);
            tscFontSize.Text = rs;

            tscForeColor.ColorText = (si.Color.IsEmpty ? si.ColorText : ColorTranslator.ToHtml(si.Color));
            tscBackColor.ColorText = (si.BackgroundColor.IsEmpty ? si.BackgroundColorText : ColorTranslator.ToHtml(si.BackgroundColor));
            tsmBackgroundColor.Image = NColorToolStriptItem.GetSelectedColorImage(tscBackColor.ColorText);
            tsmFontColor.Image = NColorToolStriptItem.GetSelectedColorImage(tscForeColor.ColorText);
            //SetStatusNameAndPosition();
        }

        private void SetUnChecked(ToolStripItemCollection coll)
        {
            foreach (ToolStripMenuItem mi in coll)
            {
                mi.Checked = false;
            }
            ReportItemSelectedChanged();
        }

        private void PrepareScreen()
        {
            InitToolbarFont();
            InitToolbarFontSize();
            InitToolbarColors();

            mainProperties.Controls.Find("bClose", false)[0].Visible = false;

            Label lb = (Label)mainProperties.Controls.Find("label1", false)[0];
            lb.Visible = false;

            ComboBox cb = (ComboBox)mainProperties.Controls.Find("cbReportItems", false)[0];
            cb.Location = new Point(cb.Location.X, cb.Location.Y - lb.Height);

            PropertyGrid pg = (PropertyGrid)mainProperties.Controls.Find("pgSelected", false)[0];
            pg.Location = new Point(pg.Location.X, pg.Location.Y - lb.Height);
            pg.Size = new System.Drawing.Size(pg.Width, pg.Height + lb.Height);

            mainProperties.Dock = DockStyle.Fill;
            SetLocalMDIChildWindow(null);
        }

        private void OpenReportList(ReportListViewType listType)
        {
            frmReportList rl = new frmReportList(listType);
            if (rl.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            foreach (NReportInfo info in rl.SelectedNReports)
            {
                CreateMDIChild(info);
            }
        }

        private void ExportReport(OutputPresentationType renderType)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Rapor Kaydet";

            switch (renderType)
            {
                case OutputPresentationType.HTML:
                    sfd.Filter = "HTML Dosyasý (*.html)|*.html";
                    break;
                case OutputPresentationType.PDF:
                    sfd.Filter = "PDF Dosyasý (*.pdf)|*.pdf";
                    break;
                case OutputPresentationType.PDFOldStyle:
                    break;
                case OutputPresentationType.XML:
                    break;
                case OutputPresentationType.ASPHTML:
                    break;
                case OutputPresentationType.Internal:
                    break;
                case OutputPresentationType.MHTML:
                    break;
                case OutputPresentationType.CSV:
                    break;
                case OutputPresentationType.RTF:
                    break;
                case OutputPresentationType.Word:
                    sfd.Filter = "Word Dosyasý (*.doc)|*.doc";
                    break;
                case OutputPresentationType.Excel:
                    sfd.Filter = "Excel Dosyasý (*.xlsx)|*.xlsx";
                    break;
                case OutputPresentationType.TIF:
                    break;
                case OutputPresentationType.TIFBW:
                    break;
                default:
                    break;
            }

            if (sfd.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                return;

            mc.RdlEditor.Viewer.SetParameters();

            mc.RdlEditor.SaveAs(sfd.FileName, renderType);
            RadMessageBox.Show("Rapor kaydedildi", "Bilgi", MessageBoxButtons.OK, RadMessageIcon.Info, MessageBoxDefaultButton.Button1);
        }

        private void FillDataSourceSets()
        {
            if (!this.tsmDataSet.Enabled)
                return;

            // Run thru all the existing DataSets
            this.tsmDataSet.DropDownItems.Clear();
            this.tvDatas.Nodes["DataSets"].Nodes.Clear();
            this.tvDatas.Nodes["DataSources"].Nodes.Clear();
            this.tvDatas.Nodes["Images"].Nodes.Clear();
            this.tsmDataSet.DropDownItems.Add(new ToolStripMenuItem(Strings.RdlDesigner_menuData_Popup_New, null,
                        new EventHandler(this.dataSetsToolStripMenuItem_Click)));

            DesignXmlDraw draw = mc.DrawCtl;
            XmlNode rNode = draw.GetReportNode();
            XmlNode dsNode = draw.GetNamedChildNode(rNode, "DataSets");
            if (dsNode != null)
            {
                foreach (XmlNode dNode in dsNode)
                {
                    if (dNode.Name != "DataSet")
                        continue;
                    XmlAttribute nAttr = dNode.Attributes["Name"];
                    if (nAttr == null)	// shouldn't really happen
                        continue;
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(nAttr.Value, null, new EventHandler(this.dataSetsToolStripMenuItem_Click));
                    tsmi.Tag = nAttr.Value;
                    this.tsmDataSet.DropDownItems.Add(tsmi);

                    RadTreeNode nd = new RadTreeNode(nAttr.Value);
                    nd.Tag = nAttr.Value;
                    nd.ImageIndex = 3;
                    this.tvDatas.Nodes["DataSets"].Nodes.Add(nd);

                }
            }

            XmlNode dstNode = draw.GetNamedChildNode(rNode, "DataSources");
            if (dstNode != null)
            {
                foreach (XmlNode dtNode in dstNode)
                {
                    if (dtNode.Name != "DataSource")
                        continue;
                    XmlAttribute nAttr = dtNode.Attributes["Name"];
                    if (nAttr == null)	// shouldn't really happen
                        continue;

                    RadTreeNode nd = new RadTreeNode(nAttr.Value);
                    nd.Tag = nAttr.Value;
                    nd.ImageIndex = 2;
                    this.tvDatas.Nodes["DataSources"].Nodes.Add(nd);

                }
            }

            XmlNode eiNode = draw.GetNamedChildNode(rNode, "EmbeddedImages");
            if (eiNode != null)
            {
                foreach (XmlNode iNode in eiNode)
                {
                    if (iNode.Name != "EmbeddedImage")
                        continue;
                    XmlAttribute nAttr = iNode.Attributes["Name"];
                    if (nAttr == null)	// shouldn't really happen
                        continue;
                    RadTreeNode nd = new RadTreeNode(nAttr.Value);
                    nd.Tag = nAttr.Value;
                    nd.ImageIndex = 4;
                    this.tvDatas.Nodes["Images"].Nodes.Add(nd);
                }
            }
            tvDatas.ExpandAll();
        }

        private void OpenDataSources()
        {
            if (mc == null)
                return;
            mc.Editor.StartUndoGroup(Strings.RdlDesigner_Undo_DataSourcesDialog);
            using (DialogDataSources dlgDS = new DialogDataSources(mc.SourceFile, mc.DrawCtl))
            {
                dlgDS.StartPosition = FormStartPosition.CenterParent;
                DialogResult dr = dlgDS.ShowDialog();
                mc.Editor.EndUndoGroup(dr == DialogResult.OK);
                if (dr == DialogResult.OK)
                    mc.Modified = true;
                FillDataSourceSets();
            }
        }

        private void OpenEmbeddedImages(string selectedName)
        {
            if (mc == null)
                return;

            mc.Editor.StartUndoGroup(Strings.RdlDesigner_Undo_EmbeddedImagesDialog);
            using (DialogEmbeddedImages dlgEI = new DialogEmbeddedImages(mc.DrawCtl, selectedName))
            {
                dlgEI.StartPosition = FormStartPosition.CenterParent;
                DialogResult dr = dlgEI.ShowDialog();
                mc.Editor.EndUndoGroup(dr == DialogResult.OK);
                if (dr == DialogResult.OK)
                    mc.Modified = true;
                FillDataSourceSets();
            }
        }

        private void OpenDataSets(string selectedName)
        {
            if (mc == null || mc.DrawCtl == null || mc.ReportDocument == null)
                return;

            mc.Editor.StartUndoGroup(Strings.RdlDesigner_Undo_DataSetDialog);

            string dsname = selectedName;

            // Find the dataset we need
            List<XmlNode> ds = new List<XmlNode>();
            DesignXmlDraw draw = mc.DrawCtl;
            XmlNode rNode = draw.GetReportNode();
            XmlNode dsNode = draw.GetCreateNamedChildNode(rNode, "DataSets");
            XmlNode dataset = null;

            // find the requested dataset: the menu text equals the name of the dataset
            int dsCount = 0;		// count of the datasets
            string onlyOneDsname = null;
            foreach (XmlNode dNode in dsNode)
            {
                if (dNode.Name != "DataSet")
                    continue;
                XmlAttribute nAttr = dNode.Attributes["Name"];
                if (nAttr == null)	// shouldn't really happen
                    continue;
                if (dsCount == 0)
                    onlyOneDsname = nAttr.Value;		// we keep track of 1st name; 

                dsCount++;
                if (nAttr.Value == dsname)
                    dataset = dNode;
            }

            bool bNew = false;
            if (dataset == null)	// This must be the new menu item
            {
                dataset = draw.CreateElement(dsNode, "DataSet", null);	// create empty node
                bNew = true;
            }
            ds.Add(dataset);

            using (PropertyDialog pd = new PropertyDialog(mc.DrawCtl, ds, PropertyTypeEnum.DataSets))
            {
                DialogResult dr = pd.ShowDialog();
                if (pd.Changed || dr == DialogResult.OK)
                {
                    if (dsCount == 1)
                    // if we used to just have one DataSet we may need to fix up DataRegions 
                    //	that were defaulting to that name
                    {
                        dsCount = 0;
                        bool bUseName = false;
                        foreach (XmlNode dNode in dsNode)
                        {
                            if (dNode.Name != "DataSet")
                                continue;
                            XmlAttribute nAttr = dNode.Attributes["Name"];
                            if (nAttr == null)	// shouldn't really happen
                                continue;

                            dsCount++;
                            if (onlyOneDsname == nAttr.Value)
                                bUseName = true;
                        }
                        if (bUseName && dsCount > 1)
                        {
                            foreach (XmlNode drNode in draw.ReportNames.ReportItems)
                            {
                                switch (drNode.Name)
                                {
                                    // If a DataRegion doesn't have a dataset name specified use previous one
                                    case "Table":
                                    case "List":
                                    case "Matrix":
                                    case "Chart":
                                        XmlNode aNode = draw.GetNamedChildNode(drNode, "DataSetName");
                                        if (aNode == null)
                                            draw.CreateElement(drNode, "DataSetName", onlyOneDsname);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    mc.Modified = true;
                }
                else if (bNew)	// if canceled and new DataSet get rid of temp node
                {
                    dsNode.RemoveChild(dataset);
                }
                if (pd.Delete)	// user must have hit a delete button for this to get set
                    dsNode.RemoveChild(dataset);

                if (!dsNode.HasChildNodes)		// If no dataset exists we remove DataSets
                    draw.RemoveElement(rNode, "DataSets");

                mc.Editor.EndUndoGroup(pd.Changed || dr == DialogResult.OK);
            }
            FillDataSourceSets();
        }

        #endregion

        #region [_PROTECTEDS_]

        internal static void VetricalTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_designer.mc == null)
                return;

            HitLocation hl = _designer.mc._drawPanel.HitContainer(_designer.mc._designControl.GetMousePosition(),
                _designer.mc._designControl.PointsX(_designer.mc._designControl.GetHorizontalScroll().Value),
                _designer.mc._designControl.PointsY(_designer.mc._designControl.GetVerticalScroll().Value));
            DialogNewTable tbl = new DialogNewTable(_designer.mc._drawPanel, hl.HitContainer, true);
            if (tbl.ShowDialog() == DialogResult.OK)
            {
                _designer.mc._designControl.InsertNewNReportItem(sender, tbl.TableVerticalString);
                _designer.mc.OnHit(_designer.mc._designControl.GetMousePosition());
            }

        }

        #endregion

        #region [_PUBLICS_]

        public static void ShowWaitDialog()
        {
            if (_designer.mc != null)
                _designer.mc.RdlEditor.ShowPreviewWaitDialog(_designer.mc.DrawCtl.GetNamedChildNode(_designer.mc.DrawCtl.GetReportNode(), "ReportParameters") == null);
        }

        #endregion

        #region [_EVENTS_]

        private void frmNetsisRdlDeginer_Load(object sender, EventArgs e)
        {
            if (File.Exists(NConstants.RadDockDesignXMLFileName))
                radDock1.LoadFromXml(NConstants.RadDockDesignXMLFileName);

            this.tvDatas.Nodes["DataSets"].Tag = Strings.RdlDesigner_menuData_Popup_New;
            this.tvDatas.Nodes["Images"].Tag = string.Empty;

            DragDropService service = this.radDock1.GetService<DragDropService>();
            service.PreviewDockPosition += new DragDropDockPositionEventHandler(service_PreviewDockPosition);

            ContextMenuService menuService = this.radDock1.GetService<ContextMenuService>();
            menuService.ContextMenuDisplaying += menuService_ContextMenuDisplaying;

            rcmAddNewDataset.Click += rcmAddNewDataset_Click;
            rcmAddNewDatasource.Click += rcmAddNewDatasource_Click;
        }

        private void service_PreviewDockPosition(object sender, DragDropDockPositionEventArgs e)
        {
            ((DragDropService)sender).AllowedStates = AllowedDockState.Docked;
        }

        private void menuService_ContextMenuDisplaying(object sender, ContextMenuDisplayingEventArgs e)
        {
            if (e.MenuType == ContextMenuType.DockWindow && e.DockWindow.DockTabStrip is ToolTabStrip)
            {
                for (int i = 0; i < e.MenuItems.Count; i++)
                {
                    RadMenuItemBase menuItem = e.MenuItems[i];
                    if (menuItem.Name == "TabbedDocument" || menuItem is RadMenuSeparatorItem)
                    {
                        menuItem.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                    }
                }
            }
        }

        private void DesignTabChanged(object sender, System.EventArgs e)
        {
            TabChange();
        }

        private void SelectionChanged(object sender, System.EventArgs e)
        {
            ReportItemSelectedChanged();
        }

        private void acNew_Execute(object sender, EventArgs e)
        {
            using (DialogDatabase dlgDB = new DialogDatabase((RdlDesigner)null))
            {
                dlgDB.StartPosition = FormStartPosition.CenterParent;
                dlgDB.ShowDialog();
                if (dlgDB.DialogResult == DialogResult.Cancel)
                    return;
                CreateMDIChild(new NReportInfo(dlgDB.GetReportName(), dlgDB.ResultReport));
                mc.Modified = true;
            }
        }

        private void acOpen_Execute(object sender, EventArgs e)
        {
            OpenReportList(ReportListViewType.Show);
        }

        private void acSave_Execute(object sender, EventArgs e)
        {
            if (mc == null)
                return;
            mc.SaveReport();
            if (mc.Editor != null)
                mc.Editor.ClearUndo();
        }

        private void acSaveAs_Execute(object sender, EventArgs e)
        {
            if (mc == null)
                return;
            if (!mc.FileSaveAs())
                return;
            mc.Viewer.Folder = Path.GetDirectoryName(mc.SourceFile.LocalPath);
            mc.Viewer.ReportName = Path.GetFileNameWithoutExtension(mc.SourceFile.LocalPath);
            mc.Text = Path.GetFileName(mc.SourceFile.LocalPath);
            if (mc.Editor != null)
                mc.Editor.ClearUndo();
        }

        private void documentWindow2_SizeChanged(object sender, EventArgs e)
        {
            if (mc == null)
                return;
            mc.WindowState = FormWindowState.Normal;
            mc.WindowState = FormWindowState.Maximized;
        }

        private void tsbBold_Click(object sender, EventArgs e)
        {
            SetSelectedComponentStyle("FontWeight", acBold.Checked ? "Bold" : "Normal");
        }

        private void tsbItalic_Click(object sender, EventArgs e)
        {
            SetSelectedComponentStyle("FontStyle", acItalic.Checked ? "Italic" : "Normal");
        }

        private void tsbUnderline_Click(object sender, EventArgs e)
        {
            SetSelectedComponentStyle("TextDecoration", acUnderline.Checked ? "Underline" : "None");
        }

        private void tscFontFamilies_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedComponentStyle("FontFamily", tscFontFamilies.Text);
        }

        private void tscFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedComponentStyle("FontSize", tscFontSize.Text + "pt");
        }

        private void tsbLeftAlign_Click(object sender, EventArgs e)
        {
            TextAlignEnum ta = TextAlignEnum.General;
            if (sender == acLeftAlign)
            {
                ta = TextAlignEnum.Left;
                acRightAlign.Checked = acCentre.Checked = false;
            }
            else if (sender == acRightAlign)
            {
                ta = TextAlignEnum.Right;
                acLeftAlign.Checked = acCentre.Checked = false;
            }
            else if (sender == acCentre)
            {
                ta = TextAlignEnum.Center;
                acRightAlign.Checked = acLeftAlign.Checked = false;
            }

            SetSelectedComponentStyle("TextAlign", ta.ToString());
        }

        private void tscForeColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedComponentStyle("Color", tscForeColor.ColorText);
        }

        private void tscBackColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedComponentStyle("BackgroundColor", tscBackColor.ColorText);
        }

        private void tsmClose_Click(object sender, EventArgs e)
        {
            radDock1.ActiveWindow.Close();
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radDock1_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            try
            {
                if (e.NewWindow.Controls[0] is frmMDIChild)
                {
                    SetLocalMDIChildWindow((frmMDIChild)e.NewWindow.Controls[0]);
                }
                else if (radDock1.DockWindows.Count == 0)
                {
                    SetLocalMDIChildWindow(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radDock1_DockWindowClosing(object sender, DockWindowCancelEventArgs e)
        {
            if (e.NewWindow.Controls.Count == 0)
                return;
            if (e.NewWindow.Controls[0] is frmMDIChild)
            {
                SetLocalMDIChildWindow((frmMDIChild)e.NewWindow.Controls[0]);
                e.Cancel = !CloseDesignToolWindow();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void radDock1_DockWindowClosed(object sender, DockWindowEventArgs e)
        {
            if (radDock1.ActiveWindow != null && radDock1.ActiveWindow is DocumentWindow)
            {
                SetLocalMDIChildWindow((frmMDIChild)radDock1.ActiveWindow.Controls[0]);
            }
        }

        private void ReportItemInserted(object sender, System.EventArgs e)
        {
            if (mc == null)
                return;
            mc.CurrentInsert = null;
        }

        private void rlwTools_ItemMouseDown(object sender, ListViewItemMouseEventArgs e)
        {
            if (mc == null)
                return;
            DragDropEffects dde1 = DoDragDrop(e.Item, DragDropEffects.Copy);
            //mc.SetFocus();
        }

        private void tsmUndo_Click(object sender, EventArgs e)
        {
            if (mc == null || mc.Editor == null || !(mc.Editor.CanUndo))
                return;
            mc.Editor.Undo();
            if (mc.DesignTab == "design")
            {
                mc.Editor.DesignCtl.SetScrollControls();
            }
            this.SelectionChanged(this, new EventArgs());
        }

        private void tsmRedo_Click(object sender, EventArgs e)
        {
            if (mc.Editor != null && mc.Editor.CanRedo)
                mc.Editor.Redo();
        }

        private void veriToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void menuEditCut_Click(object sender, System.EventArgs ea)
        {
            if (mc != null && mc.RdlEditor != null && mc.RdlEditor.SelectionLength > 0)
                mc.RdlEditor.Cut();
        }

        private void menuEditCopy_Click(object sender, System.EventArgs ea)
        {
            if (mc != null && mc.RdlEditor != null && mc.RdlEditor.SelectionLength > 0)
                mc.RdlEditor.Copy();
        }

        private void menuEditPaste_Click(object sender, System.EventArgs ea)
        {
            if (mc != null && mc.RdlEditor != null
                && (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) || Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap)))
                mc.RdlEditor.Paste();
        }

        private void menuEditDelete_Click(object sender, System.EventArgs ea)
        {
            if (mc != null && mc.RdlEditor != null && mc.RdlEditor.SelectionLength > 0)
                mc.RdlEditor.SelectedText = "";
        }

        private void menuEditSelectAll_Click(object sender, System.EventArgs ea)
        {
            if (mc != null && mc.RdlEditor != null)
                mc.RdlEditor.SelectAll();
        }

        private void menuEditFind_Click(object sender, System.EventArgs ea)
        {
            if (mc == null || mc.RdlEditor == null)
                return;

            if (mc.RdlEditor.DesignTab == "preview")
            {
                mc.RdlEditor.PreviewCtl.ShowFindPanel = true;
                mc.RdlEditor.PreviewCtl.FindNext();
            }
            else
            {
                FindTab tab = new FindTab(mc.RdlEditor);
                tab.Show();
            }
        }

        private void menuEdit_FormatXml(object sender, System.EventArgs ea)
        {
            if (mc == null || mc.RdlEditor == null || mc.RdlEditor.Text.Length <= 0)
                return;

            try
            {
                mc.RdlEditor.Text = DesignerUtility.FormatXml(mc.RdlEditor.Text);
                mc.RdlEditor.Modified = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Strings.RdlDesigner_Showl_FormatXML);
            }
        }

        private void menuEditReplace_Click(object sender, System.EventArgs ea)
        {
            if (mc == null || mc.RdlEditor == null)
                return;
            FindTab tab = new FindTab(mc.RdlEditor);
            tab.tcFRG.SelectedTab = tab.tabReplace;
            tab.Show();
        }

        private void menuEditGoto_Click(object sender, System.EventArgs ea)
        {
            if (mc == null || mc.RdlEditor == null)
                return;
            FindTab tab = new FindTab(mc.RdlEditor);
            tab.tcFRG.SelectedTab = tab.tabGoTo;
            tab.Show();
        }

        private void tsmDataSource_Click(object sender, EventArgs e)
        {
            OpenDataSources();
        }

        private void tsmImages_Click(object sender, EventArgs e)
        {
            OpenEmbeddedImages(null);
        }

        private void tsmDesign_Click(object sender, EventArgs e)
        {
            SetTab("design");
        }

        private void tsmRDLText_Click(object sender, EventArgs e)
        {
            SetTab("edit");
        }

        private void tsmPreview_Click(object sender, EventArgs e)
        {
            SetTab("preview");
        }

        private void tsmFontFamilies_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (!e.ClickedItem.Text.Contains("..."))
            {
                SetSelectedComponentStyle("FontFamily", e.ClickedItem.Text);
                SetUnChecked(tsmFontFamilies.DropDownItems);
                ((ToolStripMenuItem)e.ClickedItem).Checked = true;
                return;
            }

            tsmFontFamilies.DropDownItems.Clear();
            foreach (FontFamily ff in FontFamily.Families)
            {
                tsmFontFamilies.DropDownItems.Add(ff.Name);
            }
        }

        private void tsmFontSize_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            SetSelectedComponentStyle("FontSize", e.ClickedItem.Text + "pt");
            SetUnChecked(tsmFontSize.DropDownItems);
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;
        }

        private void tsmFontFamilies_DropDownOpening(object sender, EventArgs e)
        {
            SetUnChecked(tsmFontFamilies.DropDownItems);
            if (mc == null || mc.SelectedStyle == null)
                return;
            foreach (ToolStripMenuItem mi in tsmFontFamilies.DropDownItems)
            {
                if (mi.Text == mc.SelectedStyle.FontFamily)
                {
                    mi.Checked = true;
                    return;
                }
            }
        }

        private void tsmFontSize_DropDownOpening(object sender, EventArgs e)
        {
            SetUnChecked(tsmFontSize.DropDownItems);
            if (mc == null || mc.SelectedStyle == null)
                return;

            string rs = string.Format(NumberFormatInfo.InvariantInfo, "{0:0.#}", mc.SelectedStyle.FontSize);
            foreach (ToolStripMenuItem mi in tsmFontSize.DropDownItems)
            {
                if (mi.Text == rs)
                {
                    mi.Checked = true;
                    return;
                }
            }
        }

        private void tsmFontColor_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            tsmFontColor.Image = ((NColorToolStriptItem)e.ClickedItem).GetSelectedColorImage();
            SetSelectedComponentStyle("Color", ((NColorToolStriptItem)e.ClickedItem).ColorName);
        }

        private void tsmBackgroundColor_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            tsmBackgroundColor.Image = ((NColorToolStriptItem)e.ClickedItem).GetSelectedColorImage();
            SetSelectedComponentStyle("BackgroundColor", ((NColorToolStriptItem)e.ClickedItem).ColorName);
        }

        private void rlwTools_KeyDown(object sender, KeyEventArgs e)
        {
            if (mc == null || e.KeyCode != Keys.Enter || rlwTools.SelectedItem == null)
                return;
            mc.AddNewItem(rlwTools.SelectedItem.Tag.ToString());
        }

        private void acDelete_Execute(object sender, EventArgs e)
        {
            OpenReportList(ReportListViewType.Delete);
        }

        private void acExport_Execute(object sender, EventArgs e)
        {
            OpenReportList(ReportListViewType.Export);
        }

        private void acImport_Execute(object sender, EventArgs e)
        {
            OpenReportList(ReportListViewType.Import);
        }

        private void frmNetsisRdlDeginer_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (DockWindow doc in radDock1.DockWindows.DocumentWindows)
            {
                doc.Close();
            }
            if (radDock1.DockWindows.DocumentWindows.Length != 0)
                e.Cancel = true;
            else
                radDock1.SaveToXml(NConstants.RadDockDesignXMLFileName);
        }

        private void tsbToExcel_Click(object sender, EventArgs e)
        {
            ExportReport(OutputPresentationType.Excel);
        }

        private void tsbHTML_Click(object sender, EventArgs e)
        {
            ExportReport(OutputPresentationType.HTML);
        }

        private void tsbPdf_Click(object sender, EventArgs e)
        {
            ExportReport(OutputPresentationType.PDF);
        }

        private void tsbWord_Click(object sender, EventArgs e)
        {
            ExportReport(OutputPresentationType.Word);
        }

        private void tvDatas_NodeMouseDoubleClick(object sender, RadTreeViewEventArgs e)
        {
            string nodeName = (e.Node.Parent == null ? e.Node.Name : e.Node.Parent.Name);
            e.Node.ExpandAll();
            switch (nodeName)
            {
                case "DataSources":
                    OpenDataSources();
                    break;
                case "DataSets":
                    OpenDataSets(e.Node.Tag.ToString());
                    break;
                case "Images":
                    OpenEmbeddedImages(e.Node.Tag.ToString());
                    break;
                default:
                    break;
            }

        }

        private void tvDatas_NodeExpandedChanged(object sender, RadTreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
                e.Node.Image = ilDatasource.Images[(e.Node.Expanded ? 1 : 0)];
        }

        void rcmAddNewDatasource_Click(object sender, EventArgs e)
        {
            OpenDataSources();
        }

        void rcmAddNewDataset_Click(object sender, EventArgs e)
        {
            OpenDataSets(null);
        }

        private void tsmSettings_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.ShowDialog();
        }

        #endregion                              
      
    }
}
