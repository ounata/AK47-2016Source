using MCS.Library.Core;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMappingPerformance.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //ObjectToInsertSqlPerformanceTest();
            DataRowToObjectPerformanceTest();
        }

        private static void ObjectToInsertSqlPerformanceTest()
        {
            TestObject testObj = new TestObject() { ID = UuidHelper.NewUuidString(), Name = "GFW", Amount = 100 };

            Action action = () =>
            {
                for (int i = 0; i < 200000; i++)
                    ORMapping.GetInsertSql(testObj, TSqlBuilder.Instance);
            };

            Console.WriteLine("Obj to sql elapsed time: {0:#,##0}ms", action.Duration().TotalMilliseconds);
        }

        private static void DataRowToObjectPerformanceTest()
        {
            DataRow row = PrepareTestTable().Rows[0];

            Action action = () =>
            {
                for (int i = 0; i < 200000; i++)
                {
                    TestObject testObj = new TestObject();

                    ORMapping.DataRowToObject(row, testObj);
                }
            };

            Console.WriteLine("DataRow to obj elapsed time: {0:#,##0}ms", action.Duration().TotalMilliseconds);
        }

        private static DataTable PrepareTestTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID");
            table.Columns.Add("NAME");
            table.Columns.Add("AMOUNT", typeof(Decimal));
            table.Columns.Add("LOCAL_TIME", typeof(DateTime));
            table.Columns.Add("UTC_TIME", typeof(DateTime));
            table.Columns.Add("StringField1");
            table.Columns.Add("StringField2");
            table.Columns.Add("StringField3");
            table.Columns.Add("StringField4");
            table.Columns.Add("StringField5");
            table.Columns.Add("StringField6");
            table.Columns.Add("StringField7");
            table.Columns.Add("StringField8");
            table.Columns.Add("StringField9");
            table.Columns.Add("StringField10");
            table.Columns.Add("StringField11");
            table.Columns.Add("StringField12");
            table.Columns.Add("StringField13");
            table.Columns.Add("StringField14");
            table.Columns.Add("StringField15");
            table.Columns.Add("StringField16");
            table.Columns.Add("StringField17");
            table.Columns.Add("StringField18");
            table.Columns.Add("StringField19");
            table.Columns.Add("StringField20");
            table.Columns.Add("StringField21");
            table.Columns.Add("StringField22");
            table.Columns.Add("StringField23");
            table.Columns.Add("StringField24");
            table.Columns.Add("StringField25");
            table.Columns.Add("StringField26");
            table.Columns.Add("StringField27");
            table.Columns.Add("StringField28");
            table.Columns.Add("StringField29");
            table.Columns.Add("StringField30");

            DataRow row = table.NewRow();

            row["ID"] = UuidHelper.NewUuidString();
            row["NAME"] = UuidHelper.NewUuidString();

            row["AMOUNT"] = 240000;

            DateTime now = DateTime.Now;
            row["LOCAL_TIME"] = now;
            row["UTC_TIME"] = now.ToUniversalTime();

            table.Rows.Add(row);

            return table;
        }
    }
}
