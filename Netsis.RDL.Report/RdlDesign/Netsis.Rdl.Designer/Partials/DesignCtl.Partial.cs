using fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers;
using fyiReporting.RdlDesign.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace fyiReporting.RdlDesign
{
    public partial class DesignCtl
    {        

        #region [_PRIVATES_]        

        
        #endregion

        #region [_PROTECTEDS_]

        public VScrollBar GetVerticalScroll()
        {
            return this._vScroll;
        }

        public HScrollBar GetHorizontalScroll()
        {
            return this._hScroll;
        }

        public void InsertNewNReportItem(object sender, string text)
        {            
            menuInsertReportItem(sender, new EventArgs(), text);
        }

        public void SetMousePosition(Point position)
        {
            _MousePosition = position;
        }

        public Point GetMousePosition()
        {
            return _MousePosition;
        }      

        #endregion

    }
}
