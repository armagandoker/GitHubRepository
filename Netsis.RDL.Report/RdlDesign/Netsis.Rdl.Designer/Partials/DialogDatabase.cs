using fyiReporting.RDL;
using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using fyiReporting.RdlDesign.Resources;
using Telerik.WinControls;

namespace fyiReporting.RdlDesign
{
    public partial class DialogDatabase
    {   

        #region [_PRIVATES_]

        private void ChangeControlsPropertiesOnLoad()
        {
            tcDialog.DrawItem += tcDialog_DrawItem;
            tcDialog.Appearance = TabAppearance.Normal;
            tcDialog.DrawMode = TabDrawMode.OwnerDrawFixed;

            foreach (TabPage tp in tcDialog.TabPages)
            {
                tp.BackColor = this.BackColor;
                tp.BorderStyle = BorderStyle.None;
            }

            this.Text = "Yeni Bağlantı Bilgileri";
            btnOK.Text = "Tamam";
            btnCancel.Text = "İptal";
            
            tcDialog.TabPages.RemoveAt(6);

            tcDialog.TabPages[0].Text = "Rapor Bilgileri";
            tcDialog.TabPages[1].Text = "Bağlantı Bilgileri";
            tcDialog.TabPages[2].Text = "Parametreler";
            tcDialog.TabPages[4].Text = "Gruplama";
            tcDialog.TabPages[5].Text = "Rapor Syntax";

            tcDialog.SelectedIndexChanged -= tabControl1_SelectedIndexChanged;
            tcDialog.SelectedIndexChanged += tcDialog_SelectedIndexChanged;            

            SetControlProps(tcDialog.TabPages[0]);
            SetControlProps(tcDialog.TabPages[1]);
            SetControlProps(tcDialog.TabPages[2]);
            //SetControlProps(tcDialog.TabPages[3]);
            SetControlProps(tcDialog.TabPages[4]);

            /*this._TemplateList = this._TemplateList.Replace("Globals!PageNumber + ' of ' + Globals!TotalPages",
                "Globals!PageNumber.ToString() + \" of \" + Globals!TotalPages.ToString()"); */

            string str = @"    <Left>1pt</Left>
        <Top>1pt</Top>
      </Table>";

            this._TemplateTable = this._TemplateTable.Replace("</Table>", str);

            buttonDatabaseSearch.Click -= buttonDatabaseSearch_Click;
            buttonDatabaseSearch.Click += buttonDBSearch_Click;
            comboDatabaseList.SelectedIndexChanged += comboDBList_SelectedIndexChanged;
            comboDatabaseList.DropDown += comboDatabaseList_DropDown;
            comboDatabaseList.TextChanged += comboDatabaseList_TextChanged;

            buttonSqliteSelectDatabase.TabIndex = 4;
            comboServerList.TabIndex = 5;
            buttonSearchSqlServers.TabIndex = 6;
            textBoxSqlUser.TabIndex = 7;
            textBoxSqlPassword.TabIndex = 8;
            comboDatabaseList.TabIndex = 9;
            buttonDatabaseSearch.TabIndex = 10;
            btnOK.TabIndex = 0;
            btnCancel.TabIndex = 1;

            groupBox1.Visible = false;
            label1.Location = new Point(label1.Location.X, label1.Location.Y - 100);
            label2.Location = new Point(label2.Location.X, label2.Location.Y - 100);
            label3.Location = new Point(label3.Location.X, label3.Location.Y - 100);
            label6.Location = new Point(label6.Location.X, label6.Location.Y - 100);
            tbReportName.Location = new Point(tbReportName.Location.X, tbReportName.Location.Y - 100);
            tbReportDescription.Location = new Point(tbReportDescription.Location.X, tbReportDescription.Location.Y - 100);
            tbReportAuthor.Location = new Point(tbReportAuthor.Location.X, tbReportAuthor.Location.Y - 100);
            cbOrientation.Location = new Point(cbOrientation.Location.X, cbOrientation.Location.Y - 100);

            btnOK.Click -= btnOK_Click;
            btnOK.Click += btnNewOK_Click;

            tbConnection.Text = string.Empty;

            TextBox txtPName = (TextBox)(reportParameterCtl1.Controls.Find("tbParmName", true).FirstOrDefault());
            txtPName.TextChanged += UserControlPropertiesHelper.txtPName_TextChanged;
            txtPName.Tag = reportParameterCtl1.Controls.Find("tbParmPrompt", true).FirstOrDefault();

            tvTablesColumns.NodeMouseDoubleClick += tvTablesColumns_NodeMouseDoubleClick;

            UserControlPropertiesHelper.AddSQLTableViewFilterToTreeView(tvTablesColumns);
           
        }

