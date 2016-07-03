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
    /// 缴费支付列表查询
    /// </summary>
    [Serializable]
    public class ChargePaymentQueryCriteriaModel
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
        /// 充值申请人
        /// </summary>
        [ConditionMapping("a.ApplierName", Operation = "=")]
        public string ApplierName { get; set; }

        /// <summary>
        /// 付款日期
        /// </summary>
        [ConditionMapping("a.PayTime", UtcTimeToLocal = true, Operation = ">=")]
        public DateTime PayTimeStart { get; set; }
        [ConditionMapping("a.PayTime", UtcTimeToLocal = true, Operation = "<", AdjustDays = 1)]
        public DateTime PayTimeEnd { get; set; }

        /// <summary>
        /// 充值类型
        /// </summary>
        [InConditionMapping("a.ChargeType")]
        public string[] ChargeTypes
        {
            get;
            set;
        }

        /// <summary>
        /// 支付状态
        /// </summary>
        [InConditionMapping("b.PayStatus")]
        public string[] PayStatuses
        {
            get
            {
                return new string[] { ((int)PayStatusDefine.Paid).ToString() };
            }
        }

        /// <summary>
        /// 支付类型
        /// </summary>
        [InConditionMapping("b.PayType")]
        public string[] PayTypes { get; set; }

        /// <summary>
        /// 对账状态
        /// </summary>
        [InConditionMapping("b.CheckStatus")]
        public string[] CheckStatuses { get; set; }

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