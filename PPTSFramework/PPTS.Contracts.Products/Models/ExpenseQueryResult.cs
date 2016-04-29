using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Products.Models
{
    /// <summary>
    /// 服务费数据契约对象信息
    /// </summary>
    [DataContract]
    public class ExpenseQueryResult
    {
        /// <summary>
        /// 服务费头信息
        /// </summary>
        [DataMember]
        public Expense Expense { get; set; }

    }
}
