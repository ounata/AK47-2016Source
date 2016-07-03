using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;

namespace PPTS.Data.Products.Adapters
{
    /// <summary>
    /// 产品 产品班组属性 相关的Adapter的基类
    /// </summary>
    public class ProductExOfCourseAdapter : ProductAdapterBase<ProductExOfCourse, ProductExOfCourseCollection>
    {
        public static readonly ProductExOfCourseAdapter Instance = new ProductExOfCourseAdapter();

        public ProductExOfCourseCollection LoadByProductID(string productId)
        {
            return this.Load(builder => builder.AppendItem("ProductID", productId));
        }

        public void LoadByProductIDInContext(string productId, Action<ProductExOfCourseCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("ProductID", productId)), action);
        }

        public void LoadByProductIDInContext(string productId,string[] campusIds, Action<ProductExOfCourseCollection> action)
        {

            if (campusIds == null) { LoadByProductIDInContext(productId, action); return; }
            var sql = ProductPermissionAdapter.Instance.GetExsitsByCampusIdsSQL(productId, campusIds);
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("ProductID", productId).AppendItem("exists", sql, "", true)), action);
        }

        public void UpdateByProductIDInContext(string productId, ProductExOfCourse productExOfCourse)
        {
            productId.CheckStringIsNullOrEmpty("productId");
            productExOfCourse.NullCheck("productExOfCourse");

            Dictionary<string, object> context = new Dictionary<string, object>();

            SqlContextItem sqlContext = this.GetSqlContext();

            this.DeleteInContext(builder => builder.AppendItem("ProductID", productId));

            productExOfCourse.ProductID = productId;
            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
            this.InnerInsertInContext(productExOfCourse, sqlContext, context, StringExtension.EmptyStringArray);

        }
    }
}
