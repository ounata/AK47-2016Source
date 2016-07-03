using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Contracts.Search.Models;
using MCS.Library.Core;
using System.Collections.Generic;
using System.Collections;
using PPTS.Data.Customers.Adapters;
using PPTS.Services.Search.Services;
using System.Data;
using MCS.Library.Data.Adapters;

namespace PPTS.Contracts.Proxies.Test
{
    [TestClass]
    public class CustomerSearcheTest
    {
        [TestMethod]
        public void UpdateByCustomerInfoTest()
        {
            CustomerSearchUpdateModel model = new CustomerSearchUpdateModel()
            {
                CustomerID = "605917",
                ObjectID = "605917",
                Type = CustomerSearchUpdateType.Customer
            };
            new CustomerSearchUpdateService().UpdateByCustomerInfo(model);
            Data.Customers.Entities.CustomerSearchCollection csc = CustomerSearchAdapter.Instance.LoadByInBuilder(new MCS.Library.Data.Adapters.InLoadingCondition(builder => { builder.DataField = "CustomerID"; builder.AppendItem(model.CustomerID); }));
            csc.ForEach(cs => { Console.WriteLine(cs.CustomerName); Console.WriteLine(cs.ServiceModifyTime); });
        }

        [TestMethod]
        public void UpdateByCustomerCollectionInfoTest()
        {
            string[] customerIDs = { "85438", "1958876", "3990032", "605917", "1000801", "1000895", "1000981", "1001018", "1001054", "1001144", "1001906", "1001943", "1001962", "1002", "1002236", "1002267", "1002280", "1002326", "1002337", "1002907", "1002964", "1003215", "1003298", "1003300", "1003422", "1003612", "1003618", "1003624", "1004252", "1004283", "1004399", "1004406", "1004462", "1004488", "1004502", "1004509", "1004563", "1005178", "1005284", "1005324", "1005340", "1005391", "1005451", "1005469", "1005529", "1005578", "1006042", "1006058", "1006206", "1006260", "1006404", "1006421", "1006489", "1006508", "1006705", "1007173", "1007194", "1007204", "1007368", "1007474", "1007697", "1007718", "1007836", "1007850", "1008574", "1008610", "1008667", "1008712", "1008718", "1008782", "1008845", "1008860", "1008915", "1009849", "1009906", "1009915", "1009957", "1009996", "1010", "1010031", "1010058", "1010164", "1010610", "1010683", "1010727", "1010801", "1010815", "1010831", "1011111", "1011236", "1011266", "1012070", "1012088", "1012089", "1012133", "1012171", "1012212", "1012278", "1012343", "1012430", "1012886", "1012980", "1013029", "1013046", "1013098", "1013366", "1013383", "1013385", "1013429", "1014", "1014095", "1014097", "1014121", "1014302", "1014329", "1014437", "1014459", "1014641", "1015137", "1015191", "1015278", "1015292", "1015296", "1015304", "1015437", "1015443", "1015444", "1015841", "1015845", "1015877", "1015910", "1015927", "1016", "1016144", "1016278", "1016458", "1016846", "1016850", "1016901", "1016928", "1016954", "1016968", "1017028", "1017105", "1017132", "1017630", "1017665", "1017669", "1017678", "1017711", "1017742", "1017756", "1017809", "1017833", "1018164", "1018196", "1018221", "1018434", "1018479", "1018577", "1018608", "1018785", "1018794", "1019478", "1019591", "1019604", "1019618", "1019792", "1019822", "1019833", "1019865", "102", "1020687", "1020769", "1020784", "1020787", "1020860", "1020924", "1021307", "1021538", "1021551", "1022118", "1022126", "1022134", "1022136", "1022208", "1022220", "1022223", "1022316", "1022358", "1022608", "1022636", "1022644", "1022855", "1022880", "1022882", "1022948", "1022958", "1022964", "1023384" };
            List<CustomerSearchUpdateModel> customerIDList = new List<CustomerSearchUpdateModel>();
            customerIDs.ForEach(id => customerIDList.Add(new CustomerSearchUpdateModel() { CustomerID = id, Type = CustomerSearchUpdateType.Customer }));
            new CustomerSearchUpdateService().UpdateByCustomerCollectionInfo(customerIDList);
            Data.Customers.Entities.CustomerSearchCollection csc = CustomerSearchAdapter.Instance.LoadByInBuilder(new MCS.Library.Data.Adapters.InLoadingCondition(builder => { builder.DataField = "CustomerID"; builder.AppendItem(customerIDs); }));
            csc.ForEach(cs => { Console.WriteLine(cs.CustomerName); Console.WriteLine(cs.ServiceModifyTime); });
        }

