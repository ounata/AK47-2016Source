using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Authorization
{
    /// <summary>
    /// 表单类型特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityAuthAttribute : Attribute
    {
        /// <summary>
        /// 授权表单类型(应用于表结构选择)
        /// </summary>
        public RecordType RecordType
        {
            get; set;
        }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description
        { get; set; }
    }
}
