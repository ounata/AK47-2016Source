using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MCS.Library.Data.DataObjects;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 业绩分配模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class RefundAllotModel
    {
        /// <summary>
        /// 业绩分配总金额
        /// </summary>
        [DataMember]
        public decimal TotalMoney
        {
            set;
            get;
        }

        /// <summary>
        /// 业绩分配总课时
        /// </summary>
        [DataMember]
        public decimal TotalAmount
        {
            set;
            get;
        }

        private List<RefundAllotItemModel> _items = new List<RefundAllotItemModel>();
        /// <summary>
        /// 业绩分配列表
        /// </summary>
        [DataMember]
        public List<RefundAllotItemModel> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (value == null)
                    _items.Clear();
                else
                    _items = value;
            }
        }

        public static RefundAllotModel Load(string applyID)
        {
            RefundAllotModel model = new RefundAllotModel();
            AccountRefundAllotCollection allots = AccountRefundAllotAdapter.Instance.LoadCollectionByApplyID(applyID);
            foreach (AccountRefundAllot allot in allots)
            {
                model.TotalAmount += allot.AllotAmount;
                model.TotalMoney += allot.AllotMoney;
                RefundAllotItemModel item = AutoMapper.Mapper.DynamicMap<RefundAllotItemModel>(allot);
                model.Items.Add(item);
            }
            return model;
        }
    }
}