using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTS.Data.Orders.Entities;


namespace PPTS.Data.Orders.Adapters
{
    public class GenericAssetAdapter<T, TCollection> : VersionedOrderAdapterBase<T, TCollection>
       where T : Asset
       where TCollection : IList<T>, new()
    {
        public static readonly GenericAssetAdapter<T, TCollection> Instance = new GenericAssetAdapter<T, TCollection>();

        protected GenericAssetAdapter()
        {
        }

        public T Load(string assetid)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(assetid), "AssetID"), DateTime.MinValue).SingleOrDefault();
        }

        /// <summary>
        /// 查找 关联订单资产
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public T LoadByItemId(string itemId)
        {
            return Load(b => b.AppendItem("AssetRefID", itemId), DateTime.MinValue).SingleOrDefault();
        }

        /// <summary>
        /// 检查未排课时数量，如果未排课时数量已经不够排课需要数量时，引发异常，需要事务回滚
        /// </summary>
        public void CheckUnAssignedAmountInContext(string assetid, decimal assignAmount)
        {
            WhereSqlClauseBuilder wSCB = new WhereSqlClauseBuilder();
            wSCB.AppendItem("AssetID", assetid);
            wSCB.AppendItem("(Amount - AssignedAmount)", assignAmount, ">=", true);

            StringBuilder sb = new StringBuilder();
            sb.Append(@"IF NOT EXISTS(SELECT * FROM OM.Assets_Current WHERE {0}) ")
                .Append(" BEGIN RAISERROR('添加课表失败，该订购单没有剩余课时！', 16, 1) WITH NOWAIT;END;");

            //this.LoadInContext(new WhereLoadingCondition(p => p.AppendItem("AssetID", assetid)
            //.AppendItem("(Amount - AssignedAmount)", assignAmount, ">=", true)), b => { }, DateTime.MinValue);

            SqlContextItem sCI = this.GetSqlContext();
            sCI.AppendSqlInContext(TSqlBuilder.Instance, sb.ToString(), wSCB.ToSqlString(TSqlBuilder.Instance));
        }


        public void PrepareDateTimeVar()
        {
            SqlContextItem sCI = this.GetSqlContext();
            sCI.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, "declare @currentTime datetime;set @currentTime = GETUTCDATE();");
        }

        public void LoadInContext(string assetid, Action<T> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(assetid), "AssetID"),
                collection => action(collection.SingleOrDefault()), DateTime.MinValue);
        }

        protected override void BeforeInnerUpdateCollection(IEnumerable<T> data, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateCollection(data, context);
            data.ForEach(i =>
            {
                if (i.AssetCode.IsNullOrWhiteSpace()) { i.AssetCode = Helper.GetAssetCode("OD"); }
                if (i.AssetName.IsNullOrWhiteSpace()) { i.AssetName = i.AssetCode + i.ProductName; }
            });
        }

    }
}

