using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using System.Collections.Generic;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class CustomerTeacherRelationsQueryResult
    {
        public PagedQueryResult<CustomerTeacherAssignApply, CustomerTeacherAssignApplyCollection> QueryResult
        {
            get; set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }
    }
}