using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections;

namespace fyiReporting.RdlDesign
{
    public partial class DialogExprEditor : System.Windows.Forms.Form
	{
		#region Windows Form Designer generated code
		private DesignXmlDraw _Draw;
private SplitContainer splitContainer1;
private Button bCopy;
private Label lOp;
private TextBox tbExpr;
private Label lExpr;
private TreeView tvOp;
private Panel panel1;
private Button bCancel;
private Button bOK;
private System.ComponentModel.Container components = null;

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogExprEditor));
			this.panel1 = new System.Windows.Forms.Panel();
			this.bCancel = new System.Windows.Forms.Button();
			this.bOK = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tvOp = new System.Windows.Forms.TreeView();
			this.bCopy = new System.Windows.Forms.Button();
			this.lOp = new System.Windows.Forms.Label();
			this.tbExpr = new System.Windows.Forms.TextBox();
			this.lExpr = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Controls.Add(this.bCancel);
			this.panel1.Controls.Add(this.bOK);
			this.panel1.Name = "panel1";
			// 
			// bCancel
			// 
			resources.ApplyResources(this.bCancel, "bCancel");
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Name = "bCancel";
			// 
			// bOK
			// 
			resources.ApplyResources(this.bOK, "bOK");
			this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bOK.Name = "bOK";
			// 
			// splitContainer1
			// 
			resources.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
			this.splitContainer1.Panel1.Controls.Add(this.tvOp);
			this.splitContainer1.Panel1.Controls.Add(this.bCopy);
			this.splitContainer1.Panel1.Controls.Add(this.lOp);
			// 
			// splitContainer1.Panel2
			// 
			resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
			this.splitContainer1.Panel2.Controls.Add(this.tbExpr);
			this.splitContainer1.Panel2.Controls.Add(this.lExpr);
			// 
			// tvOp
			// 
			resources.ApplyResources(this.tvOp, "tvOp");
			this.tvOp.Name = "tvOp";
			// 
			// bCopy
			// 
			resources.ApplyResources(this.bCopy, "bCopy");
			this.bCopy.Name = "bCopy";
			this.bCopy.Click += new System.EventHandler(this.bCopy_Click);
			// 
			// lOp
			// 
			resources.ApplyResources(this.lOp, "lOp");
			this.lOp.Name = "lOp";
			// 
			// tbExpr
			// 
			this.tbExpr.AcceptsReturn = true;
			this.tbExpr.AcceptsTab = true;
			resources.ApplyResources(this.tbExpr, "tbExpr");
			this.tbExpr.Name = "tbExpr";
			// 
			// lExpr
			// 
			resources.ApplyResources(this.lExpr, "lExpr");
			this.lExpr.Name = "lExpr";
			// 
			// DialogExprEditor
			// 
			resources.ApplyResources(this, "$this");
			this.CancelButton = this.bCancel;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DialogExprEditor";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

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