        [TestMethod]
        public void InitCustomerSearchTest()
        {
            string[] customerIDs = { "85438", "1958876", "3990032", "605917", "1000801", "1000895", "1000981", "1001018", "1001054", "1001144", "1001906", "1001943", "1001962", "1002", "1002236", "1002267", "1002280", "1002326", "1002337", "1002907", "1002964", "1003215", "1003298", "1003300", "1003422", "1003612", "1003618", "1003624", "1004252", "1004283", "1004399", "1004406", "1004462", "1004488", "1004502", "1004509", "1004563", "1005178", "1005284", "1005324", "1005340", "1005391", "1005451", "1005469", "1005529", "1005578", "1006042", "1006058", "1006206", "1006260", "1006404", "1006421", "1006489", "1006508", "1006705", "1007173", "1007194", "1007204", "1007368", "1007474", "1007697", "1007718", "1007836", "1007850", "1008574", "1008610", "1008667", "1008712", "1008718", "1008782", "1008845", "1008860", "1008915", "1009849", "1009906", "1009915", "1009957", "1009996", "1010", "1010031", "1010058", "1010164", "1010610", "1010683", "1010727", "1010801", "1010815", "1010831", "1011111", "1011236", "1011266", "1012070", "1012088", "1012089", "1012133", "1012171", "1012212", "1012278", "1012343", "1012430", "1012886", "1012980", "1013029", "1013046", "1013098", "1013366", "1013383", "1013385", "1013429", "1014", "1014095", "1014097", "1014121", "1014302", "1014329", "1014437", "1014459", "1014641", "1015137", "1015191", "1015278", "1015292", "1015296", "1015304", "1015437", "1015443", "1015444", "1015841", "1015845", "1015877", "1015910", "1015927", "1016", "1016144", "1016278", "1016458", "1016846", "1016850", "1016901", "1016928", "1016954", "1016968", "1017028", "1017105", "1017132", "1017630", "1017665", "1017669", "1017678", "1017711", "1017742", "1017756", "1017809", "1017833", "1018164", "1018196", "1018221", "1018434", "1018479", "1018577", "1018608", "1018785", "1018794", "1019478", "1019591", "1019604", "1019618", "1019792", "1019822", "1019833", "1019865", "102", "1020687", "1020769", "1020784", "1020787", "1020860", "1020924", "1021307", "1021538", "1021551", "1022118", "1022126", "1022134", "1022136", "1022208", "1022220", "1022223", "1022316", "1022358", "1022608", "1022636", "1022644", "1022855", "1022880", "1022882", "1022948", "1022958", "1022964", "1023384" };
            List<string> customerIDList = new List<string>();
            customerIDs.ForEach(id => customerIDList.Add(id));
            new CustomerSearchUpdateService().InitCustomerSearch(customerIDList);
            Data.Customers.Entities.CustomerSearchCollection csc = CustomerSearchAdapter.Instance.LoadByInBuilder(new MCS.Library.Data.Adapters.InLoadingCondition(builder => { builder.DataField = "CustomerID"; builder.AppendItem(customerIDs); }));
            csc.ForEach(cs => { Console.WriteLine(cs.CustomerName); Console.WriteLine(cs.ServiceModifyTime); });
        }

        [TestMethod]
        public void UpdateByAssignsInfoTest()
        {
            CustomerSearchUpdateModel model = new CustomerSearchUpdateModel()
            {
                CustomerID = "3705922",
                ObjectID = "3705922",
                Type = CustomerSearchUpdateType.Assign
            };
            new CustomerSearchUpdateService().UpdateByCustomerInfo(model);
        }


        [TestMethod]
        public void UpdateByAssignsCollectionInfoTest()
        {
            string sql = "select distinct CustomerID from om.Assets_Current where CategoryType=1 and ISNULL(CustomerID,'') <> ''";
            DataSet ds = DbHelper.RunSqlReturnDS(sql, PPTS.Data.Orders.Adapters.OrdersAdapter.Instance.GetDbContext().Name);
            DataTable dt = ds.Tables[0];

            List<CustomerSearchUpdateModel> models = new List<CustomerSearchUpdateModel>();
            foreach (DataRow dr in dt.Rows)
            {
                models.Add(new CustomerSearchUpdateModel()
                {
                    CustomerID = dr["CustomerID"].ToString(),
                    ObjectID = dr["CustomerID"].ToString(),
                    Type = CustomerSearchUpdateType.Assign
                });
            }
            new CustomerSearchUpdateService().UpdateByCustomerCollectionInfo(models);
        }

        [TestMethod]
        public void UpdateByAssignInfoByTaskTest()
        {
            CustomerSearchUpdateModel model = new CustomerSearchUpdateModel()
            {
                CustomerID = "1179982",
                ObjectID = "1179982",
                Type = CustomerSearchUpdateType.Assign
            };

            PPTS.Data.Orders.UpdateCustomerSearchByCustomerTask.Instance.UpdateByCustomerInfoByTask(model);

            // PPTSCustomerSearchUpdateServiceProxy.Instance.UpdateByCustomerInfoByTask(model);
        }

        /*
        [TestMethod]
        public void UpdateByCustomerInfoByTaskTest()
        {
            CustomerSearchUpdateModel model = new CustomerSearchUpdateModel()
            {
                CustomerID = "605917",
                ObjectID = "605917",
                Type = CustomerSearchUpdateType.Customer
            };
            PPTSCustomerSearchUpdateServiceProxy.Instance.UpdateByCustomerInfoByTask(model);
        }

        [TestMethod]
        public void UpdateByCustomerInfoByTaskTest2()
        {
            SysAccomplishedTaskAdapter.Instance.ClearAll();
            SysTaskAdapter.Instance.ClearAll();
            CustomerSearchUpdateModel model = new CustomerSearchUpdateModel()
            {
                CustomerID = "605917",
                ObjectID = "605917",
                Type = CustomerSearchUpdateType.Customer
            };
            InvokeServiceTask task = new InvokeServiceTask()
            {
                TaskID = UuidHelper.NewUuidString(),
                TaskTitle = "CustomerSearch更新客户信息任务",
                ResourceID = UuidHelper.NewUuidString()
            };
            task.SvcOperationDefs.Add(PPTSCustomerSearchUpdateServiceProxy.Instance.PrepareCustomerWfServiceOperation(model));
            task.FillData();
            InvokeServiceTaskAdapter.Instance.Push(task);

            InvokeServiceTaskExecutor executor = new InvokeServiceTaskExecutor();

            executor.Execute(task);

            //return WfServiceInvoker.InvokeContext["ReturnValue"] as UserData;
        }
        */
    }
}
