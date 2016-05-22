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
    /// 教师岗位信息
    /// </summary>
    [DataContract]
    public class CustomerRelationByTeacherQueryResult
    {
        /// <summary>
        /// 学员信息集合
        /// </summary>
        [DataMember]
        public List<Customer> CustomerCollection { get; set; }

        /// <summary>
        /// 教师岗位信息
        /// </summary>
        [DataMember]
        public TeacherJobModel TeacherJob { get; set; }
    }
}
