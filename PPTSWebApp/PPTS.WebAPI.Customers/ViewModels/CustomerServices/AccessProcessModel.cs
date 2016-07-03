using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerServices
{
    [Serializable]
    public class AccessProcessModel
    {
        /// <summary>
        /// 申请人
        /// </summary>
        public string ServiceID { get; set; }

        /// <summary>
        /// 学员ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 申请解冻原因
        /// </summary>
        public string ProcessMemo { get; set; }

        /// <summary>
        /// 处理人姓名
        /// </summary>
        public string ApplyName { get; set; }

        /// <summary>
        /// 学员姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 学员编号
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// 校区名称
        /// </summary>
        public string CampusName { get; set; }

        public string ResourceID
        {
            get;
            set;
        }

        public string ActivityID
        {
            get;
            set;
        }

        public string ActivityName
        {
            get;
            set;
        }

        public string ProcessID
        {
            get;
            set;
        }
    }
}