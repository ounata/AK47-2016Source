using MCS.Library.Core;
using MCS.Library.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Common;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class ParentAdapterTest
    {
        [TestMethod]
        public void UpdateParent()
        {
            Parent parent = DataHelper.PrepareParentData();

            using (DbContext context = ParentAdapter.Instance.GetDbContext())
            {
                ParentAdapter.Instance.UpdateInContext(parent);

                context.ExecuteTimePointSqlInContext();
            }

            Console.WriteLine(parent.ParentCode);

            Parent loaded = ParentAdapter.Instance.Load(parent.ParentID);

            Assert.IsNotNull(loaded);
            parent.AreEqual(loaded);
        }

        [TestMethod]
        public void LoadInheritedParent()
        {
            InheritedParent parent = DataHelper.PrepareParentData();

            using (DbContext context = ParentAdapter.Instance.GetDbContext())
            {
                ParentAdapter.Instance.UpdateInContext(parent);

                context.ExecuteTimePointSqlInContext();
            }

            Console.WriteLine(parent.ParentCode);

            Parent loaded = GenericParentAdapter<InheritedParent, List<InheritedParent>>.Instance.Load(parent.ParentID);

            Assert.IsNotNull(loaded);
            parent.AreEqual(loaded);
        }

        [TestMethod]
        public void LoadPrimaryParentInContext()
        {
            InheritedPotentialCustomer customer = DataHelper.PreparePotentialCustomerData();
            InheritedParent parent = DataHelper.PrepareParentData();
            CustomerParentRelation relation = DataHelper.PrepareCustomerRelation(customer.CustomerID, parent.ParentID);

            PotentialCustomerAdapter.Instance.Update(customer);
            ParentAdapter.Instance.Update(parent);
            CustomerParentRelationAdapter.Instance.Update(relation);

            Parent loaded = null;

            ParentAdapter.Instance.LoadPrimaryParentInContext(customer.CustomerID, p => loaded = p);

            ParentAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            parent.AreEqual(loaded);
        }
    }
}
