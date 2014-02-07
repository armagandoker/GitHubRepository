using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace fyiReporting.RdlDesign
{
    internal partial class DialogNewTable
    {

        #region [_CONSTRUCTORS_]

        internal DialogNewTable(DesignXmlDraw dxDraw, XmlNode container, bool FormType): this(dxDraw,container)
        {
            groupBox1.Visible = !FormType;
            label4.Visible = !FormType;
            chkGrandTotals.Visible = !FormType;
            cbGroupColumn.Visible = !FormType;
        }

        #endregion

        #region [_PRIVATES_]

        private void ChangeControlsPropertiesOnLoad()
        {
            this.Text = "Yeni Tablo";
            groupBox1.Text = "Alanları Ayarla";
            rbHorz.Text = "Yatay (Standart kolon)";
            rbVert.Text = "Dikey (Alan Başına Satır)";
            rbVertComp.Text = "Yatay-Dikey";

            label2.Text = "Dataset Alanları";
            label3.Text = "Tablo Kolonları";
            bUp.Text = "Yukarı";
            bDown.Text = "Aşağı";
            label4.Text = "Guruplama için Kolon Seçin";
            chkGrandTotals.Text = "Genel Toplam Hesapla";
            
            bOK.Text = "Tamam";
            bCancel.Text = "İptal";

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

        #region [_PUBLICS_]

        public string TableVerticalString
        {
            get
            {
                StringBuilder table = new StringBuilder("<ReportItems><Table>");
                table.AppendFormat("<DataSetName>{0}</DataSetName>", this.cbDataSets.Text);
                table.Append("<NoRows>Query returned no rows!</NoRows><Style>" +
                    "<BorderStyle><Default>Solid</Default></BorderStyle></Style>");

                table.Append("<TableColumns><TableColumn><Width>1in</Width></TableColumn><TableColumn><Width>2in</Width></TableColumn></TableColumns>");

                table.Append("<Details><TableRows>" + Environment.NewLine);

                foreach (string colname in this.lbTableColumns.Items)
                {
                    string dcol = string.Format("Fields!{0}.Value", colname);
             
  
                    string val = String.Format("<Value>={0}</Value>", dcol);
                    table.AppendFormat(
                        @"<TableRow><Height>12 pt</Height><TableCells>" +
                        "<TableCell><ReportItems><Textbox>" +
                        "<Value>{0}</Value>" +
                        "<CanGrow>true</CanGrow>" +
                        "<Style><BorderStyle><Default>Solid</Default></BorderStyle>" +
                        "<BorderColor /><BorderWidth />" +
                        "</Style></Textbox></ReportItems></TableCell>" +
                         "<TableCell><ReportItems><Textbox>" +
                        "<Value>{1}</Value>" +                 
                        "<Style xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition\">" +
                        "<BorderStyle><Default>Solid</Default></BorderStyle>" +
                        "<BorderColor /><BorderWidth />" +
                        "</Style></Textbox></ReportItems></TableCell>" +
                        "</TableCells></TableRow>" +
                        Environment.NewLine, colname, val);                    
              
                }
                table.Append("</TableRows></Details></Table></ReportItems>");

                return table.ToString();
            }
        }

        #endregion

    }
}
