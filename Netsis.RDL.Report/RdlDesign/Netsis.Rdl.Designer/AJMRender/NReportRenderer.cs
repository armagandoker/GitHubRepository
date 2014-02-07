using fyiReporting.RDL;
using Netsis.Framework.AJM.Contracts;
using Netsis.Framework.Utils.Helpers;
using Netsis.Rdl.Contracts;
using Netsis.Rdl.Contracts.Config;
using Netsis.Rdl.Contracts.Entities;
using Netsis.Rdl.Contracts.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace fyiReporting.RdlEngine
{
    public class NReportRenderer : INAjmExecute
    {

        #region [_FIELDS_]

        public const string NLogConfigDateFormat = "yyyy-MM-dd HH.mm.ss.ff";
        Report report = null;
        NReportInfo reportInfo = null;

        #endregion

        #region [_PRIVATES_]

        private int RenderReport(int taskID, int reportID, ListDictionary parameters, OutputPresentationType renderType)
        {

            report = GetReport(reportID);
            report.RunGetData(parameters);
            return Render(taskID, reportID, report, renderType);
        }

        private Report GetReport(int reportID)
        {
            Report r;
            reportInfo = NReportInfoHelper.GetNReport(reportID);
            RDLParser rdlp = new RDLParser(reportInfo.Source);
            r = rdlp.Parse();
            if (r.ErrorMaxSeverity > 0)
            {
                if (r.ErrorMaxSeverity > 4)
                    throw new Exception(String.Format("Raporda aşağıdaki hatalar mevcut\r\n\r\n{0}", String.Join("\r\n", r.ErrorItems)));
                r.ErrorReset();
            }
            return r;
        }

        private static string GetExtension(OutputPresentationType renderType)
        {
            switch (renderType)
            {
                case OutputPresentationType.PDF:
                    return "pdf";
                case OutputPresentationType.Excel:
                    return "xlsx";
                case OutputPresentationType.Word:
                    return "doc";
                case OutputPresentationType.HTML:
                    return "html";
                default:
                    return "pdf";
            }
        }

        private static int Render(int taskID, int reportID, Report report, OutputPresentationType renderType)
        {
            NReportMemoryStream sg = null;          
            try
            {
                sg = new NReportMemoryStream();
                switch (renderType)
                {
                    case OutputPresentationType.PDF:
                        report.RunRender(sg, OutputPresentationType.PDF);
                        break;
                    case OutputPresentationType.HTML:
                        report.RunRender(sg, OutputPresentationType.HTML);
                        break;
                    case OutputPresentationType.Excel:
                        report.RunRender(sg, OutputPresentationType.Excel);
                        break;
                    case OutputPresentationType.Word:
                        report.RunRender(sg, OutputPresentationType.RTF);
                        break;
                    default:                    
                        throw new Exception(string.Format("Unsupported file extension '{0}'.  Must be 'pdf', 'xslx', 'word' or 'html'", renderType.ToString()));
                }
                NReportOutput output = new NReportOutput()
                {
                    Creation = DateTime.Now,
                    FileExtension = GetExtension(renderType),
                    Data = sg.GetBytes(),
                    ReportId = reportID,
                    TaskId = taskID
                };
                NReportInfoHelper.InsertNReportOutput(output);                
                return output.Id.Value;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sg != null)
                {
                    sg.Dispose();
                    sg = null;
                }
            }
        }

        #endregion

        #region [_PUBLICS_]

        public static void ConvertTurkish(Report report)
        {
            if (!NReportConfig.Instance().TurkishConvertion)
                return;
            Rows tempRows = null;
            Type tp = report.Cache.GetType();
            Hashtable hTable = (Hashtable)tp.GetField("_RunCache", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(report.Cache);
            foreach (var itm in hTable.Values)
            {
                if (!(itm is Rows))
                    continue;
                tempRows = (Rows)itm;
                foreach (Row rw in tempRows.Data)
                {
                    for (int i = 0; i < rw.Data.Count(); i++)
                    {
                        if (rw.Data[i] is string)
                            rw.Data[i] = TurkishConvertor.ConvertToTurkish((string)rw.Data[i]);
                        else if (rw.Data[i] is char)
                            rw.Data[i] = TurkishConvertor.ConvertToTurkish((char)rw.Data[i]);
                    }
                }
            }
        }


        public void Execute(NAjmExecArgs args)
        {
            Dictionary<String, Object> pArray = (Dictionary<String, Object>)args.TaskParams["Parameters"];
            ListDictionary dict = new ListDictionary();
            foreach (var itm in pArray)
            {
                dict.Add(itm.Key, itm.Value);
            }
            int gg = args.SSOId;
            int outputID = RenderReport(args.JobId, (int)args.TaskParams["ReportID"], dict, (OutputPresentationType)(int)args.TaskParams["RenderType"]);
            NReportNotificationEntity entity = new NReportNotificationEntity()
            {
                MessageText = String.Format("{0} adlı rapor hazırlandı", reportInfo.Name),
                ReportOutputID = outputID,
                SSOUserID = args.SSOId,
                Subject = "Raporlama Servisi"
            };
            NotificationServiceProxy.SendMessage(entity);
        }

        #endregion

    }

}
