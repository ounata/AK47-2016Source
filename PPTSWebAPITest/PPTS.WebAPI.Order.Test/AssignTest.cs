using System;
using PPTS.Data.Orders.Adapters;
using PPTS.WebAPI.Orders.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PPTS.WebAPI.Orders.Test
{
    [TestClass]
    public class AssignTest
    {

    

        [TestMethod]
        public void GetAssignCondition()
        {

        }

        [TestMethod]
        public void AssignReset()
        {
            
        }

        [TestMethod]
        public void GetOrder()
        {
            var oM = DataHelper.PrepareOrder();
            var oIM = DataHelper.PrepareOrderItem(oM.OrderID);
            var aM = DataHelper.PrepareAsset(oIM.ItemID);

            OrdersAdapter.Instance.Update(oM);
            OrderItemAdapter.Instance.Update(oIM);
            AssetAdapter.Instance.Update(aM);

            var controll = PrepareController();
            
            

        }


        private static StudentAssignmentController PrepareController()
        {
            //IUser user = OguObjectSettings.GetConfig().Objects["hq"].User;
            //Thread.CurrentPrincipal = new DeluxePrincipal(new DeluxeIdentity(user));

            return new StudentAssignmentController();
        }
    }
}
