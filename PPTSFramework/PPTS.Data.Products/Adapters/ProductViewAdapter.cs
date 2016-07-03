using PPTS.Data.Products.Entities;
using MCS.Library.Data.Adapters;
using System.Collections.Generic;
using System;
using MCS.Library.Data.Builder;
using MCS.Library.Core;

namespace PPTS.Data.Products.Adapters
{
    public class ProductViewAdapter : UpdatableAndLoadableAdapterBase<ProductView, ProductViewCollection>
    {
        public static ProductViewAdapter Instance = new ProductViewAdapter();

        public string TableName { get { return GetTableName(); } }

        public void LoadByProductIDInContext(string productId, Action<ProductViewCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("ProductID", productId)), action);
        }

        public void LoadByProductIDInContext(string productId,string[] campusIds, Action<ProductViewCollection> action)
        {
            if(campusIds == null) { LoadByProductIDInContext(productId, action); return; }
            var sql = ProductPermissionAdapter.Instance.GetExsitsByCampusIdsSQL(productId, campusIds);
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("ProductID", productId).AppendItem("exists", sql, "", true)), action);

        }


        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSProductConnectionName;
        }
    }
}
