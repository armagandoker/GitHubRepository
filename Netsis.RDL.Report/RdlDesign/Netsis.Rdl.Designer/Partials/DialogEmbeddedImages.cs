using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign
{

    internal partial class DialogEmbeddedImages
    {

        #region [_CONSTRUCTORS_]

        internal DialogEmbeddedImages(DesignXmlDraw draw, string selectedName) : this(draw)
        {
            if (selectedName != null)
            {
                for(int i = 0; i < lbImages.Items.Count; i++)
                {
                    EmbeddedImageValues itm = (EmbeddedImageValues)lbImages.Items[i];
                    if (itm.Name != selectedName)
                        continue;
                    lbImages.SelectedIndex = i;
                    break;
                }
            }
        }

        #endregion

        #region [_PRIVATES_]

        private void ChangeControlsPropertiesOnLoad()
        {
            this.Text = "Resimler";
            bPaste.Text = "Yapıştır";
            bRemove.Text = "Sil";
            label1.Text = "Ad";
            lDataProvider.Text = "MIME Tipi";
            bImport.Text = "İçe Al";            

            foreach (Control ctl in this.Controls)
            {
                if(!(ctl is PictureBox))
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

    }

}
