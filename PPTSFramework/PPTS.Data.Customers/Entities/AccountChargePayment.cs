using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a AccountChargePayment.
    /// 账户缴费支付单表
    /// </summary>
    [Serializable]
    [ORTableMapping("AccountChargePayments")]
    [DataContract]
    public class AccountChargePayment
    {
        public AccountChargePayment()
        {
        }

        /// <summary>
        /// 申请单ID
        /// </summary>
        [ORFieldMapping("ApplyID")]
        [DataMember]
        public string ApplyID
        {
            get;
            set;
        }

        /// <summary>
        /// 支付单ID
        /// </summary>
        [ORFieldMapping("PayID", PrimaryKey = true)]
        [DataMember]
        public string PayID
        {
            get;
            set;
        }

        /// <summary>
        /// 支付单号
        /// </summary>
        [ORFieldMapping("PayNo")]
        [DataMember]
        public string PayNo
        {
            get;
            set;
        }

        /// <summary>
        /// 支付时间
        /// </summary>
        [ORFieldMapping("PayTime")]
        [DataMember]
        public DateTime PayTime
        {
            get;
            set;
        }

        /// <summary>
        /// 支付方式（1--Cash[现金],2--Telex[电汇],3--POS[POS刷卡],4--Cheque[支票],5--OtherPOS[其他Pos刷卡],6--ICBC[工行pos刷卡],7--BalancePPTS[账户余额[PPTS]老账户],9--Tmall[天猫短信码],11--VouchersCode[代金券],12--UnionpayPos[银联封顶pos刷卡],13--UnionPayNotButtCapPos[银联普通封顶],14--UnionPayNotButtCapIsNotPos[银联不封顶],15--FastMoneyNotButtCapPos[快钱普通封顶],16--FastMoneyNotButtCapIsNot[快钱不封顶]）
        /// </summary>
        [ORFieldMapping("PayType")]
        [DataMember]
        public string PayType
        {
            get;
            set;
        }

        /// <summary>
        /// 支付状态（未支付，已支付，已失败）
        /// </summary>
        [ORFieldMapping("PayStatus")]
        [DataMember]
        public string PayStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 支付金额
        /// </summary>
        [ORFieldMapping("PayMoney")]
        [DataMember]
        public decimal PayMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 支付说明
        /// </summary>
        [ORFieldMapping("PayMemo")]
        [DataMember]
        public string PayMemo
        {
            get;
            set;
        }

        /// <summary>
        /// 付款单位名称（电汇或支票）
        /// </summary>
        [ORFieldMapping("PayCorp")]
        [DataMember]
        public string PayCorp
        {
            get;
            set;
        }

        /// <summary>
        /// 电汇号（电汇）
        /// </summary>
        [ORFieldMapping("TelexNo")]
        [DataMember]
        public string TelexNo
        {
            get;
            set;
        }

        /// <summary>
        /// 支票号（支票）
        /// </summary>
        [ORFieldMapping("ChequeNo")]
        [DataMember]
        public string ChequeNo
        {
            get;
            set;
        }

        /// <summary>
        /// 刷卡交易参考号（刷卡）
        /// </summary>
        [ORFieldMapping("CardReceiptNo")]
        [DataMember]
        public string CardReceiptNo
        {
            get;
            set;
        }

        /// <summary>
        /// 天猫短信码（天猫）
        /// </summary>
        [ORFieldMapping("TmallSMSNo")]
        [DataMember]
        public string TmallSMSNo
        {
            get;
            set;
        }

        /// <summary>
        /// 手机后四位（天猫）
        /// </summary>
        [ORFieldMapping("PhoneTail4")]
        [DataMember]
        public string PhoneTail4
        {
            get;
            set;
        }

        /// <summary>
        /// 代金券
        /// </summary>
        [ORFieldMapping("CashCoupon")]
        [DataMember]
        public string CashCoupon
        {
            get;
            set;
        }

        /// <summary>
        /// 回款人
        /// </summary>
        [ORFieldMapping("Salesman")]
        [DataMember]
        public string Salesman
        {
            get;
            set;
        }

        /// <summary>
        /// 付款人（默认学员姓名）
        /// </summary>
        [ORFieldMapping("Payer")]
        [DataMember]
        public string Payer
        {
            get;
            set;
        }

        /// <summary>
        /// 收款人ID
        /// </summary>
        [ORFieldMapping("PayeeID")]
        [DataMember]
        public string PayeeID
        {
            get;
            set;
        }

        /// <summary>
        /// 收款人姓名
        /// </summary>
        [ORFieldMapping("PayeeName")]
        [DataMember]
        public string PayeeName
        {
            get;
            set;
        }

        /// <summary>
        /// 收款人岗位ID
        /// </summary>
        [ORFieldMapping("PayeeJobID")]
        [DataMember]
        public string PayeeJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 收款人岗位名称
        /// </summary>
        [ORFieldMapping("PayeeJobName")]
        [DataMember]
        public string PayeeJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 打印次数
        /// </summary>
        [ORFieldMapping("PrintCount")]
        [DataMember]
        public int PrintCount
        {
            get;
            set;
        }

        /// <summary>
        /// 是否对账
        /// </summary>
        [ORFieldMapping("Checked")]
        [DataMember]
        public int Checked
        {
            get;
            set;
        }

        /// <summary>
        /// 对账日期
        /// </summary>
        [ORFieldMapping("CheckTime")]
        [DataMember]
        public DateTime CheckTime
        {
            get;
            set;
        }

        /// <summary>
        /// 对账人ID
        /// </summary>
        [ORFieldMapping("CheckerID")]
        [DataMember]
        public string CheckerID
        {
            get;
            set;
        }

        /// <summary>
        /// 对账人姓名
        /// </summary>
        [ORFieldMapping("CheckerName")]
        [DataMember]
        public string CheckerName
        {
            get;
            set;
        }

        /// <summary>
        /// 对账人岗位ID
        /// </summary>
        [ORFieldMapping("CheckerJobID")]
        [DataMember]
        public string CheckerJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 对账人岗位姓名
        /// </summary>
        [ORFieldMapping("CheckerJobName")]
        [DataMember]
        public string CheckerJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [ORFieldMapping("CreatorName")]
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime")]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [ORFieldMapping("ModifierID")]
        [DataMember]
        public string ModifierID
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改人姓名
        /// </summary>
        [ORFieldMapping("ModifierName")]
        [DataMember]
        public string ModifierName
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [ORFieldMapping("ModifyTime")]
        [DataMember]
        public DateTime ModifyTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class AccountChargePaymentCollection : EditableDataObjectCollectionBase<AccountChargePayment>
    {
    }
}