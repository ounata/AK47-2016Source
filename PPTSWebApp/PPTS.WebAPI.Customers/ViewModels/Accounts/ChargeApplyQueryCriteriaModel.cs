using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 缴费申请列表查询条件
    /// </summary>
    [Serializable]
    public class ChargeApplyQueryCriteriaModel
    {
        /// <summary>
        /// 校区ID
        /// </summary>
        [InConditionMapping("a.CampusID")]
        public string[] CampusIDs { get; set; }

        /// <summary>
        /// 学员编号
        /// </summary>
        [ConditionMapping("a.CustomerCode", Operation = "=")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// 缴费单号
        /// </summary>
        [ConditionMapping("a.ApplyNo", Operation = "=")]
        public string ApplyNo { get; set; }

        /// <summary>
        /// 学员或家长姓名
        /// </summary>
        [ConditionMapping("d.CustomerSearchContent", Template = "CONTAINS(${DataField}$, ${Data}$)")]
        public string CustomerOrParentName { get; set; }

        /// <summary>
        /// 家长联系方式
        /// </summary>
        [ConditionMapping("d.ParentSearchContent", Template = "CONTAINS(${DataField}$, ${Data}$)")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 咨询师姓名
        /// </summary>
        [ConditionMapping("a.ConsultantName", Operation = "=")]
        public string ConsultantName { get; set; }

        /// <summary>
        /// 学管师姓名
        /// </summary>
        [ConditionMapping("a.EducatorName", Operation = "=")]
        public string EducatorName { get; set; }

        /// <summary>
        /// 当时年级
        /// </summary>
        [InConditionMapping("a.CustomerGrade")]
        public string[] Grades { get; set; }
        
        /// <summary>
        /// 充值日期
        /// </summary>
        [ConditionMapping("a.ApplyTime", Operation = ">=")]
        public DateTime ApplyTimeStart { get; set; }
        [ConditionMapping("ApplyTime", Operation = "<", AdjustDays = 1)]
        public DateTime ApplyTimeEnd { get; set; }

        /// <summary>
        /// 充值类型
        /// </summary>
        [InConditionMapping("a.ChargeType")]
        public string[] ChargeTypes { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        [ConditionMapping("a.ChargeMoney", Operation = ">=")]
        public DateTime ChargeMoneyStart { get; set; }
        [ConditionMapping("a.ChargeMoney", Operation = "<", AdjustDays = 1)]
        public DateTime ChargeMoneyEnd { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [InConditionMapping("a.AuditStatus")]
        public string[] AuditStatuses { get; set; }

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