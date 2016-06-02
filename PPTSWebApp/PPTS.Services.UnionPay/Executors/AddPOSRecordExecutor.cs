using PPTS.Data.Customers.Executors;
using PPTS.Services.UnionPay.Model;
using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Adapters;
using MCS.Library.Data.Mapping;
using System.Text;

namespace PPTS.Services.UnionPay.Executors
{
    [DataExecutorDescription("银联对账单导入接口-更新银联交易记录")]
    public class AddPOSRecordExecutor : PPTSEditCustomerExecutorBase<POSRecordModel>
    {
        public AddPOSRecordExecutor(POSRecordModel model) : base(model, null)
        {
            model.POSRecordCollection.NullCheck("刷卡记录信息集合为空");
            (model.POSRecordCollection.Count <= 0).TrueThrow("刷卡记录为空");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            StringBuilder sqlBuilder = new StringBuilder();
            this.Model.POSRecordCollection.ForEach((POSRecord record) => {
                sqlBuilder.AppendFormat("if not exists({0})", ExitsSql(record));
                sqlBuilder.AppendLine(" ");
                sqlBuilder.AppendLine("begin");
                sqlBuilder.AppendLine(InsertSql(record));
                sqlBuilder.AppendLine("end");
            });
            POSRecordAdapter.Instance.GetSqlContext().AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sqlBuilder.ToString());
        }

        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }

        protected override void PersistOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PersistOperationLog(context);

        }

        private string ExitsSql(POSRecord record)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("TransactionID", record.TransactionID).AppendItem("MerchantID", record.MerchantID).AppendItem("POSID", record.POSID);

            string sql = string.Format(@"select top 1 1 from {0} where {1}", POSRecordAdapter.Instance.GetQueryMappingInfo().GetQueryTableName(), whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }

        private string InsertSql(POSRecord record)
        {
            ORMappingItemCollection mapping = POSRecordAdapter.Instance.GetMappingInfo();

            string sql = ORMapping.GetInsertSql<POSRecord>(record, mapping, TSqlBuilder.Instance);
            return sql;

        }
    }
}