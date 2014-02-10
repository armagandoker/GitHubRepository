using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netsis.Framework.Utils.Extensions;
using Netsis.Framework.Service;
using Netsis.Framework.Service.Client;
using System.ServiceModel;
using Netsis.Framework.Core.Security.Account;
using Netsis.Framework.Types;
using Netsis.Framework.Core.DataAccess;
using Netsis.Framework.Persister;
using Netsis.Framework.Utils;
using Netsis.Services.NtfService.Contracts.DTO;
using Netsis.Services.NtfService.Contracts.Types;
using Netsis.Services.NtfService.Contracts;
using Netsis.Rdl.Contracts.Entities;
using Netsis.Rdl.Contracts.Config;
using NDF.Library.Conf;

namespace Netsis.Rdl.Contracts.Helpers
{
    internal sealed class NotificationServiceProxy
    {
        #region [_FIELDS_]

        private static INNtfService ntfService = null;    
        private static NtfCallbackHandler callback = null;        

        #endregion

        #region [_PRIVATE_]     

        private static void ConnectToService()
        {
            if (callback.Assigned())            
                callback.Disable();            
            callback = new NtfCallbackHandler();

            //throw new Exception("net.tcp:" + NDFSettings.Current.NotificationServiceAddr + "/");

            ntfService = NWcfClientUtils.GetChannelForDuplex<INNtfService, INNtfServiceCallback>(callback, "net.tcp:" + NDFSettings.Current.NotificationServiceAddr);            
            //ntfService = NWcfClientUtils.GetChannelForDuplex<INNtfService, INNtfServiceCallback>(callback, "//localhost:2023/NNtfService/");
            
            ((ICommunicationObject)ntfService).Faulted += NDFNotifier_Faulted;            
            callback.Disable();      
            callback.Enable();
        }      

        private static void Abort()
        {
            callback.Disable();
            NWcfServiceUtils.CloseConnection((ICommunicationObject)ntfService);
        }

        private static void NDFNotifier_Faulted(object sender, EventArgs e)
        {
            Abort();
        }

        #endregion

        #region [_INTERNALS_]

        internal static void SendMessage(NReportNotificationEntity entity)
        {
            ConnectToService();
            //
            ((ICommunicationObject)ntfService).SynchCall<INNtfService>
               (
                   codeBlock =>
                   {
                       try
                       {
                           NotificationInfo info = new NotificationInfo();
                           info.HttpLink = string.Format("{0}?ID={1}", NReportConfig.Instance().AJMReportDownloadPage, entity.ReportOutputID.ToString());
                           info.Message = entity.MessageText;
                           info.MessageType = NotificationInfoType.Information;
                           info.Subject = entity.Subject;

                           ntfService.SendToUser(Newtonsoft.Json.JsonConvert.SerializeObject(info), entity.Subject, entity.SSOUserID, entity.SSOUserID.ToString());
                          
                           callback.Disable();
                           NWcfServiceUtils.CloseConnection((ICommunicationObject)ntfService);
                       }
                       catch (Exception ex)
                       {
                           throw ex;
                       }
                   },
                   message => { },
                   error => { },
                   true,
                   1
               );
        }      

        #endregion

        #region [_CLASSES_]

        class NtfCallbackHandler : NtfCallbackHandlerBase
        {
            protected override void DoSetMessage(NtfMessage message)
            {                
            }
        }

        [Serializable]
        public class NotificationInfo
        {
            public string Subject { get; set; }
            public string Message { get; set; }
            public NotificationInfoType MessageType { get; set; }
            public String HttpLink { get; set; }
        }

        [Serializable]
        public enum NotificationInfoType
        {
            Information = 0,
            Reminder = 1,
            Warning = 2,
            Error = 3
        }

        #endregion
    }
}
