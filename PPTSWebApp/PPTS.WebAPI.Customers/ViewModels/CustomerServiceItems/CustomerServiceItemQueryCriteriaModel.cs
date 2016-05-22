using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerServiceItems
{
    public class CustomerServiceItemQueryCriteriaModel
    {

        /// <summary>
        /// 明细ID
        /// </summary>
        [ConditionMapping("ci.ItemID")]
        public string ItemID { get; set; }

        /// <summary>
        /// 服务ID
        /// </summary>
        [ConditionMapping("ci.ServiceID")]
        public string ServiceID { get; set; }

        // <summary>
        /// 服务ID
        /// </summary>
        [ConditionMapping("c.CustomerID")]
        public string CustomerID { get; set; }



        [ConditionMapping("HandleTime", Operation = ">=")]
        public DateTime HandleTimeStart { get; set; }

        [ConditionMapping("HandleTime", Operation = "<", AdjustDays = 1)]
        public DateTime HandleTimeEnd { get; set; }

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