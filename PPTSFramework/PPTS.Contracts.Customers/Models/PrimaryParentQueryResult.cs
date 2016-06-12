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
    public class PrimaryParentQueryResult
    {
        /// <summary>
        /// 主监护人信息
        /// </summary>
        [DataMember]
        public Parent Parent { set; get; }
    }
}
