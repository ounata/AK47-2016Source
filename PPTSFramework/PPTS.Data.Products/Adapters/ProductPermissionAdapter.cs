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
        public new static ProductPermissionAdapter Instance = new ProductPermissionAdapter();

        public string GetProductIdsByCampusIdsSQL(params string [] campusIds)
        {
            
            var where = new InSqlClauseBuilder("CampusID").AppendItem(campusIds);
            var connective = new ConnectiveSqlClauseCollection(where);

            return string.Format("select ProductID from {0} where {1}", this.GetTableName(), connective.ToSqlString(TSqlBuilder.Instance));
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


    }
}
