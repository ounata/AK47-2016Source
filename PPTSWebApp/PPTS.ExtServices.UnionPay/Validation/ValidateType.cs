using System;

namespace PPTS.ExtServices.UnionPay.Validation
{
    [Flags]
    public enum ValidateType
    {
        /// <summary>
        /// 是否时null或Empty
        /// </summary>
        NullOrEmpty = 0,
        /// <summary>
        /// 是否是最小日期
        /// </summary>
        IsMiniDate = 1,
        /// <summary>
        /// 是否是decimal
        /// </summary>
        IsDecimal = 2,
        /// <summary>
        /// 数字是零
        /// </summary>
        IsLessEqualZero = 4
    }
}