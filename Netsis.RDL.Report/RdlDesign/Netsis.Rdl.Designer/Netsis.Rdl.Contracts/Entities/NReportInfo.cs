using Netsis.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Netsis.Rdl.Contracts.Entities
{    
    public partial class NReportInfo : NEntity
    {

        public NReportInfo(string name, string source)
        {
            this.Name = name;
            this.Source = source;
        }

        public NReportInfo ToDTO()
        {
            return new NReportInfo()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Owner = this.Owner,
                Source = this.Source,
                Creation = this.Creation,
                LastUpdate = this.LastUpdate               
            };
        }

    }
}
