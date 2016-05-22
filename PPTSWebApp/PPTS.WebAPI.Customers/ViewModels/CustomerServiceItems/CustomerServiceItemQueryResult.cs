using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Common.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerServiceItems
{
    public class CustomerServiceItemQueryResult
    {
        public PagedQueryResult<CustomerServiceItemListMosel, CustomerServiceItemListMoselCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}