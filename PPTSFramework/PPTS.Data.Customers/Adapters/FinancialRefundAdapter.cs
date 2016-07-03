using PPTS.Data.Customers.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTS.Data.Customers.Entities;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data;

namespace PPTS.Data.Customers.Adapters
{
    public class FinancialRefundAdapter : CustomerAdapterBase<FinancialRefund,FinancialRefundCollection>
    {
        public static readonly FinancialRefundAdapter Instance = new FinancialRefundAdapter();

        /// <summary>
        /// 根据对账状态查询需同步的账户退费申请信息
        /// </summary>
        /// <param name="checkStatus">对账状态</param>
        /// <param name="startSynTime">开始同步时间</param>
        /// <param name="synStatus">同步状态</param>
        /// <returns></returns>
        public FinancialRefundCollection LoadCollectionByCheckStatus(string checkStatus, DateTime startSynTime, string synStatus)
        {
            checkStatus.CheckStringIsNullOrEmpty("checkStatus");
            return this.QueryData(QueryAccountRefundApplyByCheckStatus(checkStatus, startSynTime, synStatus));
        }

        private string QueryAccountRefundApplyByCheckStatus(string checkStatus, DateTime startSynTime, string synStatus)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("CheckStatus", checkStatus)
                .AppendItem("VerifyTime", "cast('"+ startSynTime.ToString() + "' as datetime)", ">=",true)
                .AppendItem("IsSyn", synStatus);

            string sql = string.Format(@"select CampusID,CampusName,VerifyTime,RefundType,ApplyNo,RealRefundMoney,Drawer,CustomerCode
                    ,CustomerName from {0} where {1}"
                    , AccountRefundApplyAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
                    , whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;

        }

        public void UpdateAccountRefundApplyByIDs(string[] applyIDs,string synStatus)
        {
            applyIDs.NullCheck("applyIDs");
            (applyIDs.Length > 0).FalseThrow("没有可更新退费单");

            SqlContextItem sqlContext = this.GetSqlContext();

            UpdateSqlClauseBuilder updateBuilder = new UpdateSqlClauseBuilder();
            updateBuilder.AppendItem("IsSyn", synStatus)
                .AppendItem("SynTime", "GETUTCDATE()", "=", true);

            StringBuilder strBuilder = new StringBuilder();
            foreach(var id in applyIDs)
            {
                strBuilder.AppendFormat(",'{0}'", id);
            }
            string strApplyIDs = string.Format("({0})", strBuilder.ToString().Substring(1));

            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("ApplyID", strApplyIDs, "in", true);

            sqlContext.AppendSqlInContext(TSqlBuilder.Instance, "update {0} set {1} where {2}"
                , AccountRefundApplyAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
                , updateBuilder.ToSqlString(TSqlBuilder.Instance)
                , whereBuilder.ToSqlString(TSqlBuilder.Instance));
        }
    }
}
