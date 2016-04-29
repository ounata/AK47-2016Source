using PPTS.Data.Products.Entities;
using MCS.Library.Data.Adapters;
using System.Collections.Generic;
using System;

namespace PPTS.Data.Products.Adapters
{
    public class ProductViewAdapter : UpdatableAndLoadableAdapterBase<ProductView, ProductViewCollection>
    {
        public static ProductViewAdapter Instance = new ProductViewAdapter();

        public void LoadByProductIDInContext(string productId, Action<ProductViewCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("ProductID", productId)), action);
        }


        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSProductConnectionName;
        }
    }
}
