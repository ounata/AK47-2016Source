using MCS.Library.Data;
using PPTS.Data.Products.Entities;
using System.Collections.Generic;
using System.Linq;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using System;

namespace PPTS.Data.Products.Adapters
{
    public class GenericProductAdapter<T, TCollection> : ProductAdapterBase<T, TCollection>
        where T : Product
        where TCollection : IList<T>, new()
    {
        public static readonly GenericProductAdapter<T, TCollection> Instance = new GenericProductAdapter<T, TCollection>();


        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public T Load(string productID)
        {
            return this.Load(builder => builder.AppendItem("ProductID", productID)).SingleOrDefault();
        }

        public void LoadInContext(string productID, Action<T> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(productID), "ProductID"),
                collection => action(collection.SingleOrDefault()));
        }

        protected override void BeforeInnerUpdateInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            if (data.ProductCode.IsNullOrEmpty())
                data.ProductCode = Helper.GetProductCode("PT");
        }

        protected override void BeforeInnerUpdate(T data, Dictionary<string, object> context)
        {

            if (data.ProductCode.IsNullOrEmpty())
                data.ProductCode = Helper.GetProductCode("PT");
        }

    }
}
