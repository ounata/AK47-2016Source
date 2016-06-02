using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.Controllers;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.Controllers.Tests
{
    [TestClass()]
    public class PotentialCustomersControllerTests
    {
        private PotentialCustomersController pController;
        [TestInitialize]
        public void init()
        {
            pController = new PotentialCustomersController();
        }
        [TestMethod()]
        public void CreateCustomerStaffRelationsTest()
        {
            EditCustomerStaffRelationsModel staffRealtion = new EditCustomerStaffRelationsModel();

            var arr = new CustomerStaffRelation[] {
                new CustomerStaffRelation() {
                    CreateTime =DateTime.Now,
                    CustomerID="1000029",
                    CreatorID="s001",
                    CreatorName="张三",
                    StaffID="16424060001",
                    StaffJobID="71861",
                    StaffJobName="校教育咨询师",
                    StaffJobOrgID="2120-Org",
                    StaffJobOrgName="北京分公司通州校区",
                    StaffName="于志国"}
            };
            var list = new List<CustomerStaffRelation>(arr);
            staffRealtion.CustomerStaffRelations = list;
            pController.CreateCustomerStaffRelations(staffRealtion);
        }
    }
}