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
    /// 买赠折扣返回集合对象
    /// </summary>
    [DataContract]
    public class PresentQueryResult
    {
        /// <summary>
        /// 买赠折扣主表
        /// </summary>
        [DataMember]
        public Present Present { get; set; }

        /// <summary>
        /// 买赠折扣明细对象信息集合
        /// </summary>
        [DataMember]
        public List<PresentItem> PresentItemCollection { get; set; }
    }
}
