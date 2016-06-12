﻿using PPTS.ExtServices.UnionPay.Validation;
using System;

namespace PPTS.ExtServices.UnionPay.Models.Statement
{
    [Serializable]
    public class StatementModel
    {
        ///// <summary>
        ///// 交易日期
        ///// </summary>
        //[Validate(ValidateType.IsMiniDate, ErrorMessage = "交易日期不正确")]
        //public DateTime SaleDate
        //{ get; set; }

        ///// <summary>
        ///// 刷卡交易时间
        ///// </summary>
        //[Validate(ValidateType.IsMiniDate, ErrorMessage = "刷卡交易时间不正确")]
        //public DateTime SaleTime
        //{ get; set; }

        /// <summary>
        /// 清算日期
        /// </summary>
        [Validate(ValidateType.IsMiniDate, ErrorMessage = "清算日期不正确")]
        public DateTime LiquidationDate
        { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        [Validate(ValidateType.IsMiniDate,ErrorMessage = "交易时间不正确")]
        public DateTime TransactionTime
        { get; set; }

        /// <summary>
        /// 参考号
        /// </summary>
        [Validate(ValidateType.NullOrEmpty,ErrorMessage = "参考号为空")]
        public string RefNum
        { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [Validate(ValidateType.NullOrEmpty,ErrorMessage = "卡号不能为空")]
        public string CardNumber
        { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [Validate(ValidateType.NullOrEmpty, ErrorMessage = "商户号为空")]
        public string MerchantNumber
        { get; set; }

        /// <summary>
        /// 终端号
        /// </summary>
        [Validate(ValidateType.NullOrEmpty, ErrorMessage = "终端号不能为空")]
        public string TerminalNo
        { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        [Validate(ValidateType.IsDecimal, RegexContent = @"^[-+]?(([0-9]+)|([0-9]+\.[0-9]{1,2}))$", ErrorMessage = "交易金额的格式不正确")]
        [Validate(ValidateType.IsLessEqualZero, ErrorMessage = "交易金额应大于零")]
        public decimal Amount
        { get; set; }
        

        
    }
}