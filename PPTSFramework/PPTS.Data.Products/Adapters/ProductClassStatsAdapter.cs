using MCS.Library.Data.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Adapters
{
    public class ProductClassStatsAdapter : ProductAdapterBase<ProductClassStat, ProductClassStatCollection>
    {
        public  static ProductClassStatsAdapter Instance = new ProductClassStatsAdapter();

    }
}
