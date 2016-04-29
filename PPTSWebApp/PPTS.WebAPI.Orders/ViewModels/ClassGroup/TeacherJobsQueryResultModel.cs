using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    public class TeacherJobsQueryResultModel
    {
        public PagedQueryResult<TeacherJobView, TeacherJobViewCollection> QueryResult { get; set; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}