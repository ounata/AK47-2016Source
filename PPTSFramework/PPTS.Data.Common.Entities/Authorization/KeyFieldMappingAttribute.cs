using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Authorization
{
    /// <summary>
    /// 主键映射字段名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class KeyFieldMappingAttribute : Attribute
    {
        public KeyFieldMappingAttribute(string name)
        {
            Name = name;
        }
        /// <summary>
        /// 映射名称
        /// </summary>
        public string Name
        { get; set; }
    }
}
