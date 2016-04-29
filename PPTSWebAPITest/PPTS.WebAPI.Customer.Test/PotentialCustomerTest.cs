using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.Controllers;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Threading;

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
                Keyword = "用户",
                PageParams = new PageRequestParams(),
                OrderBy = new OrderBySqlClauseBuilder().AppendItem("pcf.CreateTime", FieldSortDirection.Descending).ToOrderByRequestItems()
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

            CreatablePortentialCustomerModel model = new CreatablePortentialCustomerModel();

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

            model.PrimaryParent.AreEqual(returnModel.Parent);
            model.Customer.PrimaryPhone.AreEqual(returnModel.Customer.PrimaryPhone);
            model.Customer.SecondaryPhone.AreEqual(returnModel.Customer.SecondaryPhone);
        }

        [TestMethod]
        public void UpdatePotentialCustomerForSave()
        {
            PotentialCustomersController controller = PrepareController();

            var model = new CreatablePortentialCustomerModel();

            model.PrimaryParent = DataHelper.PrepareParentData();
            model.Customer = DataHelper.PreparePotentialCustomerData();

            Console.WriteLine(model.Customer.CustomerID);

            controller.CreateCustomer(model);

            DBTimePointActionContext.Current.TimePoint = DateTime.MinValue;

            EditablePotentialCustomerModel modelNeedToUpdate = controller.UpdateCustomer(model.Customer.CustomerID);

            modelNeedToUpdate.Customer.CustomerName = "New Name";
            modelNeedToUpdate.Customer.PrimaryPhone.PhoneNumber = "13601307607";

            controller.UpdateCustomer(modelNeedToUpdate);

            EditablePotentialCustomerModel returnModel = controller.UpdateCustomer(model.Customer.CustomerID);

            Assert.AreEqual(modelNeedToUpdate.Customer.CustomerID, returnModel.Customer.CustomerID);
            modelNeedToUpdate.Customer.PrimaryPhone.AreEqual(returnModel.Customer.PrimaryPhone);
            Assert.AreEqual(modelNeedToUpdate.Customer.CustomerName, returnModel.Customer.CustomerName);
        }

        private static PotentialCustomersController PrepareController()
        {
            IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;

            Thread.CurrentPrincipal = new DeluxePrincipal(new DeluxeIdentity(user));

            return new PotentialCustomersController();
        }
    }
}
