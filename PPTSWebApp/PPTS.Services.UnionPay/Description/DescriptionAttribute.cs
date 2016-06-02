using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.Services.UnionPay.Description
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
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