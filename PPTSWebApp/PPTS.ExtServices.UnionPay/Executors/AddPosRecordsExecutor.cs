using MCS.Library.Core;
using MCS.Library.Data.Executors;
using PPTS.Data.Customers.Executors;
using PPTS.ExtServices.UnionPay.Models.Statement;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Data;
using PPTS.Data.Customers.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.Mapping;
using System.Text;

namespace PPTS.ExtServices.UnionPay.Executors
{
    [DataExecutorDescription(Description = "银联接口-实时交易")]
    public class AddPosRecordsExecutor : PPTSEditCustomerExecutorBase<POSRecordsModel>
    {
        public AddPosRecordsExecutor(POSRecordsModel model) : base(model, null)
        {
            model.NullCheck("交易信息为空");
            model.PosRecord.NullCheck("银联刷卡记录不能为空");
        }
        

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("if not exists({0})", ExistSql(this.Model.PosRecord));
            sql.AppendLine(" ");
            sql.AppendLine("begin");
            sql.AppendLine(InsertSql(this.Model.PosRecord));
            sql.AppendLine("end");
            POSRecordAdapter.Instance.GetSqlContext().AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql.ToString());

            //POSRecordAdapter.Instance.UpdateInContext(this.Model.PosRecord);
        }

        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }

        protected override void PersistOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PersistOperationLog(context);
            context.Logs.ForEach(log => log.ResourceID = Model.PosRecord.MerchantID + Model.PosRecord.POSID + Model.PosRecord.TransactionID);
        }

        private string InsertSql(POSRecord record)
        {
            ORMappingItemCollection mappingCollection = POSRecordAdapter.Instance.GetMappingInfo();
            string sql = ORMapping.GetInsertSql<POSRecord>(record, mappingCollection, TSqlBuilder.Instance);
            return sql;
        }

        private string ExistSql(POSRecord record)
        {
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("TransactionID", record.TransactionID).AppendItem("MerchantID", record.MerchantID).AppendItem("POSID", record.POSID);
            string sql = string.Format(@"select top 1 1 from {0} where {1}", POSRecordAdapter.Instance.GetQueryMappingInfo().GetQueryTableName(), whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
    }
}