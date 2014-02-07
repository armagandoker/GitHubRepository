using Netsis.Rdl.Contracts.Helpers;
//using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml;

namespace fyiReporting.RdlDesign.Netsis.Rdl.Designer.Helpers
{
    internal class ReportPreviewHelper
    {

        private string _MainReport;
        private Dictionary<string, SubReportInfos> _SubReports;
        private Dictionary<string, ReportDataSource> _ReportSources;
        private List<ReportDataSet> _MainReportDataSets;
        private Dictionary<string, List<ReportDataSet>> _SubReportDataSets;
        private XmlDocument _MainDoc;

        public string MainReport { get { return _MainReport; } }
        public Dictionary<string, SubReportInfos> SubReports { get { return _SubReports; } }        

        public ReportPreviewHelper ParseReportXML(string report)
        {
            this._ReportSources = new Dictionary<string, ReportDataSource>();
            this._MainReportDataSets = new List<ReportDataSet>();
            this._SubReportDataSets = new Dictionary<string, List<ReportDataSet>>();
            this._MainReport = report;
            this._MainDoc = new XmlDocument();
            this._MainDoc.LoadXml(report);                  
            LoadSubReports();

            LoadReportSources(this._MainDoc.DocumentElement);
            this._MainReportDataSets = GetReportDataSetList(this._MainDoc.DocumentElement);
            foreach (KeyValuePair<string, SubReportInfos> sb in this._SubReports)
            {
                if (sb.Value.UseMasterReportDataSource)
                    continue;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sb.Value.Source);
                LoadReportSources(doc.DocumentElement);
                this._SubReportDataSets.Add(sb.Key, GetReportDataSetList(doc.DocumentElement));
            }
                                              
            return this;
        }

        public DataSet GetMainReportDataSet()
        {
            DataSet ds = new DataSet();
            foreach (ReportDataSet rds in this._MainReportDataSets)
            {
                FillDataSet(ds, rds, this._ReportSources[rds.Query.DataSourceName]);
            }
            return ds;
        }

        public DataSet GetSubReportDataSet(IList<string> dataSourceNames, string reportName)
        {
            DataSet ds = new DataSet();
            List<ReportDataSet> rdsList = this._SubReportDataSets[reportName];
            foreach (ReportDataSet rds in rdsList)
            {
                FillDataSet(ds, rds, this._ReportSources[rds.Query.DataSourceName]);
            }
            return ds;
        }

        private static void FillDataSet(DataSet ds, ReportDataSet rds, ReportDataSource source)
        {
            IDbConnection conn = CreateNewConnection(source);    
            IDbCommand comm = conn.CreateCommand();
            comm.CommandText = rds.Query.CommandText;
            IDataAdapter da = CreateNewDataAdapter(comm);            
            try
            {
                conn.Open();
                da.Fill(ds);
                ds.Tables[ds.Tables.Count - 1].TableName = rds.Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                comm.Dispose();
                conn.Dispose();
                da = null;
            }
        }

        private static IDbConnection CreateNewConnection(ReportDataSource source)
        {
            switch (source.ConnectionProps.ConnectionProvider)
            {
                case ConnectionType.SQL:
                    return new SqlConnection(source.ConnectionProps.ConnectionString);                    
                case ConnectionType.Oracle:
                    //return new OracleConnection(source.ConnectionProps.ConnectionString);           
                default:
                    return null;
            }
        }

        private static IDataAdapter CreateNewDataAdapter(IDbCommand command)
        {
            if (command is SqlCommand)
                return new SqlDataAdapter((SqlCommand)command);
            //else if (command is OracleCommand)
                //return new OracleDataAdapter((OracleCommand)command);
            return null;
        }

        private void LoadReportSources(XmlElement xmlElement)
        {
            XmlNode dataSourcesNode = GetNamedChildNode(xmlElement, "DataSources");
            if (dataSourcesNode == null)
                return;
            foreach (XmlNode dNode in dataSourcesNode)
            {
                if (dNode.Name != "DataSource")
                    continue;
                XmlAttribute nAttr = dNode.Attributes["Name"];
                if (nAttr == null)
                    continue;
                XmlNode query = GetNamedChildNode(dNode, "ConnectionProperties");
                ReportConnectionProperties rcp = new ReportConnectionProperties();
                this._ReportSources.Add(nAttr.Value, new ReportDataSource(nAttr.Value, rcp));
                foreach (XmlNode q in query)
                {
                    switch (q.Name)
                    {
                        case "DataProvider":
                            rcp.ConnectionProvider = (q.FirstChild.Value.ToString() == "SQL" ? ConnectionType.SQL : ConnectionType.Oracle);
                            break;
                        case "ConnectString":
                            rcp.ConnectionString = q.FirstChild.Value.ToString();
                            break;
                        default:
                            break;
                    }
                }
            }            
        }

