using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Adapters
{
    public class ProductPermissionAdapter : ProductAdapterBase<ProductPermission, ProductPermissionCollection>
    {
        public static readonly ProductPermissionAdapter Instance = new ProductPermissionAdapter();

        public string GetProductIdsByCampusIdsSQL(params string [] campusIds)
        {
            
            var where = new InSqlClauseBuilder("CampusID").AppendItem(campusIds);
            var connective = new ConnectiveSqlClauseCollection(where);

            return string.Format("select ProductID from {0} where {1}", this.GetTableName(), connective.ToSqlString(TSqlBuilder.Instance));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="feildProductId">[PM].[Products].ProductID or 11111</param>
        /// <param name="campusIds"></param>
        /// <returns></returns>
        public string GetExsitsByCampusIdsSQL(string feildProductId,params string[] campusIds)
        {

            var where = new InSqlClauseBuilder("CampusID").AppendItem(campusIds);
            var where2 = new WhereSqlClauseBuilder(LogicOperatorDefine.And).AppendItem("ProductID", feildProductId);
            var connective = new ConnectiveSqlClauseCollection(where, where2);

            return string.Format("( select 1 from {0} where {1} )", this.GetTableName(), connective.ToSqlString(TSqlBuilder.Instance));
        }

        public string GetExsitsByCampusIdsSQL(string[] ProductIds, params string[] campusIds)
        {

            var where = new InSqlClauseBuilder("CampusID").AppendItem(campusIds);
            var where2 = new InSqlClauseBuilder("ProductID").AppendItem(ProductIds);
            var connective = new ConnectiveSqlClauseCollection(LogicOperatorDefine.And);
            connective.Add(where);
            connective.Add(where2);

            return string.Format("( select 1 from {0} where {1} )", this.GetTableName(), connective.ToSqlString(TSqlBuilder.Instance));
        }


        public void UpdateByProductIDInContext(string productId, ProductPermissionCollection collection)
        {
            productId.CheckStringIsNullOrEmpty("productId");
            collection.NullCheck("ProductPermissionCollection");

            if (collection.Count < 1) return;

            Dictionary<string, object> context = new Dictionary<string, object>();

            SqlContextItem sqlContext = this.GetSqlContext();

            foreach (var item in collection)
            {
                item.ProductID = productId;
                sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
                this.InnerInsertInContext(item, sqlContext, context, StringExtension.EmptyStringArray);
            }

        }


        public void LoadByProductIDInContext(string productId, Action<ProductPermissionCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("ProductID", productId)), action);
        }
        public void LoadByProductIDInContext(string productId, string[] campusIds, Action<ProductPermissionCollection> action)
        {
            if (campusIds == null) {  LoadByProductIDInContext(productId, action); return; }

            var sql = ProductPermissionAdapter.Instance.GetExsitsByCampusIdsSQL(productId, campusIds);
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("ProductID", productId).AppendItem("exists", sql, "", true)), action);
        }

        /// <summary>
        /// 是否 存在校区 在 产品列表中
        /// </summary>
        /// <param name="campusIds"></param>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public bool IsExistsCampusInProduct(string[] campusIds, string[] productIds)
        {
            var sqlList = new List<string>();

            for (int i = 0; i < productIds.Length; i++)
            {
                var whereSqlBuilder = new WhereSqlClauseBuilder();
                whereSqlBuilder.AppendItem("ProductID", productIds[i]);
                whereSqlBuilder.AppendItem("CampusID", campusIds[i]);

                sqlList.Add(string.Format("({0})", whereSqlBuilder.ToSqlString(TSqlBuilder.Instance)));
            }

            var sql = string.Format("select count(1) from {0} where {1}", GetQueryTableName(), string.Join("OR", sqlList));

            return (int)DbHelper.RunSqlReturnScalar(sql, GetConnectionName()) == productIds.Length;

        }
    }
}
