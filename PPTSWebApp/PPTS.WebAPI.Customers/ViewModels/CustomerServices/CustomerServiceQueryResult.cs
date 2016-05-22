using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Common.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerServices
{
    public class CustomerServiceQueryResult
    {
        public PagedQueryResult<CustomerServiceModel, CustomerServiceModelCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}