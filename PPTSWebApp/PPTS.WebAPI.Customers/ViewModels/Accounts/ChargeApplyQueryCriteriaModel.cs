using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
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
        /// 学员或家长姓名或联系电话
        /// </summary>
        [NoMapping]
        public string SearchText { get; set; }
        
        /// <summary>
        /// 缴费单号
        /// </summary>
        [ConditionMapping("a.ApplyNo", Operation = "=")]
        public string ApplyNo { get; set; }

        /// <summary>
        /// 申请人岗位类型
        /// </summary>
        [InConditionMapping("a.ApplierJobType")]
        public string[] ApplierJobTypes { get; set; }

        /// <summary>
        /// 充值申请人
        /// </summary>
        [ConditionMapping("a.ApplierName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string ApplierName { get; set; }

        /// <summary>
        /// 付款日期
        /// </summary>
        [ConditionMapping("a.PayTime", UtcTimeToLocal = true, Operation = ">=")]
        public DateTime PayTimeStart { get; set; }
        [ConditionMapping("a.PayTime", UtcTimeToLocal = true, Operation = "<", AdjustDays = 1)]
        public DateTime PayTimeEnd { get; set; }

        /// <summary>
        /// 当时年级
        /// </summary>
        [InConditionMapping("a.CustomerGrade")]
        public string[] Grades { get; set; }

        /// <summary>
        /// 充值类型
        /// </summary>
        [InConditionMapping("a.ChargeType")]
        public string[] ChargeTypes { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        [ConditionMapping("a.ChargeMoney", Operation = ">=")]
        public decimal ChargeMoneyStart { get; set; }
        [ConditionMapping("a.ChargeMoney", Operation = "<", AdjustDays = 1)]
        public decimal ChargeMoneyEnd { get; set; }

        /// <summary>
        /// 充值类型
        /// </summary>
        [InConditionMapping("a.PayStatus")]
        public string[] PayStatuses { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [InConditionMapping("a.AuditStatus")]
        public string[] AuditStatuses { get; set; }

        /// <summary>
        /// 信息来源一级
        /// </summary>
        [NoMapping]
        [ConstantCategory("C_CODE_ABBR_BO_Customer_Source")]
        public string[] SourceMainTypes { get; set; }

        /// <summary>
        /// 信息来源二级
        /// </summary>
        [NoMapping]
        public string[] SourceSubTypes { get; set; }

        /// <summary>
        /// 归属关系
        /// </summary>
        [NoMapping]
        public string[] BelongRelationTypes { get; set; }

        /// <summary>
        /// 归属人姓名
        /// </summary>
        [NoMapping]
        public string BelongerName { get; set; }

        /// <summary>
        /// 查询部门
        /// </summary>
        [NoMapping]
        public string[] QueryDepts { get; set; }

        /// <summary>
        /// 查询部门ID
        /// </summary>
        [NoMapping]
        public string QueryDeptID { get; set; }

        /// <summary>
        /// 建档人岗位类型
        /// </summary>
        [NoMapping]
        public string[] CustomerCreatorJobTypes { get; set; }

        /// <summary>
        /// 建档人姓名
        /// </summary>
        [NoMapping]
        public string CustomerCreatorName { get; set; }

        /// <summary>
        /// 建档日期
        /// </summary>
        [NoMapping]
        public DateTime CustomerCreateTimeStart { get; set; }
        [NoMapping]
        public DateTime CustomerCreateTimeEnd { get; set; }

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