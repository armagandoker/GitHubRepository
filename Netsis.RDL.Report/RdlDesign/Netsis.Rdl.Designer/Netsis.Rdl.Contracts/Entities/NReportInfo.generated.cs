using Netsis.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Netsis.Rdl.Contracts.Entities
{

    [DataContract, Serializable]
    public partial class NReportInfo : NEntity
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Owner { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public DateTime? Creation { get; set; }

        [DataMember]
        public DateTime? LastUpdate { get; set; }

        [DataMember]
        public override string EID { get { return this.Id.ToString(); } }

        public NReportInfo()
        {
        }

    }
}
