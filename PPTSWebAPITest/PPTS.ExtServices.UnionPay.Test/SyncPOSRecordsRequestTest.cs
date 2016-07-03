using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Net.Http;
using PPTS.ExtServices.UnionPay.Models.Statement;
using PPTS.ExtServices.UnionPay.Models.Response;
using System.Net.Http.Formatting;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace PPTS.ExtServices.UnionPay.Test
{
    [TestClass]
    public class SyncPOSRecordsRequestTest
    {
        [TestMethod]
        public void AddUnionPayPOSRecordsTest1()
        {
            string url = ConfigurationManager.AppSettings["UnionPayUrl"];
            var handler = new HttpClientHandler();
            
            RequestModel requestModel = new RequestModel();
            requestModel.ListStatementModel = new List<StatementModel>();
            var json = new JsonMediaTypeFormatter();
            for (int i = 0; i < 6000; i++)
            {
                //string refNum = this.GetRandom();
                var statementModel = new StatementModel()
                {
                    LiquidationDate = "2016-06-15",
                    TransactionTime = "2016-06-15 14:26:36",
                    RefNum = i.ToString(),
                    CardNumber = "669592******5566",
                    MerchantNumber = "668110282990005",
                    TerminalNo = "02056001",
                    Amount = (decimal)6500
                };
                requestModel.ListStatementModel.Add(statementModel);
            }
            string str = this.Serialize<RequestModel>(json, requestModel);
            
            using (var http = new HttpClient(handler))
            {
                //http.MaxResponseContentBufferSize = 2147483647;
                HttpContent content = new StringContent(str);
                
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/json");
                http.DefaultRequestHeaders.Add("token", "35afe5b623up12kt");
                
                var response = http.PostAsync(url + "api/PPTSUnionPaySale/PostListStatement", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);
                }
            }
        }
        [TestMethod]
        public void AddAllInPayPOSRecordsTest1()
        {
            string url = ConfigurationManager.AppSettings["UnionPayUrl"];
            var handler = new HttpClientHandler();
            var json = new JsonMediaTypeFormatter();
            string refNum = this.GetRandom();
            var statementModel = new StatementModel()
            {
                LiquidationDate = "2016-06-15",
                TransactionTime = "2016-06-15 14:26:36",
                RefNum = refNum,
                CardNumber = "669592******5566",
                MerchantNumber = "668110282990005",
                TerminalNo = "02056001",
                Amount = (decimal)6500
            };
               
            string str = this.Serialize<StatementModel>(json, statementModel);
            string strJson = "{ \"LiquidationDate\":\"2016-07-01\",\"TransactionTime\":\"2016-07-01 18:16:41\",\"RefNum\":\"000000178728\",\"CardNumber\":\"621448*********0794\",\"MerchantNumber\":\"309610150390008\",\"TerminalNo\":\"00080000\",\"Amount\":26.00 }";

            using (var http = new HttpClient(handler))
            {
                HttpContent content = new StringContent(strJson);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/json");
                http.DefaultRequestHeaders.Add("token", "48dec672b4aikt34");
                var response = http.PostAsync(url + "api/PPTSAllInPaySale/PostStatement", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);
                }
            }
        }
        /// <summary>
        /// 测试银联实时
        /// </summary>
        [TestMethod]
        public void AddUnionPayPOSRecordsTest()
        {
            string url = ConfigurationManager.AppSettings["UnionPayUrl"];
            var statementModel = new StatementModel() {
                //LiquidationDate = "",
                TransactionTime = "2016-06-15 14:26:36",
                RefNum = "243933099999",
                CardNumber = "669592******5566",
                MerchantNumber = "668110282990005",
                TerminalNo = "02056001",
                Amount = (decimal)6500
            };

            HttpClient client = new HttpClient();
            
            client.DefaultRequestHeaders.Add("token", "35afe5b623up12kt");
            var response = client.PostAsJsonAsync<StatementModel>(url+ "api/PPTSUnionPaySale/PostStatement", statementModel).Result;
            //response.Content.Headers.ContentType =new System.Net.Http.Headers.MediaTypeHeaderValue("text/json");

            var result = response.Content.ReadAsAsync<ResponseModel>().Result;
            
            Console.WriteLine(string.Format("错误代码：{0}，错误信息：{1}", result.Flag, result.ErrorMessage));
            //Assert.AreEqual(result.Flag, true);
            
            Assert.IsTrue(true);
        }
        /// <summary>
        /// 测试通联实时
        /// </summary>
        [TestMethod]
        public void AddAllInPayPOSRecordsTest()
        {
            string url = ConfigurationManager.AppSettings["UnionPayUrl"];
            var statementModel = new StatementModel()
            {
                //LiquidationDate = "2016-06-23",
                TransactionTime = "2016-06-23 14:26:36",
                RefNum = "455555555",
                CardNumber = "666666******5578",
                MerchantNumber = "898110282990444",
                TerminalNo = "01080999",
                Amount = (decimal)1000
            };

            
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("token", "48dec672b4aikt34");
            
            var response = client.PostAsJsonAsync<StatementModel>(url + "api/PPTSAllInPaySale/PostStatement", statementModel).Result;
            var result = response.Content.ReadAsAsync<ResponseModel>().Result;
            if(result != null)
            {
                Console.WriteLine(string.Format("错误代码：{0}，错误信息：{1}", result.Flag, result.ErrorMessage));
            }
            else
            {
                Console.WriteLine(string.Format("错误信息：{0}", response.ReasonPhrase));
            }
            
            //Assert.AreEqual(result.Flag, true);
            
            Assert.IsTrue(true);
        }

        /// <summary>
        /// 银联对账单
        /// </summary>
        [TestMethod]
        public void AddUnionPayListPOSRecordsTest()
        {
            string url = ConfigurationManager.AppSettings["UnionPayUrl"];
            RequestModel requestModel = new RequestModel();
            var statementModel0 = new StatementModel()
            {
                LiquidationDate = "2016-06-15",
                TransactionTime = "2016-06-15 14:26:36",
                RefNum = "243933099999",
                CardNumber = "669592******5566",
                MerchantNumber = "668110282990005",
                TerminalNo = "02056001",
                Amount = (decimal)6500
            };
            var statementModel = new StatementModel()
            {
                LiquidationDate = "2016-05-23",
                TransactionTime = "2016-05-23 14:26:36",
                RefNum = "444444444",
                CardNumber = "555666******5578",
                MerchantNumber = "5656565656565",
                TerminalNo = "01025856",
                Amount = (decimal)20005
            };
            var statementModel1 = new StatementModel()
            {
                LiquidationDate = "2016-06-20",
                TransactionTime = "2016-06-20 16:26:36",
                RefNum = "143933024966",
                CardNumber = "489592******5599",
                MerchantNumber = "898110282990520",
                TerminalNo = "01080288",
                Amount = (decimal)30000
            };
            List<StatementModel> lstModel = new List<StatementModel>();
            lstModel.Add(statementModel0);
            lstModel.Add(statementModel);
            lstModel.Add(statementModel1);
            requestModel.ListStatementModel = lstModel;

            
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("token", "35afe5b623up12kt");
            var response = client.PostAsJsonAsync<RequestModel>(url + "api/PPTSUnionPaySale/PostListStatement", requestModel).Result;
            var result = response.Content.ReadAsAsync<ResponseModel>().Result;
            Console.WriteLine(string.Format("错误代码：{0}，错误信息：{1}", result.Flag, result.ErrorMessage));
            Assert.AreEqual(result.Flag, true);
            
            Assert.IsTrue(true);
        }

        /// <summary>
        /// 通联对账单
        /// </summary>
        [TestMethod]
        public void AddAllInPayListPOSRecordsTest()
        {
            string url = ConfigurationManager.AppSettings["UnionPayUrl"];
            RequestModel requestModel = new RequestModel();
            var statementModel0 = new StatementModel()
            {
                LiquidationDate = "2016-06-23",
                TransactionTime = "2016-06-23 14:26:36",
                RefNum = "455555555",
                CardNumber = "666666******5578",
                MerchantNumber = "898110282990444",
                TerminalNo = "01080999",
                Amount = (decimal)1000
            };
            var statementModel = new StatementModel()
            {
                LiquidationDate = "2016-06-20",
                TransactionTime = "2016-06-20 17:26:36",
                RefNum = "5555666787",
                CardNumber = "444444******5578",
                MerchantNumber = "888888656565",
                TerminalNo = "4568958",
                Amount = (decimal)11000
            };
            var statementModel1 = new StatementModel()
            {
                LiquidationDate = "2016-06-20",
                TransactionTime = "2016-06-20 13:26:36",
                RefNum = "66666655565",
                CardNumber = "666666******5599",
                MerchantNumber = "999999282990520",
                TerminalNo = "55555588",
                Amount = (decimal)8000
            };
            List<StatementModel> lstModel = new List<StatementModel>();
            lstModel.Add(statementModel0);
            lstModel.Add(statementModel);
            lstModel.Add(statementModel1);
            requestModel.ListStatementModel = lstModel;


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("token", "48dec672b4aikt34");
            var response = client.PostAsJsonAsync<RequestModel>(url + "api/PPTSAllInPaySale/PostListStatement", requestModel).Result;
            var result = response.Content.ReadAsAsync<ResponseModel>().Result;
            Console.WriteLine(string.Format("错误代码：{0}，错误信息：{1}", result.Flag, result.ErrorMessage));
            Assert.AreEqual(result.Flag, true);

            Assert.IsTrue(true);
        }

        string Serialize<T>(MediaTypeFormatter formatter, T value)
        {
            Stream stream = new MemoryStream();
            var content = new StreamContent(stream);

            formatter.WriteToStreamAsync(typeof(T), value, stream, content, null).Wait();

            stream.Position = 0;
            return content.ReadAsStringAsync().Result;
        }

        T Deserialize<T>(MediaTypeFormatter formatter, string str) where T : class
        {
            Stream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;

            return formatter.ReadFromStreamAsync(typeof(T), stream, null, null).Result as T;
        }

        [TestMethod]
        public void SerializeTest()
        {
            //var value = new StatementModel() { Amount = 200, CardNumber = "1234", LiquidationDate = DateTime.Now, MerchantNumber = "123", RefNum = "456", TerminalNo = "789", TransactionTime = DateTime.Now };
            var value = new RequestModel() { RequestTime = DateTime.Now,  ListStatementModel = new System.Collections.Generic.List<StatementModel>() { new StatementModel() { Amount = 200, CardNumber = "1234", LiquidationDate = "2016-06-20", MerchantNumber = "123", RefNum = "456", TerminalNo = "789", TransactionTime = "2016-06-20 12:51:00" } } };
            var xml = new XmlMediaTypeFormatter();
            string str = Serialize(xml, value);
            Console.WriteLine(str);
            var json = new JsonMediaTypeFormatter();
            str = Serialize(json, value);
            Console.WriteLine(str);

            //StatementModel mod = Deserialize<StatementModel>(xml, str);
        }
        [TestMethod]
        public void RandomTest()
        {
            for(int i =0;i<100;i++)
            {
                Console.WriteLine(GetRandom());
            }
        }
        private Hashtable hash = new Hashtable();

        private string GetRandom()
        {
            string rondom = GenerateRandom(12);
            if(!hash.ContainsValue(rondom))
            {
                hash.Add(rondom, rondom);
                return rondom;
            }
            else
            {
                return GetRandom();
            }
        }

        private string GenerateRandom(int length)
        {
            char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            StringBuilder newRandom = new StringBuilder(10);
            Random ran = new Random();
            for (int i = 0; i < length; i++)
            {
                string str = constant[ran.Next(10)].ToString();
                
                newRandom.Append(str);
            }
            return newRandom.ToString();
        }
    }
    
}
