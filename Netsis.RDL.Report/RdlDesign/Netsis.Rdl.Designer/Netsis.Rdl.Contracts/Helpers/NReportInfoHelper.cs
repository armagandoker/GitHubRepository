using Netsis.Framework.Persister;
using Netsis.Rdl.Contracts.Entities;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netsis.Rdl.Contracts.Helpers
{
    public class NReportInfoHelper
    {               

        public static List<NReportInfo> GetReportList()
        {
            using (var persister = new NPersister())
            {
                var query = persister.CreateQuery(@"Select r.Id as Id, r.Name as Name, r.Description as Description, 
                        r.Owner as Owner, r.Creation as Creation, r.LastUpdate as LastUpdate From NReportInfo as r");
                return query.SetResultTransformer(Transformers.AliasToBean<NReportInfo>()).List<NReportInfo>().ToList();
            };
        }

        public static NReportInfo GetNReport(int id)
        {
            using (var persister = new NPersister())
            {
                var query = persister.CreateQuery(@"Select r.Id as Id, r.Name as Name, r.Description as Description, r.Owner as Owner, 
                    r.Source as Source, r.Creation as Creation, r.LastUpdate as LastUpdate From NReportInfo as r where r.Id = " + id.ToString());
                return query.SetResultTransformer(Transformers.AliasToBean<NReportInfo>()).List<NReportInfo>().FirstOrDefault();
            };
        }

        public static NReportInfo GetNSubReport(string reportName)
        {
            string[] splitValues = reportName.Split('-');
            
            int reportKod = 0;
            if (splitValues != null && splitValues.Length > 0 && Int32.TryParse(splitValues[0].Trim(), out reportKod))
                return GetNReport(reportKod);
            
            return null;
        }

        public static void SaveReport(NReportInfo ri)
        {
            using (var persister = new NPersister())
            {
                if (ri.Id != null)
                    persister.Update(ri);
                else
                    persister.Save(ri);
                persister.Commit();
            }; 
        }

        public static void DeleteNReport(List<int> idList)
        {
            if (idList == null || idList.Count == 0)
                return;
            using (var persister = new NPersister())
            {
               persister.CreateQuery(String.Format("DELETE  FROM NReportInfo WHERE Id IN (:idsList)"))
                    .SetParameterList("idsList", idList.ToArray())
                    .ExecuteUpdate();
               persister.Commit();
            };
        }

        public static void InsertNReportOutput(NReportOutput output)
        {
            using (var persister = new NPersister())
            {
                persister.Save(output);                    
                persister.Commit();
            }; 
        }

        public static NReportOutput GetNReportOutput(int reportOutputID)
        {
            using (var persister = new NPersister())
            {
                var query = persister.CreateQuery(@"Select r.Id as Id, r.ReportId as ReportId, r.TaskId as TaskId, r.FileExtension as FileExtension, r.Data as Data
                        , r.Creation as Creation From NReportOutput as r as r where r.Id = " + reportOutputID.ToString());
                return query.SetResultTransformer(Transformers.AliasToBean<NReportInfo>()).List<NReportOutput>().FirstOrDefault();
            };
        }

        public static List<NReportOutput> GetAndRemoveReportOutputs()
        {
            using (var persister = new NPersister())
            {
                var query = persister.CreateQuery(@"Select r.Id as Id, r.ReportId as ReportId, r.TaskId as TaskId, 
                        r.FileExtension as FileExtension, r.Data as Data, r.Creation as Creation From NReportOutput as r");
                List<NReportOutput> lst = query.SetResultTransformer(Transformers.AliasToBean<NReportOutput>()).List<NReportOutput>().ToList();

                persister.CreateQuery(String.Format("DELETE  FROM NReportOutput")).ExecuteUpdate();
                persister.Commit();

                return lst;
            };
        }


    }
}
