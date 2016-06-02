using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
    [Serializable]
    [ORTableMapping("SM.TeacherSearch")]
    [DataContract]
    public class TeacherSearch
    {
        public TeacherSearch()
        { }

        /// <summary>
        /// 学员ID
        /// </summary>
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
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
        /// 教师岗位ID
        /// </summary>
        [ORFieldMapping("TeacherJobID")]
        [DataMember]
        public string TeacherJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 教师岗位所属学科组ID
        /// </summary>
        [ORFieldMapping("TeacherJobOrgID")]
        [DataMember]
        public string TeacherJobOrgID
        {
            get;
            set;
        }

        /// <summary>
        /// 教师岗位所属学科组名称
        /// </summary>
        [ORFieldMapping("TeacherJobOrgName")]
        [DataMember]
        public string TeacherJobOrgName
        {
            get;
            set;
        }

        /// <summary>
        /// 教授科目代码
        /// </summary>
        [ORFieldMapping("Subject")]
        [DataMember]
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// 教授科目名称
        /// </summary>
        [ORFieldMapping("SubjectName")]
        [DataMember]
        public string SubjectName
        {
            get;
            set;
        }
    }



    [Serializable]
    [DataContract]
    public class TeacherSearchCollection : EditableDataObjectCollectionBase<TeacherSearch>
    {
    }
}
