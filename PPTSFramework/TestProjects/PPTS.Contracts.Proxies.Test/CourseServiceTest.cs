using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Contracts.Orders.Models;

namespace PPTS.Contracts.Proxies.Test
{
    /// <summary>
    /// 课程相关单元测试
    /// </summary>
    [TestClass]
    public class CourseServiceTest
    {

        [TestMethod]
        public void QueryPeriodCourseByCustomerIDTest()
        {
            HasPeriodCourseQueryResult queryResult = PPTSCourseQueryServiceProxy.Instance.QueryPeriodCourseByCustomerID("3985829", DateTime.Now);
            Assert.IsNotNull(queryResult);
            Console.WriteLine("HasPeriodInside:" + queryResult.HasPeriodInside);
            Console.WriteLine("HasPeriodOutside:" + queryResult.HasPeriodOutside);

        }

        [TestMethod]
        public void QueryOrderInfoForRefundByAccountIDTest()
        {
            OrderInfoForRefundQueryModel model = new OrderInfoForRefundQueryModel()
            {
                AccountID = "145221",
                LastChargePayDate =DateTime.Parse( "2016-04-01"),
                LastestChargePayDate = DateTime.Parse("2016-03-01"),
                LastestRefundVerifyDate = DateTime.Parse("2016-05-01")
            };
            OrderInfoForRefundQueryResult queryResult = PPTSCourseQueryServiceProxy.Instance.QueryOrderInfoForRefundByAccountID(model);
            Assert.IsNotNull(queryResult);
            Console.WriteLine("AccountID:{0}", queryResult.AccountID);
            Console.WriteLine("AssetMoney:{0}", queryResult.AssetMoney);
            Console.WriteLine("ConsumptionValue:{0}", queryResult.ConsumptionValue);
            Console.WriteLine("ReallowanceMoney:{0}", queryResult.ReallowanceMoney);
            Console.WriteLine("StartTime:{0}", queryResult.StartTime);

        }
    }
}
