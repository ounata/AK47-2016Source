using System;

namespace PPTS.ExtServices.UnionPay.Validation
{
    public class ValidateModel
    {/// <summary>
     /// 验证类型
     /// </summary>
        public ValidateType Type { get; set; }

        /// <summary>
        /// 验证函数
        /// </summary>
        public Func<bool> CheckFunc { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}