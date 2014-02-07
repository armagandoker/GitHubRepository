namespace fyiReporting.RdlDesign
{
    partial class StaticSeriesCtl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaticSeriesCtl));
			this.label1 = new System.Windows.Forms.Label();
			this.lbDataSeries = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.chkShowLabels = new System.Windows.Forms.CheckBox();
			this.txtSeriesName = new System.Windows.Forms.TextBox();
			this.txtLabelValue = new System.Windows.Forms.TextBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnDel = new System.Windows.Forms.Button();
			this.btnLabelValue = new System.Windows.Forms.Button();
			this.btnDataValue = new System.Windows.Forms.Button();
			this.btnSeriesName = new System.Windows.Forms.Button();
			this.txtDataValue = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cbPlotType = new System.Windows.Forms.ComboBox();
			this.chkLeft = new System.Windows.Forms.RadioButton();
			this.chkRight = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.txtX = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btnX = new System.Windows.Forms.Button();
			this.chkMarker = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.cbLine = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.colorPicker1 = new fyiReporting.RdlDesign.ColorPicker();
			this.SuspendLayout();
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// lbDataSeries
			// 
			this.lbDataSeries.FormattingEnabled = true;
			resources.ApplyResources(this.lbDataSeries, "lbDataSeries");
			this.lbDataSeries.Name = "lbDataSeries";
			this.lbDataSeries.SelectedIndexChanged += new System.EventHandler(this.lbDataSeries_SelectedIndexChanged);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// chkShowLabels
			// 
			resources.ApplyResources(this.chkShowLabels, "chkShowLabels");
			this.chkShowLabels.Name = "chkShowLabels";
			this.chkShowLabels.UseVisualStyleBackColor = true;
			this.chkShowLabels.CheckedChanged += new System.EventHandler(this.chkShowLabels_CheckedChanged);
			// 
			// txtSeriesName
			// 
			resources.ApplyResources(this.txtSeriesName, "txtSeriesName");
			this.txtSeriesName.Name = "txtSeriesName";
			this.txtSeriesName.TextChanged += new System.EventHandler(this.txtSeriesName_TextChanged);
			// 
			// txtLabelValue
			// 
			resources.ApplyResources(this.txtLabelValue, "txtLabelValue");
			this.txtLabelValue.Name = "txtLabelValue";
			this.txtLabelValue.TextChanged += new System.EventHandler(this.txtLabelValue_TextChanged);
			// 
			// btnAdd
			// 
			resources.ApplyResources(this.btnAdd, "btnAdd");
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnDel
			// 
			resources.ApplyResources(this.btnDel, "btnDel");
			this.btnDel.Name = "btnDel";
			this.btnDel.UseVisualStyleBackColor = true;
			this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
			// 
			// btnLabelValue
			// 
			resources.ApplyResources(this.btnLabelValue, "btnLabelValue");
			this.btnLabelValue.Name = "btnLabelValue";
			this.btnLabelValue.UseVisualStyleBackColor = true;
			this.btnLabelValue.Click += new System.EventHandler(this.FunctionButtonClick);
			// 
			// btnDataValue
			// 
			resources.ApplyResources(this.btnDataValue, "btnDataValue");
			this.btnDataValue.Name = "btnDataValue";
			this.btnDataValue.UseVisualStyleBackColor = true;
			this.btnDataValue.Click += new System.EventHandler(this.FunctionButtonClick);
			// 
			// btnSeriesName
			// 
			resources.ApplyResources(this.btnSeriesName, "btnSeriesName");
			this.btnSeriesName.Name = "btnSeriesName";
			this.btnSeriesName.UseVisualStyleBackColor = true;
			this.btnSeriesName.Click += new System.EventHandler(this.FunctionButtonClick);
			// 
			// txtDataValue
			// 
			resources.ApplyResources(this.txtDataValue, "txtDataValue");
			this.txtDataValue.Name = "txtDataValue";
			this.txtDataValue.TextChanged += new System.EventHandler(this.txtDataValue_TextChanged);
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// cbPlotType
			// 
			this.cbPlotType.FormattingEnabled = true;
			this.cbPlotType.Items.AddRange(new object[] {
            resources.GetString("cbPlotType.Items"),
            resources.GetString("cbPlotType.Items1")});
			resources.ApplyResources(this.cbPlotType, "cbPlotType");
			this.cbPlotType.Name = "cbPlotType";
			this.cbPlotType.SelectedIndexChanged += new System.EventHandler(this.cbPlotType_SelectedIndexChanged);
			// 
			// chkLeft
			// 
			resources.ApplyResources(this.chkLeft, "chkLeft");
			this.chkLeft.Name = "chkLeft";
			this.chkLeft.TabStop = true;
			this.chkLeft.UseVisualStyleBackColor = true;
			this.chkLeft.CheckedChanged += new System.EventHandler(this.chkLeft_CheckedChanged);
			// 
			// chkRight
			// 
			resources.ApplyResources(this.chkRight, "chkRight");
			this.chkRight.Name = "chkRight";
			this.chkRight.TabStop = true;
			this.chkRight.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// btnUp
			// 
			resources.ApplyResources(this.btnUp, "btnUp");
			this.btnUp.Name = "btnUp";
			this.btnUp.UseVisualStyleBackColor = true;
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnDown
			// 
			resources.ApplyResources(this.btnDown, "btnDown");
			this.btnDown.Name = "btnDown";
			this.btnDown.UseVisualStyleBackColor = true;
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// txtX
			// 
			resources.ApplyResources(this.txtX, "txtX");
			this.txtX.Name = "txtX";
			this.txtX.TextChanged += new System.EventHandler(this.txtX_TextChanged);
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// btnX
			// 
			resources.ApplyResources(this.btnX, "btnX");
			this.btnX.Name = "btnX";
			this.btnX.UseVisualStyleBackColor = true;
			this.btnX.Click += new System.EventHandler(this.FunctionButtonClick);
			// 
			// chkMarker
			// 
			resources.ApplyResources(this.chkMarker, "chkMarker");
			this.chkMarker.Name = "chkMarker";
			this.chkMarker.UseVisualStyleBackColor = true;
			this.chkMarker.CheckedChanged += new System.EventHandler(this.chkMarker_CheckedChanged);
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			// 
			// cbLine
			// 
			this.cbLine.FormattingEnabled = true;
			this.cbLine.Items.AddRange(new object[] {
            resources.GetString("cbLine.Items"),
            resources.GetString("cbLine.Items1"),
            resources.GetString("cbLine.Items2"),
            resources.GetString("cbLine.Items3"),
            resources.GetString("cbLine.Items4")});
			resources.ApplyResources(this.cbLine, "cbLine");
			this.cbLine.Name = "cbLine";
			this.cbLine.SelectedIndexChanged += new System.EventHandler(this.cbLine_SelectedIndexChanged);
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			// 
			// colorPicker1
			// 
			this.colorPicker1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.colorPicker1.DropDownHeight = 1;
			this.colorPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			resources.ApplyResources(this.colorPicker1, "colorPicker1");
			this.colorPicker1.FormattingEnabled = true;
			this.colorPicker1.Name = "colorPicker1";
			this.colorPicker1.SelectedIndexChanged += new System.EventHandler(this.colorPicker1_SelectedIndexChanged);
			// 
			// StaticSeriesCtl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label8);
			this.Controls.Add(this.colorPicker1);
			this.Controls.Add(this.cbLine);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.chkMarker);
			this.Controls.Add(this.btnX);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtX);
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.chkRight);
			this.Controls.Add(this.chkLeft);
			this.Controls.Add(this.cbPlotType);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtDataValue);
			this.Controls.Add(this.btnSeriesName);
			this.Controls.Add(this.btnDataValue);
			this.Controls.Add(this.btnLabelValue);
			this.Controls.Add(this.btnDel);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.txtLabelValue);
			this.Controls.Add(this.txtSeriesName);
			this.Controls.Add(this.chkShowLabels);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lbDataSeries);
			this.Controls.Add(this.label1);
			this.Name = "StaticSeriesCtl";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbDataSeries;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkShowLabels;
        private System.Windows.Forms.TextBox txtSeriesName;
        private System.Windows.Forms.TextBox txtLabelValue;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnLabelValue;
        private System.Windows.Forms.Button btnDataValue;
        private System.Windows.Forms.Button btnSeriesName;
        private System.Windows.Forms.TextBox txtDataValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPlotType;
        private System.Windows.Forms.RadioButton chkLeft;
        private System.Windows.Forms.RadioButton chkRight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnX;
        private System.Windows.Forms.CheckBox chkMarker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbLine;
        private ColorPicker colorPicker1;
        private System.Windows.Forms.Label label8;
    }
}
