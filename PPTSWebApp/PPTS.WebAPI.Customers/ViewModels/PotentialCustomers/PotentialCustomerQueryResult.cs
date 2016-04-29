using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Common.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class PotentialCustomerQueryResult
    {
        public PagedQueryResult<PotentialCustomerSearchModel, PotentialCustomerSearchModelCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}