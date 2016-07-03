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
using PPTS.Data.Customers;
using System;

namespace PPTS.ExtServices.UnionPay.Executors
{
    [DataExecutorDescription(Description = "银联接口-实时交易")]
    public class AddPosRecordsExecutor : PPTSEditCustomerExecutorBase<POSRecordsModel>
    {
        public AddPosRecordsExecutor(POSRecordsModel model) : base(model, null)
        {
            model.NullCheck("交易信息为空");
            model.POSRecordCollection.NullCheck("银联刷卡记录不能为空");
            (model.POSRecordCollection.Count <= 0).TrueThrow("银联刷卡记录数量必须大于零");
        }
        

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            StringBuilder sqlBuilder = new StringBuilder();
            this.Model.POSRecordCollection.ForEach((POSRecord record) => {
                sqlBuilder.AppendFormat("if not exists({0})", ExistSql(record));
                sqlBuilder.AppendLine(" ");
                sqlBuilder.AppendLine("begin");
                sqlBuilder.AppendLine(InsertSql(record));
                sqlBuilder.AppendLine("end");
                if(record.FromType == Convert.ToInt32(PaySourceType.Async).ToString())
                { 
                    sqlBuilder.AppendLine("else");
                    sqlBuilder.AppendLine("begin");
                    sqlBuilder.AppendLine(UpdateSql(record));
                    sqlBuilder.AppendLine("end");
                }
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
            context.Logs.ForEach(log => log.OperationDescription = this.Model.LogContent );
        }

        private string InsertSql(POSRecord record)
        {
            ORMappingItemCollection mappingCollection = POSRecordAdapter.Instance.GetMappingInfo();
            string sql = ORMapping.GetInsertSql<POSRecord>(record, mappingCollection, TSqlBuilder.Instance);
            return sql;
        }

        private string UpdateSql(POSRecord record)
        {
            UpdateSqlClauseBuilder updateBuilder = new UpdateSqlClauseBuilder();
            updateBuilder.AppendItem("SettlementDate", TimeZoneContext.Current.ConvertTimeToUtc(record.SettlementDate));

            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("TransactionID", record.TransactionID).AppendItem("MerchantID", record.MerchantID).AppendItem("POSID", record.POSID);

            string sql = string.Format(@"update {0} set {1} where {2}"
                    ,POSRecordAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
                    ,updateBuilder.ToSqlString(TSqlBuilder.Instance)
                    ,whereBuilder.ToSqlString(TSqlBuilder.Instance));
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