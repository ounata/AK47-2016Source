using System;

namespace PPTS.ExtServices.UnionPay.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ValidateAttribute : Attribute
    {
        public ValidateAttribute(ValidateType validateType)
        {
            ValidateType = validateType;
        }
        /// <summary>
        /// 验证类型
        /// </summary>
        public ValidateType ValidateType { get; }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string RegexContent { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 日期时间格式
        /// </summary>
        public string DateFormat { get; set; }
    }
}