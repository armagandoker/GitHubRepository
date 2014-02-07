using Netsis.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Netsis.Rdl.Contracts.Entities
{

    [DataContract, Serializable]
    public partial class NReportOutput : NEntity
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public int? TaskId { get; set; }

        [DataMember]
        public int? ReportId { get; set; }

        [DataMember]
        public string FileExtension { get; set; }

        [DataMember]
        public byte[] Data { get; set; }

        [DataMember]
        public DateTime? Creation { get; set; }       

        [DataMember]
        public override string EID { get { return this.Id.ToString(); } }

        public NReportOutput()
        {
        }

    }
}
