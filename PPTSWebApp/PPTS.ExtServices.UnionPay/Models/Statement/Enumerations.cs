using MCS.Library.Core;

namespace PPTS.ExtServices.UnionPay.Models.Statement
{
    public enum UnionPaySourceType
    {
        /// <summary>
        /// 1--接口(实时接口)来源
        /// </summary>
        Sync = 1,

        /// <summary>
        /// 2--对账(异步接口)来源
        /// </summary>
        Async = 2
    }

    /// <summary>
    /// POS交易类型，银联（1）通联（4）
    /// </summary>
    public enum POSTransactionType
    {
        /// <summary>
        /// 银联
        /// </summary>
        [EnumItemDescription("银联")]
        UnionPay = 1,

        /// <summary>
        /// 通联
        /// </summary>
        [EnumItemDescription("通联")]
        AllInPay = 4
    }
}