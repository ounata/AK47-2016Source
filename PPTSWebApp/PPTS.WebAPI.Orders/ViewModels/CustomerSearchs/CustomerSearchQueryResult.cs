using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.Data;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Common.Entities;

namespace PPTS.WebAPI.Orders.ViewModels.CustomerSearchs
{
    public class CustomerSearchQueryResult
    {
        public PagedQueryResult<CustomerSearch, CustomerSearchCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}