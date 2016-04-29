using MCS.Library.Data.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            builder.AppendItem("CreateTime", "GETUTCDATE()", "", true);

            Console.WriteLine(builder.ToSqlString(TSqlBuilder.Instance));
        }
    }
}
