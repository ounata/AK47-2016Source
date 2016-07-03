using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    [Serializable]
    public class FinancialRefund
    {
        /// <summary>
        /// 申请ID
        /// </summary>
        public string ApplyID
        {
            get;
            set;
        }
        /// <summary>
        /// 分公司ID
        /// </summary>
        public string BranchID
        {
            get;
            set;
        }
        /// <summary>
        /// 分公司名称
        /// </summary>
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
        /// 财务最后确认时间
        /// </summary>
        [ORFieldMapping("VerifyTime")]
        public DateTime VerifyTime
        {
            get;
            set;
        }
        /// <summary>
        /// 退费类型（0-正常退费，1-坏账退费）
        /// </summary>
        [ORFieldMapping("RefundType")]
        public string RefundType
        {
            get;
            set;
        }
        /// <summary>
        /// 申请单号
        /// </summary>
        [ORFieldMapping("ApplyNo")]
        public string ApplyNo
        {
            get;
            set;
        }
        /// <summary>
        /// 实退金额
        /// </summary>
        [ORFieldMapping("RealRefundMoney")]
        public string RealRefundMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 领款人（默认是学员姓名）
        /// </summary>
        [ORFieldMapping("Drawer")]
        public string Drawer
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
    public class FinancialRefundCollection : EditableDataObjectCollectionBase<FinancialRefund>
    { }
}
