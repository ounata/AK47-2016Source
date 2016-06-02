using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Microsoft.Owin.Hosting;
using System.Net.Http;
using PPTS.ExtServices.UnionPay.Models.Statement;
using PPTS.ExtServices.UnionPay.Models.Response;

namespace PPTS.ExtServices.UnionPay.Test
{
    [TestClass]
    public class SyncPOSRecordsRequestTest
    {
        [TestMethod]
        public void AddPOSRecordsTest()
        {
            string url = ConfigurationManager.AppSettings["UnionPayUrl"];

            var statementModel = new StatementModel() {
                LiquidationDate = DateTime.Parse("2016-05-23"),
                TransactionTime = DateTime.Parse("2016-05-23 14:26:36"),
                RefNum = "143933024955",
                CardNumber = "489592******5578",
                MerchantNumber = "898110282990510",
                TerminalNo = "01080209",
                Amount = (decimal)2700
            };
            using (WebApp.Start<Startup>(url))
            {
                HttpClient client = new HttpClient();
                var response = client.PostAsJsonAsync<StatementModel>(url+ "api/PPTSUnionPaySale/PutStatement", statementModel).Result;
                var result = response.Content.ReadAsAsync<ResponseModel>().Result;
                Console.WriteLine(string.Format("错误代码：{0}，错误信息：{1}", result.Flag, result.ErrorMessage));
                Assert.AreEqual(result.Flag, "0");
            }
             Assert.IsTrue(true);
        }
    }
}
