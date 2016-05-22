﻿using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Products.Entities;
using System;

namespace PPTS.Data.Products.Adapters
{
    public class ProductAdapter: GenericProductAdapter<Product,ProductCollection>
    {
        public new static ProductAdapter Instance = new ProductAdapter();

        public bool StopSellProduct(string productId)
        {
            productId.CheckStringIsNullOrEmpty("productId");

            var builder = new WhereSqlClauseBuilder();
            builder.AppendItem("ProductId", productId);
            string sql = string.Format("update {0} set enddate=Convert(varchar(10),getutcdate(),120) where {1}", this.GetTableName(), builder.ToSqlString(TSqlBuilder.Instance));
            return DbHelper.RunSql(sql, this.GetConnectionName())>0;

        }

        public bool StartSellProduct(string productId,DateTime date)
        {

            productId.CheckStringIsNullOrEmpty("productId");
            date.NullCheck("date");

            var builder = new WhereSqlClauseBuilder();
            builder.AppendItem("ProductId", productId);
            
            string sql = string.Format("update {0} set startdate='"+ date.ToString("yyyy-MM-dd") + "' where {1}", this.GetTableName(), builder.ToSqlString(TSqlBuilder.Instance));
            return DbHelper.RunSql(sql, this.GetConnectionName())>0;

        }


        public bool DelaySellProduct(string productId, DateTime date)
        {
            productId.CheckStringIsNullOrEmpty("productId");
            date.NullCheck("date");

            var builder = new WhereSqlClauseBuilder();
            builder.AppendItem("ProductId", productId);

            string sql = string.Format("update {0} set enddate='" + date.ToString("yyyy-MM-dd") + "' where {1}", this.GetTableName(), builder.ToSqlString(TSqlBuilder.Instance));
            return DbHelper.RunSql(sql, this.GetConnectionName()) > 0;
        }



    }
}