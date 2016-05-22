using MCS.Library.Core;
using MCS.Library.Net.SNTP;
using PPTS.Data.Common;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Test
{
    internal static class DataHelper
    {
        public static InheritedParent PrepareParentData()
        {
            InheritedParent result = new InheritedParent();

            result.ParentID = UuidHelper.NewUuidString();
            result.ParentName = string.Format("测试添加家长{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            result.Gender = GenderType.Female;

            return result;
        }

        public static InheritedPotentialCustomer PreparePotentialCustomerData()
        {
            InheritedPotentialCustomer result = new InheritedPotentialCustomer();

            result.CustomerID = UuidHelper.NewUuidString();
            result.CustomerName = string.Format("测试添加潜在用户{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            result.Gender = GenderType.Female;

            return result;
        }

        public static InheritedCustomer PrepareCustomerData()
        {
            InheritedCustomer result = new InheritedCustomer();

            result.CustomerID = UuidHelper.NewUuidString();
            result.CustomerCode = UuidHelper.NewUuidString();
            result.CustomerName = string.Format("测试添加用户{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            result.Gender = GenderType.Female;

            return result;
        }

        public static PhoneCollection PreparePhonesData(string ownerID)
        {
            PhoneCollection phones = new PhoneCollection();

            phones.Add(new Phone() { OwnerID = ownerID, ItemID = 0, PhoneType = PhoneTypeDefine.HomePhone, PhoneNumber = "021-68190909", IsPrimary = true });
            phones.Add(new Phone() { OwnerID = ownerID, ItemID = 1, PhoneType = PhoneTypeDefine.MobilePhone, PhoneNumber = "18901067455", IsPrimary = false });

            return phones;
        }

        public static CustomerParentRelation PrepareCustomerRelation(string studentID, string parentID)
        {
            CustomerParentRelation relation = new CustomerParentRelation() { CustomerID = studentID, ParentID = parentID };

            relation.CustomerRole = "1";
            relation.ParentRole = "1";
            relation.IsPrimary = true;

            return relation;
        }

        public static CustomerFollow PrepareFollow(string studentID)
        {
            CustomerFollow follow = new CustomerFollow()
            {
                CustomerID = studentID,
                FollowerID = UuidHelper.NewUuidString(),
                FollowTime = SNTPClient.AdjustedTime,
                NextFollowTime = SNTPClient.AdjustedTime.AddDays(1),
                OrgID = UuidHelper.NewUuidString(),
                OrgType = "Campus",
                OrgName = "Campus"
            };

            return follow;
        }
    }
}
