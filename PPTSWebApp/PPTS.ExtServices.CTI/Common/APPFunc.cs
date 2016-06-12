using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using MCS.Library.Core;
using PPTS.ExtServices.CTI.Models.CallingOrgCenterConfig;
using System.Security.Cryptography;
using System.Text;

namespace PPTS.ExtServices.CTI.Common
{
    public static class APPFunc
    {
        private static XmlNode configNode = null;
        static APPFunc()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(string.Format(@"{0}App_Data\Config.xml", AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\"));
            configNode = doc.SelectSingleNode("config");
        }

        /// <summary>
        /// 读取配置文件，获得岗位归属于呼叫中心的信息
        /// </summary>
        /// <returns></returns>
        public static List<CallingOrgCenterCofigModel> GetJobCallingList()
        {

            var nodeList = configNode.SelectNodes(string.Format("CallingOrgCenter/orgcenter"));
            if (nodeList == null || nodeList.Count == 0)
            {
                return null;
            }

            List<CallingOrgCenterCofigModel> listCallingOrgCenterCofigModel = new List<CallingOrgCenterCofigModel>();
            foreach (XmlNode item in nodeList)
            {
                CallingOrgCenterCofigModel calloingModel = new CallingOrgCenterCofigModel();
                calloingModel.JobName = item.GetAttributeText("jobName");
                calloingModel.CallingType = item.GetAttributeText("Value");
                calloingModel.SortId = item.GetAttributeText("Sortid");
                calloingModel.SourceSystem = item.GetAttributeText("SourceSystem");

                listCallingOrgCenterCofigModel.Add(calloingModel);
            }
            return listCallingOrgCenterCofigModel;
        }
        /// <summary>
        /// 读取配置文件，根据CTI参数获得PPTS信息来源值
        /// </summary>
        /// <param name="strCTISource"></param>
        /// <returns></returns>
        public static string GetERPSourceValue(string strCTISource)
        {
            string strERPSourceValue = string.Empty;

            var source = configNode.SelectSingleNode(string.Format("CTItoERPSourceInfo/SecondSourceInfo[@CTISourceValue='{0}']", strCTISource));
            if (null != source)
            {
                strERPSourceValue = source.GetAttributeText("ERPSourceValue");
            }
            return strERPSourceValue;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strEncryptionContext"></param>
        /// <returns></returns>
        public static string MD5Encryption(string strEncryptionContext)
        {
            if (strEncryptionContext.IsNullOrEmpty())
            {
                return string.Empty;
            }
            MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();

            string strMD5Encrypt = BitConverter.ToString(cryptoServiceProvider.ComputeHash(UTF8Encoding.Default.GetBytes(strEncryptionContext)), 4, 8);

            strMD5Encrypt = strMD5Encrypt.Replace("-", "");

            return strMD5Encrypt.ToLower();
        }

        /// <summary>
        /// 判断分公司ID是否已经存在于配置表中，存在返回true
        /// 配置表中的分公司是指未上线新系统的分公司
        /// </summary>
        /// <param name="strBranchId"></param>
        /// <returns></returns>
        public static bool ExistesBranchCurrent(string strBranchId)
        {
            bool exist = false;

            var nodeList = configNode.SelectNodes("NewSystemBranch/BranchInfo");
            if(nodeList == null || nodeList.Count == 0)
            {
                exist = false;
            }
            foreach (XmlNode node in nodeList)
            {
                if (strBranchId == (node.GetAttributeText("id") == null ? string.Empty : node.GetAttributeText("id")))
                {
                    exist = true;
                }
            }

            return exist;
        }
    }
}