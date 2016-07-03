using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.ServiceFees
{
    public class ExpensesCriteriaModel
    {
        [ConditionMapping("ExpenseType")]
        public string serviceFeeTypes { set; get; }

        [InConditionMapping("CampusIDs")]
        public string[] OrgIds { get; set; }

        public string CreatorName { set; get; }
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