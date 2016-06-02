using System;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Data;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class CustomerTeacherRelationsQueryCriteriaModel
    {
        [ConditionMapping("CustomerID")]
        public string CustomerID { get; set; }

        [NoMapping]
        public PageRequestParams PageParams
        {
            get;
            set;
        }

        [NoMapping]
        public OrderByRequestItem[] OrderBy
        {
            get;
            set;
        }
    }
}