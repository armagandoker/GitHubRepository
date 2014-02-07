using System;
using System.Collections.Generic;
using System.Text;
using fyiReporting.RDL;
using System.Drawing;
using System.ComponentModel;
using System.Xml;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using fyiReporting.CRI;

namespace Netsis.Rdl.CustomComponents
{
    public class ReportRadialGauge : ICustomReportItem
    {
        static public readonly float OptimalHeight = 40.91f;          // Optimal height at magnification 1    
        static public readonly float OptimalWidth = 40.91f;            // Optimal width at mag 1
        
        int _maxValue = 120;
        int _currentValue = 20;
        int _incrementValue = 10;
        Color _centerColor = Color.Black;
        Color _outerColor = Color.LightBlue;
        int _outherMargine = 3;
        
        int _borderSize = 10;
        Color _borderCenterColor = Color.Gray;
        Color _borderOuterColor = Color.Black;
       
        int _lineIndicatorWidth = 12;
        Color _lineIndicatorColor = Color.YellowGreen;
        int _lineIndicatorMargine = 60;

        int _centerEllipseSize = 30;
        Color _centerEllipseCenterColor = Color.Black;
        Color _centerEllipseOuterColor = Color.Gray;

        int _fontSize = 10;        
        FontStyle _fontStyle = FontStyle.Regular;
        Color _fontColor = Color.Black;
        int _fontMargine = 50;

        int _startAngle = 230;
        int _endAngle = 310;
        Color _tickLineColor = Color.Black;
        int _tickLineWidth = 3;
        int _tickMargine = 20;
        int _tickHeight = 10;        

        #region [_PRIVATES_]

        private void PaintLines()
        {

        }

        private void DrawEllipseBorder(ref Bitmap bm, ref Graphics gBmp)
        {
                        
            double tmp = ((double)bm.Width / 2.0f) - (double)_outherMargine;
            double fCenter = (double)_borderSize / tmp;

            GraphicsPath path1 = new GraphicsPath();
            path1.AddEllipse(_outherMargine, _outherMargine, bm.Width - _outherMargine, bm.Height - _outherMargine);
            PathGradientBrush pthGrBrush1 = new PathGradientBrush(path1);
            Color[] colors = { this._borderOuterColor, this._borderCenterColor, this._borderOuterColor, Color.Transparent };
            float[] relativePositions = { 0f, (float)fCenter / 2.0f, (float)fCenter, 1.0f };

            ColorBlend colorBlend = new ColorBlend();
            colorBlend.Colors = colors;
            colorBlend.Positions = relativePositions;
            pthGrBrush1.InterpolationColors = colorBlend;
            gBmp.FillEllipse(pthGrBrush1, _outherMargine, _outherMargine, bm.Width - _outherMargine, bm.Height - _outherMargine);
            
        }

        private void DrawEllipse(ref Bitmap bm, ref Graphics gBmp)
        {            
            GraphicsPath path = new GraphicsPath();
            int _globalMargine = this._borderSize + _outherMargine;
            path.AddEllipse(_globalMargine, _globalMargine, bm.Width - (2 * _globalMargine), bm.Height - (2 * _globalMargine));
            PathGradientBrush pthGrBrush = new PathGradientBrush(path);
            pthGrBrush.CenterColor = this._centerColor;
            Color[] colors = { this._outerColor };
            pthGrBrush.SurroundColors = colors;
            gBmp.FillEllipse(pthGrBrush, _globalMargine, _globalMargine, bm.Width - (2 * _globalMargine), bm.Height - (2 * _globalMargine));
        }

        private void DrawCenter(ref Bitmap bm, ref Graphics gBmp)
        {
            int sX = (int)(((double)bm.Width / 2) - (_centerEllipseSize / 2));
            int sY = (int)(((double)bm.Height / 2) - (_centerEllipseSize / 2));           

            GraphicsPath path1 = new GraphicsPath();
            path1.AddEllipse(sX, sY, _centerEllipseSize, _centerEllipseSize);
            PathGradientBrush pthGrBrush1 = new PathGradientBrush(path1);
            Color[] colors = { _centerEllipseOuterColor, _centerEllipseCenterColor, _centerEllipseOuterColor };
            float[] relativePositions = { 0f, 0.4f, 1.0f};

            ColorBlend colorBlend = new ColorBlend();
            colorBlend.Colors = colors;
            colorBlend.Positions = relativePositions;
            pthGrBrush1.InterpolationColors = colorBlend;
            gBmp.FillEllipse(pthGrBrush1, sX, sY, _centerEllipseSize, _centerEllipseSize);
            
        }

