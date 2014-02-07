using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer.Components
{
    public class NColorToolStriptItem : ToolStripMenuItem
    {
        
        private string _Color = "AliceBlue";
        private Color _BlockColor = Color.Empty;

        public string ColorName
        {
            get
            {
                return _Color;
            }
            set
            {
                _Color = value;                
                _BlockColor = DesignerUtility.ColorFromHtml(value, Color.Empty);
                this.Text = _Color;
            }
        }

        public NColorToolStriptItem()
        {
        }

        public NColorToolStriptItem(string colorName)
        {
            this.ColorName = colorName;
        }

        protected override void OnPaint(PaintEventArgs e)
        {            
            Graphics g = e.Graphics;
            if(_BlockColor != Color.Empty)
                g.FillRectangle(new SolidBrush(_BlockColor), NConstants.RECTCOLOR_LEFT, NConstants.RECTCOLOR_TOP,
                    NConstants.RECTCOLOR_WIDTH, NConstants.RECTCOLOR_WIDTH);
        }

        public static ToolStripDropDown GetDropDownColorItems()
        {
            Color BlockColor = Color.Empty;
            ToolStripDropDown drop_down = new ToolStripDropDown();
            drop_down.LayoutStyle = ToolStripLayoutStyle.Table;
            ((TableLayoutSettings)drop_down.LayoutSettings).ColumnCount = NConstants.COLOR_COLUMN_COUNT; // Or whatever you need 

            foreach (string clr in StaticLists.ColorListColorSort)
            {
                BlockColor = DesignerUtility.ColorFromHtml(clr, Color.Empty);
                if (BlockColor.IsEmpty)
                    continue;
                NColorToolStriptItem itm = new NColorToolStriptItem(clr);
                itm.ToolTipText = clr;
                itm.AutoSize = false;
                itm.Size = new System.Drawing.Size(NConstants.COLOR_ITEM_WIDTH, NConstants.COLOR_ITEM_HEIGHT);
                drop_down.Items.Add(itm);
            }
            return drop_down;
        }

        public Image GetSelectedColorImage()
        {            
            Bitmap b = new Bitmap(16, 16);
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    b.SetPixel(i, j, this._BlockColor);
                }
            }
            return b;
        }

        public static Image GetSelectedColorImage(string colorName)
        {
            Color clr = Color.FromName(colorName);
            if (clr.IsEmpty)
                return null;
            Bitmap b = new Bitmap(16, 16);
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    b.SetPixel(i, j, clr);
                }
            }
            return b;
        }

    }
}
