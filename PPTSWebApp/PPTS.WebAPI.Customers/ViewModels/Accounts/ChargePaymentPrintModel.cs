using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 缴费支付单打印信息模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class ChargePaymentPrintModel
    {
        /// <summary>
        /// 支付单ID
        /// </summary>
        [DataMember]
        public string PayID
        {
            set;
            get;
        }       
    }
}