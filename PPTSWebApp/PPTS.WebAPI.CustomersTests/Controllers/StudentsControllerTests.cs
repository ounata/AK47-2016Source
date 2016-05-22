using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.Controllers;
using PPTS.WebAPI.Customers.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.Controllers.Tests
{
    [TestClass()]
    public class StudentsControllerTests
    {
        [TestMethod()]
        public void getTeachersTest()
        {
            TeachersQueryCriteriaModel criteria = new TeachersQueryCriteriaModel();
            var result = TeacherJobViewAdapter.Instance.Load(builder => builder.AppendItem("CampusID", "18-org").AppendItem("JobOrgType", (int)criteria.JobOrgType));

        }

        [TestMethod()]
        public void getAllCustomerTeacherRelationsTest()
        {
            CustomerTeacherRelationQueryCriteriaModel criteria = new CustomerTeacherRelationQueryCriteriaModel() { CustomerID= "806587" };
            var result = CustomerTeacherRelationAdapter.Instance.Load(builder => builder.AppendItem("CustomerID", criteria.CustomerID), DateTime.MinValue);
            
        }
    }
}