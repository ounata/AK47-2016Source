using System;

namespace PPTS.ExtServices.UnionPay.Validation
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute() { }

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name
        { get; set; }

        /// <summary>
        /// 索引
        /// </summary>
        public int Index
        { get; set; }
    }
}