using System;
using System.Collections.Generic;
using System.Text;
using fyiReporting.RDL;
using System.Drawing;
using System.ComponentModel;
using System.Xml;
using System.Drawing.Drawing2D;
using fyiReporting.CRI;

namespace Netsis.Rdl.CustomComponents
{
    public class ReportDirectionIndicator : ICustomReportItem
    {
        static public readonly float OptimalHeight = 10.91f;          // Optimal height at magnification 1    
        static public readonly float OptimalWidth = 40.91f;            // Optimal width at mag 1
        string _degree = "0";
        int _width = 15;
        int _height = 60;
        private Color _startColor = Color.AliceBlue;
        private Color _endColor = Color.Blue;        

        #region ICustomReportItem Members

        public bool IsDataRegion()
        {
            return true;
        }

        public void DrawImage(ref Bitmap bm)
        {
            int currentValue = Int32.Parse(this._degree);

            Graphics gBmp = null;
            bm = new Bitmap(bm.Width, bm.Height);            
            gBmp = Graphics.FromImage(bm);
            gBmp.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gBmp.SmoothingMode = SmoothingMode.AntiAlias;
            gBmp.FillRectangle(Brushes.White, 0, 0, bm.Width, bm.Height);

            LinearGradientBrush brBG = new LinearGradientBrush(new Rectangle(0, 0, bm.Width, bm.Height), this._startColor, this._endColor, LinearGradientMode.Horizontal);
            
            Pen p = new Pen(brBG, this._width);            
            p.EndCap = LineCap.ArrowAnchor;
            p.DashStyle = DashStyle.Solid;
            p.DashCap = DashCap.Triangle;

            int arrowWdt = 20;
            
            int sX = 0;
            int sY = 0;
            switch (this._degree)
            {
                case "0":
                case "360":
                    sX = (int)((double)(bm.Width - this._height) / 2);
                    sY = (int)((double)(bm.Height - (this._width - arrowWdt)) / 2);
                    gBmp.DrawLine(p, sX, sY, sX + this._height, sY);
                    break;
                case "270":
                    sX = (int)((double)(bm.Width - (this._width - arrowWdt)) / 2);
                    sY = (int)((double)(bm.Height - this._height) / 2);
                    gBmp.DrawLine(p, sX, sY, sX, sY + this._height);
                    break;
                case "180":
                    sX = (int)((double)(bm.Width - this._height) / 2);
                    sY = (int)((double)(bm.Height - (this._width - arrowWdt)) / 2);
                    gBmp.DrawLine(p, bm.Width - sX, sY, sX , sY);
                    break;                
                default:
                    sX = (int)((double)(bm.Width - (this._width - arrowWdt)) / 2);
                    sY = (int)((double)(bm.Height - this._height) / 2);
                    gBmp.DrawLine(p, sX, bm.Height - sY, sX, sY);
                    break;
            }                       
        }

        /// <summary>
        /// Design time: Draw a hard coded BarCode for design time;  Parameters can't be
        /// relied on since they aren't available.
        /// </summary>
        /// <param name="bm"></param>
        public void DrawDesignerImage(ref System.Drawing.Bitmap bm)
        {
            DrawImage(ref bm);
        }        

        public void SetProperties(IDictionary<string, object> props)
        {
            try
            {
                _degree = props["Degree"].ToString();               
                _startColor = Color.FromName(props["StartColor"].ToString());
                _endColor = Color.FromName(props["EndColor"].ToString());
                _height = Int32.Parse(props["Height"].ToString());
                _width = Int32.Parse(props["Width"].ToString()); 
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception("Property must be specified : " + ex.Message);
            }
        }