        private void DrawDashBoardLines(ref Bitmap bm, ref Graphics gBmp)
        {
            int xAxis = (int)((bm.Width / 2) - this._tickMargine);
            int yAxis = (int)((bm.Height / 2) - this._tickMargine);

            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;

            int x3 = 0;
            int y3 = 0;
            double degree = 0;

            double unitDegree = (double)(360 - (this._endAngle - this._startAngle)) / (double)this._maxValue;

            double radian = 0;
            Font fn = new Font(new FontFamily("Arial"), this._fontSize, this._fontStyle, GraphicsUnit.Pixel); 
            string str = String.Empty;

            for (int i = 0; i <= this._maxValue; i += this._incrementValue)
            {
                degree = (double)this._startAngle - (i * unitDegree);
                radian = Math.PI * degree / 180.0;                
                x1 = (int)(Math.Cos(radian) * xAxis);
                y1 = (int)(Math.Sin(radian) * yAxis);

                x2 = (int)(Math.Cos(radian) * (xAxis - this._lineIndicatorWidth));
                y2 = (int)(Math.Sin(radian) * (yAxis - this._lineIndicatorWidth));

                str = i.ToString();
                SizeF strSize = gBmp.MeasureString(str, fn);

                x3 = (int)((Math.Cos(radian) * (xAxis - (_fontMargine - this._tickMargine))));
                y3 = (int)((Math.Sin(radian) * (yAxis - (_fontMargine - this._tickMargine))));

                gBmp.DrawLine(new Pen(this._tickLineColor, this._tickLineWidth), (bm.Width / 2) + x1, (bm.Height / 2) - y1, (bm.Width / 2) + x2, (bm.Height / 2) - y2);
                gBmp.DrawString(str, fn, new SolidBrush(_fontColor), new PointF((bm.Width / 2) + x3 - (strSize.Width / 2.0f), (bm.Height / 2) - y3 - (strSize.Height / 2.0f)));     
                
            }

            degree = (double)this._startAngle - (this._currentValue * unitDegree);
            radian = Math.PI * degree / 180.0;
            x1 = (int)(Math.Cos(radian) * (bm.Width / 2 - _lineIndicatorMargine));
            y1 = (int)(Math.Sin(radian) * (bm.Height / 2 -_lineIndicatorMargine));

            Pen pn = new Pen(_lineIndicatorColor, _lineIndicatorWidth);
            pn.EndCap = LineCap.Triangle;
            gBmp.DrawLine(pn, (bm.Width / 2), (bm.Height / 2), (bm.Width / 2) + x1 , (bm.Height / 2) - y1);
        }

        #endregion

        #region ICustomReportItem Members

        public bool IsDataRegion()
        {
            return true;
        }

