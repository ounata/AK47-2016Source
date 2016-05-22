using System;
using System.Linq;
using System.Collections.Generic;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.Builder;
using MCS.Library.Net.SNTP;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountChargeApplyAdapter : CustomerAdapterBase<AccountChargeApply, AccountChargeApplyCollection>
    {
        public static readonly AccountChargeApplyAdapter Instance = new AccountChargeApplyAdapter();

        private AccountChargeApplyAdapter()
        {
        }

        public AccountChargeApplyCollection LoadCollectionByCustomerID(string customerID)
        {
            return this.Load(builder => builder.AppendItem("CustomerID", customerID));
        }

        /// <summary>
        /// 根据缴费申请单ID获取缴费单申请信息。
        /// </summary>
        /// <param name="applyID">申请单ID</param>
        /// <returns></returns>
        public AccountChargeApply LoadByApplyID(string applyID)
        {
            return this.Load(builder => builder.AppendItem("ApplyID", applyID)).SingleOrDefault();
        }
        public void LoadByApplyIDInContext(string applyID, Action<AccountChargeApply> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(applyID), "ApplyID")
                , collection => action(collection.SingleOrDefault()));
        }

        /// <summary>
        /// 根据学员ID获取未支付的缴费单申请信息
        /// </summary>
        /// <param name="customerID">学员ID</param>
        /// <returns></returns>
        public AccountChargeApply LoadUnpayByCustomerID(string customerID)
        {
            return this.Load(builder => builder.AppendItem("CustomerID", customerID)
            .AppendItem("PayStatus", (int)PayStatusDefine.Unpay)).SingleOrDefault();
        }

        /// <summary>
        /// 获得指定客户ID的有效缴费单信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="date">日期，默认DateTime.Min</param>
        /// <returns></returns>
        public AccountChargeApplyCollection LoadValidChargeByCustomerID(string customerID, DateTime date)
        {
            return this.QueryData(PrepareValidChargeSQLByCustomerID(customerID, date));
        }

        protected override void BeforeInnerUpdateInContext(AccountChargeApply data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdateInContext(data, sqlContext, context);
            this.InitData(data);
        }

        protected override void BeforeInnerUpdate(AccountChargeApply data, Dictionary<string, object> context)
        {
            base.BeforeInnerUpdate(data, context);
            this.InitData(data);
        }

        private void InitData(AccountChargeApply data)
        {
            if (data.ApplyID.IsNullOrEmpty())
                data.ApplyID = System.Guid.NewGuid().ToString();
            if (data.ApplyNo.IsNullOrEmpty())
                data.ApplyNo = Helper.GetApplyNo("NC");
            if (data.AccountID.IsNullOrEmpty())
                data.AccountID = data.ApplyID;
            if (data.AccountCode.IsNullOrEmpty())
                data.AccountCode = data.ApplyNo;
        }

        /// <summary>
        /// 拼接有效缴费单信息查询SQL 
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="date">日期，默认DateTime.Min</param>
        /// <returns></returns>
        private string PrepareValidChargeSQLByCustomerID(string customerID, DateTime date)
        {
            WhereSqlClauseBuilder payBuilder = new WhereSqlClauseBuilder();
            payBuilder.AppendItem("aca.CustomerID", customerID)
                      .AppendItem("aca.ApplyStatus", ApplyStatusDefine.Approved.GetHashCode());
            if (date > DateTime.MinValue)
                payBuilder.AppendItem("aca.ApplyTime", TimeZoneContext.Current.ConvertTimeToUtc(date), ">=");

            OrderBySqlClauseBuilder payOrderByBuilder = new OrderBySqlClauseBuilder();
            payOrderByBuilder.AppendItem("aca.ApplyTime", FieldSortDirection.Descending);

            WhereSqlClauseBuilder accountBuilder = new WhereSqlClauseBuilder();
            accountBuilder.AppendItem("ac.CustomerID", customerID);

            string sql = string.Format(@"select aca.* from {0} aca 
                                        where {1} and aca.AccountID in 
                                        (select ac.AccountID from {2} ac where {3}) ",
                                        this.GetQueryMappingInfo().GetQueryTableName(),
                                        payBuilder.ToSqlString(TSqlBuilder.Instance),
                                        AccountAdapter.Instance.GetQueryMappingInfo().GetQueryTableName(),
                                        accountBuilder.ToSqlString(TSqlBuilder.Instance)
            );
            return sql;
        }
    }
}