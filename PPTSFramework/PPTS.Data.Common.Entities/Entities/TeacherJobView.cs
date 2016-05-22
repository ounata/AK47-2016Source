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
    [ORTableMapping("MT.v_TeacherJobs")]
    [DataContract]
	public class TeacherJobView
	{		
		public TeacherJobView()
		{
		}		

		/// <summary>
		/// 岗位ID
		/// </summary>
		[ORFieldMapping("JobID", PrimaryKey=true)]
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
        /// 教师编码
        /// </summary>
        [ORFieldMapping("TeacherCode")]
        [DataMember]
        public string TeacherCode
        {
            get;
            set;
        }

        /// <summary>
        /// 教师姓名
        /// </summary>
        [ORFieldMapping("TeacherName")]
        [DataMember]
        public string TeacherName
        {
            get;
            set;
        }

        /// <summary>
        /// 教师OA编码
        /// </summary>
        [ORFieldMapping("TeacherOACode")]
        [DataMember]
        public string TeacherOACode
        {
            get;
            set;
        }

        /// <summary>
        /// 性别
        /// </summary>
        /// 
        [ConstantCategory("C_CODE_ABBR_GENDER")]
        [ORFieldMapping("Gender")]
        [DataMember]
        public string Gender
        {
            get;
            set;
        }

        /// <summary>
        /// 出生日期
        /// </summary>
        [ORFieldMapping("Birthday")]
        [DataMember]
        public DateTime Birthday
        {
            get;
            set;
        }

        /// <summary>
        /// 教授年级（用逗号分割名称）
        /// </summary>
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_GRADE")]
        [ORFieldMapping("GradeMemo")]
        [DataMember]
        public string GradeMemo
        {
            get;
            set;
        }

        /// <summary>
        /// 教授科目（用逗号分割名称）
        /// </summary>
        [ConstantCategory("C_CODE_ABBR_BO_Product_TeacherSubject")]
        [ORFieldMapping("SubjectMemo")]
        [DataMember]
        public string SubjectMemo
        {
            get;
            set;
        }

        /// <summary>
        /// 是否全职
        /// </summary>
        /// 
        [ConstantCategory("Common_TeacherType")]
        [ORFieldMapping("IsFullTime")]
        [DataMember]
		public int IsFullTime
		{
			get;
            set;
		}

        [ORFieldMapping("JobStatus")]
        [DataMember]
        public string JobStatus { get; set; }

    }

    [Serializable]
    [DataContract]
    public class TeacherJobViewCollection : EditableDataObjectCollectionBase<TeacherJobView>
    {
    }
}