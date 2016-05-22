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
    /// 服务费扣减情况信息
    /// </summary>
    [DataContract]
    public class CustomerExpenseCollectionQueryResult
    {
        /// <summary>
        /// 综合服务费扣减集合
        /// </summary>
        [DataMember]
        public List<CustomerExpenseRelation> CustomerExpenseRelationCollection { get; set; }
    }
}
