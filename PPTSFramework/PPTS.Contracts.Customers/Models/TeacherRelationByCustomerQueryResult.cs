using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Models
{
    /// <summary>
    /// 按学生ID获取学员教师关系信息
    /// </summary>
    [DataContract]
    public class TeacherRelationByCustomerQueryResult
    {
        /// <summary>
        /// 教师信息集合
        /// </summary>
        [DataMember]
        public List<TeacherJobModel> TeacherJobCollection { get; set; }

        /// <summary>
        /// 学员信息结合
        /// </summary>
        [DataMember]
        public Customer Customer { get; set; }
    }
}
