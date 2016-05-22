using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Security;
using MCS.Library.Net.SNTP;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 缴费单支付明细模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class ChargePaymentItemModel : AccountChargePayment
    {
        /// <summary>
        /// 能否打印
        /// </summary>
        [DataMember]
        public bool CanPrint
        {
            get
            {
                return this.PayStatus == PayStatusDefine.Paid;
            }
        }

        /// <summary>
        /// 初始化对账人信息
        /// </summary>
        public void InitChecker(IUser user)
        {
            this.CheckerID = user.ID;
            this.CheckerName = user.Name;
            this.CheckerJobID = user.GetCurrentJob().ID;
            this.CheckerJobName = user.GetCurrentJob().Name;
            this.CheckTime = SNTPClient.AdjustedTime;
            this.CheckStatus = CheckStatusDefine.Checked;
        }
    }
}