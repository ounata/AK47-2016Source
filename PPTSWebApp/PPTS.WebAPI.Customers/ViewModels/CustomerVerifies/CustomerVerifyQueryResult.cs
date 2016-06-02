using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVerifies
{
    public class CustomerVerifyQueryResult
    {
        public PagedQueryResult<CustomerVerifyQueryModel, CustomerVerifiesQueryCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}
