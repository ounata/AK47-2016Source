using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Entities
{
    [Serializable]
    [ORTableMapping("WF.WFRelationConfig")]
    [DataContract]
    public class WFRelationConfig
    {
        public WFRelationConfig()
        {
        }

        /// <summary>
        /// 主键
        /// </summary>
        [ORFieldMapping("RelationConfigID", PrimaryKey = true)]
        [DataMember]
        public string RelationConfigID
        {
            get;
            set;
        }

        /// <summary>
        /// 应用名称--工作流应用分类信息--优先级1
        /// </summary>
        [ORFieldMapping("ApplicationName")]
        [DataMember]
        public string ApplicationName
        {
            get;
            set;
        }

        /// <summary>
        /// 模块名称--工作流模块分类信息--优先级2
        /// </summary>
        [ORFieldMapping("ProgramName")]
        [DataMember]
        public string ProgramName
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流Key
        /// </summary>
        [ORFieldMapping("ProcessKey")]
        [DataMember]
        public string ProcessKey
        {
            get;
            set;
        }

        /// <summary>
        /// 机构ID-优先级3
        /// </summary>
        [ORFieldMapping("OrgID")]
        [DataMember]
        public string OrgID
        {
            get;
            set;
        }

        /// <summary>
        /// 机构类型,1--总部，2--分公司，3--校区，8--大区
        /// </summary>
        [ORFieldMapping("OrgType")]
        [DataMember]
        public DepartmentType OrgType
        {
            get; set;
        }

        /// <summary>
        /// 提交人岗位名称-优先级4
        /// </summary>
        [ORFieldMapping("JobName")]
        [DataMember]
        public string JobName
        {
            get;
            set;
        }

        /// <summary>
        /// 生效时间
        /// </summary>
        [ORFieldMapping("StartTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 失效时间
        /// </summary>
        [ORFieldMapping("EndTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime EndTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class WFRelationConfigCollection : EditableDataObjectCollectionBase<WFRelationConfig>
    {
    }
}
