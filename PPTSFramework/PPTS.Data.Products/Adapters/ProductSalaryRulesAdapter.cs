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
    /// 产品 薪酬规则 相关的Adapter的基类
    /// </summary>
    public class ProductSalaryRuleAdapter : ProductAdapterBase<ProductSalaryRule, ProductSalaryRuleCollection>
    {
        public static readonly ProductSalaryRuleAdapter Instance = new ProductSalaryRuleAdapter();

        public ProductSalaryRuleCollection LoadByProductID(string productId)
        {
            return this.Load(builder => builder.AppendItem("ProductID", productId));
        }

        public void LoadByProductIDInContext(string productId, Action<ProductSalaryRuleCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("ProductID", productId)), action);
        }
        public void LoadByProductIDInContext(string productId, string[] campusIds, Action<ProductSalaryRuleCollection> action)
        {
            if (campusIds == null) { LoadByProductIDInContext(productId, action); return; }
            var sql = ProductPermissionAdapter.Instance.GetExsitsByCampusIdsSQL(productId, campusIds);
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("ProductID", productId).AppendItem("exists", sql, "", true)), action);
        }

        public void UpdateByProductIDInContext(string productId, ProductSalaryRuleCollection salaryRules)
        {
            productId.CheckStringIsNullOrEmpty("productId");
            salaryRules.NullCheck("salaryRules");

            if (salaryRules.Count < 1) return;

            Dictionary<string, object> context = new Dictionary<string, object>();

            SqlContextItem sqlContext = this.GetSqlContext();

            //this.DeleteInContext(builder => builder.AppendItem("ProductID", productId));

            foreach (ProductSalaryRule salaryRule in salaryRules)
            {
                salaryRule.ProductID = productId;
                sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
                this.InnerInsertInContext(salaryRule, sqlContext, context, StringExtension.EmptyStringArray);
            }
        }
    }
}
