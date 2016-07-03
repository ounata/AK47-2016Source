using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 退费支付列表查询条件
    /// </summary>
    [Serializable]
    public class RefundApplyQueryCriteriaModel
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
        /// 申请人岗位类型
        /// </summary>
        [InConditionMapping("a.ApplierJobType")]
        public string[] ApplierJobTypes { get; set; }

        /// <summary>
        /// 退款操作人姓名
        /// </summary>
        [ConditionMapping("a.ApplierName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string ApplierName { get; set; }

        /// <summary>
        /// 业务终审日期
        /// </summary>
        [ConditionMapping("a.ApproveTime", UtcTimeToLocal = true, Operation = ">=")]
        public DateTime ApproveTimeStart { get; set; }
        [ConditionMapping("ApplyTime", UtcTimeToLocal = true, Operation = "<", AdjustDays = 1)]
        public DateTime ApproveTimeEnd { get; set; }

        /// <summary>
        /// 财务终审日期
        /// </summary>
        [ConditionMapping("a.VerifyTime", UtcTimeToLocal = true, Operation = ">=")]
        public DateTime VerifyTimeStart { get; set; }
        [ConditionMapping("ApplyTime", UtcTimeToLocal = true, Operation = "<", AdjustDays = 1)]
        public DateTime VerifyTimeEnd { get; set; }

        /// <summary>
        /// 退款确认状态
        /// </summary>
        [InConditionMapping("a.VerifyStatus")]
        public string[] VerifyStatuses
        {
            set;
            get;
        }

        /// <summary>
        /// 对账状态
        /// </summary>
        [InConditionMapping("a.CheckStatus")]
        public string[] CheckStatuses { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        [InConditionMapping("a.ApplyStatus")]
        public string[] ApplyStatuses
        {
            get
            {
                return new string[] { ((int)ApplyStatusDefine.Approved).ToString() };
            }
        }

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