using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Authorization
{
    /// <summary>
    /// 关系客户字段ID别名
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class CustomerFieldMappingAttribute : Attribute
    {
        public CustomerFieldMappingAttribute(string name)
        { Name = name; }
        /// <summary>
        /// 映射名称
        /// </summary>
        public string Name
        { get; set; }
    }
}
