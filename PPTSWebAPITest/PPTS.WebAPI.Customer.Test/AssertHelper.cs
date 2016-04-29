﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customer.Test
{
    internal static class AssertHelper
    {
        public static void AreEqual(this Parent expected, Parent actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.ParentID, actual.ParentID);
            Assert.AreEqual(expected.ParentName, actual.ParentName);
            Assert.AreEqual(expected.ParentCode, actual.ParentCode);
            Assert.AreEqual(expected.Gender, actual.Gender);
        }

        public static void AreEqual(this PotentialCustomer expected, PotentialCustomer actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.CustomerID, actual.CustomerID);
            Assert.AreEqual(expected.CustomerName, actual.CustomerName);
            Assert.AreEqual(expected.CustomerCode, actual.CustomerCode);
            Assert.AreEqual(expected.Gender, actual.Gender);
        }

        public static void AreEqual(this Phone expected, Phone actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.OwnerID, actual.OwnerID);
            Assert.AreEqual(expected.ItemID, actual.ItemID);
            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
        }

        public static void AreEqual(this PhoneCollection expected, PhoneCollection actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.Count, actual.Count);

            foreach (Phone expectedItem in expected)
            {
                Phone actualItem = actual.Find(p => p.OwnerID == expectedItem.OwnerID && p.ItemID == expectedItem.ItemID);

                Assert.IsNotNull(actualItem);

                expectedItem.AreEqual(actualItem);
            }
        }

        public static void AreEqual(this CustomerParentRelation expected, CustomerParentRelation actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.CustomerID, actual.CustomerID);
            Assert.AreEqual(expected.ParentID, actual.ParentID);
            Assert.AreEqual(expected.CustomerRole, actual.CustomerRole);
            Assert.AreEqual(expected.ParentRole, actual.ParentRole);
        }
    }
}
