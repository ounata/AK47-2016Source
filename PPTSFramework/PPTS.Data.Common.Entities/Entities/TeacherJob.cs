using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Common.Entities
{
    /// <summary>
    /// This object represents the properties and methods of a TeacherJob.
    /// 教师岗位表
    /// </summary>
    [Serializable]
    [ORTableMapping("MT.TeacherJobs")]
    [DataContract]
    public class TeacherJob
    {
        public TeacherJob()
        {
        }

        /// <summary>
        /// 岗位ID
        /// </summary>
        [ORFieldMapping("JobID", PrimaryKey = true)]
        [DataMember]
        public string JobID
        {
            get;
            set;
        }

        /// <summary>
        /// 岗位名称
        /// </summary>
        [ORFieldMapping("JobName")]
        [DataMember]
        public string JobName
        {
            get;
            set;
        }

        /// <summary>
        /// 岗位组织机构ID
        /// </summary>
        [ORFieldMapping("JobOrgID")]
        [DataMember]
        public string JobOrgID
        {
            get;
            set;
        }

        /// <summary>
        /// 岗位组织机构名称
        /// </summary>
        [ORFieldMapping("JobOrgName")]
        [DataMember]
        public string JobOrgName
        {
            get;
            set;
        }

        /// <summary>
        /// 岗位组织机构简称
        /// </summary>
        [ORFieldMapping("JobOrgShortName")]
        [DataMember]
        public string JobOrgShortName
        {
            get;
            set;
        }


        /// <summary>
        /// 岗位组织机构类型
        /// </summary>
        [ORFieldMapping("JobOrgType")]
        [DataMember]
        public OrgTypeDefine JobOrgType
        {
            get;
            set;
        }

        /// <summary>
        /// 岗位状态
        /// </summary>
        [ORFieldMapping("JobStatus")]
        [DataMember]
        public JobStatusDefine JobStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 教师ID
        /// </summary>
        [ORFieldMapping("TeacherID")]
        [DataMember]
        public string TeacherID
        {
            get;
            set;
        }

        /// <summary>
        /// 校区ID
        /// </summary>
        [ORFieldMapping("CampusID")]
        [DataMember]
        public string CampusID
        {
            get;
            set;
        }

        /// <summary>
        /// 校区名称
        /// </summary>
        [ORFieldMapping("CampusName")]
        [DataMember]
        public string CampusName
        {
            get;
            set;
        }

        /// <summary>
        /// 是否全职
        /// </summary>
        [ORFieldMapping("IsFullTime")]
        [DataMember]
        public bool IsFullTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class TeacherJobCollection : EditableDataObjectCollectionBase<TeacherJob>
    {
    }
}