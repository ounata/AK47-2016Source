using MCS.Library.Data;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System.Collections.Generic;

namespace PPTS.WebAPI.Customers.ViewModels.Parents
{
    public class ParentsSearchQueryResult
    {
        public PagedQueryResult<ParentModel, ParentModelCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<Data.Common.Entities.BaseConstantEntity>> Dictionaries { get; set; }
    }
}