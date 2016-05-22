using MCS.Library.Data.Mapping;
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
    /// 折扣表数据契约对象信息
    /// </summary>
    [DataContract]
    public class DiscountQueryResult
    {
        [DataMember]
        public Discount Discount { get; set; }

        /// <summary>
        /// 折扣表明细数据契约对象信息集合
        /// </summary>
        [DataMember]
        public List<DiscountItem> DiscountItemCollection { get; set; }
    }
}
