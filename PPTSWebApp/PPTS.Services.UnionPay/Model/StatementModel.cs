using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Services.UnionPay.Description;

namespace PPTS.Services.UnionPay.Model
{
    [Serializable]
    public class StatementModel
    {
        [Description(Name = "清算日期", Index = 1)]
        public DateTime LiquidationDate
        { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        [Description(Name = "交易时间", Index = 2)]
        public DateTime TransactionTime
        { get; set; }

        /// <summary>
        /// 参考号
        /// </summary>
        [Description(Name = "参考号", Index = 3)]
        public string RefNum
        { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [Description(Name = "卡号", Index = 4)]
        public string CardNumber
        { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [Description(Name = "商户号", Index = 0)]
        public string MerchantNumber
        { get; set; }

        /// <summary>
        /// 终端号
        /// </summary>
        [Description(Name = "终端号", Index = 5)]
        public string TerminalNo
        { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        [Description(Name = "交易金额", Index = 6)]
        public decimal Amount
        { get; set; }
    }
}