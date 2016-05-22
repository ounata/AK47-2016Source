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

        public void ExchangeInContext(T exchangeAsset, T asset)
        {

            
            
            VersionStrategyUpdateSqlHelper.ConstructUpdateSql(GetSqlContext(), (strB, context) =>
            {
                var sqlContext = ((SqlContextItem)context);

                //ConnectiveSqlClauseCollection connectiveBuilder = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder();
                //WhereSqlClauseBuilder keyBuilder = new WhereSqlClauseBuilder();
                //keyBuilder.AppendItem("AssetRefID", itemId);
                //connectiveBuilder.Add(keyBuilder);

                //UpdateSqlClauseBuilder updateSqlBuilder = new UpdateSqlClauseBuilder();
                //updateSqlBuilder.AppendItem("ExchangedAmount", "Amount", "=", true);
                //updateSqlBuilder.AppendItem("VersionStartTime", "@currentTime", "=", true);

                //var whereSQL = connectiveBuilder.ToSqlString(TSqlBuilder.Instance);
                //var sql = string.Format("\n update {0} set {1} where {2} \n ", GetTableName(), updateSqlBuilder.ToSqlString(TSqlBuilder.Instance),whereSQL);

                //strB.AppendFormat(
                //    "UPDATE {0} SET VersionEndTime = {1} WHERE {2}",
                //    GetMappingInfo().TableName,
                //    "@currentTime",
                //    connectiveBuilder.ToSqlString(TSqlBuilder.Instance));
                //strB.Append(TSqlBuilder.Instance.DBStatementSeperator);
                //strB.AppendFormat("IF @@ROWCOUNT > 0\n");
                //strB.AppendFormat("\t{0}\n", sql);
                //strB.AppendFormat("ELSE\n");
                //strB.AppendFormat("\tRAISERROR ({0}, 16, 1)",
                //TSqlBuilder.Instance.CheckUnicodeQuotationMark(string.Format("对象\"{0}\"的版本不是最新的，不能更新", itemId)));

                //var sql = "insert into {0}{1}";



                sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, strB.ToString());
                UpdateInContext(exchangeAsset);

                asset.AssetCode = Helper.GetAssetCode("AS");
                var sql = string.Format("insert into {0}{1}" ,GetTableName(), VersionStrategyUpdateSqlBuilder<T>.DefaultInstance.PrepareInsertSqlBuilder(asset, GetMappingInfo(), StringExtension.EmptyStringArray).ToSqlString(TSqlBuilder.Instance));
                sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql);


            });

            

        }
        

    }
}

