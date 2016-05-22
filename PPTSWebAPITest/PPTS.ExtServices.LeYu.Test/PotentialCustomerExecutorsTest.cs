using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.ExtServices.LeYu.Models.PotentialCustomers;
using System.Collections.Generic;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Common.Adapters;
using MCS.Library.Net.SNTP;
using MCS.Library.Core;
using PPTS.ExtServices.LeYu.Models.CallingOrgCenterConfig;
using PPTS.ExtServices.LeYu.Common;
using System.Linq;
using PPTS.Data.Common.Security;
using PPTS.Data.Common;

namespace PPTS.ExtServices.LeYu.Test
{
    [TestClass]
    public class PotentialCustomerExecutorsTest
    {
        [TestMethod]
        public void test1()
        {
            //List<CallingOrgCenterCofigModel> lst = new List<CallingOrgCenterCofigModel>();
            //lst.Add(new CallingOrgCenterCofigModel() { CallingType = "xx", JobName = "xxx", SortId = "0" });
            //lst.Add(new CallingOrgCenterCofigModel() { CallingType = "xxx", JobName = "xxxx", SortId = "1" });
            //lst.Add(new CallingOrgCenterCofigModel() { CallingType = "xxxx", JobName = "xxxxx", SortId = "2" });
            //int s = lst.Min(a => Convert.ToInt32(a.SortId));

            //PPTSJobCollection jobs = new PPTSJobCollection() {
            //    new PPTSJob() { ID = "1",Name = "111"},
            //    new PPTSJob() { ID = "2",Name = "222"},
            //    new PPTSJob() { ID = "3",Name = "333"}
            //};
            //List<PPTSJob> jobst = jobs.Where(j => j.ID == "1").ToList();
            //Assert.IsNotNull(jobst);
            //jobst.ForEach(
            //    j => {
            //        Console.WriteLine(j.ID);
            //    }
            //    );
           
            Assert.IsTrue(true);
            
        }

        [TestMethod]
        public void AddPotentialCustomerExecutorTest()
        {


            //CreatablePortentialCustomerModel model = new CreatablePortentialCustomerModel();
            //model.PotentialCustomer = new Data.Customers.Entities.PotentialCustomer()
            //{
            //    OrgID= "2126",
            //    OrgName= "北京分公司远大路校区",
            //    OrgType=PPTS.Data.Common.OrgTypeDefine.Campus,
            //    //CustomerID = UuidHelper.NewUuidString(),
            //    CustomerID = "c5a7be62-7e5a-a0d6-4052-b319fb07bfd3",
            //    CustomerName = "张三"
            //};
            //model.Parent = new Data.Customers.Entities.Parent()
            //{
            //    ParentID = "8db414f2-6494-b961-4054-f92e9824e471",
            //    //ParentID = UuidHelper.NewUuidString(),
            //    ParentName = "张三父"
            //};
            
            
            //Dictionary<string, IEnumerable<PPTS.Data.Common.Entities.BaseConstantEntity>> dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(PotentialCustomer), typeof(Parent));

            //KeyValuePair<string, IEnumerable<PPTS.Data.Common.Entities.BaseConstantEntity>> a = dictionaries.Where(d => d.Key == "C_Code_Abbr_BO_Customer_Source").SingleOrDefault();
            //a.Value.Where(v => v.Value == "北京晚报");
            
            //model.CustomerParentRelation = new Data.Customers.Entities.CustomerParentRelation()
            //{
            //    CustomerID = model.PotentialCustomer.CustomerID,
            //    CustomerRole = "1",
            //    ParentID = model.Parent.ParentID,
            //    ParentRole = "1"
            //};
            //model.CustomerFollow = new CustomerFollow()
            //{
            //    OrgID = "2126",
            //    OrgName = "北京分公司远大路校区",
            //    OrgType = PPTS.Data.Common.OrgTypeDefine.Campus,
            //    CustomerID = model.PotentialCustomer.CustomerID,
            //    FollowerID = UuidHelper.NewUuidString(),
            //    FollowTime = SNTPClient.AdjustedTime
            //};

            //model.UpdatePotentialCustomer();
            /*
            delete from [CM].[CustomerParentRelations] where CustomerID='c5a7be62-7e5a-a0d6-4052-b319fb07bfd3'
            delete from CM.CustomerFollows where  CustomerID='c5a7be62-7e5a-a0d6-4052-b319fb07bfd3'
            delete from [CM].[PotentialCustomers] where CustomerID='c5a7be62-7e5a-a0d6-4052-b319fb07bfd3'
            delete from [CM].[Parents] where ParentID= '8db414f2-6494-b961-4054-f92e9824e471'*/
            Assert.IsTrue(true);
        }
        
    }
}
