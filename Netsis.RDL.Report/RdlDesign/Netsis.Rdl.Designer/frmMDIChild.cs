using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Components;
using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using fyiReporting.RdlDesign.Resources;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Netsis.Rdl.Contracts.Entities;
using Netsis.Rdl.Contracts.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer
{

    public delegate void HitEventHandler(object sender, Point hitPoint);

    internal partial class frmMDIChild : fyiReporting.RdlDesign.MDIChild
    {

        #region [_FIELDS_]

        internal DesignCtl _designControl = null;
        internal DesignXmlDraw _drawPanel = null;
        RdlViewer.RdlViewer _viewer = null;

        ContextMenuStrip mainStript = null;
        ToolStripMenuItem _insertMenuItems = null;

        Dictionary<string, ToolStripMenuItem> _insertItemDictionary = null;
        ReportViewer _reportViewer = null;

        FieldInfo _DesignerMousePosition = null;       
        ReportPreviewHelper _PreviewHelper = new ReportPreviewHelper();

        public event HitEventHandler Hit;

        private NReportInfo _ReportInfo = null;
        public NReportInfo ReportInfo
        {
            get
            {
                return _ReportInfo;
            }
            set
            {
                _ReportInfo = value;
                this.SourceRdl = _ReportInfo.Source;
            }
        }

        #endregion

        #region [_CONSTRUCTORS_]

        public frmMDIChild()
            : base(100,100)
        {
            InitializeComponent();
        }

        public frmMDIChild(int  width, int height)
            : base(width, height)
        {
            InitializeComponent();
        }

        #endregion

        #region [_EVENTS_]

        internal virtual void OnHit(Point hitPoint)
        {
            if (Hit != null)
            {
                Hit(this, hitPoint);
            }
        }

        private void frmMDIChild_Load(object sender, EventArgs e)
        {
            _insertItemDictionary = DesignerContextMenuStriptHelper.ChangeMenuStriptText(this);

            mainStript = (ContextMenuStrip)this.RdlEditor.DesignCtl.GetType().GetField("ContextMenuDefault",
               System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField)
               .GetValue(this.RdlEditor.DesignCtl);

            _insertMenuItems = (ToolStripMenuItem)mainStript.Items.Find("MenuDefaultInsert", false)[0];

            TabControl tb = ((TabControl)RdlEditor.Controls[0]);
            tb.ImageList = imageList1;
            /*tb.TabPages[2].Controls.Clear();

            _reportViewer = new ReportViewer();
            _reportViewer.ProcessingMode = ProcessingMode.Local;
            //_reportViewer.ShowExportButton = false;
            _reportViewer.ShowPrintButton = false;
            _reportViewer.PageCountMode = PageCountMode.Actual;
            _reportViewer.Dock = DockStyle.Fill;
            
            _reportViewer.LocalReport.SubreportProcessing += localReport_SubreportProcessing;
            _reportViewer.LocalReport.ShowDetailedSubreportMessages = true;    
              
            tb.TabPages[2].Controls.Add(_reportViewer);*/

            tb.TabPages[0].Text = "Dizayn";
            tb.TabPages[1].Text = "RDL Text";
            tb.TabPages[2].Text = "Önizleme";

            tb.TabPages[0].ImageIndex = 0;
            tb.TabPages[1].ImageIndex = 1;
            tb.TabPages[2].ImageIndex = 2;

            _designControl = (DesignCtl)tb.TabPages[0].Controls.Find("dcDesign", false)[0];           

            _drawPanel = (DesignXmlDraw)_designControl.Controls[0];
            _drawPanel.AllowDrop = true;
            FieldInfo fi = _drawPanel.GetType().GetField("AREABACKCOLOR", BindingFlags.Instance | BindingFlags.NonPublic);
            fi.SetValue(_drawPanel, FakeTelericThemeHelper.FORM_BACKCOLOR);
            _drawPanel.DragOver += _drawPanel_DragOver;
            _drawPanel.DragDrop += _drawPanel_DragDrop;                              
            
        }        

        #endregion

        #region [_PRIVATES_]

        private void _drawPanel_DragDrop(object sender, DragEventArgs e)
        {
            ListViewDataItem item = (ListViewDataItem)e.Data.GetData(typeof(ListViewDataItem));
            Point pt = this.PointToClient(new Point(e.X, e.Y));
            Point finalPoint = new Point(pt.X - 15, pt.Y - 15);            
            _designControl.SetMousePosition(finalPoint);
            if (_insertItemDictionary.ContainsKey(item.Tag.ToString()))
            {
                _insertItemDictionary[item.Tag.ToString()].PerformClick();
                OnHit(finalPoint);
            }
        }

        private void _drawPanel_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        #endregion

        #region [_PROTECTEDS_]

        internal void SetDataSourceOfReportViewer()
        {
            if (_reportViewer == null) 
                return;
            _PreviewHelper.ParseReportXML(DrawCtl.GetReportNode().OuterXml);
            LocalReport localReport = _reportViewer.LocalReport;
            localReport.DataSources.Clear();
            localReport.EnableExternalImages = true;
            localReport.EnableHyperlinks = true;
            localReport.LoadReportDefinition(new MemoryStream(Encoding.ASCII.GetBytes(_PreviewHelper.MainReport)));

            foreach (var si in _PreviewHelper.SubReports)
            {                
                localReport.LoadSubreportDefinition(si.Value.ReportName, new MemoryStream(Encoding.ASCII.GetBytes(si.Value.Source)));
            }

            foreach (DataTable dt in _PreviewHelper.GetMainReportDataSet().Tables)
            {                          
                localReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(dt.TableName, dt));
            }
            DateTime nw = DateTime.Now;
            //localReport.Render("Pdf");
            double result = DateTime.Now.Subtract(nw).TotalMilliseconds;
            int a = 3;
           _reportViewer.RefreshReport();
        }

        void localReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {                        
            DataSet ds = _PreviewHelper.GetSubReportDataSet(e.DataSourceNames, e.ReportPath);
            foreach (DataTable dt in ds.Tables)
            {
                e.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(dt.TableName, dt));
            }            
        }

        internal Point SetMousePositionOnDesignCtrl()
        {
            Point pt = this.PointToClient(this.PointToScreen(new Point(_designControl.Location.X, _designControl.Location.Y)));
            Point finalPoint = new Point(pt.X + 10, pt.Y + _designControl.PixelsY(_designControl.SepHeight) + _designControl.PixelsY(_designControl.PageHeaderHeight));
            _designControl.SetMousePosition(finalPoint);
            return finalPoint;
        }
       
        internal void AddNewItem(string itemTag)
        {
            Point finalPoint = SetMousePositionOnDesignCtrl();
            if (_insertItemDictionary.ContainsKey(itemTag))
            {
                _insertItemDictionary[itemTag].PerformClick();
                OnHit(finalPoint);
                _designControl.SetSelection(null);
            }
        }        

        internal void SaveReport()
        {
            if (this.ReportInfo.Id == null)            
                this.ReportInfo.Creation = DateTime.Now;                                      
            else            
                this.ReportInfo.LastUpdate = DateTime.Now;

            XmlNode reportNode = DrawCtl.GetReportNode();
            XmlNode description = DrawCtl.GetNamedChildNode(reportNode, "Description");
            XmlNode author = DrawCtl.GetNamedChildNode(reportNode, "Author");  
            
            this.ReportInfo.Description = description.FirstChild.Value;
            this.ReportInfo.Owner = author.FirstChild.Value;      
            this.ReportInfo.Source = this.SourceRdl;
            NReportInfoHelper.SaveReport(this.ReportInfo);
            this.Tag = this.ReportInfo.Id;
            this.Modified = false;
        }

        public bool CloseOk()
        {
            if (!Modified)
                return true;

            DialogResult r =
                    RadMessageBox.Show(this, String.Format(Strings.MDIChild_ShowH_WantSaveChanges,
                    ReportInfo.Name == null ? Strings.MDIChild_ShowH_Untitled : ReportInfo.Name),
                    Strings.MDIChild_ShowH_fyiReportingDesigner,
                    MessageBoxButtons.YesNoCancel,
                    RadMessageIcon.Question, MessageBoxDefaultButton.Button3);

            bool bOK = true;
            if (r == DialogResult.Cancel)
                bOK = false;
            else if (r == DialogResult.Yes)
            {
                SaveReport();                    
            }
            return bOK;
        }

        public void ToPDF()
        {
           byte[] ss = _reportViewer.LocalReport.Render("WORD");
           File.WriteAllBytes(@"C:\Users\armagan.doker@netsis.com.tr\Desktop\Others\localRep.doc", ss);
        }

        #endregion

    }
}
