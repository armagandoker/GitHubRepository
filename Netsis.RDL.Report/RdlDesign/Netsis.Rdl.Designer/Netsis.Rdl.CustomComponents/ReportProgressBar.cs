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
    public class ReportProgressBar : ICustomReportItem
    {
        static public readonly float OptimalHeight = 10.91f;          // Optimal height at magnification 1    
        static public readonly float OptimalWidth = 40.91f;            // Optimal width at mag 1
        private string _value = "50";
        private string _maxValue = "100";
        private string _labelText = "Label";
        private Color _startColor = Color.AliceBlue;
        private Color _endColor = Color.Blue;
        ProgressBarLabelValueType _labelValueDisplayType = ProgressBarLabelValueType.LabelAndValue;

        #region ICustomReportItem Members

        public bool IsDataRegion()
        {
            return true;
        }

        public void DrawImage(ref Bitmap bm)
        {
            DrawImage(ref bm, _value);
        }

        /// <summary>
        /// Design time: Draw a hard coded BarCode for design time;  Parameters can't be
        /// relied on since they aren't available.
        /// </summary>
        /// <param name="bm"></param>
        public void DrawDesignerImage(ref System.Drawing.Bitmap bm)
        {
            DrawImage(ref bm, _value);
        }

        public void DrawImage(ref Bitmap bm, string value)
        {
            double currentValue = Double.Parse(this._value);
            double maxValue = Double.Parse(this._maxValue);

            Graphics gBmp = null;                      
            bm = new Bitmap(bm.Width, bm.Height);
            gBmp = Graphics.FromImage(bm);

            gBmp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;                        
            gBmp.InterpolationMode = InterpolationMode.High;
            gBmp.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;                                                  
            gBmp.FillRectangle(Brushes.WhiteSmoke, 0, 0, bm.Width , bm.Height);
            
            LinearGradientBrush brBG = new LinearGradientBrush(new Rectangle(0, 0, bm.Width, bm.Height), this._startColor, this._endColor, LinearGradientMode.Horizontal);
            double wd = (currentValue > maxValue ? bm.Width : bm.Width * currentValue / maxValue);
            gBmp.FillRectangle(brBG, 0, 0, (int)wd, bm.Height);

            string str = GetDisplayString(currentValue, maxValue);
            gBmp.DrawString(str, SystemFonts.DefaultFont, Brushes.Black,
               new PointF(bm.Width / 2 - (gBmp.MeasureString(str, SystemFonts.DefaultFont).Width / 2.0F),
               bm.Height / 2 - (gBmp.MeasureString(str, SystemFonts.DefaultFont).Height / 2.0F)));          
            
        }

        public void SetProperties(IDictionary<string, object> props)
        {
            try
            {
                _value = props["Value"].ToString();
                _maxValue = props["MaxValue"].ToString();
                _labelText = props["LabelText"].ToString();
                _startColor = Color.FromName(props["StartColor"].ToString());
                _endColor = Color.FromName(props["EndColor"].ToString());
                _labelValueDisplayType = (ProgressBarLabelValueType)Int32.Parse(props["LabelValueDisplayType"].ToString());
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("Code property must be specified");
            }
        }

        public object GetPropertiesInstance(XmlNode iNode)
        {
            ProgressBarProperties bcp = new ProgressBarProperties(this, iNode);
            foreach (XmlNode n in iNode.ChildNodes)
            {
                if (n.Name != "CustomProperty")
                    continue;
                string pname = XmlHelpers.GetNamedElementValue(n, "Name", "");
                switch (pname)
                {
                    case "Value":
                        bcp.SetValue(XmlHelpers.GetNamedElementValue(n, "Value", ""));
                        break;
                    case "MaxValue":
                        bcp.SetMaxValue(XmlHelpers.GetNamedElementValue(n, "Value", ""));
                        break;
                    case "LabelText":
                        bcp.SetLabelText(XmlHelpers.GetNamedElementValue(n, "Value", ""));
                        break;
                    case "StartColor":
                        bcp.SetStartColor(Color.FromName(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "EndColor":
                        bcp.SetEndColor(Color.FromName(XmlHelpers.GetNamedElementValue(n, "Value", "")));
                        break;
                    case "LabelValueDisplayType":
                        bcp.SetLabelValueDisplayType((ProgressBarLabelValueType)(Int32.Parse(XmlHelpers.GetNamedElementValue(n, "Value", ""))));
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

            ProgressBarProperties bcp = inst as ProgressBarProperties;
            if (bcp == null)
                return;


            this._value = bcp.Value;
            this._labelText = bcp.LabelText;
            XmlHelpers.CreateChild(node, "Value", bcp.Value.ToString());
            XmlHelpers.CreateChild(node, "MaxValue", bcp.MaxValue.ToString());
            XmlHelpers.CreateChild(node, "LabelText", bcp.LabelText.ToString());
            XmlHelpers.CreateChild(node, "StartColor", bcp.StartColor.Name);
            XmlHelpers.CreateChild(node, "EndColor", bcp.EndColor.Name);
            XmlHelpers.CreateChild(node, "LabelValueDisplayType", ((int)bcp.LabelValueDisplayType).ToString());
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
                "   <Name>Value</Name>" +
                "   <Value>50</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>MaxValue</Name>" +
                "   <Value>100</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>LabelText</Name>" +
                "   <Value>Metin</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>StartColor</Name>" +
                "   <Value>AliceBlue</Value>" +
                "</CustomProperty>" +
                "<CustomProperty>" +
                "   <Name>EndColor</Name>" +
                "   <Value>Blue</Value>" +
                "</CustomProperty>" +
                 "<CustomProperty>" +
                "   <Name>LabelValueDisplayType</Name>" +
                "   <Value>3</Value>" +
                "</CustomProperty>" +
               "</CustomProperties>" +
                "</CustomReportItem>";
        }

        private string GetDisplayString(double currentValue, double maxValue)
        {
            double ratio = (currentValue / maxValue) * 100;
            switch (this._labelValueDisplayType)
            {
                case ProgressBarLabelValueType.None:
                    return string.Empty;
                case ProgressBarLabelValueType.Label:
                    return this._labelText;
                case ProgressBarLabelValueType.Value:
                    return string.Format("{0}%", ((int)ratio).ToString());         
                case ProgressBarLabelValueType.LabelAndValue:
                    return string.Format("{0} : {1}%", _labelText, ((int)ratio).ToString());                    
                case ProgressBarLabelValueType.ValueAndLabel:
                    return string.Format("{0}% : {1}", ((int)ratio).ToString(), _labelText);
                default:
                    return string.Empty;
            }
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
        public class ProgressBarProperties
        {
            string _value;
            string _maxValue;
            string _labelText;
            ReportProgressBar _bar;
            XmlNode _node;
            Color _startColor;
            Color _endColor;
            ProgressBarLabelValueType _labelValueDisplayType;

            internal ProgressBarProperties(ReportProgressBar bar, XmlNode node)
            {
                _bar = bar;
                _node = node;
            }

            internal void SetValue(string value)
            {
                _value = value;
            }

            internal void SetMaxValue(string value)
            {
                _maxValue = value;
            }

            internal void SetLabelText(string labelText)
            {
                _labelText = labelText;
            }

            internal void SetStartColor(Color color)
            {
                _startColor = color;
            }

            internal void SetEndColor(Color color)
            {
                _endColor = color;
            }

            internal void SetLabelValueDisplayType(ProgressBarLabelValueType labelValueDisplayType)
            {
                _labelValueDisplayType = labelValueDisplayType;
            }

            [Category("Değerler"),Description("Progress bar değeri.")]
            public string Value
            {
                get { return _value; }
                set { _value = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Değerler"), Description("Progress bar maksimum değeri.")]
            public string MaxValue
            {
                get { return _maxValue; }
                set { _maxValue = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Değerler"), Description("Progress bar metni.")]
            public string LabelText
            {
                get { return _labelText; }
                set { _labelText = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Değerler"), Description("Görünecek yazı tipi.")]
            public ProgressBarLabelValueType LabelValueDisplayType
            {
                get { return _labelValueDisplayType; }
                set { _labelValueDisplayType = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Renkler"),Description("Progress Bar başlangıç rengi.")]
            public Color StartColor
            {
                get { return _startColor; }
                set { _startColor = value; _bar.SetPropertiesInstance(_node, this); }
            }

            [Category("Renkler"), Description("Progress bar bitiş rengi.")]
            public Color EndColor
            {
                get { return _endColor; }
                set { _endColor = value; _bar.SetPropertiesInstance(_node, this); }
            }


        }

        public enum ProgressBarLabelValueType
        {
            None = 0,
            Label = 1,
            Value = 2,
            LabelAndValue = 3,
            ValueAndLabel = 4
        }
    }
}
