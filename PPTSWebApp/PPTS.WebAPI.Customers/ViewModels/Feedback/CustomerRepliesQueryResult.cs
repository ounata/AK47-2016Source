using MCS.Library.Data;
using PPTS.Data.Common.Entities;
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
        public PagedQueryResult<CustomerRepliesQueryModel, CustomerRepliesQueryCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}