        private List<ReportDataSet> GetReportDataSetList(XmlElement xmlElement)
        {
            List<ReportDataSet> retVal = new List<ReportDataSet>();
            XmlNode dataSetsNode = GetNamedChildNode(xmlElement, "DataSets");
            if (dataSetsNode == null)
                return retVal;
            foreach (XmlNode dNode in dataSetsNode)
            {
                if (dNode.Name != "DataSet")
                    continue;
                XmlAttribute nAttr = dNode.Attributes["Name"];
                if (nAttr == null)	// shouldn't really happen
                    continue;
                XmlNode query = GetNamedChildNode(dNode, "Query");

                ReportQuery rq = new ReportQuery();
                retVal.Add(new ReportDataSet(nAttr.Value, rq));
                foreach (XmlNode q in query)
                {
                    switch (q.Name)
                    {
                        case "DataSourceName":
                            rq.DataSourceName = q.FirstChild.Value.ToString(); ;
                            break;
                        case "CommandText":
                            rq.CommandText = q.FirstChild.Value.ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
            return retVal;
        }

        private void LoadSubReports()
        {
            this._SubReports = new Dictionary<string, SubReportInfos>();
            XmlNodeList subReportNodeList = this._MainDoc.GetElementsByTagName("Subreport");            
            if (subReportNodeList == null)
                return;

            foreach (XmlNode subReportNode in subReportNodeList)
            {
                XmlAttribute nAttr = subReportNode.Attributes["Name"];
                XmlNode reportName = GetNamedChildNode(subReportNode, "ReportName");
                bool useMasterTrans = GetNamedChildNode(subReportNode, "MergeTransactions").FirstChild.Value == "true";

                SubReportInfos inf = new SubReportInfos(nAttr.Value, reportName.FirstChild.Value, NReportInfoHelper.GetNSubReport(reportName.FirstChild.Value).Source, useMasterTrans);
                this._SubReports.Add(inf.ReportName, inf);
            }                    
        }

        internal XmlNode GetNamedChildNode(XmlNode xNode, string name)
        {
            if (xNode == null)
                return null;

            foreach (XmlNode cNode in xNode.ChildNodes)
            {
                if (cNode.NodeType == XmlNodeType.Element && cNode.Name == name)
                    return cNode;
            }
            return null;
        }

    }

    #region [_CLASSES_]

    internal class ReportQuery
    {
        public string DataSourceName { get; set; }
        public string CommandText { get; set; }        

        internal ReportQuery()
        {
        }

        internal ReportQuery(string datasourceName, string commandText)
        {
            this.DataSourceName = datasourceName;
            this.CommandText = commandText;
        }
    }

    internal class ReportDataSet
    {
        public string Name { get; set; }
        public ReportQuery Query { get; set; }

        internal ReportDataSet(string name)
        {
            this.Name = name;
        }

        internal ReportDataSet(string name, ReportQuery query)
        {
            this.Name = name;
            this.Query = query;
        }
    }

    internal enum ConnectionType
    {
        SQL = 0,
        Oracle =1
    }

    internal class ReportConnectionProperties
    {
        public ConnectionType ConnectionProvider { get; set; }
        public string ConnectionString { get; set; }

        internal ReportConnectionProperties(ConnectionType connectionProvider, string connString)
        {
            this.ConnectionProvider = connectionProvider;
            this.ConnectionString = connString;
        }

        internal ReportConnectionProperties()
        {            
        }
    }

    internal class ReportDataSource
    {
        public string Name { get; set; }
        public ReportConnectionProperties ConnectionProps { get; set; }

        internal ReportDataSource(string name)
        {
            this.Name = name;
        }

        internal ReportDataSource(string name, ReportConnectionProperties connProps)
        {
            this.Name = name;
            this.ConnectionProps = connProps;
        }
    }

    internal class SubReportInfos
    {
        public string Name { get; set; }
        public string ReportName { get; set; }
        public string Source { get; set; }
        public bool UseMasterReportDataSource { get; set; }

        internal SubReportInfos(string name, string reportName, string source, bool useMasterTrans)
        {
            this.Name = name;
            this.ReportName = reportName;
            this.Source = source;
            this.UseMasterReportDataSource = useMasterTrans;
        }

    }

    #endregion

}
