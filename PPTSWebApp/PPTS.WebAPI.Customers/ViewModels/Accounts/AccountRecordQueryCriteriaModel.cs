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
    /// 账户日志列表查询条件
    /// </summary>
    [Serializable]
    public class AccountRecordQueryCriteriaModel
    {
        /// <summary>
        /// 学员ID
        /// </summary>
        [ConditionMapping("CustomerID", Operation = "=")]
        public string CustomerID { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        [InConditionMapping("RecordType")]
        public string[] RecordTypes
        {
            get
            {
                List<string> list = new List<string>();
                if (this.IncomeExpend != null)
                    list.AddRange(this.IncomeExpend);
                if (this.FrozenRelease != null)
                    list.AddRange(this.FrozenRelease);
                return list.ToArray();
            }
        }

        /// <summary>
        /// 收入支出
        /// </summary>
        [NoMapping]
        public string[] IncomeExpend { get; set; }

        /// <summary>
        /// 冻结解冻
        /// </summary>
        [NoMapping]
        public string[] FrozenRelease { get; set; }
        
        /// <summary>
        /// 业务日期
        /// </summary>
        [ConditionMapping("BillTime", UtcTimeToLocal = true, Operation = ">=")]
        public DateTime BillTimeStart { get; set; }
        [ConditionMapping("BillTime", UtcTimeToLocal = true, Operation = "<", AdjustDays = 1)]
        public DateTime BillTimeEnd { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [ConditionMapping("BillMoney", UtcTimeToLocal = true, Operation = ">=")]
        public decimal BillMoneyStart { get; set; }
        [ConditionMapping("BillMoney", UtcTimeToLocal = true, Operation = "<", AdjustDays = 1)]
        public decimal BillMoneyEnd { get; set; }
        
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