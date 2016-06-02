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
        /// 支付金额大写
        /// </summary>
        [DataMember]
        public string PayMoneyText
        {
            get
            {
                return Data.Common.Helper.MoneyToCapital(this.PayMoney);
            }
        }

        /// <summary>
        /// 百万位
        /// </summary>
        [DataMember]
        public string Million
        {
            get
            {
                int v1 = (int)(this.PayMoney / 1000000 % 10);
                if (v1 != 0)
                    return v1.ToString();

                int v2 = (int)(this.PayMoney / 100000 % 10);
                if (v2 != 0)
                    return "￥";

                return string.Empty;
            }
        }
        /// <summary>
        /// 十万位
        /// </summary>
        [DataMember]
        public string HundredThousand
        {
            get
            {
                int v1 = (int)(this.PayMoney / 100000 % 10);
                if (v1 != 0)
                    return v1.ToString();

                int v2 = (int)(this.PayMoney / 10000 % 10);
                if (v2 != 0)
                    return "￥";

                if (string.IsNullOrEmpty(this.Million))
                    return string.Empty;
                return "0";
            }
        }
        /// <summary>
        /// 万位
        /// </summary>
        [DataMember]
        public string TenThousand
        {
            get
            {
                int v1 = (int)(this.PayMoney / 10000 % 10);
                if (v1 != 0)
                    return v1.ToString();

                int v2 = (int)(this.PayMoney / 1000 % 10);
                if (v2 != 0)
                    return "￥";

                if (string.IsNullOrEmpty(this.HundredThousand))
                    return string.Empty;
                return "0";
            }
        }
        /// <summary>
        /// 千位
        /// </summary>
        [DataMember]
        public string Thousand
        {
            get
            {
                int v1 = (int)(this.PayMoney / 1000 % 10);
                if (v1 != 0)
                    return v1.ToString();

                int v2 = (int)(this.PayMoney / 100 % 10);
                if (v2 != 0)
                    return "￥";

                if (string.IsNullOrEmpty(this.TenThousand))
                    return string.Empty;
                return "0";
            }
        }
        /// <summary>
        /// 百位
        /// </summary>
        [DataMember]
        public string Hundred
        {
            get
            {
                int v1 = (int)(this.PayMoney / 100 % 10);
                if (v1 != 0)
                    return v1.ToString();

                int v2 = (int)(this.PayMoney / 10 % 10);
                if (v2 != 0)
                    return "￥";

                if (string.IsNullOrEmpty(this.Thousand))
                    return string.Empty;
                return "0";
            }
        }
        /// <summary>
        /// 十位
        /// </summary>
        [DataMember]
        public string Ten
        {
            get
            {
                int v1 = (int)(this.PayMoney / 10 % 10);
                if (v1 != 0)
                    return v1.ToString();

                int v2 = (int)(this.PayMoney % 10);
                if (v2 != 0)
                    return "￥";

                if (string.IsNullOrEmpty(this.Hundred))
                    return string.Empty;
                return "0";
            }
        }
        /// <summary>
        /// 元位
        /// </summary>
        [DataMember]
        public string Yuan
        {
            get
            {
                int v = (int)(this.PayMoney % 10);
                return v.ToString();
            }
        }
        /// <summary>
        /// 角
        /// </summary>
        [DataMember]
        public string Jiao
        {
            get
            {
                int v = (int)(this.PayMoney * 10 % 10);
                return v.ToString();
            }
        }
        /// <summary>
        /// 分
        /// </summary>
        [DataMember]
        public string Cent
        {
            get
            {
                int v = (int)(this.PayMoney * 100 % 10);
                return v.ToString();
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