using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Authorization
{
    /// <summary>
    /// 具有记录的组织机构权限范围属性配置
    /// </summary>
    public class RecordOrgScopeAttribute : ScopeBaseAttribute
    {
        /// <summary>
        /// 授权机构类型
        /// </summary>
        public OrgType OrgType { get; set; }
        
        /// <summary>
        /// 授权关系类型
        /// </summary>
        public RelationType RelationType { get; set; }

        /// <summary>
        /// 被授权记录类型
        /// </summary>
        public virtual RecordType RecordType { get; set; }
    }
}
