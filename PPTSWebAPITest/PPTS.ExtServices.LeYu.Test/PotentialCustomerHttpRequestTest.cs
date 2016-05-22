using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using System.Web;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using MCS.Library.OGUPermission;

namespace PPTS.ExtServices.LeYu.Test
{
    /// <summary>
    /// HttpRequestTest 的摘要说明
    /// </summary>
    [TestClass]
    public class PotentialCustomerHttpRequestTest
    {
        /// <summary>
        /// 第一组测试，成功测试数据
        /// </summary>
        [TestMethod]
        public void SaveLeYuInfoRequestTest()
        {
            string resultValue = null;
            string url = System.Configuration.ConfigurationManager.AppSettings["LeyuUrl"];
            //添加参数
            IDictionary<string, string> dicParam = new Dictionary<string, string>();
            dicParam.Add("Name", "testleyukehu");
            dicParam.Add("Column1", "13546895421");
            dicParam.Add("Column2", "F1");
            dicParam.Add("Column3", "testCustomerName");
            dicParam.Add("Column4", "男");
            dicParam.Add("Column5", "11");
            dicParam.Add("Column6", "");
            dicParam.Add("Column7", "北京-方庄");
            dicParam.Add("FirstPage", "http://nanjing.xueda.com/School/154.Shtml?k=ppc%26f=zblsogouppc");
            dicParam.Add("ReferUrl", "so.360.cn");
            dicParam.Add("CHATPAGE", "");
            dicParam.Add("CREATENAME", "陈丹丹(chendandan_9)");
            dicParam.Add("NOTE", "测试1");
            dicParam.Add("AREA", "中国北京");
            dicParam.Add("KEYWORD", "怎样辅导一年级孩子");
            int i = 0;
            StringBuilder strParam = new StringBuilder();
            dicParam.ForEach(k =>
                {
                    if (i > 0)
                    {
                        strParam.AppendFormat("&{0}={1}", k.Key, k.Value);
                    }
                    else
                    {
                        strParam.AppendFormat("{0}={1}", k.Key, k.Value);
                    }
                    i++;
                }
            );

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "post";
            
            byte[] getBytes = Encoding.UTF8.GetBytes(strParam.ToString());
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = getBytes.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(getBytes, 0, getBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                resultValue = streamReader.ReadToEnd();
                Console.WriteLine(resultValue);
            }
            Assert.IsNotNull(resultValue);
        }

        /// <summary>
        /// 第二组测试数据
        /// 
        /// </summary>
        [TestMethod]
        public void SaveLeYuInfoRequestTest1()
        {
            string resultValue = null;
            string url = System.Configuration.ConfigurationManager.AppSettings["LeyuUrl"];
            //添加参数
            IDictionary<string, string> dicParam = new Dictionary<string, string>();
            dicParam.Add("Name", "testleyukehua");
            dicParam.Add("Column1", "13546895523");
            dicParam.Add("Column2", "F1");
            dicParam.Add("Column3", "testCustomerNamea");
            dicParam.Add("Column4", "女");
            dicParam.Add("Column5", "23");
            dicParam.Add("Column6", "010-8408832-6622");
            dicParam.Add("Column7", "北京");
            dicParam.Add("FirstPage", "http://nanjing.xueda.com/School/154.Shtml");
            dicParam.Add("ReferUrl", "so.360.cn");
            dicParam.Add("CHATPAGE", "k=ppc%26f=googleppc");
            dicParam.Add("CREATENAME", "陈丹丹(chendandan_9)");
            dicParam.Add("NOTE", "测试1");
            dicParam.Add("AREA", "中国北京");
            dicParam.Add("KEYWORD", "怎样辅导一年级孩子");
            int i = 0;
            StringBuilder strParam = new StringBuilder();
            dicParam.ForEach(k =>
            {
                if (i > 0)
                {
                    strParam.AppendFormat("&{0}={1}", k.Key, k.Value);
                }
                else
                {
                    strParam.AppendFormat("{0}={1}", k.Key, k.Value);
                }
                i++;
            }
            );

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "post";

            byte[] getBytes = Encoding.UTF8.GetBytes(strParam.ToString());
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = getBytes.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(getBytes, 0, getBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                resultValue = streamReader.ReadToEnd();
                Console.WriteLine(resultValue);
            }
            Assert.IsNotNull(resultValue);
        }

