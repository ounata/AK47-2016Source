using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customer.Controllers;
using PPTS.WebAPI.Customer.ViewModels.PotentialCustomers;
using System;

namespace PPTS.WebAPI.Customer.Test
{
    [TestClass]
    public class PotentialCustomerTest
    {
        [ClassInitialize()]
        public static void Init(TestContext context)
        {
        }

        [TestMethod]
        public void GetAllPotentialCustomersForGetAll()
        {
            PotentialCustomersController controller = PrepareController();

            var model = new CreatablePortentialCustomerModel();

            model.PrimaryParent = DataHelper.PrepareParentData();
            model.Customer = DataHelper.PreparePotentialCustomerData();

            controller.CreateCustomer(model);

            PotentialCustomerQueryResult result = controller.GetAllCustomers(new PotentialCustomerQueryCriteriaModel
            {
                Name = model.Customer.CustomerName,
                CustomerCode = model.Customer.CustomerCode,
                PageParams = new PageRequestParams(),
                OrderBy = new OrderBySqlClauseBuilder().AppendItem("CreateTime", FieldSortDirection.Descending).ToOrderByRequestItems()
            });

            Assert.IsNotNull(result.QueryResult.PagedData.Count > 0);
            Assert.IsNotNull(result.QueryResult.TotalCount > 0);
        }

        [TestMethod]
        public void CreatePotentialCustomerForGet()
        {
            PotentialCustomersController controller = PrepareController();

            var model = controller.CreateCustomer();

            Assert.IsNotNull(model.Customer);
            Assert.IsNotNull(model.PrimaryParent);
            Assert.IsTrue(model.Dictionaries.Count > 0);
        }

        [TestMethod]
        public void CreatePotentialCustomerForSave()
        {
            PotentialCustomersController controller = PrepareController();

            var model = new CreatablePortentialCustomerModel();

            model.PrimaryParent = DataHelper.PrepareParentData();
            model.Customer = DataHelper.PreparePotentialCustomerData();

            controller.CreateCustomer(model);

            PotentialCustomer customerLoaded = PotentialCustomerAdapter.Instance.Load(model.Customer.CustomerID);
            Parent parentLoaded = ParentAdapter.Instance.Load(model.PrimaryParent.ParentID);
            CustomerParentRelation relationLoaded = CustomerParentRelationAdapter.Instance.Load(model.Customer.CustomerID, model.PrimaryParent.ParentID);
            PhoneCollection customerPhones = PhoneAdapter.Instance.LoadByOwnerID(model.Customer.CustomerID);
            PhoneCollection parentPhones = PhoneAdapter.Instance.LoadByOwnerID(model.PrimaryParent.ParentID);

            Assert.IsNotNull(customerLoaded);
            Assert.IsNotNull(parentLoaded);
            Assert.IsNotNull(relationLoaded);

            Assert.AreEqual(model.Customer.CustomerID, relationLoaded.CustomerID);
            Assert.AreEqual(model.PrimaryParent.ParentID, relationLoaded.ParentID);

            Assert.AreEqual(2, customerPhones.Count);
            Assert.AreEqual(2, parentPhones.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(SystemSupportException))]
        public void CreatePotentialCustomerValidator()
        {
            PotentialCustomersController controller = PrepareController();

            var model = new CreatablePortentialCustomerModel();

            model.PrimaryParent = DataHelper.PrepareParentData();
            model.PrimaryParent.ParentName = string.Empty;
            model.Customer = DataHelper.PreparePotentialCustomerData();
            model.Customer.CustomerName = string.Empty;

            controller.CreateCustomer(model);
        }

        [TestMethod]
        public void UpdatePotentialCustomerForGet()
        {
            PotentialCustomersController controller = PrepareController();

            var model = new CreatablePortentialCustomerModel();

            model.PrimaryParent = DataHelper.PrepareParentData();
            model.Customer = DataHelper.PreparePotentialCustomerData();

            controller.CreateCustomer(model);

            EditablePotentialCustomerModel returnModel = controller.UpdateCustomer(model.Customer.CustomerID);

            Assert.AreEqual(model.Customer.CustomerID, returnModel.Customer.CustomerID);
            Assert.AreEqual(model.Customer.PrimaryPhone, returnModel.Customer.PrimaryPhone);
            Assert.AreEqual(model.Customer.SecondaryPhone, returnModel.Customer.SecondaryPhone);
        }

        [TestMethod]
        public void UpdatePotentialCustomerForSave()
        {
            PotentialCustomersController controller = PrepareController();

            var model = new CreatablePortentialCustomerModel();

            model.PrimaryParent = DataHelper.PrepareParentData();
            model.Customer = DataHelper.PreparePotentialCustomerData();

            controller.CreateCustomer(model);

            EditablePotentialCustomerModel modelNeedToUpdate = controller.UpdateCustomer(model.Customer.CustomerID);

            modelNeedToUpdate.Customer.CustomerName = "New Name";
            modelNeedToUpdate.Customer.PrimaryPhone = "13601307607";

            controller.UpdateCustomer(modelNeedToUpdate);

            EditablePotentialCustomerModel returnModel = controller.UpdateCustomer(model.Customer.CustomerID);

            Assert.AreEqual(modelNeedToUpdate.Customer.CustomerID, returnModel.Customer.CustomerID);
            Assert.AreEqual(modelNeedToUpdate.Customer.PrimaryPhone, returnModel.Customer.PrimaryPhone);
            Assert.AreEqual(modelNeedToUpdate.Customer.CustomerName, returnModel.Customer.CustomerName);
        }

        private static PotentialCustomersController PrepareController()
        {
            return new PotentialCustomersController();
        }
    }
}
