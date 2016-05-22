using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Models
{
    /// <summary>
    /// 教师岗位信息集合
    /// </summary>
    [DataContract]
    public class TeacherJobModel
    {
        /// <summary>
        /// 教师岗位信息
        /// </summary>
        [DataMember]
        public TeacherJobView TeacherJob { get; set; }

        /// <summary>
        /// 教师教授科目集合
        /// </summary>
        [DataMember]
        public List<TeacherTeaching> TeacherTeachingCollection
        { get; set; }
    }
}
