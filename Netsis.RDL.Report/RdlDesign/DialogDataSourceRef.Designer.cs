using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace fyiReporting.RdlDesign
{
    public partial class DialogDataSourceRef : System.Windows.Forms.Form
	{
		#region Windows Form Designer generated code
		private System.Windows.Forms.Label label1;
private System.Windows.Forms.TextBox tbPassword;
private System.Windows.Forms.Label label2;
private System.Windows.Forms.TextBox tbFilename;
private System.Windows.Forms.Button bGetFilename;
private System.Windows.Forms.Label label3;
private System.Windows.Forms.ComboBox cbDataProvider;
private System.Windows.Forms.Label label4;
private System.Windows.Forms.TextBox tbConnection;
private System.Windows.Forms.CheckBox ckbIntSecurity;
private System.Windows.Forms.Label label5;
private System.Windows.Forms.TextBox tbPrompt;
private System.Windows.Forms.Button bOK;
private System.Windows.Forms.Button bCancel;
private System.Windows.Forms.TextBox tbPassword2;
private System.Windows.Forms.Label label6;
private System.Windows.Forms.Button bTestConnection;
private System.Windows.Forms.ComboBox cbOdbcNames;
private System.Windows.Forms.Label lODBC;
private System.ComponentModel.Container components = null;

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogDataSourceRef));
			this.label1 = new System.Windows.Forms.Label();
			this.tbPassword = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.bGetFilename = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.cbDataProvider = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tbConnection = new System.Windows.Forms.TextBox();
			this.ckbIntSecurity = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tbPrompt = new System.Windows.Forms.TextBox();
			this.bOK = new System.Windows.Forms.Button();
			this.bCancel = new System.Windows.Forms.Button();
			this.tbPassword2 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.bTestConnection = new System.Windows.Forms.Button();
			this.cbOdbcNames = new System.Windows.Forms.ComboBox();
			this.lODBC = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// tbPassword
			// 
			resources.ApplyResources(this.tbPassword, "tbPassword");
			this.tbPassword.Name = "tbPassword";
			this.tbPassword.TextChanged += new System.EventHandler(this.validate_TextChanged);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// tbFilename
			// 
			resources.ApplyResources(this.tbFilename, "tbFilename");
			this.tbFilename.Name = "tbFilename";
			this.tbFilename.TextChanged += new System.EventHandler(this.validate_TextChanged);
			// 
			// bGetFilename
			// 
			resources.ApplyResources(this.bGetFilename, "bGetFilename");
			this.bGetFilename.Name = "bGetFilename";
			this.bGetFilename.Click += new System.EventHandler(this.bGetFilename_Click);
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// cbDataProvider
			// 
			resources.ApplyResources(this.cbDataProvider, "cbDataProvider");
			this.cbDataProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDataProvider.Name = "cbDataProvider";
			this.cbDataProvider.SelectedIndexChanged += new System.EventHandler(this.cbDataProvider_SelectedIndexChanged);
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// tbConnection
			// 
			resources.ApplyResources(this.tbConnection, "tbConnection");
			this.tbConnection.Name = "tbConnection";
			this.tbConnection.TextChanged += new System.EventHandler(this.validate_TextChanged);
			// 
			// ckbIntSecurity
			// 
			resources.ApplyResources(this.ckbIntSecurity, "ckbIntSecurity");
			this.ckbIntSecurity.Name = "ckbIntSecurity";
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// tbPrompt
			// 
			resources.ApplyResources(this.tbPrompt, "tbPrompt");
			this.tbPrompt.Name = "tbPrompt";
			// 
			// bOK
			// 
			resources.ApplyResources(this.bOK, "bOK");
			this.bOK.Name = "bOK";
			this.bOK.Click += new System.EventHandler(this.bOK_Click);
			// 
			// bCancel
			// 
			resources.ApplyResources(this.bCancel, "bCancel");
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Name = "bCancel";
			// 
			// tbPassword2
			// 
			resources.ApplyResources(this.tbPassword2, "tbPassword2");
			this.tbPassword2.Name = "tbPassword2";
			this.tbPassword2.TextChanged += new System.EventHandler(this.validate_TextChanged);
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// bTestConnection
			// 
			resources.ApplyResources(this.bTestConnection, "bTestConnection");
			this.bTestConnection.Name = "bTestConnection";
			this.bTestConnection.Click += new System.EventHandler(this.bTestConnection_Click);
			// 
			// cbOdbcNames
			// 
			resources.ApplyResources(this.cbOdbcNames, "cbOdbcNames");
			this.cbOdbcNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbOdbcNames.Name = "cbOdbcNames";
			this.cbOdbcNames.Sorted = true;
			this.cbOdbcNames.SelectedIndexChanged += new System.EventHandler(this.cbOdbcNames_SelectedIndexChanged);
			// 
			// lODBC
			// 
			resources.ApplyResources(this.lODBC, "lODBC");
			this.lODBC.Name = "lODBC";
			// 
			// DialogDataSourceRef
			// 
			this.AcceptButton = this.bOK;
			resources.ApplyResources(this, "$this");
			this.CancelButton = this.bCancel;
			this.Controls.Add(this.cbOdbcNames);
			this.Controls.Add(this.lODBC);
			this.Controls.Add(this.bTestConnection);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.tbPassword2);
			this.Controls.Add(this.bCancel);
			this.Controls.Add(this.bOK);
			this.Controls.Add(this.tbPrompt);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.ckbIntSecurity);
			this.Controls.Add(this.tbConnection);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cbDataProvider);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.bGetFilename);
			this.Controls.Add(this.tbFilename);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbPassword);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DialogDataSourceRef";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
	}
}
