using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Text;
using System.IO;

namespace PPTS.ExtServices.CTI.Test
{
    [TestClass]
    public class PotentialCustomerRequestTest
    {
        [TestMethod]
        public void SaveCTIRequestTest()
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["CTIUrl"];

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "post";
            //密钥
            string MD5Key = string.Empty;
            MD5Key = PPTS.ExtServices.CTI.Common.APPFunc.MD5Encryption("2012-07-12 14:07:33chendandan_9");
            string xml = "xml" + "=" + @"<root>  
                    <customerid>44294030001</customerid>  
                    <ani>14733339872</ani>  
                    <agentid>chendandan_9</agentid> 
                    <parentname>家长姓名</parentname>  
                    <parentsex>男</parentsex>  
                    <stuname>学生姓名</stuname>  
                    <stusex>男</stusex>  
                    <parenttel></parenttel>  
                    <mobile>15645892233</mobile>  
                    <stugrade>小学二年级</stugrade>  
                    <stuschool>就读学校</stuschool>  
                    <StuCity>北京</StuCity>  
                    <CityID_PPTS>35</CityID_PPTS>  
                    <WPName_PPTS>方庄校区</WPName_PPTS> 
                    <WPID_PPTS>63</WPID_PPTS>  
                    <orgid>8</orgid>  
                    <InfoFrom>呼出</InfoFrom>  
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
                    " + "&auth=" + MD5Key;
            byte[] postBytes = Encoding.UTF8.GetBytes(xml);
            request.ContentType = "application/x-www-form-urlencoded;";
            request.ContentLength = postBytes.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(postBytes, 0, postBytes.Length);
            }
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                Console.WriteLine(reader.ReadToEnd());
            }
            Assert.IsTrue(true);
        }
    }
}
