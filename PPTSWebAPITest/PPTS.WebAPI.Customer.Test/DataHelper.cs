﻿using MCS.Library.Core;
using PPTS.Data.Common;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;

namespace PPTS.WebAPI.Customer.Test
{
    internal static class DataHelper
    {
        public static ParentModel PrepareParentData()
        {
            var result = new ParentModel();

            result.ParentID = UuidHelper.NewUuidString();
            result.ParentName = String.Format("测试添加家长{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            result.Gender = GenderType.Female;
            result.PrimaryPhone = "13501126279".ToPhone(result.ParentID, true);
            result.SecondaryPhone = "021-67889099".ToPhone(result.ParentID, false);

            return result;
        }

        public static PotentialCustomerModel PreparePotentialCustomerData()
        {
            var result = new PotentialCustomerModel();

            result.CustomerID = UuidHelper.NewUuidString();
            result.CustomerName = String.Format("测试添加潜在用户{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            result.Gender = GenderType.Female;
            result.PrimaryPhone = "18901026477".ToPhone(result.CustomerID, true);
            result.SecondaryPhone = "021-56768900".ToPhone(result.CustomerID, false);

            return result;
        }

        public static PhoneCollection PreparePhonesData(string ownerID)
        {
            PhoneCollection phones = new PhoneCollection();

            phones.Add(new Phone() { OwnerID = ownerID, ItemID = 0, PhoneType = PhoneTypeDefine.HomePhone, PhoneNumber = "021-68190909", IsPrimary = true });
            phones.Add(new Phone() { OwnerID = ownerID, ItemID = 1, PhoneType = PhoneTypeDefine.MobilePhone, PhoneNumber = "18901067455", IsPrimary = false });

            return phones;
        }

        public static CustomerParentRelation PrepareCustomerParentRelation(string studentID, string parentID)
        {
            var relation = new CustomerParentRelation() { CustomerID = studentID, ParentID = parentID };

            relation.CustomerRole = "1";
            relation.ParentRole = "1";
            relation.IsPrimary = true;

            return relation;
        }
    }
}
