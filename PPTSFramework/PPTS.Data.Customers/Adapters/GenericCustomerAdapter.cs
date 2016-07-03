using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class GenericCustomerAdapter<T, TCollection> : VersionedCustomerAdapterBase<T, TCollection>
        where T : Customer
        where TCollection : IList<T>, new()
    {
        public static readonly GenericCustomerAdapter<T, TCollection> Instance = new GenericCustomerAdapter<T, TCollection>();

        protected GenericCustomerAdapter()
        {
        }

        public T Load(string customerID)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(customerID), "CustomerID"), DateTime.MinValue).SingleOrDefault();
        }

        public T LoadByCustomerCode(string customerCode)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(customerCode), "CustomerCode"), DateTime.MinValue).SingleOrDefault();
        }

        public void LoadInContext(string customerID, Action<T> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(customerID), "CustomerID"),
                collection => action(collection.SingleOrDefault()), DateTime.MinValue);
        }

        /// <summary>
        /// 在上下文中更新跟进信息
        /// </summary>
        /// <param name="data"></param>
        public void UpdateReferralSummaryInContext(T summary)
        {
            SqlContextItem sqlContext = this.GetSqlContext();

            sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, this.PreapreReferralInsertCount(summary));

            string updateSql = VersionStrategyUpdateSqlBuilder<T>.DefaultInstance.ToUpdateSql(
                    summary, this.GetMappingInfo(),
                    () => this.GetReferralSummaryUpdateSql(summary),
                    () => this.GetReferralInsertSql(summary),
                    false);

            sqlContext.AppendSqlWithSperatorInContext(TSqlBuilder.Instance, updateSql);
        }

        private string PreapreReferralInsertCount(T summary)
        {
            ORMappingItemCollection customerMapping = ORMapping.GetMappingInfo<Customer>();

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("CustomerID", summary.CustomerID);

            StringBuilder strB = new StringBuilder();

            strB.Append("DECLARE @referralCount INT");
            strB.Append(TSqlBuilder.Instance.DBStatementSeperator);
            strB.AppendFormat("SELECT @referralCount = ReferralCount + 1 FROM {0} WHERE {1}", customerMapping.TableName, builder.ToSqlString(TSqlBuilder.Instance));

            return strB.ToString();
        }

        private string GetReferralSummaryUpdateSql(T summary)
        {
            return VersionStrategyUpdateSqlBuilder<T>.DefaultInstance.PrepareUpdateSql(summary, this.GetMappingInfo(), StringExtension.EmptyStringArray);
        }

        private string GetReferralInsertSql(T summary)
        {
            ORMappingItemCollection mapping = this.GetMappingInfo();

            InsertSqlClauseBuilder builder = VersionStrategyUpdateSqlBuilder<T>.DefaultInstance.PrepareInsertSqlBuilder(summary, mapping, new string[] { "ReferralCount" });

            builder.AppendItem("ReferralCount", "@referralCount", "=", true);

            return string.Format("INSERT INTO {0}{1}", this.GetTableName(), builder.ToSqlString(TSqlBuilder.Instance));
        }

    }
}
