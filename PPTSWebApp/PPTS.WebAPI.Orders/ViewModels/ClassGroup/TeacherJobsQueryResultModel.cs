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

        public static PagedQueryResult<TeacherJobView, TeacherJobViewCollection> Trim(PagedQueryResult<TeacherJobView, TeacherJobViewCollection> QueryResult) {
            foreach (var item in QueryResult.PagedData)
            {
                item.GradeMemo = item.GradeMemo.TrimStart(',').TrimEnd(',');
                item.SubjectMemo = item.SubjectMemo.TrimStart(',').TrimEnd(',');
            }
            return QueryResult;            
        }
    }
}