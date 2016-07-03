using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class FinancialIncomeAdapter : CustomerAdapterBase<FinancialIncome,FinancialIncomeCollection>
    {
        public static readonly FinancialIncomeAdapter Instance = new FinancialIncomeAdapter();

        /// <summary>
        /// 根据对账状态查询账户充值收入
        /// </summary>
        /// <param name="checkStatus">对账状态</param>
        /// <param name="startSynTime">同步开始时间</param>
        /// <param name="synStatus">同步状态</param>
        /// <returns></returns>
        public FinancialIncomeCollection LoadCollectionByCheckStatus(string checkStatus, DateTime startSynTime, string synStatus)
        {
            checkStatus.CheckStringIsNullOrEmpty("checkStatus");
            return this.QueryData(QueryAccountChargePaymentByCheckStatusSQL(checkStatus, startSynTime, synStatus));
        }

        /// <summary>
        /// 查询账户缴费支付单，根据对账状态
        /// </summary>
        /// <param name="checkStatus">对账状态</param>
        /// <param name="startSynTime">同步开始时间</param>
        /// <param name="synStatus">同步状态</param>
        /// <returns></returns>
        private string QueryAccountChargePaymentByCheckStatusSQL(string checkStatus,DateTime startSynTime, string synStatus)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("payment.CheckStatus", checkStatus)
            .AppendItem("payment.PayTime", "cast('"+ startSynTime.ToString() + "' as datetime)", ">=",true)
            .AppendItem("IsSyn",synStatus);

            string sql = string.Format(@"select applies.CampusID,applies.CampusName,payment.PayTime,applies.ApplyNo,
                applies.ChargeType,payment.PayType,payment.PayMoney,payment.PayID,applies.CustomerCode,
                applies.CustomerName from {0} as payment inner join {1} as applies
                on payment.ApplyID = applies.ApplyID
                where {2}"
                , AccountChargePaymentAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
                ,AccountChargeApplyAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
                ,whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }

        /// <summary>
        /// 更新账户缴费支付单
        /// </summary>
        /// <param name="payIDs">支付单IDs</param>
        /// <param name="synStatus">同步状态</param>
        public void UpdateAccountChargePaymentByIDs(string[] payIDs,string synStatus)
        {
            payIDs.NullCheck("payIDs");
            (payIDs.Length > 0).FalseThrow("没有可更新支付单");

            SqlContextItem sqlContext = this.GetSqlContext();

            UpdateSqlClauseBuilder updateBuilder = new UpdateSqlClauseBuilder();
            updateBuilder.AppendItem("IsSyn", synStatus)
                .AppendItem("SynTime", "GETUTCDATE()", "=", true);

            StringBuilder strBuilder = new StringBuilder();
            foreach(var id in payIDs)
            {
                strBuilder.AppendFormat(",'{0}'", id);
            }

            string strPayIDs = string.Format("({0})", strBuilder.ToString().Substring(1));
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("PayID", strPayIDs, "in", true);

            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, "update {0} set {1} where {2}"
                , AccountChargePaymentAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
                , updateBuilder.ToSqlString(TSqlBuilder.Instance)
                , whereBuilder.ToSqlString(TSqlBuilder.Instance));

        }
    }
}
