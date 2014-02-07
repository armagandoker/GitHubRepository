namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer.Components
{
    partial class NColorPickerPopup
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NColorPickerPopup
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NColorPickerPopup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ColorPickerPopup_KeyPress);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorPickerPopup_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColorPickerPopup_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
