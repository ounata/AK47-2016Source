using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class GenericPotentialCustomerAdapter<T, TCollection> : VersionedCustomerAdapterBase<T, TCollection>
        where T : CustomerBase
        where TCollection : IList<T>, new()
    {
        public static readonly GenericPotentialCustomerAdapter<T, TCollection> Instance = new GenericPotentialCustomerAdapter<T, TCollection>();

        protected GenericPotentialCustomerAdapter()
        {
        }

        public T Load(string customerID)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(customerID), "CustomerID"), DateTime.MinValue).SingleOrDefault();
        }

        public void LoadInContext(string customerID, Action<T> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(customerID), "CustomerID"),
                collection => action(collection.SingleOrDefault()), DateTime.MinValue);
        }

        /// <summary>
        /// 更新跟进信息，主要是包括跟进计数
        /// </summary>
        /// <param name="data"></param>
        public void UpdateFollowSummary(T summary)
        {
            string prepareSql = this.PreapreFollowInsertCount(summary);

            string updateSql = VersionStrategyUpdateSqlBuilder<T>.DefaultInstance.ToUpdateSql(
                    summary, this.GetMappingInfo(),
                    () => this.GetFollowSummaryUpdateSql(summary),
                    () => this.GetFollowInsertSql(summary),
                    true);

            string sql = prepareSql + TSqlBuilder.Instance.DBStatementSeperator + updateSql;

            DbHelper.RunSqlWithTransaction(sql, this.GetConnectionName());
        }
        /// <summary>
        /// 在上下文中更新跟进信息，主要是包括跟进计数
        /// </summary>
        /// <param name="data"></param>
        public void UpdateFollowSummaryInContext(T summary)
        {
            SqlContextItem sqlContext = this.GetSqlContext();

            sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, this.PreapreFollowInsertCount(summary));

            string updateSql = VersionStrategyUpdateSqlBuilder<T>.DefaultInstance.ToUpdateSql(
                    summary, this.GetMappingInfo(),
                    () => this.GetFollowSummaryUpdateSql(summary),
                    () => this.GetFollowInsertSql(summary),
                    false);

            sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, updateSql);
        }


        protected override void BeforeInnerUpdateInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            if (data.CustomerCode.IsNullOrEmpty())
                data.CustomerCode = Helper.GetCustomerCode("S");
        }

        protected override void BeforeInnerUpdate(T data, Dictionary<string, object> context)
        {
            if (data.CustomerCode.IsNullOrEmpty())
                data.CustomerCode = Helper.GetCustomerCode("S");
        }

        private string PreapreFollowInsertCount(T summary)
        {
            ORMappingItemCollection followMapping = ORMapping.GetMappingInfo<CustomerFollow>();

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("CustomerID", summary.CustomerID);

            StringBuilder strB = new StringBuilder();

            strB.Append("DECLARE @followCount INT");
            strB.Append(TSqlBuilder.Instance.DBStatementSeperator);
            strB.AppendFormat("SELECT @followCount = COUNT(*) FROM {0} WHERE {1}", followMapping.TableName, builder.ToSqlString(TSqlBuilder.Instance));

            return strB.ToString();
        }

        private string GetFollowSummaryUpdateSql(T summary)
        {
            return VersionStrategyUpdateSqlBuilder<T>.DefaultInstance.PrepareUpdateSql(summary, this.GetMappingInfo(), StringExtension.EmptyStringArray);
        }

        private string GetFollowInsertSql(T summary)
        {
            ORMappingItemCollection mapping = this.GetMappingInfo();

            InsertSqlClauseBuilder builder = VersionStrategyUpdateSqlBuilder<T>.DefaultInstance.PrepareInsertSqlBuilder(summary, mapping, new string[] { "FollowedCount" });

            builder.AppendItem("FollowedCount", "@followCount", "=", true);

            return string.Format("INSERT INTO {0}{1}", this.GetTableName(), builder.ToSqlString(TSqlBuilder.Instance));
        }
    }
}
