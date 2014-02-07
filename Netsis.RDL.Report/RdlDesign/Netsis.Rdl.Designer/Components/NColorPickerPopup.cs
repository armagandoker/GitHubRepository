using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer.Components
{
    public partial class NColorPickerPopup : System.Windows.Forms.Form
    {

        int _rowCount = 0;
        NColorPicker _ColorPicker = null;
        List<Color> _ColorList = null;

        public NColorPickerPopup(NColorPicker cp)            
        {
            InitializeComponent();
            this._ColorPicker = cp;
            LoadColorList();
            this.Width = NConstants.COLOR_ITEM_WIDTH * NConstants.COLOR_COLUMN_COUNT;
            this._rowCount = (int)(Math.Floor((double)(_ColorList.Count / NConstants.COLOR_COLUMN_COUNT))) + 1;
            this.Height = this._rowCount * NConstants.COLOR_ITEM_HEIGHT;            
        }

        private void LoadColorList()
        {
            _ColorList = new List<Color>();
            Color clr = Color.Empty;
            foreach (string c in StaticLists.ColorListColorSort)
            {
                clr = DesignerUtility.ColorFromHtml(c, Color.Empty);
                if (!clr.IsEmpty)
                    _ColorList.Add(clr);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;            
            int row = 0;
            int col = 0;                     
            int col_width = NConstants.COLOR_ITEM_WIDTH;

            foreach (Color c in _ColorList)
            {               
                Rectangle rc = new Rectangle((row * col_width) + NConstants.RECTCOLOR_LEFT, (col * NConstants.COLOR_ITEM_HEIGHT) + NConstants.RECTCOLOR_TOP, 
                        NConstants.RECTCOLOR_WIDTH, NConstants.RECTCOLOR_WIDTH);
               
                g.FillRectangle(new SolidBrush(c), rc);
                row++;
                if (row >= this._rowCount)
                {
                    row = 0;
                    col++;
                }
            }
            this.Focus();
        }

        private void ColorPickerPopup_MouseDown(object sender, MouseEventArgs e)
        {
            int row = e.Location.X / NConstants.COLOR_ITEM_WIDTH;
            int col = e.Location.Y / NConstants.COLOR_ITEM_HEIGHT;

            int item = col * this._rowCount + row;

            if (item >= _ColorList.Count)
                return;

            _ColorPicker.ColorText = _ColorList[item].Name;
            _ColorPicker.SelectionOccured();
            this.Hide();
        }

        private void ColorPickerPopup_MouseMove(object sender, MouseEventArgs e)
        {
            string status;
            //if (e.Location.Y > this.Height - lStatus.Height)    // past bottom of rectangle
            if (e.Location.Y > this.Height)    // past bottom of rectangle
                status = "";
            else
            {                                                   // calc position in box
                int row = e.Location.X / NConstants.RECTCOLOR_WIDTH; ;
                int col = e.Location.Y / NConstants.COLOR_ITEM_HEIGHT;

                int item = col * this._rowCount + row;

                status = item >= _ColorList.Count ? "" : _ColorList[item].Name;
            }
            //lStatus.Text = status;
        }
       
        private void ColorPickerPopup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)System.Windows.Forms.Keys.Escape)
            {
                Hide();
            }
        }       

    }
}
