using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Entities
{
    [Serializable]
    [ORTableMapping("WF.WFJobConfig")]
    [DataContract]
    public class WFJobConfig
    {
        public WFJobConfig()
        {
        }

        /// <summary>
        /// 主键
        /// </summary>
        [ORFieldMapping("JobConfigID", PrimaryKey = true)]
        [DataMember]
        public string JobConfigID
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流Key-优先级1
        /// </summary>
        [ORFieldMapping("ProcessKey")]
        [DataMember]
        public string ProcessKey
        {
            get;
            set;
        }

        /// <summary>
        /// 机构ID--记录了生效的机构范围--优先级2
        /// </summary>
        [ORFieldMapping("OrgID")]
        [DataMember]
        public string OrgID
        {
            get;
            set;
        }

        /// <summary>
        /// 提交人岗位名称--优先级3
        /// </summary>
        [ORFieldMapping("JobName")]
        [DataMember]
        public string JobName
        {
            get;
            set;
        }

        /// <summary>
        /// 岗位ID
        /// </summary>
        [ORFieldMapping("JobID")]
        [DataMember]
        public string JobID
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
    public class WFJobConfigCollection : EditableDataObjectCollectionBase<WFJobConfig>
    {

    }
}
