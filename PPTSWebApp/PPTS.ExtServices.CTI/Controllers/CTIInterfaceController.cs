using MCS.Library.Core;
using PPTS.ExtServices.CTI.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace PPTS.ExtServices.CTI.Controllers
{
    public class CTIInterfaceController : Controller
    {
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(string xml, string auth)
        {
            try
            {
                bool exist = false;

                XmlDocument xmlDocument = XmlHelper.CreateDomDocument(xml);

                XmlNodeList nodeList = xmlDocument.SelectNodes("root");
                
                nodeList.IsNotNull(n =>
                {
                    if (n.Count == 1)
                    {
                        XmlNode node = n[0];

                        string strBranchID = node.GetSingleNodeText("orgid").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("orgid");
                        //判断分公司ID是否在配置文件中,存在返回true
                        
                        exist = APPFunc.ExistesBranchCurrent(strBranchID);
                    }
                });
                //exist为false，表示是新系统上线的分公司，需要post新系统的方法
                string strAppSetName = string.Empty;
                if (exist)
                {
                    //旧系统
                    strAppSetName = "OldCTIUrl";
                    
                }
                else
                {
                    strAppSetName = "CTIUrl";
                }
                string url = System.Configuration.ConfigurationManager.AppSettings[strAppSetName];

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "post";
                xml = string.Format("xml={0}&auth={1}", xml, auth);
                //xml = "xml="+ xml + "&auth=" + auth;
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
                    string strResult = reader.ReadToEnd();
                    return Content(strResult);
                }
            }
            catch (Exception ex)
            {
                return Content(CreateErrorXml(string.Format("呼叫中心数据转换失败:{0}", ex.Message), "1"));
            }
        }
        #region 返回的错误信息格式

        /// <summary>
        /// 返回的错误信息格式
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <param name="strErrKey"></param>
        /// <returns></returns>
        private string CreateErrorXml(string strErrMsg, string strErrKey)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlNode xmlNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlNode);

            XmlElement xmlElementRoot = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(xmlElementRoot);

            XmlElement xmlElementError = xmlDoc.CreateElement("error");
            xmlElementError.InnerText = strErrKey;
            xmlElementRoot.AppendChild(xmlElementError);

            XmlElement xmlElementMsg = xmlDoc.CreateElement("msg");
            xmlElementMsg.InnerText = strErrMsg;
            xmlElementRoot.AppendChild(xmlElementMsg);

            return xmlDoc.OuterXml;

        }

        #endregion 返回的错误信息格式
    }
}