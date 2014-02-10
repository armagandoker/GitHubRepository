using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Netsis.Rdl.Contracts.Config
{
    public class NReportConfig
    {

        #region [_FIELDS_]

        private const string NReportConfigFileName = "NReportConfig.config";
        private static NReportConfig _reportConfig = null;        

        #endregion

        #region [_PROPERTIES_]

        public bool TurkishConvertion { get; set; }
        public string AJMReportDownloadPage { get; set; }

        #endregion

        #region [_CONSTRUCTORS_]

        internal NReportConfig()
        {
            this.TurkishConvertion = false;
            this.AJMReportDownloadPage = "ReportDownloadHandler.ashx";
        }

        #endregion

        #region [_PRIVATES_]

        private static string GetConfigFilePath()
        {
            return GetFullFilePath(NReportConfigFileName);
        }

        #endregion

        #region [_PUBLICS_]

        public static NReportConfig Instance()
        {
            if (_reportConfig == null)
            {      
                string fileName = GetConfigFilePath();
                if (File.Exists(fileName))
                {
                    string jSonString = File.ReadAllText(GetConfigFilePath());
                    _reportConfig = JsonConvert.DeserializeObject<NReportConfig>(jSonString);
                }
                else
                {
                    _reportConfig = new NReportConfig();
                    _reportConfig.SaveSettings();
                }
            }
            return _reportConfig;
        }

        public void SaveSettings()
        {
            string fileName = GetConfigFilePath();
            if(File.Exists(fileName))
                File.Delete(fileName);
            File.WriteAllText(fileName, JsonConvert.SerializeObject(this));
        }

        public static string GetFullFilePath(string fileName)
        {
            return String.Format(@"{0}\{1}", Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), fileName);
        }

        #endregion

    }
}
