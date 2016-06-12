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

    [DataContract]
    public class CategoryQueryResult
    {
        /// <summary>
        /// 2级 分类列表
        /// </summary>
        [DataMember]
        public IEnumerable<CategoryEntity> CategoryCollection { set; get; }
    }
}
