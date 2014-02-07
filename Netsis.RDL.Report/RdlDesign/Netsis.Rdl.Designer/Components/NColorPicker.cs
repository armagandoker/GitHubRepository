using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer.Components
{
    public class NColorPicker : ToolStripComboBox
    {
       
        NColorPickerPopup _DropListBox;

        string _colorText = string.Empty;
        public string ColorText
        {
            get
            {
                return _colorText;
            }
            set
            {               
                _colorText = value;
                Invalidate();
            }
        }

        public NColorPicker()
        {                       
            this.ComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            DropDownStyle = ComboBoxStyle.DropDownList; // DropDownList
            DropDownHeight = 1;            
            Font = new Font("Arial", 8, FontStyle.Bold | FontStyle.Italic);

            _DropListBox = new NColorPickerPopup(this);

			if (!DesignMode)
			{
				Items.AddRange(StaticLists.ColorList);
			}            
            this.ComboBox.DrawItem += new DrawItemEventHandler(OnDrawItem);
            _DropListBox.Location = this.ComboBox.PointToScreen(new Point(0, this.Height));

        }

        internal void OpenDropList()
        {
            _DropListBox.Location = this.ComboBox.PointToScreen(new Point(0, this.Height));
            _DropListBox.Show();
            _DropListBox.Activate();
        }

        protected override void OnDropDown(EventArgs e)
        {            
            OpenDropList();
        }

        public void SelectionOccured()
        {
            this.OnSelectedIndexChanged(new EventArgs());
        }

        protected void OnDrawItem(object sender , DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Color BlockColor = DesignerUtility.ColorFromHtml(this._colorText, Color.Empty);            
            g.FillRectangle(new SolidBrush(BlockColor), NConstants.RECTCOLOR_LEFT, NConstants.RECTCOLOR_TOP, NConstants.RECTCOLOR_WIDTH,NConstants.COLOR_ITEM_HEIGHT);

            /*if (BlockColor != Color.Empty)
                g.DrawString(this._colorText, new Font(new FontFamily("Arial"), 10), new SolidBrush(Color.Black), new PointF(NConstants.RECTCOLOR_WIDTH + 5, 2));*/                 
        }      

    }
}