        public object GetPropertiesInstance(XmlNode iNode)
        {
            ReportDirectionIndicatorProperties bcp = new ReportDirectionIndicatorProperties(this, iNode);
            foreach (XmlNode n in iNode.ChildNodes)
            {
                if (n.Name != "CustomProperty")
                    continue;
                string pname = XmlHelpers.GetNamedElementValue(n, "Name", "");
                switch (pname)
                {
                    case "Degree":
                        bcp.SetDegree(XmlHelpers.GetNamedElementValue(n, "Value", ""));
                        break;                   
                    case "StartColor":
                        bcp.SetStartColor(Color.FromName(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "EndColor":
                        bcp.SetEndColor(Color.FromName(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "Height":
                        bcp.SetHeight(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "Width":
                        bcp.SetWidth(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break; 
                    default:
                        break;
                }
            }

            return bcp;
        }

        public void SetPropertiesInstance(XmlNode node, object inst)
        {
            node.RemoveAll();       // Get rid of all properties

            ReportDirectionIndicatorProperties bcp = inst as ReportDirectionIndicatorProperties;
            if (bcp == null)
                return;


            this._degree = bcp.Degree;
            XmlHelpers.CreateChild(node, "Degree", bcp.Degree.ToString());
            XmlHelpers.CreateChild(node, "EndColor", bcp.EndColor.Name);
            XmlHelpers.CreateChild(node, "StartColor", bcp.StartColor.Name);
            XmlHelpers.CreateChild(node, "Height", bcp.Height.ToString());
            XmlHelpers.CreateChild(node, "Width", bcp.Width.ToString());            
        }


        /// <summary>
        /// Design time call: return string with <CustomReportItem> ... </CustomReportItem> syntax for 
        /// the insert.  The string contains a variable {0} which will be substituted with the
        /// configuration name.  This allows the name to be completely controlled by
        /// the configuration file.
        /// </summary>
        /// <returns></returns>
        public string GetCustomReportItemXml()
        {
            return "<CustomReportItem><Type>{0}</Type>" +
                string.Format("<Height>{0}mm</Height><Width>{1}mm</Width>", OptimalHeight, OptimalWidth) +
                "<CustomProperties>" +
                "<CustomProperty>" +
                "   <Name>Degree</Name>" +
                "   <Value>180</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>Height</Name>" +
                "   <Value>60</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>Width</Name>" +
                "   <Value>15</Value>" +
                "</CustomProperty>" +   
                "<CustomProperty>" +
                "   <Name>StartColor</Name>" +
                "   <Value>AliceBlue</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>EndColor</Name>" +
                "   <Value>Blue</Value>" +
                "</CustomProperty>" +               
               "</CustomProperties>" +
                "</CustomReportItem>";
        }        

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            return;
        }

        #endregion


        /// <summary>
        /// BarCodeProperties- All properties are type string to allow for definition of
        /// a runtime expression.
        /// </summary>
        public class ReportDirectionIndicatorProperties
        {
            string _degree;                        
            ReportDirectionIndicator _bar;
            XmlNode _node;
            Color _startColor;
            Color _endColor;
            int _width;
            int _height;

            internal ReportDirectionIndicatorProperties(ReportDirectionIndicator bar, XmlNode node)
            {
                _bar = bar;
                _node = node;
            }

            internal void SetDegree(string value)
            {
                _degree = value;
            }

            internal void SetWidth(int value)
            {
                _width = value;
            }

            internal void SetHeight(int value)
            {
                _height = value;
            } 

            internal void SetStartColor(Color color)
            {
                _startColor = color;
            }

            internal void SetEndColor(Color color)
            {
                _endColor = color;
            }           

            [Category("Değerler"),Description("Ok açısı.")]
            public string Degree
            {
                get { return _degree; }
                set { _degree = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Değerler"), Description("Ok genişiliği.")]
            public int Width
            {
                get { return _width; }
                set { _width = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Değerler"), Description("Ok Uzunluğu.")]
            public int Height
            {
                get { return _height; }
                set { _height = value; _bar.SetPropertiesInstance(_node, this); }
            }  

            [Category("Renkler"),Description("Ok başlangıç rengi.")]
            public Color StartColor
            {
                get { return _startColor; }
                set { _startColor = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Renkler"), Description("Ok bitiş rengi.")]
            public Color EndColor
            {
                get { return _endColor; }
                set { _endColor = value; _bar.SetPropertiesInstance(_node, this); }
            }


        }
       
    }
}
