using MCS.Library.Data;
using System.Collections.Generic;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customer.ViewModels.PotentialCustomers
{
    public class CustomerStaffRelationQueryResult
    {
        public PagedQueryResult<CustomerStaffRelation, CustomerStaffRelationCollection> QueryResult { get; set; }
    }
}