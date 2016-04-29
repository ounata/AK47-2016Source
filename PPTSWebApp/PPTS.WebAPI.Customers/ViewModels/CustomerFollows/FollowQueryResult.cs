using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerFollows
{
    public class FollowQueryResult
    {
        public PagedQueryResult<FollowQueryModel, CustomerFollowQueryCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}