        public void tvTablesColumns_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
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

        private void btnNewOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbReportName.Text.Trim()))
            {
                RadMessageBox.Show("Lütfen Rapor Adı giriniz.", "Hata", MessageBoxButtons.OK, RadMessageIcon.Error, MessageBoxDefaultButton.Button1);
                tcDialog.SelectedIndex = 0;
            }
            else
            {
                if(CheckDBConnection() && CheckSQLColumns())
                    btnOK_Click(sender, e);
            }
        }

        void tcDialog_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tag = (string)tcDialog.TabPages[tcDialog.SelectedIndex].Tag;
            bool retVal = true;
            switch (tag)
            {                
                case "sql":		// obtain table and column information      
                    retVal = CheckDBConnection();
                    break;
                case "group":	// obtain group by information using connection & sql
                case "syntax":	
                    retVal = CheckDBConnection();
                    if (retVal)
                        retVal = CheckSQLColumns();
                    break;
                default:
                    break;
            }
            if (retVal)
            {
                tabControl1_SelectedIndexChanged(sender, e);
                if (tag == "sql")
                {
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
                    UserControlPropertiesHelper.AddNodesToTreeViewTag(tvTablesColumns);
                }
            }
        }

        bool CheckDBConnection()
        {
            try
            {
                IDbConnection cnSQL = RdlEngineConfig.GetConnection(GetDataProvider(), GetDataConnection());
				if (cnSQL == null)				
                    throw new Exception("Hata");                    				
				cnSQL.Open();
                cnSQL.Close();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(string.Format(Strings.DesignerUtility_Show_ConnectDataProviderError, GetDataProvider()), Strings.DesignerUtility_Show_SQLError, MessageBoxButtons.OK, RadMessageIcon.Error, MessageBoxDefaultButton.Button1);
                tcDialog.SelectedIndex = 1;
                return false;
            }
            return true;
        }

        bool CheckSQLColumns()
        {            
            if (string.IsNullOrEmpty(tbSQL.Text.Trim()))
            {                
                RadMessageBox.Show("Tablo yada view seçimi yapmalısınız.", Strings.DesignerUtility_Show_SQLError, MessageBoxButtons.OK, RadMessageIcon.Error, MessageBoxDefaultButton.Button1);
                tcDialog.SelectedIndex = 3;
                return false;
            }
            return true;
        }

        void comboDatabaseList_TextChanged(object sender, EventArgs e)
        {
            SetConnectionString();
        }

        void comboDatabaseList_DropDown(object sender, EventArgs e)
        {
            if (comboDatabaseList.Items.Count == 0)
                buttonDatabaseSearch.PerformClick();
        }

        private void buttonDBSearch_Click(object sender, EventArgs e)
        {
            if (comboServerList.Items.Count == 0 || comboServerList.SelectedValue == null)
            {                
                string str = (comboServerList.Text.Trim() == string.Empty ? "localhost" : comboServerList.Text);
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add(str, str);               
                comboServerList.ValueMember = "Value";
                comboServerList.DisplayMember = "Key";
                comboServerList.DataSource = new BindingSource(dict, null);
                comboServerList.SelectedIndex = 0;
            }
            buttonDatabaseSearch_Click(sender, e);
        }

        private void comboDBList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDatabaseList.Items.Count != 0)
                SetConnectionString();
            
        }

        private void SetConnectionString()
        {
            string connString = "Server={0};DataBase={1};User Id={2};Password={3};Connect Timeout=5";
            tbConnection.Text = string.Format(connString, comboServerList.Text, comboDatabaseList.Text, textBoxSqlUser.Text, textBoxSqlPassword.Text);
        }

        void tcDialog_DrawItem(object sender, DrawItemEventArgs e)
        {
            FakeTelericThemeHelper.ChangeTabColor(e, tcDialog);
        }        

        private void SetControlProps(Control mainControl)
        {
            ComboBox cmb = null;
            Label lbl = null;
            foreach (Control ctrl in mainControl.Controls)
            {
                switch (ctrl.Name)
                {

                    #region [_TAB 1_]

                    case "rbTable":
                        ctrl.Text = "Tablo";
                        break;
                    case "rbList":
                        ctrl.Text = "Liste";
                        break;
                    case "label1":
                        ctrl.Text = "İsim";
                        break;
                    case "label2":
                        ctrl.Text = "Açıklama";
                        break;
                    case "label3":
                        ctrl.Text = "Hazırlayan";
                        break;
                    case "label6":
                        ctrl.Text = "Sayfa Tipi";
                        break;
                    case "groupBox1":
                        ctrl.Text = "Rapor Tipi";
                        break;
                    case "groupBox2":
                        ctrl.Visible = false;
                        //ctrl.Text = "RDL Şema";
                        break;
                    case "rbSchemaNo":
                        ctrl.Text = "Yok";
                        break;
                    case "cbOrientation":
                        cmb = (ComboBox)ctrl;
                        cmb.Items.Clear();
                        cmb.Items.Add("Dikey (8.5\" by 11\")");
                        cmb.Items.Add("Yatay (11\" by 8.5\")");
                        cmb.SelectedIndex = 0;
                        break;

                    #endregion

                    #region [_TAB 2_]

                    case "label7":
                        ctrl.Text = "Bağlantı Tipi";
                        break;
                    case "label9":
                        ctrl.Text = "Veritabanı";
                        break;
                    case "label10":
                        ctrl.Text = "Kullanıcı Adı";
                        break;
                    case "label11":
                        ctrl.Text = "Şifre";
                        break;
                    case "lConnection":
                        ctrl.Text = "Bağlantı";
                        lbl = (Label)ctrl;
                        lbl.TextChanged += lbl_TextChanged;
                        break;
                    case "buttonSearchSqlServers":
                        ctrl.Text = "Server Ara";
                        break;
                    case "buttonDatabaseSearch":
                        ctrl.Text = "Veritabanı Ara";
                        break;
                    case "cbConnectionTypes":
                        cmb = (ComboBox)ctrl;
                        cmb.Items.Clear();
                        cmb.Items.Add("SQL");
                        cmb.Items.Add("Oracle");
                        cmb.SelectedIndex = 0;
                        break;
                    case "bTestConnection":
                        ctrl.Text = "Bağlantı Test";
                        break;

                    #endregion

                    #region [_TAB 3_]

                    case "bAdd":
                        ctrl.Text = "Ekle";
                        break;
                    case "bRemove":
                        ctrl.Text = "Çıkar";
                        break;
                    case "lParmName":
                        ctrl.Text = "Ad";
                        break;
                    case "lParmType":
                        ctrl.Text = "Veri Tipi";
                        break;
                    case "lParmPrompt":
                        ctrl.Text = "Prompt";
                        break;
                    case "ckbParmAllowNull":
                        ctrl.Text = "Null";
                        break;
                    case "ckbParmAllowBlank":
                        ctrl.Text = "Boş(Sadece string).";
                        break;
                    case "ckbParmMultiValue":
                        ctrl.Text = "Çoklu Değer";
                        ctrl.Size = new Size(85, ctrl.Size.Height);
                        break;
                    case "gbDefaultValues":
                        ctrl.Text = "Default Değerler";
                        break;
                    case "rbDefaultValues":
                        ctrl.Text = "Değer";
                        break;
                    case "rbDefaultDataSetName":
                        ctrl.Text = "DataSet Adı";
                        break;
                    case "lDefaultValueFields":
                        ctrl.Text = "Değer Alanı";
                        break;
                    case "gbValidValues":
                        ctrl.Text = "Geçerli Değerler";
                        break;
                    case "rbValues":
                        ctrl.Text = "Değer";
                        break;
                    case "rbDataSet":
                        ctrl.Text = "DataSet Adı";
                        break;
                    case "lValidValuesField":
                        ctrl.Text = "Değer Alanı";
                        break;
                    case "lDisplayField":
                        ctrl.Text = "Görünecek Alan";
                        break;

                    #endregion                    

                    #region [_TAB 5_]

                    case "label4":
                        ctrl.Text = "Gruplama için bir kolon seçin";
                        break;
                    case "ckbGrandTotal":
                        ctrl.Text = "Toplam Hesapla";
                        break;
                    case "label5":
                        ctrl.Width = 250;
                        ctrl.Text = "Alt toplamlar için istediğiniz kolonları seçin";
                        break;

                    #endregion

                    default:
                        break;
                }

                SetControlProps(ctrl);
            }
        }

        private void lbl_TextChanged(object sender, EventArgs e)
        {
            ((Label)sender).TextChanged -= lbl_TextChanged;
            ((Label)sender).Text = "Bağlantı :";
            ((Label)sender).TextChanged += lbl_TextChanged;
        }

        #endregion

        #region [_PROTECTEDS_]       

        protected override void OnLoad(EventArgs e)
        {           
            base.OnLoad(e);
            FakeTelericThemeHelper.ApplyToolBar(this);
            ChangeControlsPropertiesOnLoad();
        }

        protected override void OnPaint(PaintEventArgs e)
        {         
            base.OnPaint(e);           
            FakeTelericThemeHelper.PaintToolBar(e, this);
        }

        internal string GetReportName()
        {
            return tbReportName.Text;
        }
        
        #endregion
     
    }
}
