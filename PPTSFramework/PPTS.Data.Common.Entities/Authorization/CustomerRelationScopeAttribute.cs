using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Authorization
{
    /// <summary>
    /// 客户关系类权限范围属性配置
    /// </summary>
    public class CustomerRelationScopeAttribute : ScopeBaseAttribute
    {
        /// <summary>
        /// 授权关系类型
        /// </summary>
        public RelationType RelationType { get; set; }

        /// <summary>
        /// 被授权对象类型
        /// </summary>
        public CustomerRecordType RecordType { get; set; }
    }
}
