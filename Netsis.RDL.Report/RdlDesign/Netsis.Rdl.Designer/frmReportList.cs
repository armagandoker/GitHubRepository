using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.IO;
using Newtonsoft.Json;
using Netsis.Framework.Utils.Serialization;
using System.Xml;
using Telerik.WinControls.UI;
using Netsis.Rdl.Contracts.Entities;
using Netsis.Rdl.Contracts.Helpers;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer
{
    public partial class frmReportList : Telerik.WinControls.UI.RadForm
    {

        #region [_FILEDS_]

        const string C_ID = "Id";
        const string C_NAME = "Name";
        const string C_DESCRIPTION = "Description";
        const string C_OWNER = "Owner";
        const string C_CREATION = "Creation";
        const string C_LASTUPDATE = "LastUpdate";

        ReportListViewType _ListViewType = ReportListViewType.Show;        
        public List<NReportInfo> SelectedNReports { get; set; }

        public bool ReportMultiSelection 
        {
            get
            {
                return rlw.MultiSelect;
            }
            set
            {
                rlw.MultiSelect = value;
            }
        }

        #endregion

        #region [_CONSTRUCTORS_]

        public frmReportList(ReportListViewType listViewType)
        {
            InitializeComponent();
            this._ListViewType = listViewType;
            SelectedNReports = new List<NReportInfo>();
            ConfigureType();
        }

        #endregion

        #region[_PRIVATES_]

        private void ConfigureType()
        {
            //rlw.CellDoubleClick -= rlw_CellDoubleClick;
            switch (this._ListViewType)
            {
                case ReportListViewType.Show:
                    rbExecute.Visible = false;
                    //rlw.CellDoubleClick += rlw_CellDoubleClick;
                    break;
                case ReportListViewType.Delete:                    
                    rbExecute.Text = "Sil";
                    rbOK.Text = "Kapat";
                    break;
                case ReportListViewType.Export:
                    rlw.MultiSelect = false;
                    rbExecute.Text = "Dışa Aktar";
                    rbOK.Text = "Kapat";
                    break;
                case ReportListViewType.Import:
                    rbExecute.Text = "İçeri Al";
                    rbOK.Text = "Kapat";
                    break;
                default:
                    break;
            }
        }

        private string GetValue(XmlElement xElement, string key)
        {
            if (xElement == null)
                return null;

            foreach (XmlNode cNode in xElement.ChildNodes)
            {
                if (cNode.NodeType == XmlNodeType.Element &&
                    cNode.Name == key)
                    return cNode.FirstChild.Value;
            }
            return string.Empty;
        }

        private void SetListViewDatasource()
        {                        
            rlw.DataSource = NReportInfoHelper.GetReportList();           
        }

        private void DeleteSelectedReport()
        {
            List<int> idList = new List<int>();
            foreach (GridViewRowInfo item in rlw.SelectedRows)
            {                
                idList.Add((int)item.Cells[C_ID].Value);
            }
            string message = String.Format("{0} kodlu raporlar silinecektir.\r\nDevam etmek istiyor musunuz?", String.Join(", ", idList.ToArray()));
            if (RadMessageBox.Show(message, "Soru", MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                NReportInfoHelper.DeleteNReport(idList);
                SetListViewDatasource();
            }
        }
      
        private void ExportSelected()
        {
            if (rlw.SelectedRows == null || rlw.SelectedRows.Count == 0)
                return;
            var tmp = NReportInfoHelper.GetNReport((int)rlw.SelectedRows[0].Cells[C_ID].Value);
            string serializeStr = JsonConvert.SerializeObject(tmp);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Seçileni Dışarıya Aktar";
            sfd.DefaultExt = "netrdl";
            sfd.Filter = "Rapor Dosyaları (*.nrl)|*.nrl";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, serializeStr);
                RadMessageBox.Show("Rapor aktarımı başarıyla tamamlandı", "Bilgi", MessageBoxButtons.OK, RadMessageIcon.Info, MessageBoxDefaultButton.Button1);
            }
        }

        private void ImportSelected()
        {
            NReportInfo ri = null;
            string fileContent = string.Empty;
            string[] splitList = null;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seçilenleri İçeri Al";
            ofd.Multiselect = true;
            ofd.DefaultExt = "netrdl";
            ofd.Filter = "Rapor Dosyaları (*.nrl)|*.nrl|Rdl Dosyaları (*.rdl, *.rdlc)|*.rdl; *.rdlc";            
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string fileName in ofd.FileNames)
                {
                    fileContent = File.ReadAllText(fileName);
                    char[] sChr = new char[] { '\\', '/', '.' };
                    splitList = fileName.Split(sChr);
                    if (splitList.LastOrDefault().ToLower() == "nrl")
                    {
                        ri = JsonConvert.DeserializeObject<NReportInfo>(fileContent);
                        ri.Creation = DateTime.Now;
                        ri.LastUpdate = null;
                        ri.Id = null;
                    }
                    else
                    {
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.LoadXml(fileContent);
                        XmlElement xElm = xDoc.DocumentElement;
                        ri = new NReportInfo(splitList[splitList.Count() - 2], fileContent);
                        ri.Description = GetValue(xElm, "Description");
                        ri.Owner = GetValue(xElm, "Author");
                        ri.Creation = DateTime.Now;
                    }
                    NReportInfoHelper.SaveReport(ri);
                }
                SetListViewDatasource();
                RadMessageBox.Show("Rapor başarıyla kaydedildi","Bilgi", MessageBoxButtons.OK, RadMessageIcon.Info, MessageBoxDefaultButton.Button1);
            }
        }

        private void Execute()
        {
            switch (this._ListViewType)
            {
                case ReportListViewType.Show:
                    break;
                case ReportListViewType.Delete:
                    DeleteSelectedReport();
                    break;
                case ReportListViewType.Export:
                    ExportSelected();
                    break;
                case ReportListViewType.Import:
                    ImportSelected();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region [_EVENTS_]

        private void frmReportList_Load(object sender, EventArgs e)
        {
            SetListViewDatasource();                        
        }       

        private void rbOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            if (this._ListViewType == ReportListViewType.Show && rlw.SelectedRows != null && rlw.SelectedRows.Count > 0)
            {
                foreach (GridViewRowInfo row in rlw.SelectedRows)
                {
                    this.SelectedNReports.Add(NReportInfoHelper.GetNReport((int)row.Cells[C_ID].Value));
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }            
        }

        private void rbExecute_Click(object sender, EventArgs e)
        {
            Execute();
        }

        private void rlw_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (this._ListViewType == ReportListViewType.Export)
            {
                Execute();
            }
            else
            {
                this.SelectedNReports.Add(NReportInfoHelper.GetNReport((int)e.Row.Cells[C_ID].Value));
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        #endregion                             

        private void frmReportList_Shown(object sender, EventArgs e)
        {
            if(this._ListViewType == ReportListViewType.Import)
                Execute();
        }

    }

    public enum ReportListViewType
    {
        Show = 0,
        Delete = 1,
        Export = 2,
        Import = 3
    }

}
