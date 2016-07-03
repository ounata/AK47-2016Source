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
    /// 缴费单支付模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class ChargePaymentModel
    {
        /// <summary>
        /// 付款人（默认学员姓名）
        /// </summary>
        [DataMember]
        public string Payer
        {
            get;
            set;
        }

        /// <summary>
        /// 收款人ID
        /// </summary>
        [DataMember]
        public string PayeeID
        {
            get;
            set;
        }

        /// <summary>
        /// 收款人姓名
        /// </summary>
        [DataMember]
        public string PayeeName
        {
            get;
            set;
        }

        /// <summary>
        /// 收款人岗位ID
        /// </summary>
        [DataMember]
        public string PayeeJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 收款人岗位名称
        /// </summary>
        [DataMember]
        public string PayeeJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 支付金额
        /// </summary>
        [DataMember]
        public decimal PaidMoney
        {
            set;
            get;
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        [DataMember]
        public DateTime NowTime
        {
            get
            {
                return SNTPClient.AdjustedTime.Date;
            }
        }

        /// <summary>
        /// 支付时间
        /// </summary>
        [DataMember]
        public DateTime PayTime
        {
            set;
            get;
        }

        /// <summary>
        /// 收款时间允许的开始时间
        /// </summary>
        [DataMember]
        public DateTime PayStartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 收款允许的结束时间
        /// </summary>
        [DataMember]
        public DateTime PayEndTime
        {
            get;
            set;
        }
        
        List<ChargePaymentItemModel> _item = new List<ChargePaymentItemModel>();
        /// <summary>
        /// 缴费支付表
        /// </summary>
        [DataMember]
        public List<ChargePaymentItemModel> Items
        {
            get
            {
                return _item;
            }
            set
            {
                if (value == null)
                    _item.Clear();
                else
                    _item = value;
            }
        }

        /// <summary>
        /// 初始化收款人
        /// </summary>
        /// <param name="user"></param>
        public void InitPayee(IUser user)
        {
            this.PayeeID = user.ID;
            this.PayeeName = user.DisplayName;
            this.PayeeJobID = user.GetCurrentJob().ID;
            this.PayeeJobName = user.GetCurrentJob().Name;
            this.PayTime = SNTPClient.AdjustedTime.Date;

            DateTime thisDate = this.PayTime;
            DateTime thatDate = thisDate.AddMonths(-1);
            GlobalArgs args = ConfigsCache.GetGlobalArgs();
            if (args.IsClosedToAcccount(thatDate))
                this.PayStartTime = thisDate.AddDays(-1 * thisDate.Day + 1);
            else
                this.PayStartTime = thatDate.AddDays(-1 * thatDate.Day + 1);
            this.PayEndTime = thisDate;
        }

        public static ChargePaymentModel Load(string applyID)
        {
            ChargePaymentModel model = new ChargePaymentModel();
            AccountChargePaymentCollection payments = AccountChargePaymentAdapter.Instance.LoadCollectionByApplyID(applyID);
            foreach (AccountChargePayment payment in payments)
            {
                model.PaidMoney += payment.PayMoney;

                ChargePaymentItemModel item = payment.ProjectedAs<ChargePaymentItemModel>();
                model.Items.Add(item);
            }
            return model;
        }
    }
}