using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Models
{
    [DataContract]
    public class CustomerQueryResult
    {
        /// <summary>
        /// 学员信息
        /// </summary>
        [DataMember]
        public Customer Customer { get; set; }

        /// <summary>
        /// 学员员工关系信息
        /// </summary>
        [DataMember]
        public List<CustomerStaffRelation> CustomerStaffRelationCollection { get; set; }
    }
}