        public void DrawImage(ref Bitmap bm)
        {                                    
            Graphics gBmp = null;
            bm = new Bitmap(bm.Width, bm.Height, System.Drawing.Imaging.PixelFormat.Format64bppPArgb);
            bm.SetResolution(1600, 1200);            
            gBmp = Graphics.FromImage(bm);
            gBmp.InterpolationMode = InterpolationMode.High;
            gBmp.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gBmp.SmoothingMode = SmoothingMode.AntiAlias;
            gBmp.CompositingQuality = CompositingQuality.HighQuality;
            gBmp.PageUnit = GraphicsUnit.Pixel;
                              
            gBmp.FillRectangle(Brushes.White, 0, 0, bm.Width, bm.Height);

            DrawEllipseBorder(ref bm, ref gBmp);           
            DrawEllipse(ref bm, ref gBmp);            
            DrawDashBoardLines(ref bm, ref gBmp);
            DrawCenter(ref bm, ref gBmp);
                             
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
                _maxValue = Int32.Parse(props["MaxValue"].ToString());               
                _currentValue = Int32.Parse(props["CurrentValue"].ToString());
                _incrementValue = Int32.Parse(props["IncrementValue"].ToString());
                _centerColor = Color.FromName(props["CenterColor"].ToString());
                _outerColor = Color.FromName(props["OuterColor"].ToString());
                _lineIndicatorWidth = Int32.Parse(props["LineIndicatorWidth"].ToString());
                _lineIndicatorColor = Color.FromName(props["LineIndicatorColor"].ToString());
                _lineIndicatorMargine = Int32.Parse(props["LineIndicatorMargine"].ToString());
                _centerEllipseSize = Int32.Parse(props["CenterEllipseSize"].ToString());
                _centerEllipseCenterColor = Color.FromName(props["CenterEllipseCenterColor"].ToString());
                _centerEllipseOuterColor = Color.FromName(props["CenterEllipseOuterColor"].ToString());
                _fontSize = Int32.Parse(props["FontSize"].ToString());
                _fontStyle = (FontStyle)Int32.Parse(props["FontStyle"].ToString());
                _fontColor = Color.FromName(props["FontColor"].ToString());
                _startAngle = Int32.Parse(props["StartAngle"].ToString());
                _endAngle = Int32.Parse(props["EndAngle"].ToString());
                _tickLineColor = Color.FromName(props["TickLineColor"].ToString());
                _tickLineWidth = Int32.Parse(props["TickLineWidth"].ToString());
                _fontMargine = Int32.Parse(props["FontMargine"].ToString());
                _tickMargine = Int32.Parse(props["TickMargine"].ToString());
                _tickHeight = Int32.Parse(props["TickHeight"].ToString());
                _borderCenterColor = Color.FromName(props["BorderCenterColor"].ToString());
                _borderOuterColor = Color.FromName(props["BorderOuterColor"].ToString());
                _outherMargine = Int32.Parse(props["OutherMargine"].ToString());
                _borderSize = Int32.Parse(props["BorderSize"].ToString()); 
  
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception("Property must be specified : " + ex.Message);
            }
        }

