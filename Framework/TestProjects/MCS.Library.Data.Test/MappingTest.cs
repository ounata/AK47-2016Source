using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Data.Test.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;

namespace MCS.Library.Data.Test
{
    [TestClass]
    public class MappingTest
    {
        [TestMethod]
        public void PrimaryKeyValuePairsTest()
        {
            TestObject data = new TestObject();

            data.ID = UuidHelper.NewUuidString();

            Dictionary<string, object> pairs = ORMapping.GetPrimaryKeyValuePairs(data);

            Assert.AreEqual(data.ID, pairs["ID"]);
        }

        [TestMethod]
        public void UserObjectMapping()
        {
            User user = new User() { UserID = UuidHelper.NewUuidString(), UserName = "沈峥", Gender = GenderType.Male };

            Console.WriteLine(ORMapping.GetInsertSql(user, TSqlBuilder.Instance));

            user.ModifyTime = DateTime.Now;

            Console.WriteLine(ORMapping.GetUpdateSql(user, TSqlBuilder.Instance));
        }

        [TestMethod]
        public void ChangedFieldsBuilderTest()
        {
            TestObject data = new TestObject();

            data.Name = "沈峥";
            data.Amount = 1000;
            data.LocalTime = DateTime.Now;
            data.UtcTime = data.LocalTime;

            WhereSqlClauseBuilder builder = ORMapping.GetWhereSqlClauseBuilderByChangedFields(data);

            Console.WriteLine(builder.ToSqlString(TSqlBuilder.Instance));
        }

        [TestMethod]
        public void ChangedFieldsBuilderWithoutPKTest()
        {
            TestObject data = new TestObject();

            data.Name = "沈峥";
            data.Amount = 1000;
            data.LocalTime = DateTime.Now;
            data.UtcTime = data.LocalTime;

            WhereSqlClauseBuilder builder = ORMapping.GetWhereSqlClauseBuilderByChangedFields(data, false);

            Console.WriteLine(builder.ToSqlString(TSqlBuilder.Instance));
        }

        [TestMethod]
        public void LocalTimeToUtcTest()
        {
            TimeZoneContext.Current.CurrentTimeZone = TimeZoneInfo.CreateCustomTimeZone("TimeZoneInfoContext", TimeSpan.FromMinutes(480), "TimeZoneInfoContext", "TimeZoneInfoContext");

            TestObject data = new TestObject();

            data.LocalTime = DateTime.Now;
            data.UtcTime = data.LocalTime;

            InsertSqlClauseBuilder builder = ORMapping.GetInsertSqlClauseBuilder(data);

            SqlClauseBuilderItemIUW itemLocalTime = (SqlClauseBuilderItemIUW)builder.Find(item => ((SqlClauseBuilderItemIUW)item).DataField == "LOCAL_TIME");
            SqlClauseBuilderItemIUW itemUtcTime = (SqlClauseBuilderItemIUW)builder.Find(item => ((SqlClauseBuilderItemIUW)item).DataField == "UTC_TIME");

            Console.Write("Local Time: {0}, Utc Time: {1}", itemLocalTime.Data, itemUtcTime.Data);

            Assert.AreNotEqual(itemLocalTime.Data, itemUtcTime.Data);
        }

        [TestMethod]
        public void UtcTimeToLocalTest()
        {
            TimeZoneContext.Current.CurrentTimeZone = TimeZoneInfo.CreateCustomTimeZone("TimeZoneInfoContext", TimeSpan.FromMinutes(480), "TimeZoneInfoContext", "TimeZoneInfoContext");

            DataTable table = PrepareTestTable();

            TestObject data = new TestObject();

            ORMapping.DataRowToObject(table.Rows[0], data);

            Console.Write("Local Time: {0}, Utc Time: {1}", data.LocalTime, data.UtcTime);

            Assert.AreEqual(data.LocalTime, data.UtcTime);
        }

        [TestMethod]
        public void SelectMappingTestlTest()
        {
            ObjectForSelect obj = new ObjectForSelect() { Name = "Shen Zheng", Age = 50 };

            Console.WriteLine(ORMapping.GetSelectSql(obj));
        }

        //[TestMethod]
        //public void ObjectToInsertSqlPerformanceTest()
        //{
        //    TestObject testObj = new TestObject() { ID = UuidHelper.NewUuidString(), Name = "GFW", Amount = 100 };

        //    Action action = () =>
        //    {
        //        for (int i = 0; i < 100000; i++)
        //            ORMapping.GetInsertSql(testObj, TSqlBuilder.Instance);
        //    };

        //    Console.WriteLine("Elapsed time: {0:#,##0}ms", action.Duration().TotalMilliseconds);
        //}

        private DataTable PrepareTestTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID");
            table.Columns.Add("NAME");
            table.Columns.Add("AMOUNT");
            table.Columns.Add("LOCAL_TIME", typeof(DateTime));
            table.Columns.Add("UTC_TIME", typeof(DateTime));

            DataRow row = table.NewRow();

            row["ID"] = UuidHelper.NewUuidString();
            row["NAME"] = UuidHelper.NewUuidString();

            row["AMOUNT"] = "849b3d75e892b66e";	//240000

            DateTime now = DateTime.Now;
            row["LOCAL_TIME"] = now;
            row["UTC_TIME"] = now.ToUniversalTime();

            table.Rows.Add(row);

            return table;
        }
    }
}
