using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Test.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Test.Data.Adapters
{
    public class RepertoryAdapter : UpdatableAndLoadableAdapterBase<Repertory, RepertoryCollection>
    {
        private string _ConnectionName = string.Empty;

        public static readonly RepertoryAdapter DefaultInstance = new RepertoryAdapter();

        public static RepertoryAdapter GetInstance(string connectionName)
        {
            return new RepertoryAdapter(connectionName);
        }

        private RepertoryAdapter()
        {
        }

        public RepertoryAdapter(string connectionName)
        {
            this._ConnectionName = connectionName;
        }

        public Repertory Load(string productID)
        {
            productID.CheckStringIsNullOrEmpty("productID");

            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(productID), "ProductID")).SingleOrDefault();
        }

        public void ChangeUsedQuantity(string productID, int changeCount)
        {
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("ProductID", productID);
            builder.AppendItem("UsedQuantity", -changeCount, ">=");
            builder.AppendItem("UsedQuantity", string.Format("TotalQuantity - {0}", changeCount), " <= ", true);

            string sql = string.Format("UPDATE {0} SET UsedQuantity = UsedQuantity + {1} WHERE {2}",
                this.GetTableName(), changeCount, builder.ToSqlString(TSqlBuilder.Instance));

            int retCount = DbHelper.RunSql(sql, this.GetConnectionName());

            (retCount > 0).FalseThrow("不能修改产品{0}的用量{1}", productID, changeCount);
        }

        public void SetUsedQuantity(string productID, int usedQuantity)
        {
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("ProductID", productID);

            string sql = string.Format("UPDATE {0} SET UsedQuantity = {1} WHERE {2}",
                this.GetTableName(), usedQuantity, builder.ToSqlString(TSqlBuilder.Instance));

            DbHelper.RunSql(sql, this.GetConnectionName());
        }

        protected override string GetConnectionName()
        {
            string result = "DataAccessTest";

            if (this._ConnectionName.IsNotEmpty())
                result = this._ConnectionName;

            return result;
        }
    }
}
