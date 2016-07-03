using MCS.Library.Data.Mapping;
using MCS.Library.Net.SNTP;
using PPTS.Data.Common;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 刷卡记录模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class PosRecordModel : POSRecord
    {
        /// <summary>
        /// 支付金额
        /// </summary>
        [DataMember]
        public decimal PayMoney
        {
            get
            {
                return this.Money;
            }
        }

        /// <summary>
        /// 收款时间
        /// </summary>
        [DataMember]
        public DateTime PayTime
        {
            set;
            get;
        }

        /// <summary>
        /// 实际刷卡时间
        /// </summary>
        [DataMember]
        public DateTime SwipeTime
        {
            get
            {
                return this.TransactionTime;
            }
        }
        
        public static PosRecordModel Load(string campusID, string payTicket, string payType)
        {
            string merchantID = campusID;       //读取校区的配置表
            string transactionID = payTicket;   //交易流水号
            string transactionType = payType.Substring(0, 1);  //收款类型变为交易类型

            POSRecord record = POSRecordAdapter.Instance.Load(merchantID, transactionID, transactionType);
            if (record != null)
            {
                PosRecordModel model = record.ProjectedAs<PosRecordModel>();

                DateTime now = SNTPClient.AdjustedTime.Date;
                GlobalArgs args = ConfigsCache.GetGlobalArgs();
                if (args.IsClosedToAcccount(model.TransactionTime))
                    model.PayTime = now.AddDays(-1 * now.Day + 1);
                else
                    model.PayTime = model.TransactionTime;

                return model;
            }
            return null;
        }
    }
}