        public object GetPropertiesInstance(XmlNode iNode)
        {
            ReportRadialGaugeProperties bcp = new ReportRadialGaugeProperties(this, iNode);
            foreach (XmlNode n in iNode.ChildNodes)
            {
                if (n.Name != "CustomProperty")
                    continue;
                string pname = XmlHelpers.GetNamedElementValue(n, "Name", "");
                switch (pname)
                {
                    case "MaxValue":
                        bcp.SetMaxValue(XmlHelpers.GetNamedElementValue(n, "Value", ""));
                        break;                   
                    case "CurrentValue":
                        bcp.SetCurrentValue(XmlHelpers.GetNamedElementValue(n, "Value", ""));
                        break;
                    case "IncrementValue":
                        bcp.SetIncrementValue(XmlHelpers.GetNamedElementValue(n, "Value", ""));
                        break;
                    case "CenterColor":
                        bcp.SetCenterColor(Color.FromName(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "OuterColor":
                        bcp.SetOuterColor(Color.FromName(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "LineIndicatorWidth":
                        bcp.SetLineIndicatorWidth(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "LineIndicatorColor":
                        bcp.SetLineIndicatorColor(Color.FromName(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "LineIndicatorMargine":
                        bcp.SetLineIndicatorMargine(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "CenterEllipseSize":
                        bcp.SetCenterEllipseSize(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "CenterEllipseCenterColor":
                        bcp.SetCenterEllipseCenterColor(Color.FromName(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "CenterEllipseOuterColor":
                        bcp.SetCenterEllipseOuterColor(Color.FromName(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "FontSize":
                        bcp.SetFontSize(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "FontStyle":
                        bcp.SetFontStyle((FontStyle)Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "FontColor":
                        bcp.SetFontColor(Color.FromName(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "StartAngle":
                        bcp.SetStartAngle(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "EndAngle":
                        bcp.SetEndAngle(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "TickLineColor":
                        bcp.SetTickLineColor(Color.FromName(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "TickLineWidth":
                        bcp.SetTickLineWidth(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "FontMargine":
                        bcp.SetFontMargine(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "TickMargine":
                        bcp.SetTickMargine(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "TickHeight":
                        bcp.SetTickHeight(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "OutherMargine":
                        bcp.SetOutherMargine(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "BorderSize":
                        bcp.SetBorderSize(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "BorderCenterColor":
                        bcp.SetBorderCenterColor(Color.FromName(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "BorderOuterColor":
                        bcp.SetBorderOuterColor(Color.FromName(XmlHelpers.GetNamedElementValue(n, "Value", "")));
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

            ReportRadialGaugeProperties bcp = inst as ReportRadialGaugeProperties;
            if (bcp == null)
                return;
            
            XmlHelpers.CreateChild(node, "MaxValue", bcp.MaxValue);
            XmlHelpers.CreateChild(node, "CurrentValue", bcp.CurrentValue);
            XmlHelpers.CreateChild(node, "IncrementValue", bcp.IncrementValue);
            XmlHelpers.CreateChild(node, "CenterColor", bcp.CenterColor.Name);
            XmlHelpers.CreateChild(node, "OuterColor", bcp.OuterColor.Name);
            XmlHelpers.CreateChild(node, "LineIndicatorWidth", bcp.LineIndicatorWidth.ToString());
            XmlHelpers.CreateChild(node, "LineIndicatorColor", bcp.LineIndicatorColor.Name);
            XmlHelpers.CreateChild(node, "LineIndicatorMargine", bcp.LineIndicatorMargine.ToString());
            XmlHelpers.CreateChild(node, "CenterEllipseSize", bcp.CenterEllipseSize.ToString());
            XmlHelpers.CreateChild(node, "CenterEllipseCenterColor", bcp.CenterEllipseCenterColor.Name);
            XmlHelpers.CreateChild(node, "CenterEllipseOuterColor", bcp.CenterEllipseOuterColor.Name);
            XmlHelpers.CreateChild(node, "FontSize", bcp.FontSize.ToString());
            XmlHelpers.CreateChild(node, "FontStyle", ((int)bcp.FontStyle).ToString());
            XmlHelpers.CreateChild(node, "FontColor", bcp.FontColor.Name);
            XmlHelpers.CreateChild(node, "StartAngle", bcp.StartAngle.ToString());
            XmlHelpers.CreateChild(node, "EndAngle", bcp.EndAngle.ToString());
            XmlHelpers.CreateChild(node, "TickLineColor", bcp.TickLineColor.Name);
            XmlHelpers.CreateChild(node, "TickLineWidth", bcp.TickLineWidth.ToString());
            XmlHelpers.CreateChild(node, "FontMargine", bcp.FontMargine.ToString());
            XmlHelpers.CreateChild(node, "TickMargine", bcp.TickMargine.ToString());
            XmlHelpers.CreateChild(node, "TickHeight", bcp.TickHeight.ToString());
            XmlHelpers.CreateChild(node, "OutherMargine", bcp.OutherMargine.ToString());
            XmlHelpers.CreateChild(node, "BorderSize", bcp.BorderSize.ToString());
            XmlHelpers.CreateChild(node, "BorderCenterColor", bcp.BorderCenterColor.Name);
            XmlHelpers.CreateChild(node, "BorderOuterColor", bcp.BorderOuterColor.Name);           
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
                "   <Name>MaxValue</Name>" +
                "   <Value>120</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>CurrentValue</Name>" +
                "   <Value>60</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>IncrementValue</Name>" +
                "   <Value>10</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>CenterColor</Name>" +
                "   <Value>Black</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>OuterColor</Name>" +
                "   <Value>LightBlue</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>LineIndicatorWidth</Name>" +
                "   <Value>12</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>LineIndicatorColor</Name>" +
                "   <Value>YellowGreen</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>LineIndicatorMargine</Name>" +
                "   <Value>60</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>CenterEllipseSize</Name>" +
                "   <Value>30</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>CenterEllipseCenterColor</Name>" +
                "   <Value>Black</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>CenterEllipseOuterColor</Name>" +
                "   <Value>Gray</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>FontSize</Name>" +
                "   <Value>10</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>FontStyle</Name>" +
                "   <Value>0</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>FontColor</Name>" +
                "   <Value>Black</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>StartAngle</Name>" +
                "   <Value>230</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>EndAngle</Name>" +
                "   <Value>310</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>TickLineColor</Name>" +
                "   <Value>Black</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>TickLineWidth</Name>" +
                "   <Value>3</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>FontMargine</Name>" +
                "   <Value>50</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>TickMargine</Name>" +
                "   <Value>20</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>TickHeight</Name>" +
                "   <Value>10</Value>" +
                "</CustomProperty>" +
                 "<CustomProperty>" +
                "   <Name>BorderCenterColor</Name>" +
                "   <Value>DarkGray</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>BorderOuterColor</Name>" +
                "   <Value>Black</Value>" +
                "</CustomProperty>" +
                 "<CustomProperty>" +
                "   <Name>OutherMargine</Name>" +
                "   <Value>3</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>BorderSize</Name>" +
                "   <Value>10</Value>" +
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
        public class ReportRadialGaugeProperties
        {
            string _maxValue;
            ReportRadialGauge _bar;
            XmlNode _node;
            string _curentValue;                        
            string _incrementValue;
            Color _centerColor;
            Color _outerColor;

            int _lineIndicatorWidth;
            Color _lineIndicatorColor;
            int _lineIndicatorMargine;

            int _centerEllipseSize;
            Color _centerEllipseCenterColor;
            Color _centerEllipseOuterColor;

            int _fontSize;
            FontStyle _fontStyle;
            Color _fontColor;

            int _startAngle;
            int _endAngle;
            Color _tickLineColor;
            int _tickLineWidth;

            int _fontMargine;
            int _tickMargine;
            int _tickHeight;

            Color _borderCenterColor;
            Color _borderOuterColor;

            int _outherMargine;
            int _borderSize;

            internal ReportRadialGaugeProperties(ReportRadialGauge bar, XmlNode node)
            {
                _bar = bar;
                _node = node;
            }

            internal void SetMaxValue(string value)
            {
                _maxValue = value;
            }

            internal void SetCurrentValue(string value)
            {
                _curentValue = value;
            }

            internal void SetIncrementValue(string value)
            {
                _incrementValue = value;
            }

            internal void SetCenterColor(Color value)
            {
                _centerColor = value;
            }

            internal void SetOuterColor(Color value)
            {
                _outerColor = value;
            }

            internal void SetLineIndicatorWidth(int value)
            {
                _lineIndicatorWidth = value;
            }

            internal void SetLineIndicatorColor(Color value)
            {
                _lineIndicatorColor = value;
            }

            internal void SetLineIndicatorMargine(int value)
            {
                _lineIndicatorMargine = value;
            }

            internal void SetCenterEllipseSize(int value)
            {
                _centerEllipseSize = value;
            }

            internal void SetCenterEllipseCenterColor(Color value)
            {
                _centerEllipseCenterColor = value;
            }

            internal void SetCenterEllipseOuterColor(Color value)
            {
                _centerEllipseOuterColor = value;
            }

            internal void SetFontSize(int value)
            {
                _fontSize = value;
            }

            internal void SetFontStyle(FontStyle value)
            {
                _fontStyle = value;
            }

            internal void SetFontColor(Color value)
            {
                _fontColor = value;
            }

            internal void SetEndAngle(int value)
            {
                _endAngle = value;
            }

            internal void SetStartAngle(int value)
            {
                _startAngle = value;
            }

            internal void SetTickLineColor(Color value)
            {
                _tickLineColor = value;
            }

            internal void SetTickLineWidth(int value)
            {
                _tickLineWidth = value;
            }

            internal void SetFontMargine(int value)
            {
                _fontMargine = value;
            }

            internal void SetTickMargine(int value)
            {
                _tickMargine = value;
            }

            internal void SetTickHeight(int value)
            {
                _tickHeight = value;
            }

            internal void SetBorderCenterColor(Color value)
            {
                _borderCenterColor = value;
            }

            internal void SetBorderOuterColor(Color value)
            {
                _borderOuterColor = value;
            }

            internal void SetOutherMargine(int value)
            {
                _outherMargine = value;
            }

            internal void SetBorderSize(int value)
            {
                _borderSize = value;
            }           

            [Category("Gösterge Değerleri"), Description("Maksimum alabileceği değer.")]
            public string MaxValue
            {
                get { return _maxValue; }
                set { _maxValue = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Gösterge Değerleri"), Description("Değer.")]
            public string CurrentValue
            {
                get { return _curentValue; }
                set { _curentValue = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Gösterge Değerleri"), Description("Artış miktarı")]
            public string IncrementValue
            {
                get { return _incrementValue; }
                set { _incrementValue = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Gösterge Özellikleri"), Description("Daire merkez rengi")]
            public Color CenterColor
            {
                get { return _centerColor; }
                set { _centerColor = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Gösterge Özellikleri"), Description("Daire dış rengi")]
            public Color OuterColor
            {
                get { return _outerColor; }
                set { _outerColor = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Gösterge Özellikleri"), Description("Başlangıç açısı")]
            public int StartAngle
            {
                get { return _startAngle; }
                set { _startAngle = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Gösterge Özellikleri"), Description("Bitiş açısı")]
            public int EndAngle
            {
                get { return _endAngle; }
                set { _endAngle = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Gösterge Özellikleri"), Description("Çizgi rengi")]
            public Color TickLineColor
            {
                get { return _tickLineColor; }
                set { _tickLineColor = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Gösterge Özellikleri"), Description("Çizgi kalınlığı")]
            public int TickLineWidth
            {
                get { return _tickLineWidth; }
                set { _tickLineWidth = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Gösterge Özellikleri"), Description("Çizgi sınırı")]
            public int TickMargine
            {
                get { return _tickMargine; }
                set { _tickMargine = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Gösterge Özellikleri"), Description("Çizgi boyu")]
            public int TickHeight
            {
                get { return _tickHeight; }
                set { _tickHeight = value; _bar.SetPropertiesInstance(_node, this); }
            }
            
            [Category("İbre Özellikleri"), Description("İbre Genişliği")]
            public int LineIndicatorWidth
            {
                get { return _lineIndicatorWidth; }
                set { _lineIndicatorWidth = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("İbre Özellikleri"), Description("İbre Rengi")]
            public Color LineIndicatorColor
            {
                get { return _lineIndicatorColor; }
                set { _lineIndicatorColor = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("İbre Özellikleri"), Description("İbre Sınırı")]
            public int LineIndicatorMargine
            {
                get { return _lineIndicatorMargine; }
                set { _lineIndicatorMargine = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("İbre Özellikleri"), Description("İbre daire boyutu")]
            public int CenterEllipseSize
            {
                get { return _centerEllipseSize; }
                set { _centerEllipseSize = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("İbre Özellikleri"), Description("İbre daire merkez rengi")]
            public Color CenterEllipseCenterColor
            {
                get { return _centerEllipseCenterColor; }
                set { _centerEllipseCenterColor = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("İbre Özellikleri"), Description("İbre daire dış rengi")]
            public Color CenterEllipseOuterColor
            {
                get { return _centerEllipseOuterColor; }
                set { _centerEllipseOuterColor = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Font Özellikleri"), Description("Font boyu")]
            public int FontSize
            {
                get { return _fontSize; }
                set { _fontSize = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Font Özellikleri"), Description("Font stili")]
            public FontStyle FontStyle
            {
                get { return _fontStyle; }
                set { _fontStyle = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Font Özellikleri"), Description("Font Rengi")]
            public Color FontColor
            {
                get { return _fontColor; }
                set { _fontColor = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Font Özellikleri"), Description("Font sınırı")]
            public int FontMargine
            {
                get { return _fontMargine; }
                set { _fontMargine = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Dış Çizgi Özellikleri"), Description("Dış çizgi renk 1")]
            public Color BorderCenterColor
            {
                get { return _borderCenterColor; }
                set { _borderCenterColor = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Dış Çizgi Özellikleri"), Description("Dış çizgi renk 2")]
            public Color BorderOuterColor
            {
                get { return _borderOuterColor; }
                set { _borderOuterColor = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Dış Çizgi Özellikleri"), Description("Dış çizgi boyu")]
            public int BorderSize
            {
                get { return _borderSize; }
                set { _borderSize = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Dış Çizgi Özellikleri"), Description("Dış çizgi sınırı")]
            public int OutherMargine
            {
                get { return _outherMargine; }
                set { _outherMargine = value; _bar.SetPropertiesInstance(_node, this); }
            }           

        }
       
    }
}