        /// <summary>
        /// 第三组测试
        /// </summary>
        [TestMethod]
        public void SaveLeYuInfoRequestTest2()
        {
            string resultValue = null;
            string url = System.Configuration.ConfigurationManager.AppSettings["LeyuUrl"];
            //添加参数
            IDictionary<string, string> dicParam = new Dictionary<string, string>();
            dicParam.Add("Name", "testleyukehua");
            dicParam.Add("Column1", "13546895523");
            dicParam.Add("Column2", "F1");
            dicParam.Add("Column3", "testCustomerNamea");
            dicParam.Add("Column4", "女");
            dicParam.Add("Column5", "21");
            dicParam.Add("Column6", "010-8408832-6622");
            dicParam.Add("Column7", "北京");
            dicParam.Add("FirstPage", "http://nanjing.xueda.com/School/154.Shtml");
            dicParam.Add("ReferUrl", "so.360.cn");
            dicParam.Add("CHATPAGE", "k=ppc%26f=googleppc");
            dicParam.Add("CREATENAME", "赵志宇(zhaozhiyu)");
            dicParam.Add("NOTE", "测试3");
            dicParam.Add("AREA", "中国北京");
            dicParam.Add("KEYWORD", "怎样辅导一年级孩子");
            int i = 0;
            StringBuilder strParam = new StringBuilder();
            dicParam.ForEach(k =>
            {
                if (i > 0)
                {
                    strParam.AppendFormat("&{0}={1}", k.Key, k.Value);
                }
                else
                {
                    strParam.AppendFormat("{0}={1}", k.Key, k.Value);
                }
                i++;
            }
            );

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "post";

            byte[] getBytes = Encoding.UTF8.GetBytes(strParam.ToString());
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = getBytes.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(getBytes, 0, getBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                resultValue = streamReader.ReadToEnd();
                Console.WriteLine(resultValue);
            }
            Assert.IsNotNull(resultValue);
        }

        [TestMethod]
        public void Test()
        {
            //ExceptionHelper.FalseThrow(0 > 0, "必须为对象{0}指定关键字", "123");
            OguObjectCollection<IOrganization> io= OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, "1-Org");
        }

        [TestMethod]
        public void SaveLeYuInfoPostRequestTest()
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["LeyuUrl"];
            //
            // TODO:  在此处添加测试逻辑
            //
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "post";
            //string param = HttpUtility.UrlEncode("xml",Encoding.Default)+"="
            //    +HttpUtility.UrlEncode(@"<root><customerid>135710</customerid></root>", Encoding.Default);
            string param = "xml" + "=" + @"<root>  
                    <customerid>135710</customerid>  
                    <ani>14733339872</ani>  
                    <agentid>hongxue</agentid> 
                    <parentname>家长姓名十六</parentname>  
                    <parentsex>男</parentsex>  
                    <stuname></stuname>  
                    <stusex></stusex>  
                    <parenttel></parenttel>  
                    <mobile></mobile>  
                    <stugrade>小学二年级</stugrade>  
                    <stuschool>就读学校</stuschool>  
                    <StuCity>北京</StuCity>  
                    <CityID_PPTS>35</CityID_PPTS>  
                    <WPName_PPTS>方庄校区</WPName_PPTS> <WPID_PPTS>63</WPID_PPTS>  
                    <orgid>18</orgid>  
                    <InfoFrom>乐语语音</InfoFrom>  
                    <StuIntro>学员情况描述</StuIntro>  
                    <Memo>备注</Memo>  
                    <TalkConts>
                    （举例2）家长对类似测评活动似乎接触不多，对于免费又能帮助孩子提高学习成绩较感兴趣，因此，未就测试内容等相关信息做过多咨询，即表示可上门，询问电销代表校区地址和可上门的时间，已答复需要预约老师的时间，稍后请校区咨询老师回电话告知时间和路线。
                    </TalkConts>  
                    <TalkReason>
                    以PPTS免费测评为由进家长进行约访，对于课外辅导机构敏感度高的家长，完全未提及报名辅导事宜
                    </TalkReason>  
                    <ParentInfo>家长情况</ParentInfo>  
                    <StuScore>学员成绩</StuScore>  
                    <LogDate>2012-07-12 14:07:33</LogDate>  <PurchaseIntention>3</PurchaseIntention>  
                    </root>
                    " + "&name=test";
            byte[] postBytes = Encoding.UTF8.GetBytes(param);
            request.ContentType = "application/x-www-form-urlencoded;";
            request.ContentLength = postBytes.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(postBytes, 0, postBytes.Length);
            }
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                Console.WriteLine(streamReader.ReadToEnd());
            }

            Assert.IsTrue(true);
        }
        
    }
}
