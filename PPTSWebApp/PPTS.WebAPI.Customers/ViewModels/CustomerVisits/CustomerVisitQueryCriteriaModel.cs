using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVisits
{
    public class CustomerVisitQueryCriteriaModel
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        [InConditionMapping("cv.CampusID")]
        public string[] OrgIds { get; set; }

        /// <summary>
        /// 学员ID
        /// </summary>
        [ConditionMapping("cv.CustomerID")]
        public string CustomerID { get; set; }

        /// <summary>
        /// 时间类型
        /// </summary>
        [ConditionMapping("TimeType")]
        public int TimeType { get; set; }

        /// <summary>
        /// 回访时间
        /// </summary>
        [ConditionMapping("VisitTime", Operation = ">=")]
        public DateTime VisitTimeStart { get; set; }

        [ConditionMapping("VisitTime", Operation = "<", AdjustDays = 1)]
        public DateTime VisitTimeEnd { get; set; }

        /// <summary>
        /// 预计下次回访时间
        /// </summary>
        [ConditionMapping("NextVisitTime", Operation = ">=")]
        public DateTime NextVisitTimeStart { get; set; }

        [ConditionMapping("NextVisitTime", Operation = "<", AdjustDays = 1)]
        public DateTime NextVisitTimeEnd { get; set; }

        /// <summary>
        /// 家长满意度
        /// </summary>
        [ConditionMapping("Satisficing")]
        public string Satisficing { get; set; }

        /// <summary>
        /// 回访方式
        /// </summary>
        [ConditionMapping("VisitWay")]
        public string VisitWay { get; set; }

        /// <summary>
        /// 回访类型
        /// </summary>
        [ConditionMapping("VisitType")]
        public string VisitType { get; set; }


        /// <summary>
        /// 学生姓名
        /// </summary>
        [ConditionMapping("CustomerName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string CustomerName { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        [ConditionMapping("CustomerCode")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// 回访人
        /// </summary>
        [ConditionMapping("VisitorName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string VisitorName { get; set; }

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