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
            UpdateAssignedAmountInContext (assetID, assignedAmount, operaterID, operaterName,"+=");
        }
        public void DecreaseAssignedAmountInContext(string assetID, decimal assignedAmount, string operaterID, string operaterName)
        {
            UpdateAssignedAmountInContext(assetID, assignedAmount, operaterID, operaterName, "-=");
        }
        /// <summary>
        /// 添加更新已分配课时数量字段的SQL语句
        /// </summary>
        private void UpdateAssignedAmountInContext(string assetID, decimal assignedAmount, string operaterID, string operaterName,string directive)
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

            sci.AppendSqlInContext(TSqlBuilder.Instance, "update {0} set {1} where {2}"
                , this.GetTableName()
                , usb.ToSqlString(TSqlBuilder.Instance)
                , wsb.ToSqlString(TSqlBuilder.Instance));
        }

        protected override void BeforeInnerUpdateInContext(Asset data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            if (data.CreateTime == DateTime.MinValue)
                data.CreateTime = DateTime.Now;
            data.ModifyTime = DateTime.Now;
        }

        protected override void BeforeInnerUpdate(Asset data, Dictionary<string, object> context)
        {
            if (data.CreateTime == DateTime.MinValue)
                data.CreateTime = DateTime.Now;
            data.ModifyTime = DateTime.Now;
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
    }
}