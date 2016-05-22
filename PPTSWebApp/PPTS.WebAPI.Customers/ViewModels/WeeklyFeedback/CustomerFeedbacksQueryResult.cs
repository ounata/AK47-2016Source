using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Feedback
{
    /// <summary>
    /// 客户反馈列表QueryResult
    /// </summary>
    public class CustomerFeedbacksQueryResult
    {
        public PagedQueryResult<CustomerFeedbacksQueryModel, CustomerFeedbacksQueryCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}