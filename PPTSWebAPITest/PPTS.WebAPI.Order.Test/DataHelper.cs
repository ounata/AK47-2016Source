using MCS.Library.Core;
using PPTS.Data.Common;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Entities;
using PPTS.WebAPI.Orders.ViewModels.AssignConditions;
using System;

namespace PPTS.WebAPI.Orders.Test
{
    internal static class DataHelper
    {
        //public static ParentModel PrepareParentData()
        //{
        //    var result = new ParentModel();
        //    result.ParentID = UuidHelper.NewUuidString();
        //    result.ParentName = String.Format("测试添加家长{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
        //    result.Gender = GenderType.Female;
        //    result.PrimaryPhone = "13501126279".ToPhone(result.ParentID, true);
        //    result.SecondaryPhone = "021-67889099".ToPhone(result.ParentID, false);
        //    return result;
        //}


        public static Order PrepareOrder()
        {
            Order oM = new Order();
            oM.OrderID = UuidHelper.NewUuidString();

            return oM;
        }
        public static OrderItem PrepareOrderItem(string orderID)
        {
            OrderItem oIM = new OrderItem();
            oIM.OrderID = orderID;
            oIM.ItemID = UuidHelper.NewUuidString();
            return oIM;
        }
        public static Asset PrepareAsset(string orderItemID)
        {
            Asset aM = new Asset();
            aM.AssetID = UuidHelper.NewUuidString();         
            aM.CustomerID = UuidHelper.NewUuidString();
            aM.CustomerCampusID = UuidHelper.NewUuidString();
            aM.Amount = new Random().Next(10,50);
            aM.AssetRefID = orderItemID;
            return aM;
        }


        public static AssignCondition PrepareAssignCondition()
        {
            throw new NotSupportedException("准备排课条件");
        }

     
    }
}
