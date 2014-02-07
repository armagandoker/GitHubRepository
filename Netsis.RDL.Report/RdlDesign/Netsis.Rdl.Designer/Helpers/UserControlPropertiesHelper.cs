using fyiReporting.RDL;
using Netsis.Rdl.Contracts.Entities;
using Netsis.Rdl.Contracts.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers
{
    static internal class UserControlPropertiesHelper
    {

        #region [_PROTECTEDS_]

        internal static void ChangeUserControlsPropertiesOnLoad(UserControl mainControl)
        {
            if (mainControl is ReportCtl)
                ChangeReportControlsPropertiesOnLoad(mainControl);
            else if (mainControl is ReportParameterCtl)
                ChangeReportParameterControlsPropertiesOnLoad(mainControl);
            else if (mainControl is ReportXmlCtl)
                ChangeReportXmlControlsPropertiesOnLoad(mainControl);
            else if (mainControl is BodyCtl)
                ChangeBodyControlsPropertiesOnLoad(mainControl);
            else if (mainControl is CodeCtl)
                ChangeCodeControlsPropertiesOnLoad(mainControl);
            else if (mainControl is ModulesClassesCtl)
                ChangeModulesControlsPropertiesOnLoad(mainControl);
            else if (mainControl is DataSetsCtl)
                ChangeDataSetsControlsPropertiesOnLoad(mainControl);
            else if (mainControl is QueryParametersCtl)
                ChangeQueryParametersControlsPropertiesOnLoad(mainControl);
            else if (mainControl is FiltersCtl)
                ChangeFiltersControlsPropertiesOnLoad(mainControl);
            else if (mainControl is DataSetRowsCtl)
                ChangeDataSetRowsControlsPropertiesOnLoad(mainControl);
            else if (mainControl is GroupingCtl)
                ChangeGroupingControlsPropertiesOnLoad(mainControl);
            else if (mainControl is SortingCtl)
                ChangeSortingControlsPropertiesOnLoad(mainControl);
            else if (mainControl is FiltersCtl)
                ChangeFiltersControlsPropertiesOnLoad(mainControl);
            else if (mainControl is ChartLegendCtl)
                ChangeChartLegendControlsPropertiesOnLoad(mainControl);
            else if (mainControl is StyleTextCtl)
                ChangeFontControlsPropertiesOnLoad(mainControl);
            else if (mainControl is StyleBorderCtl)
                ChangeStyleBorderControlsPropertiesOnLoad(mainControl);
            else if (mainControl is StyleCtl)
                ChangeStyleControlsPropertiesOnLoad(mainControl);
            else if (mainControl is ChartAxisCtl)
                ChangeChartAxisControlsPropertiesOnLoad(mainControl);
            else if (mainControl is ListCtl)
                ChangeListControlsPropertiesOnLoad(mainControl);
            else if (mainControl is ImageCtl)
                ChangeImageControlsPropertiesOnLoad(mainControl);
            else if (mainControl is InteractivityCtl)
                ChangeControlsPropertiesOnLoad(mainControl);
            else if (mainControl is ChartCtl)
                ChangeChartControlsPropertiesOnLoad(mainControl);
            else if (mainControl is TableCtl)
                ChangeTableControlsPropertiesOnLoad(mainControl);
            else if (mainControl is TableColumnCtl)
                ChangeTableColumnControlsPropertiesOnLoad(mainControl);
            else if (mainControl is TableRowCtl)
                ChangeTableRowControlsPropertiesOnLoad(mainControl);
            else if (mainControl is GridCtl)
                ChangeGridControlsPropertiesOnLoad(mainControl);
            else if (mainControl is MatrixCtl)
                ChangeMatrixControlsPropertiesOnLoad(mainControl);
            else if (mainControl is SubreportCtl)
                ChangeSubreportControlsPropertiesOnLoad(mainControl);
            else if (mainControl is PositionCtl)
                ChangePositionControlsPropertiesOnLoad(mainControl);
        }

        internal static void ChangeControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {

                    #region [_INTERACTIVITY_]

                    case "groupBox1":
                        ctl.Text = "Aksiyon";
                        break;
                    case "rbNoAction":
                        ctl.Text = "Yok";
                        break;
                    case "rbHyperlink":
                        ctl.Text = "Köprü";
                        break;
                    case "rbBookmarkLink":
                        ctl.Text = "İşaret linki";
                        break;
                    case "rbDrillthrough":
                        ctl.Text = "Rapor";
                        break;
                    case "bParameters":
                        ctl.Text = "Parametreler";
                        break;
                    case "grpBoxVisibility":
                        ctl.Text = "Görünürlük";
                        break;
                    case "label2":
                        ctl.Text = "Gizli";
                        break;
                    case "label3":
                        ctl.Text = "Toogle item (Textbox adı)";
                        break;
                    case "label1":
                        ctl.Text = "İşaret(Bookmark)";
                        ctl.Width = 85;
                        break;

                    #endregion

                    default:
                        break;
                }
                ChangeControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeStyleBorderControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "label2":
                        ctl.Text = "Stil";
                        break;
                    case "label7":
                        ctl.Text = "Renk";
                        break;
                    case "label6":
                        ctl.Text = "Genişlik";
                        break;
                    case "label8":
                        ctl.Text = "Varsayılan";
                        ctl.Width = 55;
                        ctl.Location = new System.Drawing.Point(1, 42);
                        break;
                    case "lLeft":
                        ctl.Text = "Sol";
                        break;
                    case "lRight":
                        ctl.Text = "Sağ";
                        break;
                    case "lTop":
                        ctl.Text = "Üst";
                        break;
                    case "lBottom":
                        ctl.Text = "Alt";
                        break;
                    default:
                        break;
                }
                ChangeStyleBorderControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeFontControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "label2":
                        ctl.Text = "Dekorasyon";
                        break;
                    case "label3":
                        ctl.Text = "Stil";
                        break;
                    case "label4":
                        ctl.Text = "Format";
                        break;
                    case "label5":
                        ctl.Text = "Dikey";
                        break;
                    case "label6":
                        ctl.Text = "Hiza";
                        break;
                    case "label7":
                        ctl.Text = "Yön";
                        break;
                    case "label8":
                        ctl.Text = "Yazım Modu";
                        break;
                    case "label9":
                        ctl.Text = "Renk";
                        break;
                    case "label10":
                        ctl.Text = "Ağırlık";
                        break;
                    case "label11":
                        ctl.Text = "Boyut";
                        break;
                    case "lFont":
                        ctl.Text = "Font";
                        break;
                    case "lblValue":
                        //ctl.Text = "Değer";
                        ctl.Visible = false;
                        Label lbl = new Label();
                        lbl.Size = new System.Drawing.Size(ctl.Width, ctl.Height);
                        lbl.Location = new System.Drawing.Point(ctl.Location.X, ctl.Location.Y);
                        lbl.Text = "Değer";
                        ctl.Parent.Controls.Add(lbl);
                        break;
                    default:
                        break;
                }
                ChangeFontControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeBackgroundControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "groupBox1":
                        ctl.Text = "Arka Fon";
                        break;
                    case "label3":
                        ctl.Text = "Renk";
                        ctl.Width = 40;
                        break;
                    case "label15":
                        ctl.Text = "Bitiş Rengi";
                        ctl.Width = 90;
                        break;
                    case "groupBox2":
                        ctl.Text = "Arka Fon Resmi";
                        break;
                    case "rbNone":
                        ctl.Text = "Yok";
                        break;
                    case "rbExternal":
                        ctl.Text = "Dış Kaynak";
                        break;
                    case "rbEmbedded":
                        ctl.Text = "Gömülü";
                        break;
                    case "rbDatabase":
                        ctl.Text = "Veritabanı";
                        break;
                    case "label1":
                        ctl.Text = "Tekrarlı";
                        break;
                    default:
                        break;
                }
                ChangeBackgroundControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeStyleControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "label1":
                        ctl.Text = "Data Element Adı";
                        break;
                    case "label2":
                        ctl.Text = "Data Element Çıktısı";
                        break; 
                    case "label3":
                        ctl.Text = "Renk";
                        ctl.Width = 40;
                        break;
                    case "label11":
                        ctl.Text = "Sol";
                        break;
                    case "label12":
                        ctl.Text = "Sağ";
                        break;
                    case "label13":
                        ctl.Text = "Üst";
                        break;
                    case "label14":
                        ctl.Text = "Alt";
                        break;
                    case "label15":
                        ctl.Text = "Bitiş Rengi";
                        ctl.Width = 90;
                        break;
                    case "groupBox1":
                        ctl.Text = "Arka Fon";
                        break;
                    case "grpBoxPadding":
                        ctl.Text = "Dolgu";
                        break;                    
                    default:
                        break;
                }
                ChangeStyleControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeImageControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "groupBox1":
                        ctl.Text = "Kaynak";
                        break;
                    case "rbExternal":
                        ctl.Text = "Dış Kaynak";
                        break;
                    case "rbEmbedded":
                        ctl.Text = "Gömülü";
                        break;
                    case "rbDatabase":
                        ctl.Text = "Veritabanı";
                        break;
                    case "label1":
                        ctl.Text = "Boyut";
                        break;
                    default:
                        break;
                }
                ChangeImageControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeSubreportControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "bRefreshParms":
                        ctl.Text = "Güncelle";
                        ctl.Width = 60;

                        Button bt = new Button();
                        bt.Location = ctl.Location;
                        bt.Size = ctl.Size;
                        bt.Text = ctl.Text;
                        bt.Click += bRefreshParms_Click;
                        ctl.Visible = false;
                        ctl.Parent.Controls.Add(bt);
                        SubreportCtl sbCont = (SubreportCtl)ctl.Parent;
                        Type tp = sbCont.GetType();
                        DataTable dt = (DataTable)tp.GetField("_DataTable", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(sbCont);
                        DataGrid dg = (DataGrid)tp.GetField("dgParms", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(sbCont);
                        TextBox tb = (TextBox)sbCont.Controls.Find("tbReportFile", false).FirstOrDefault();
                        bt.Tag = new SubreportCtlPropHelper(dt, dg, tb);
                        break;
                    case "label1":
                        ctl.Text = "Alt Rapor Adı";
                        break;
                    case "label2":
                        ctl.Text = "Kayıt Bulunamadı\n\rMesajı";
                        ctl.Height = 30;
                        break;
                    case "chkMergeTrans":
                        ctl.Text = "Ana rapor veri kaynak bağlantısını kullan";
                        break;
                    case "bFile":
                        Button btn = new Button();
                        btn.Location = ctl.Location;
                        btn.Size = ctl.Size;
                        btn.Text = ctl.Text;
                        btn.Click+=btn_Click;
                        ctl.Visible = false;
                        ctl.Parent.Controls.Add(btn);
                        btn.Tag = ctl.Parent.Controls.Find("tbReportFile", false).FirstOrDefault();
                        break;
                    default:
                        break;
                }
                ChangeSubreportControlsPropertiesOnLoad(ctl);
            }
        }

        static void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            frmReportList rl = new frmReportList(ReportListViewType.Show);
            rl.ReportMultiSelection = false;
            if (rl.ShowDialog() == DialogResult.OK && btn.Tag != null)
            {
                ((TextBox)btn.Tag).Text = String.Format("{0}-{1}",rl.SelectedNReports[0].Id.ToString(), rl.SelectedNReports[0].Name);
            }
        }

        static void bRefreshParms_Click(object sender, System.EventArgs e)
        {
            SubreportCtlPropHelper helper = (SubreportCtlPropHelper)((Button)sender).Tag;

            NReportInfo ri = NReportInfoHelper.GetNSubReport(helper.SubReportName.Text);
            
            RDLParser rdlp = new RDLParser(ri.Source);
            Report report = rdlp.Parse();
            
            if (report == null)
                return;					// error: message already displayed

            ICollection rps = report.UserReportParameters;
            string[] rowValues = new string[2];
            helper.ParamDataTable.Rows.Clear();
            foreach (UserReportParameter rp in rps)
            {
                rowValues[0] = rp.Name;
                rowValues[1] = "";

                helper.ParamDataTable.Rows.Add(rowValues);
            }
            helper.ParamDataGrid.Refresh();
        }

        internal static void ChangeFiltersControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "bDelete":
                        ctl.Text = "Sil";
                        break;
                    case "bUp":
                        ctl.Text = "Yukarı";
                        break;
                    case "bDown":
                        ctl.Text = "Aşağı";
                        break;
                    case "dgFilters":
                        DataGridView dg = (DataGridView)ctl;
                        dg.Columns[0].HeaderText = "Filitre Formülü";
                        dg.Columns[1].HeaderText = "Operatör";
                        dg.Columns[2].HeaderText = "Değer(ler)";
                        break;
                    default:
                        break;
                }
                ChangeFiltersControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeSortingControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "bDelete":
                        ctl.Text = "Sil";
                        break;
                    case "bUp":
                        ctl.Text = "Yukarı";
                        break;
                    case "bDown":
                        ctl.Text = "Aşağı";
                        break;
                    case "dgSorting":
                        DataGridView dg = (DataGridView)ctl;
                        dg.Columns[0].HeaderText = "Sıralama Formülü";
                        dg.Columns[1].HeaderText = "Azalan";
                        break;
                    default:
                        break;
                }
                ChangeSortingControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeGroupingControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "bDelete":
                        ctl.Text = "Sil";
                        break;
                    case "bUp":
                        ctl.Text = "Yukarı";
                        break;
                    case "bDown":
                        ctl.Text = "Aşağı";
                        break;
                    case "label1":
                        ctl.Text = "Ad";
                        break;
                    case "chkPBS":
                        ctl.Text = "Başlangıçta Sayfa Sonu";
                        ctl.Width = 145;
                        break;
                    case "chkPBE":
                        ctl.Text = "Bitişte Sayfa Sonu";
                        ctl.Width = 145;
                        break;
                    case "chkGrpHeader":
                        ctl.Text = "Gurup Başlıklarını Ekle";
                        ctl.Width = 145;
                        break;
                    case "chkGrpFooter":
                        ctl.Text = "Gurup Footerlarını Ekle";
                        ctl.Width = 145;
                        break;
                    case "chkRepeatHeader":
                        ctl.Text = "Gurup Başlıklarını Tekrarla";
                        ctl.Width = 145;
                        break;
                    case "chkRepeatFooter":
                        ctl.Text = "Gurup Footerlarını Tekrarla";
                        ctl.Width = 145;
                        break;
                    case "dgGroup":
                        DataGrid dg = (DataGrid)ctl;
                        ((DataTable)dg.DataSource).Columns[0].Caption = "Formül";
                        break;
                    default:
                        break;
                }
                ChangeGroupingControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeReportParameterControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "bAdd":
                        ctl.Text = "Ekle";
                        break;
                    case "bRemove":
                        ctl.Text = "Çıkar";
                        break;
                    case "lParmName":
                        ctl.Text = "Ad";
                        break;
                    case "lParmType":
                        ctl.Text = "Veri Tipi";
                        break;
                    case "lParmPrompt":
                        ctl.Text = "Prompt";
                        break;
                    case "ckbParmAllowNull":
                        ctl.Text = "Null olblr.";
                        break;
                    case "ckbParmAllowBlank":
                        ctl.Text = "Boş Değer Alabilir";
                        break;
                    case "ckbParmMultiValue":
                        ctl.Text = "Çoklu Değer Alabilir";
                        ctl.Width = 130;
                        break;
                    case "gbDefaultValues":
                        ctl.Text = "Varsayılan Değerler";
                        break;
                    case "rbDefaultValues":
                        ctl.Text = "Değerler";
                        ctl.Width = 70;
                        break;
                    case "rbDefaultDataSetName":
                        ctl.Text = "Dataset Adı";
                        break;
                    case "lDefaultValueFields":
                        ctl.Text = "Değer Alanı";
                        break;
                    case "gbValidValues":
                        ctl.Text = "Geçerli Değerler";
                        break;
                    case "rbValues":
                        ctl.Text = "Değerler";
                        ctl.Width = 70;
                        break;
                    case "rbDataSet":
                        ctl.Text = "Dataset Adı";
                        break;
                    case "lValidValuesField":
                        ctl.Text = "Değer Alanı";
                        break;
                    case "lDisplayField":
                        ctl.Text = "Görünür Alan";
                        break;
                    case "tbParmName":
                        TextBox txtPName = (TextBox)ctl;
                        txtPName.TextChanged += txtPName_TextChanged;
                        txtPName.Tag = ctl.Parent.Controls.Find("tbParmPrompt", false).FirstOrDefault();
                        break;
                    default:
                        break;
                }
                ChangeReportParameterControlsPropertiesOnLoad(ctl);
            }
        }

        static void txtPName_TextChanged(object sender, EventArgs e)
        {
            TextBox txtB = (TextBox)sender;
            TextBox txtRef = (TextBox)txtB.Tag;
            if (txtB.Text.StartsWith(txtRef.Text))
                txtRef.Text = txtB.Text;
        }

        internal static void ChangeCodeControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "label2":
                        ctl.Text = "Mesajlar";
                        break;
                    case "label1":
                        ctl.Text = "Visual Basic Method Kodu";
                        break;
                    case "bCheckSyntax":
                        ctl.Text = "Kontrol Et";
                        break;
                    default:
                        break;
                }
                ChangeCodeControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeModulesControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "bDeleteCM":
                        ctl.Text = "Sil";
                        break;
                    case "label1":
                        ctl.Text = "Formülde kullanılacak olan modül adlarını giriniz (örnek: MyRoutines.dll)";
                        break;
                    case "label2":
                        ctl.Text = "Formülde kullanılmak için yaratılacak sınıf adlarını giriniz";
                        break;
                    case "bDeleteClass":
                        ctl.Text = "Sil";
                        break;
                    default:
                        break;
                }
                ChangeModulesControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeReportControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {                    
                    case "label1":
                        ctl.Text = "Genişlik";
                        break;
                    case "label2":
                        ctl.Text = "Açıklama";
                        break;
                    case "label3":
                        ctl.Text = "Sahip";
                        break;
                    case "label4":
                        ctl.Text = "Uzunluk";                        
                        break;
                    case "label5":
                        ctl.Text = "Sol";
                        break;
                    case "label6":
                        ctl.Text = "Sağ";
                        break;
                    case "label7":
                        ctl.Text = "Alt";
                        break;
                    case "label8":
                        ctl.Text = "Üst";
                        break;
                    case "label9":
                        ctl.Text = "Genişlik";
                        break;
                    case "groupBox1":
                        ctl.Text = "Sayfa";
                        break;
                    case "groupBox2":
                        ctl.Text = "Sınır";
                        break;
                    case "groupBox3":
                        ctl.Text = "Sayfa Başlıkları";
                        break;
                    case "groupBox4":
                        ctl.Text = "Sayfa Alt Başlıkları";
                        break;
                    case "label11":
                        ctl.Text = "Sayfa Boyu";
                        break;
                    default:
                        break;
                }
                ChangeReportControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeReportXmlControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "label1":
                        ctl.Text = "XSL Data Transform";
                        break;
                    case "label2":
                        ctl.Text = "Data Şema";
                        break;
                    case "label3":
                        ctl.Text = "Üst Element Adı";
                        break;
                    case "label4":
                        ctl.Text = "Element Stili";
                        break;                    
                    default:
                        break;
                }
                ChangeReportXmlControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeBodyControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "label1":
                        ctl.Text = "Boy";
                        break;
                    case "label2":
                        ctl.Text = "Kolonlar";
                        break;
                    case "label3":
                        ctl.Text = "Kolon Aralıkları";
                        break;                    
                    default:
                        break;
                }
                ChangeBodyControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeDataSetsControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctrl in mainControl.Controls)
            {
                switch (ctrl.Name)
                {
                    case "lDataSetName":
                        ctrl.Text = "Ad";
                        break;
                    case "lDataSource":
                        ctrl.Text = "Veri Kaynağı";
                        break;
                    case "label1":
                        ctrl.Text = "SQL Sorgusu";
                        break;
                    case "label3":
                        ctrl.Text = "Zaman Aşımı";
                        break;
                    case "bRefresh":
                        ctrl.Text = "Alanları Güncelle";
                        break;
                    case "label2":
                        ctrl.Text = "Alanlar";
                        break;
                    case "bDeleteField":
                        ctrl.Text = "Sil";
                        break;
                    case "dgFields":
                        DataGridView gv = (DataGridView)ctrl;
                        gv.Columns[0].HeaderText = "Ad";
                        gv.Columns[1].HeaderText = "Alan";
                        gv.Columns[2].HeaderText = "Değer";
                        gv.Columns[3].HeaderText = "Tip";
                        break;
                    case "cbDataSource":
                        ComboBox cb = (ComboBox)ctrl;
                        if (cb.Items.Count > 0 && cb.SelectedIndex < 0)
                            cb.SelectedIndex = 0;
                        break;
                    default:
                        break;
                }
                ChangeDataSetsControlsPropertiesOnLoad(ctrl);
            }
        }

        internal static void ChangeQueryParametersControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "dgParms":
                        DataGridView gv = (DataGridView)ctl;
                        gv.Columns[0].HeaderText = "Paranmetre Adı";
                        gv.Columns[1].HeaderText = "Değer";
                        break;
                    default:
                        break;
                }
                ChangeQueryParametersControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeDataSetRowsControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctrl in mainControl.Controls)
            {
                switch (ctrl.Name)
                {
                    case "bDelete":
                        ctrl.Text = "Sil";
                        break;
                    case "bUp":
                        ctrl.Text = "Yukarı";
                        break;
                    case "bDown":
                        ctrl.Text = "Aşağı";
                        break;
                    case "dgFilters":
                        DataGridView gv = (DataGridView)ctrl;
                        gv.Columns[0].HeaderText = "Filitre ";
                        gv.Columns[1].HeaderText = "Operatör";
                        gv.Columns[2].HeaderText = "Değer(ler)";
                        break;
                    case "chkRowsFile":
                        ctrl.Text = "XML Dosyası Kullan";
                        break;
                    case "bClear":
                        ctrl.Text = "Temizle";
                        break;
                    case "bLoad":
                        ctrl.Text = "SQL'den Yükle";
                        break;
                    case "label1":
                        ctrl.Visible = false;
                        break;  
                    default:
                        break;
                }
                ChangeDataSetRowsControlsPropertiesOnLoad(ctrl);
            }
        }

        internal static void ChangeChartLegendControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "label1":
                        ctl.Text = "Grafik Pozisyonu";
                        break;
                    case "label2":
                        ctl.Text = "Legend Düzeni";
                        break;
                    case "chkVisible":
                        ctl.Text = "Görünürlük";
                        break;
                    case "chkInsidePlotArea":
                        ctl.Text = "Plot alanı içinde çiz";
                        break;
                    default:
                        break;
                }
                ChangeChartLegendControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeChartControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "label1":
                        ctl.Text = "Grafik Tipi";
                        break;
                    case "label2":
                        ctl.Text = "Palet";
                        break;
                    case "label4":
                        //Percent width for Bars/Columns (>100% causes column overlap)
                        ctl.Text = "Bar/Kolon için genişlik yüzdesi (>100% kolon çakışmasına yol açar)";
                        break;
                    case "label5":
                        ctl.Text = "Alt Tip";                        
                        break;
                    case "label6":                        
                        ctl.Text = "Kayıt Bulunamadı Mesajı";
                        ctl.Width = 130;
                        break;
                    case "label7":
                        ctl.Text = "Data set Adı";
                        break;
                    case "label8":
                        ctl.Text = "Vektör\r\nGibi\r\nYorumla";
                        ctl.Height = 40;
                        break;
                    case "lData1":
                        ctl.Text = "Grafik Veri(X Koor.)";
                        break;
                    case "lData2":
                        ctl.Text = "Y Koordinatı";
                        break;
                    case "lData3":
                        ctl.Text = "Balon Boyutu";
                        break;
                    case "chkDataLabel":
                        ctl.Text = "Veri Etiketi";
                        break;
                    case "chkPageBreakStart":
                        ctl.Text = "Başlangıçta Sayfa Sonu";
                        ctl.Width = 150;
                        break;
                    case "chkPageBreakEnd":
                        ctl.Text = "Sonda Sayfa Sonu";
                        ctl.Width = 150;
                        break;
                    case "chkToolTip":
                        ctl.Text = "X Bilgisi";
                        break;
                    case "checkBox1":
                        ctl.Text = "Y Bilgisi";
                        break;
                    default:
                        break;
                }
                ChangeChartControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeChartAxisControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {                    
                    case "label1":
                        ctl.Text = "Major İşaretleri Göster";
                        break;
                    case "label2":
                        ctl.Text = "Minor İşaretleri Göster";
                        break;
                    case "groupBox1":
                        ctl.Text = "Major Grid Çizgileri";
                        break;
                    case "chkMajorGLShow":
                    case "chkMinorGLShow":
                        ctl.Text = "Göster";
                        break;
                    case "label3":
                    case "label8":
                        ctl.Text = "Stil";
                        break;
                    case "label7":
                    case "label4":
                        ctl.Text = "Renk";
                        break;
                    case "label5":
                    case "label6":
                        ctl.Text = "Genişlik";
                        break;
                    case "groupBox2":
                        ctl.Text = "Minor Grid Çizgileri";
                        break;
                    case "label9":
                        ctl.Text = "Major Aralık";
                        break;
                    case "label10":
                        ctl.Text = "Minor Aralık";
                        break;
                    case "label11":
                        ctl.Text = "Maksimum Değer";
                        break;
                    case "label12":
                        ctl.Text = "Minimum Değer"; 
                        break;
                    case "chkVisible":
                        ctl.Text = "Görünür";
                        break;
                    case "chkLogScale":
                        ctl.Text = "Log Ölçeği";
                        break;
                    case "chkMargin":
                        ctl.Text = "Sınır";
                        break;
                    case "chkScalar":
                        ctl.Text = "Skalar";
                        break;
                    case "chkReverse":
                        ctl.Text = "Ters Yön";
                        break;
                    case "chkInterlaced":
                        ctl.Text = "Birbirine Geçir";
                        break;
                    case "chkMonth":
                        ctl.Text = "Ay Kategori Oranı";
                        break;
                    case "chkCanOmit":
                        ctl.Text = "Kesildiğinde Atla";
                        break;                    
                    default:
                        break;
                }
                ChangeChartAxisControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeListControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "label2":
                        ctl.Text = "Dataset Adı";
                        break;
                    case "groupBox1":
                        ctl.Text = "Sayfa Sonu";
                        break;
                    case "chkPBBefore":
                        ctl.Text = "Listeden önce";
                        break;
                    case "chkPBAfter":
                        ctl.Text = "Listeden Sonra";
                        break;
                    case "chkXmlInstances":
                        ctl.Text = "Liste nesnesini render et";
                        break;
                    case "label1":
                        ctl.Text = "Kayıt Bulunamadı\n\rMesajı";
                        ctl.Height = 40;
                        break;
                    case "label3":
                        ctl.Text = "Veri Nesne Adı";                        
                        break;
                    case "bGroups":
                        ctl.Text = "Gurup Formülü";                        
                        break;                        
                    default:
                        break;
                }
                ChangeListControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeTableControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "label2":
                        ctl.Text = "Dataset Adı";
                        break;
                    case "groupBox1":
                        ctl.Text = "Sayfa Sonu";
                        break;
                    case "chkPBBefore":
                        ctl.Text = "Tablodan önce";
                        break;
                    case "chkPBAfter":
                        ctl.Text = "Tablodan Sonra";
                        break;
                    case "groupBox3":
                        ctl.Text = "Tablo Kayıtları";
                        break;
                    case "label1":
                        ctl.Text = "Kayıt Bulunamadı\n\rMesajı";
                        ctl.Height = 40;
                        break;
                    case "chkDetails":
                        ctl.Text = "Detay Kayıtlar";
                        break;
                    case "chkHeaderRows":
                        ctl.Text = "Başlıklar";
                        break;
                    case "chkFooterRows":
                        ctl.Text = "Alt Başlıklar";
                        break;
                    case "chkHeaderRepeat":
                        ctl.Text = "Başlıkları\r\nTekrarla";
                        break;
                    case "chkFooterRepeat":
                        ctl.Text = "Alt Başlıkları\r\nTekrarla";
                        break;
                    case "chkRenderDetails":
                        ctl.Text = "Çıktıda Detayları Render Et";
                        ctl.Width = 200;
                        break;
                    case "label3":
                        ctl.Text = "Data Element Adı";
                        break;
                    case "label4":
                        ctl.Text = "Detay Liste Adı";
                        break;
                    default:
                        break;
                }
                ChangeTableControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeTableColumnControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "grpBoxVisibility":
                        ctl.Text = "Görünürlük";
                        break;
                    case "label1":
                        ctl.Text = "Kolon Genişliği";
                        ctl.Width = 90;
                        break;   
                    case "label2":
                        ctl.Text = "Gizli (İlk)";
                        break;
                    case "label3":
                        ctl.Text = "Toogle item (Textbox adı)";
                        break;
                    case "chkFixedHeader":
                        ctl.Text = "Başlıkları Sabitle";
                        ctl.Width = 100;
                        break;
                    case "label4":
                        //Note: Fixed headers must be contiguous and start at either the left or the right of the table.  Current renderers ignore this parameter.
                        ctl.Visible = false;
                        break;                  
                    default:
                        break;
                }
                ChangeTableColumnControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeTableRowControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "grpBoxVisibility":
                        ctl.Text = "Görünürlük";
                        break;
                    case "label1":
                        ctl.Text = "Satır Yüksekliği";
                        ctl.Width = 90;
                        break;
                    case "label2":
                        ctl.Text = "Gizli (İlk)";
                        break;
                    case "label3":
                        ctl.Text = "Toogle item (Textbox adı)";
                        break;                   
                    default:
                        break;
                }
                ChangeTableRowControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeGridControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {                   
                    case "groupBox1":
                        ctl.Text = "Sayfa Sonu";
                        break;
                    case "chkPBBefore":
                        ctl.Text = "Gridden önce";
                        break;
                    case "chkPBAfter":
                        ctl.Text = "Gridden Sonra";
                        break;
                    case "groupBox3":
                        ctl.Text = "Grid Kayıtları";
                        break;      
                    case "chkDetails":
                        ctl.Text = "Detay Kayıtlar";
                        break;
                    case "chkHeaderRows":
                        ctl.Text = "Başlıklar";
                        break;
                    case "chkFooterRows":
                        ctl.Text = "Alt Başlıklar";
                        break;
                    case "chkHeaderRepeat":
                        ctl.Text = "Başlıkları\r\nTekrarla";
                        break;
                    case "chkFooterRepeat":
                        ctl.Text = "Alt Başlıkları\r\nTekrarla";
                        break;                    
                    default:
                        break;
                }
                ChangeGridControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangeMatrixControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "label1":
                        ctl.Text = "Kayıt Bulunamadı\n\rMesajı";
                        ctl.Height = 40;
                        break;
                    case "label2":
                        ctl.Text = "Dataset Adı";
                        break;
                    case "label3":
                        ctl.Text = "Hücre Element Adı";
                        break;
                    case "groupBox1":
                        ctl.Text = "Sayfa Sonu";
                        break;
                    case "chkPBBefore":
                        ctl.Text = "Tablodan önce";
                        break;
                    case "chkPBAfter":
                        ctl.Text = "Tablodan Sonra";
                        break;
                    case "chkCellContents":
                        ctl.Text = "Hücre içeriğini Render Et";
                        ctl.Width = 200;
                        break;                                     
                    default:
                        break;
                }
                ChangeMatrixControlsPropertiesOnLoad(ctl);
            }
        }

        internal static void ChangePositionControlsPropertiesOnLoad(Control mainControl)
        {
            foreach (Control ctl in mainControl.Controls)
            {
                switch (ctl.Name)
                {
                    case "label1":
                        ctl.Text = "Ad";
                        break;
                    case "lblColSpan":
                        ctl.Text = "Kolon Span";
                        break;
                    case "gbPosition":
                        ctl.Text = "Pozisyon";                        
                        break;
                    case "label5":
                        ctl.Text = "Sol";
                        break;                  
                    case "label6":
                        ctl.Text = "Üst";
                        break;
                    case "label7":
                        ctl.Text = "Boy";
                        break;
                    case "label8":
                        ctl.Text = "Genişlik";                        
                        break;
                    case "gbText":
                        ctl.Text = "Text İşlemleri";
                        break;
                    case "chkCanGrow":
                        ctl.Text = "Genişlesin";
                        break;
                    case "chkCanShrink":
                        ctl.Text = "Daralsın";
                        break;
                    case "label2":
                        ctl.Text = "Çiftleyeni Gizle";
                        break;
                    case "label3":
                        ctl.Text = "XML Element Tipi";
                        break;
                    case "label4":
                        ctl.Text = "Toggle Resmi";
                        break;
                    default:
                        break;
                }
                ChangePositionControlsPropertiesOnLoad(ctl);
            }
        }

        #endregion

    }

    internal class SubreportCtlPropHelper
    {
        public DataTable ParamDataTable { get; set; }
        public DataGrid ParamDataGrid { get; set; }
        public TextBox SubReportName { get; set; }

        internal SubreportCtlPropHelper(DataTable dt, DataGrid dg, TextBox tb)
        {
            this.ParamDataTable = dt;
            this.ParamDataGrid = dg;
            this.SubReportName = tb;
        }

    }
}
