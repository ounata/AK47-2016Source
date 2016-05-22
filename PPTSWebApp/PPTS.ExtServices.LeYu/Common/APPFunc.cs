using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using MCS.Library.Core;
using PPTS.ExtServices.LeYu.Models.CallingOrgCenterConfig;

namespace PPTS.ExtServices.LeYu.Common
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

                listCallingOrgCenterCofigModel.Add(calloingModel);
            }
            return listCallingOrgCenterCofigModel;
        }

        /// <summary>
        /// 读取配置文件，根据乐语参数获得PPTS信息来源值
        /// </summary>
        /// <param name="strLeYuSource"></param>
        /// <returns></returns>
        public static string GetERPSourceValue(string strLeYuSource)
        {
            string strERPSourceValue = string.Empty;

            var source = configNode.SelectSingleNode(string.Format("CTItoERPSourceInfo/SecondSourceInfo[@LeYuSourceValue='{0}']", strLeYuSource));
            if (null != source)
            {
                strERPSourceValue = source.GetAttributeText("ERPSourceValue");
            }
            return strERPSourceValue;
        }
        
    }
}