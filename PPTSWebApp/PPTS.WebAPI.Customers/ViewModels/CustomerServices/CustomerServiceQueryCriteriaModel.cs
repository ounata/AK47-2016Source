using MCS.Library.Data;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerServices
{
    public class CustomerServiceQueryCriteriaModel
    {
        public CustomerServiceQueryCriteriaModel()
        {
            this.IsUpgradeHandle = BooleanState.Unknown;
        }
        /// <summary>
        /// 机构ID
        /// </summary>
        [InConditionMapping("cs.CampusID")]
        public string[] OrgIds { get; set; }

        /// <summary>
        /// 家长姓名
        /// </summary>
        [ConditionMapping("ParentName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string ParentName { get; set; }

        /// <summary>
        /// 学员编号
        /// </summary>
        [ConditionMapping("CustomerCode")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// 客服受理人
        /// </summary>
        [ConditionMapping("AccepterName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string AccepterName { get; set; }

        /// <summary>
        /// 当前受理人
        /// </summary>
        [ConditionMapping("HandlerName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string HandlerName { get; set; }

        /// <summary>
        /// 当前年级
        /// </summary>
        [InConditionMapping("Grade")]
        public int[] Grades { get; set; }

        /// <summary>
        /// 受理状态
        /// </summary>
        [InConditionMapping("ServiceStatus")]
        public int[] ServiceStatuses { get; set; }

        /// <summary>
        /// 来电时间
        /// </summary>
        [ConditionMapping("CallTime", Operation = ">=")]
        public DateTime CallTimeStart { get; set; }

        [ConditionMapping("CallTime", Operation = "<", AdjustDays = 1)]
        public DateTime CallTimeEnd { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        [InConditionMapping("ServiceType")]
        public int[] ServiceTypes { get; set; }

        //// <summary>
        /// 投诉次数
        /// </summary>
        [InConditionMapping("ComplaintTimes")]
        public int[] ComplaintTimes { get; set; }

        //// <summary>
        /// 客服升级
        /// </summary>
        [InConditionMapping("ComplaintUpgrade")]
        public int[] ComplaintUpgrades { get; set; }

        //// <summary>
        /// 是否升级
        /// </summary>
        ///[ConditionMapping("IsUpgradeHandle", DefaultValueUsage = DefaultValueUsageType.UseDefaultValue)]
        [ConditionMapping("IsUpgradeHandle")]
        public BooleanState IsUpgradeHandle { get; set; }

        //// <summary>
        /// 严重程度
        /// </summary>
        [InConditionMapping("ComplaintLevel")]
        public int[] ComplaintLevels { get; set; }

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