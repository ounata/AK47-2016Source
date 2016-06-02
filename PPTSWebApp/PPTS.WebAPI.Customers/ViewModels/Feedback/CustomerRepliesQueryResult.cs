using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.WebAPI.Customers.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Feedback
{
    /// <summary>
    /// 学大反馈列表QueryResult
    /// </summary>
    public class CustomerRepliesQueryResult
    {
        //public CustomerRepliesQueryResult() { }
        //public CustomerRepliesQueryResult(CustomerRepliesCriteriaModel criteria)
        //{
        //    if (!string.IsNullOrEmpty(criteria.ReplyID))
        //    {
        //        criteria.ReplyTimeEnd=CustomerReplyAdapter.Instance.Load(criteria.ReplyID).ReplyTime;
        //    }
        //}
        public PagedQueryResult<CustomerRepliesQueryModel, CustomerRepliesQueryCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}