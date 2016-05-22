using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Common.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVisits
{
    public class CustomerVisitQueryResult
    {

        public PagedQueryResult<CustomerVisitModel, CustomerVisitModelCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}