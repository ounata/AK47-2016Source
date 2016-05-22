using MCS.Library.Data.DataObjects;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 缴费支付单模型
    /// </summary>
    [Serializable]
    public class ChargePaymentQueryModel : AccountChargePayment
    {
        /// <summary>
        /// 校区ID
        /// </summary>
        [DataMember]
        public string CampusID
        {
            set;
            get;
        }

        /// <summary>
        /// 校区名称
        /// </summary>
        [DataMember]
        public string CampusName
        {
            set;
            get;
        }

        /// <summary>
        /// 学员ID
        /// </summary>
        [DataMember]
        public string CustomerID
        {
            set;
            get;
        }

        /// <summary>
        /// 学员编号
        /// </summary>
        [DataMember]
        public string CustomerCode
        {
            set;
            get;
        }

        /// <summary>
        /// 学员姓名
        /// </summary>
        [DataMember]
        public string CustomerName
        {
            set;
            get;
        }

        /// <summary>
        /// 缴费单号
        /// </summary>
        [DataMember]
        public string ApplyNo
        {
            set;
            get;
        }

        /// <summary>
        /// 充值类型
        /// </summary>
        [DataMember]
        [ConstantCategory("Account_ChargeType")]
        public string ChargeType
        {
            set;
            get;
        }

        /// <summary>
        /// 充值金额
        /// </summary>
        [DataMember]
        public decimal ChargeMoney
        {
            set;
            get;
        }

        /// <summary>
        /// 是否能对账
        /// </summary>
        [DataMember]
        public bool CanCheck
        {
            get
            {
                return this.PayStatus == PayStatusDefine.Paid
                    && this.CheckStatus == CheckStatusDefine.UnCheck;
            }
        }
    }

    [Serializable]
    public class ChargePaymentQueryModelCollection : EditableDataObjectCollectionBase<ChargePaymentQueryModel>
    {

    }
}