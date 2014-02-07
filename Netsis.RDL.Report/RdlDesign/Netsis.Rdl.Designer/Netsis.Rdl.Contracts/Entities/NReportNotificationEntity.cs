using Netsis.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Netsis.Rdl.Contracts.Entities
{
    public class NReportNotificationEntity
    {

        public int ReportOutputID { get; set; }
        public int SSOUserID { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; }

        public NReportNotificationEntity()
        {            
        }

    }
}
