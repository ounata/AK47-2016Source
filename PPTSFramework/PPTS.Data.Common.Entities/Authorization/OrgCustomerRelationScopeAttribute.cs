using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Authorization
{
    /// <summary>
    /// 具有关系的组织机构权限范围属性配置
    /// </summary>
    public class OrgCustomerRelationScopeAttribute : RecordOrgScopeAttribute
    {
        /// <summary>
        /// 授权关系类型
        /// </summary>
        public RelationType RelationType { get; set; }

        /// <summary>
        /// 被授权记录类型
        /// </summary>
        public new CustomerRecordType RecordType
        {
            get
            {
                return (CustomerRecordType)Enum.Parse(typeof(CustomerRecordType), ((int)base.RecordType).ToString());
            }
            set { base.RecordType = (RecordType)Enum.Parse(typeof(RecordType), ((int)value).ToString()); }
        }
    }
}
