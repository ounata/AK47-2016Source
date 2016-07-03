using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Authorization
{
    /// <summary>
    /// 所有者关系权限范围标识
    /// </summary>
    public class OwnerRelationScopeAttribute : ScopeBaseAttribute
    {
        //private RelationType relationType = RelationType.Owner;
        /// <summary>
        /// 授权关系类型，默认所有者
        /// </summary>
        public RelationType RelationType
        {
            get;//{ return relationType; }
            set;//{ relationType = value; }
        }

        /// <summary>
        /// 被授权记录类型
        /// </summary>
        public RecordType RecordType
        { get; set; }
    }
}
