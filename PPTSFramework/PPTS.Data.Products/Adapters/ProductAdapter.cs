using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Products.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PPTS.Data.Products.Adapters
{
    public class ProductAdapter : GenericProductAdapter<Product, ProductCollection>
    {
        public new static ProductAdapter Instance = new ProductAdapter();

        public DateTime StopSellProduct(IEnumerable<Product> model, string[] campusIds)
        {
            model.NullCheck("model");



            var inBuilder = new InSqlClauseBuilder() { DataField = "ProductId" };
            inBuilder.AppendItem(model.Select(m => m.ProductID).ToArray());

            var connective = new ConnectiveSqlClauseCollection(LogicOperatorDefine.And, inBuilder);

            if (campusIds != null)
            {
                var builder = new WhereSqlClauseBuilder();
                builder.AppendItem("exists", ProductPermissionAdapter.Instance.GetExsitsByCampusIdsSQL(model.Select(m => m.ProductID).ToArray(), campusIds), "", true);

                connective.Add(builder);
            }

            var updateBuilder = new UpdateSqlClauseBuilder();
            updateBuilder.AppendItem("ProductStatus", ((int)ProductStatus.Disabled).ToString());
            updateBuilder.AppendItem("EndDate", "@currentTime", "=", true);
            updateBuilder.AppendItem("ModifierID", model.FirstOrDefault().ModifierID);
            updateBuilder.AppendItem("ModifierName", model.FirstOrDefault().ModifierName);
            updateBuilder.AppendItem("ModifyTime", "@currentTime", "=", true);

            string sql = string.Format("declare @currentTime as datetime;set @currentTime=GETUTCDATE(); \n update {0} set {1} where {2} ; \n select @currentTime"
                , this.GetTableName()
                , updateBuilder.ToSqlString(TSqlBuilder.Instance)
                , connective.ToSqlString(TSqlBuilder.Instance));

            return Convert.ToDateTime(DbHelper.RunSqlReturnScalar(sql, GetConnectionName()));

        }

        public void DelaySellProduct(IEnumerable<Product> model, string[] campusIds)
        {
            model.NullCheck("model");

            var sqlBuilder = new System.Text.StringBuilder();
            sqlBuilder.Append("declare @currentTime as datetime;set @currentTime=GETUTCDATE(); \r\n");

            model.ForEach(m =>
            {

                var whereSqlBuilder = new WhereSqlClauseBuilder();
                whereSqlBuilder.AppendItem("@currentTime", m.EndDate, "<=", true);
                whereSqlBuilder.AppendItem("ProductId", m.ProductID);
                if (campusIds != null)
                {
                    whereSqlBuilder.AppendItem("exists", ProductPermissionAdapter.Instance.GetExsitsByCampusIdsSQL(m.ProductID, campusIds), "", true);
                }

                var updateBuilder = new UpdateSqlClauseBuilder();
                updateBuilder.AppendItem("ProductStatus", ((int)ProductStatus.Enabled).ToString());
                updateBuilder.AppendItem("EndDate", m.EndDate);
                updateBuilder.AppendItem("ModifierID", m.ModifierID);
                updateBuilder.AppendItem("ModifierName", m.ModifierName);
                updateBuilder.AppendItem("ModifyTime", "@currentTime", "=", true);

                sqlBuilder.AppendFormat(" update {0} set {1} where {2} ;\r\n"
                                    , this.GetTableName()
                                    , updateBuilder.ToSqlString(TSqlBuilder.Instance)
                                    , whereSqlBuilder.ToSqlString(TSqlBuilder.Instance));

            });

            DbHelper.RunSql(sqlBuilder.ToString(), GetConnectionName());

        }


        public void DelaySellProduct_1(IEnumerable<Product> model, string[] campusIds)
        {
            model.NullCheck("model");

            var inBuilder = new InSqlClauseBuilder() { DataField = "ProductId" };
            inBuilder.AppendItem(model.Select(m => m.ProductID).ToArray());

            var builder = new WhereSqlClauseBuilder();
            builder.AppendItem("EndDate", model.FirstOrDefault().EndDate, "<=");

            if (campusIds != null)
            {
                builder.AppendItem("exists", ProductPermissionAdapter.Instance.GetExsitsByCampusIdsSQL(model.Select(m => m.ProductID).ToArray(), campusIds), "", true);
            }

            var connective = new ConnectiveSqlClauseCollection(LogicOperatorDefine.And, inBuilder, builder);


            var updateBuilder = new UpdateSqlClauseBuilder();
            updateBuilder.AppendItem("ProductStatus", ((int)ProductStatus.Enabled).ToString());
            updateBuilder.AppendItem("EndDate", model.FirstOrDefault().EndDate);
            updateBuilder.AppendItem("ModifierID", model.FirstOrDefault().ModifierID);
            updateBuilder.AppendItem("ModifierName", model.FirstOrDefault().ModifierName);
            updateBuilder.AppendItem("ModifyTime", "@currentTime", "=", true);

            string sql = string.Format("declare @currentTime as datetime;set @currentTime=GETUTCDATE(); \n update {0} set {1} where {2} ; \n select @currentTime"
                , this.GetTableName()
                , updateBuilder.ToSqlString(TSqlBuilder.Instance)
                , connective.ToSqlString(TSqlBuilder.Instance));

            DbHelper.RunSqlReturnScalar(sql, GetConnectionName());
        }


        public void DeleteInConetxt(string[] productId, string[] campusIds)
        {

            var builder = new InSqlClauseBuilder("ProductId");
            builder.AppendItem(productId);
            var cscc = new ConnectiveSqlClauseCollection(builder);
            if (campusIds != null)
            {
                var whereSqlBuilder = new WhereSqlClauseBuilder(LogicOperatorDefine.And);
                whereSqlBuilder.AppendItem("exists", ProductPermissionAdapter.Instance.GetExsitsByCampusIdsSQL(productId, campusIds), "", true);
                cscc.Add(whereSqlBuilder);
            }
            var sql = string.Format("delete from {0} where {1}", GetTableName(), cscc.ToSqlString(TSqlBuilder.Instance));
            GetSqlContext().AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql);
        }

        /// <summary>
        /// 是否允许 手工确认
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool IsAssetConfirm(string productId)
        {

            var result = Load(b => b.AppendItem("ProductID", productId)
            .AppendItem("ConfirmMode", "1")
            .AppendItem("ConfirmStartDate", "GetUTCDate()", "<=", true)
            .AppendItem("ConfirmEndDate", "GetUTCDate()", ">=", true));

            return result.Count > 0;

        }

        /// <summary>
        /// 是否存在 同名 同价 产品
        /// </summary>
        /// <param name="model"></param>
        public void IsExistsSameProductInContext(Product model)
        {

            var whereSqlBuilder = new WhereSqlClauseBuilder();
            whereSqlBuilder.AppendItem("ProductID", model.ProductID);
            whereSqlBuilder.AppendItem("ProductName", model.ProductName);
            whereSqlBuilder.AppendItem("TargetPrice", model.TargetPrice);
            whereSqlBuilder.AppendItem("ProductStatus", "(1,2,4)", "in", true);

            var sql = string.Format(@" 
if exists (
select * from {0} ROWLOCK where {1} 
)
begin 
RAISERROR ('已有同名同价产品，请勿重复创建！', 16, 1) WITH NOWAIT;
end 

", GetQueryTableName(), whereSqlBuilder.ToSqlString(TSqlBuilder.Instance));

            GetSqlContext().AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql);

        }

    }
}
