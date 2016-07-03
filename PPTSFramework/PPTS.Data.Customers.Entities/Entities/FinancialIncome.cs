using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    [Serializable]
    public class FinancialIncome
    {
        public string BranchID
        {
            get;
            set;
        }
        public string BranchName
        {
            get;
            set;
        }
        /// <summary>
        /// 校区ID
        /// </summary>
        [ORFieldMapping("CampusID")]
        public string CampusID
        {
            get;
            set;
        }
        /// <summary>
        /// 校区名称
        /// </summary>
        [ORFieldMapping("CampusName")]
        public string CampusName
        {
            get;
            set;
        }
        /// <summary>
        /// 支付时间
        /// 收款日期
        /// </summary>
        [ORFieldMapping("PayTime")]
        public DateTime PayTime
        {
            get;
            set;
        }
        /// <summary>
        /// 申请单号
        /// 收款单据号
        /// </summary>
        [ORFieldMapping("ApplyNo")]
        public string ApplyNo
        {
            get;
            set;
        }
        /// <summary>
        /// 缴费类型
        /// 充值类型
        /// </summary>
        [ORFieldMapping("ChargeType")]
        public string ChargeType
        {
            get;
            set;
        }
        /// <summary>
        /// 支付类型
        /// 收款类型
        /// </summary>
        [ORFieldMapping("PayType")]
        public string PayType
        {
            get;
            set;
        }
        /// <summary>
        /// 支付金额
        /// 收款金额
        /// </summary>
        [ORFieldMapping("PayMoney")]
        public decimal PayMoney
        {
            get;
            set;
        }
        
        /// <summary>
        /// 支付单ID
        /// </summary>
        [ORFieldMapping("PayID")]
        public string PayID
        {
            get;
            set;
        }
        /// <summary>
        /// 学员编码
        /// </summary>
        [ORFieldMapping("CustomerCode")]
        public string CustomerCode
        {
            get;
            set;
        }
        /// <summary>
        /// 学员姓名
        /// </summary>
        [ORFieldMapping("CustomerName")]
        public string CustomerName
        {
            get;
            set;
        }
    }
    [Serializable]
    public class FinancialIncomeCollection : EditableDataObjectCollectionBase<FinancialIncome>
    { }
}
