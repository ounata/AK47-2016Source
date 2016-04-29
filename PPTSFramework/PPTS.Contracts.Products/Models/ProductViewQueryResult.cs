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
    /// 产品视图数据契约对象信息
    /// </summary>
    [DataContract]
    public class ProductViewQueryResult
    {
        public List<ProductView> ProductViews
        { get; set; }
    }
}
