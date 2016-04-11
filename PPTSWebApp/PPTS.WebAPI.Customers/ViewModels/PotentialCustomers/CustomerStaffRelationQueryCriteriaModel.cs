using System;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Data;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class CustomerStaffRelationQueryCriteriaModel
    {
        [ConditionMapping("CustomerID")]
        public string ID { get; set; }

        [ConditionMapping("RelationType")]
        public string RelationType { get; set; }

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