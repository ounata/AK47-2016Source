using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Orders.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Test
{
    [TestClass]
    public class AssignsAdapterTest
    {
        [TestMethod]
        public void LoadAccountConfirmAssignByDateTimeTest()
        {
           decimal confirmValue= AssignsAdapter.Instance.LoadAccountConfirmAssignByDateTime("145291", DateTime.MinValue);
            Console.WriteLine("confirmValue:{0}", confirmValue);
        }
        [TestMethod]
        public void LoadDiscountReturnConfirmAssignByDateTimeTest()
        {
            decimal discountReturnValue = AssignsAdapter.Instance.LoadDiscountReturnConfirmAssignByDateTime("145291",(decimal)0.78, DateTime.MinValue);
            Console.WriteLine("discountReturnValue:{0}", discountReturnValue);
        }
        [TestMethod]
        public void ExistAccountConfirmAssignByDateTimeTest()
        {
            bool isExist = AssignsAdapter.Instance.ExistAccountConfirmAssignByDateTime("145291",  DateTime.MinValue);
            Console.WriteLine("isExist:{0}", isExist);
        }
        [TestMethod]
        public void LoadAccountRefundInfoByDateTimeTest()
        {
            decimal assignValue = 0;
            decimal discountReturnValue = 0;
            AssignsAdapter.Instance.LoadAccountRefundInfoByDateTime("145291", (decimal)0.78, DateTime.MinValue,ref assignValue,ref discountReturnValue);
            Console.WriteLine("assignValue:{0}", assignValue);
            Console.WriteLine("discountReturnValue:{0}", discountReturnValue);
        }
    }
}
