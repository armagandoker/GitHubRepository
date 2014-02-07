using Netsis.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Netsis.Rdl.Contracts.Entities
{
    public partial class NReportOutput : NEntity
    {

        public NReportOutput ToDTO()
        {
            return new NReportOutput()
            {
                Id = this.Id,
                TaskId = this.TaskId,
                ReportId = this.ReportId,
                Data = this.Data,
                FileExtension = this.FileExtension,
                Creation = this.Creation,
            };
        }

    }
}
