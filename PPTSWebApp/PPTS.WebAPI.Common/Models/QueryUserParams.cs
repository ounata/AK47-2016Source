using PPTS.Data.Common;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Common.Models
{
    public class QueryUserParams
    {
        public QueryUserParams()
        {
        }

        public QueryUserParams(PPTSJobCollection jobs, JobTypeDefine jobType)
        {
            this.Jobs = jobs;
            this.JobType = jobType;
        }

        public JobTypeDefine JobType { get; set; }
        public PPTSJobCollection Jobs { get; set; }
    }
}