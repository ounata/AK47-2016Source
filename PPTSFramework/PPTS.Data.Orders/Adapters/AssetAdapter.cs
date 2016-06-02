using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTS.Data.Orders.Entities;
using MCS.Library.Data;

namespace PPTS.Data.Orders.Adapters
{
    public class AssetAdapter : OrderAdapterBase<Asset, AssetCollection>
    {
        public static readonly AssetAdapter Instance = new AssetAdapter();

        private AssetAdapter()
        {
        }

        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="asset"></param>
        /*
		public void Insert(Asset asset)
		{
			this.InnerInsert(asset, new Dictionary<string, object>());
		}
		*/

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="assetid"></param>
        /// <returns></returns>
        public Asset Load(string assetid)
        {
            return this.Load(builder => builder.AppendItem("AssetID", assetid)).SingleOrDefault();
        }

        public Asset LoadByItemId(string itemId)
        {
            return Load(builder => builder.AppendItem("AssetRefID", itemId)).SingleOrDefault();
        }

        /// <summary>
        /// 获取课时数未排完的记录
        /// </summary>
        /// <param name="operateCampusID"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public AssetCollection LoadCollection(string operateCampusID, string customerID)
        {
            return this.Load(builder => builder
            // .AppendItem("CustomerCampusID", operateCampusID)
             .AppendItem("CustomerID", customerID)
             .AppendItem("Amount", "0", ">"));
        }

        public void IncreaseAssignedAmountInContext(string assetID, decimal assignedAmount, string operaterID, string operaterName)
        {
            UpdateAssignedAmountInContext(assetID, assignedAmount, operaterID, operaterName, "+=");
        }
        public void DecreaseAssignedAmountInContext(string assetID, decimal assignedAmount, string operaterID, string operaterName)
        {
            UpdateAssignedAmountInContext(assetID, assignedAmount, operaterID, operaterName, "-=");
        }
        /// <summary>
        /// 添加更新已分配课时数量字段的SQL语句
        /// </summary>
        private void UpdateAssignedAmountInContext(string assetID, decimal assignedAmount, string operaterID, string operaterName, string directive)
        {
            assetID.CheckStringIsNullOrEmpty("assetID");
            assignedAmount.NullCheck("assignedAmount");

            SqlContextItem sci = this.GetSqlContext();

            UpdateSqlClauseBuilder usb = new UpdateSqlClauseBuilder();
            usb.AppendItem("AssignedAmount", assignedAmount, directive);
            usb.AppendItem("ModifyTime", "GETUTCDATE()", "=", true);
            usb.AppendItem("ModifierID", operaterID, "=");
            usb.AppendItem("ModifierName", operaterName, "=");

            WhereSqlClauseBuilder wsb = new WhereSqlClauseBuilder();
            wsb.AppendItem("AssetID", assetID, "=");

            sci.AppendSqlInContext(TSqlBuilder.Instance, "update {0} set {1} where {2};"
                , this.GetTableName()
                , usb.ToSqlString(TSqlBuilder.Instance)
                , wsb.ToSqlString(TSqlBuilder.Instance));
        }




        public void UpdateCollectionInContext(AssetCollection collection)
        {
            collection.NullCheck("collection");

            Dictionary<string, object> context = new Dictionary<string, object>();
            SqlContextItem sqlContext = this.GetSqlContext();

            foreach (var item in collection)
            {
                sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
                this.InnerInsertInContext(item, sqlContext, context, StringExtension.EmptyStringArray);
            }
        }

        /// <summary>
        /// 通过账户获得资产价值信息
        /// </summary>
        /// <param name="accountID">账户ID</param>
        /// <returns>资产价值</returns>
        public decimal LoadAssetsValueByAccountID(string accountID)
        {
            decimal assetsValue = 0;
            DataSet ds = DbHelper.RunSqlReturnDS(LoadAssetsValueByAccountIDSQL(accountID), this.ConnectionName);
            if (ds.Tables[0].Rows.Count > 0)
                assetsValue = (decimal)(ds.Tables[0].Rows[0][0]);
            return assetsValue;
        }

        private string LoadAssetsValueByAccountIDSQL(string accountID)
        {
            accountID.NullCheck("accountID");
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("AccountID", accountID).AppendItem("Amount", 0, ">");
            string sql = string.Format(@"select isnull(sum(isnull(Price,0)*isnull(Amount,0)),0) AssetsValue from {0} where {1}"
            , this.GetQueryMappingInfo().GetQueryTableName()
            , whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }



    }
}