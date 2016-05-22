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
    public class ChargeAllotModel
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

        private List<ChargeAllotItemModel> _items = new List<ChargeAllotItemModel>();
        /// <summary>
        /// 业绩分配列表
        /// </summary>
        [DataMember]
        public List<ChargeAllotItemModel> Items
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

        public static ChargeAllotModel Load(string applyID)
        {
            ChargeAllotModel model = new ChargeAllotModel();
            AccountChargeAllotCollection allots = AccountChargeAllotAdapter.Instance.LoadCollectionByApplyID(applyID);
            foreach (AccountChargeAllot allot in allots)
            {
                model.TotalAmount += allot.AllotAmount;
                model.TotalMoney += allot.AllotMoney;
                ChargeAllotItemModel item = AutoMapper.Mapper.DynamicMap<ChargeAllotItemModel>(allot);
                model.Items.Add(item);
            }
            return model;
        }
    }
}