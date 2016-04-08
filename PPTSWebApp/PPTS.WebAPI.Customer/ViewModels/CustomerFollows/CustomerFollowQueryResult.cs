using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customer.ViewModels.CustomerFollows
{
    public class CustomerFollowQueryResult
    {
        public PagedQueryResult<CustomerFollow, CustomerFollowCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}