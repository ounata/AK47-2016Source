using MCS.Library.Core;
using MCS.Library.Data.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Linq;

namespace MCS.Library.Data.Test
{
    [TestClass]
    public class SqlBuilderTest
    {
        [TestMethod]
        public void InsertBuilderTest()
        {
            InsertSqlClauseBuilder builder = new InsertSqlClauseBuilder();

            builder.AppendItem("A", "A").AppendItem("B", DateTime.Now);

            Console.WriteLine(builder.ToSqlString(TSqlBuilder.Instance));
        }

        [TestMethod]
        public void WhereBuilderTest()
        {
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("A", "A").AppendItem("B", DateTime.Now);
            builder.AppendItem("CreateTime", "GETDATE()", "=", true);
            builder.AppendItem("NullField", (string)null, "IS");

            Console.WriteLine(builder.ToSqlString(TSqlBuilder.Instance));

            Console.WriteLine(string.Join(", ", builder.GetFields().ToArray()));
        }

        [TestMethod]
        public void InBuilderTest()
        {
            InSqlClauseBuilder builder = new InSqlClauseBuilder("A");

            builder.AppendItem(1, 2, 3, 4, 5);

            Console.WriteLine(builder.ToSqlString(TSqlBuilder.Instance));
            Console.WriteLine(string.Join(", ", builder.GetFields().ToArray()));
        }

        [TestMethod]
        public void ConnectiveBuilderTest()
        {
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("A", "A").AppendItem("B", DateTime.Now);
            builder.AppendItem("CreateTime", "GETDATE()", "=", true);

            InSqlClauseBuilder inBuilder = new InSqlClauseBuilder("C");

            inBuilder.AppendItem(1, 2, 3, 4, 5);

            ConnectiveSqlClauseCollection connective = new ConnectiveSqlClauseCollection(LogicOperatorDefine.Or, builder, inBuilder);

            Console.WriteLine(connective.ToSqlString(TSqlBuilder.Instance));
            Console.WriteLine(string.Join(", ", connective.GetFields().ToArray()));
        }

        [TestMethod]
        public void SelectBuilderTest()
        {
            SelectSqlClauseBuilder builder = new SelectSqlClauseBuilder();

            builder.AppendValue("Hello World");
            builder.AppendValue(100);
            builder.AppendItem("Name", "沈峥");
            builder.AppendItem("CreateTime", "GETUTCDATE()", string.Empty, true);
            builder.AppendItem("创建时间", "CreateTime", string.Empty, true);
            builder.AppendFields("Salary", "Age");

            Console.WriteLine(builder.ToSqlString(TSqlBuilder.Instance));
        }

        [TestMethod]
        public void DataRowToBuilderTest()
        {
            UpdateSqlClauseBuilder builder = new UpdateSqlClauseBuilder();

            DataTable table = PrepareTestTable();

            builder.AppendItems(table.Rows[0]);

            Console.WriteLine(builder.ToSqlString(TSqlBuilder.Instance));
        }

        private DataTable PrepareTestTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID");
            table.Columns.Add("NAME");
            table.Columns.Add("AMOUNT", typeof(Decimal));
            table.Columns.Add("QUANTITY", typeof(int));
            table.Columns.Add("LOCAL_TIME", typeof(DateTime));
            table.Columns.Add("UTC_TIME", typeof(DateTime));
            table.Columns.Add("NULL_FIELD", typeof(DateTime));
            table.Columns.Add("NULLABLE", typeof(DateTime));

            DataRow row = table.NewRow();

            row["ID"] = UuidHelper.NewUuidString();
            row["NAME"] = UuidHelper.NewUuidString();

            row["AMOUNT"] = 1200.00;
            row["QUANTITY"] = 100;

            DateTime now = DateTime.Now;
            row["LOCAL_TIME"] = now;
            row["UTC_TIME"] = now.ToUniversalTime();

            row["NULL_FIELD"] = DBNull.Value;
            row["NULLABLE"] = new Nullable<DateTime>(now);

            table.Rows.Add(row);

            return table;
        }
    }
}
