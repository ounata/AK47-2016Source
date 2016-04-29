using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers;

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
        /// 支付时间
        /// </summary>
        [DataMember]
        public DateTime PayTime
        {
            set;
            get;
        }

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

        private List<ChargePaymentItemModel> _items = new List<ChargePaymentItemModel>();
        /// <summary>
        /// 缴费支付表
        /// </summary>
        [DataMember]
        public List<ChargePaymentItemModel> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (value != null)
                {
                    _items = value;
                }
            }
        }

        public static ChargePaymentModel Load(string applyID)
        {
            ChargePaymentModel model = new ChargePaymentModel();
            AccountChargePaymentCollection payments = AccountChargePaymentAdapter.Instance.LoadCollectionByApplyID(applyID);
            foreach (AccountChargePayment payment in payments)
            {
                model.PaidMoney += payment.PayMoney;

                ChargePaymentItemModel item = AutoMapper.Mapper.DynamicMap<ChargePaymentItemModel>(payment);
                model.Items.Add(item);
            }
            return model;                
        }
    }
}