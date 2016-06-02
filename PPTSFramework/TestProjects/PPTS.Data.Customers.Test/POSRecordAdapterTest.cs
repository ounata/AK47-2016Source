using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class POSRecordAdapterTest
    {
        /// <summary>
        /// 判断不存在则生成记录操作
        /// </summary>
        [TestMethod]
        public void ExistInsertText()
        {

            POSRecord record = new POSRecord();
            record.TransactionID = "1";
            record.MerchantID = "1";
            record.POSID = "1";
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("if(not exists({0}))", ExistSQL(record));
            sql.AppendLine(" ");
            sql.AppendLine("begin");
            sql.AppendLine(InsertSQL(record));
            sql.AppendLine("end");
            Console.Write(sql);
            POSRecordAdapter.Instance.GetSqlContext().AppendSqlWithSperatorInContext(TSqlBuilder.Instance, sql.ToString());
            POSRecordAdapter.Instance.GetDbContext().ExecuteNonQuerySqlInContext();
            POSRecordCollection po = POSRecordAdapter.Instance.Load(builder => builder.AppendItem("TransactionID", record.TransactionID));
            Assert.IsNotNull(po);
            Assert.IsTrue(po.Count > 0);
            Console.WriteLine("{0}", po.FirstOrDefault().TransactionID);
        }

        private string InsertSQL(POSRecord record)
        {
            ORMappingItemCollection mapping = POSRecordAdapter.Instance.GetMappingInfo();
            string sql = ORMapping.GetInsertSql<POSRecord>(record, mapping, TSqlBuilder.Instance);
            return sql;
        }

        private string ExistSQL(POSRecord record)
        {
            //WhereSqlClauseBuilder whereBuilder2 = ORMapping.GetWhereSqlClauseBuilder(record);
            WhereSqlClauseBuilder whereBuilder = new WhereSqlClauseBuilder();
            whereBuilder.AppendItem("TransactionID", record.TransactionID);
            whereBuilder.AppendItem("MerchantID", record.MerchantID);
            whereBuilder.AppendItem("POSID", record.POSID);
            string sql = string.Format(@"select top 1 1 from {0} where {1}"
            , POSRecordAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , whereBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
    }
}
