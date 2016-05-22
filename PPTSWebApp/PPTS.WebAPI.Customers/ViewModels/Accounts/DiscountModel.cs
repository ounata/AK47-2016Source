using PPTS.Data.Products.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PPTS.Contracts.Proxies;
using PPTS.Contracts.Products.Models;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 折扣信息模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class DiscountModel
    {
        /// <summary>
        /// 折扣ID
        /// </summary>
        [DataMember]
        public string DiscountID
        {
            set;
            get;
        }

        /// <summary>
        /// 折扣编码
        /// </summary>
        [DataMember]
        public string DiscountCode
        {
            set;
            get;
        }

        /// <summary>
        /// 折扣明细列表
        /// </summary>
        private List<DiscountItemModel> _items = new List<DiscountItemModel>();
        [DataMember]
        public List<DiscountItemModel> Items
        {
            get
            {
                return _items;
            }
        }

        public static DiscountModel LoadByCampusID(string campusID)
        {
            DiscountQueryResult result = PPTSConfigRuleQueryServiceProxy.Instance.QueryDiscountByCampusID(campusID);
            if (result != null && result.Discount != null)
            {
                DiscountModel model = new DiscountModel();
                model.DiscountID = result.Discount.DiscountID;
                model.DiscountCode = result.Discount.DiscountCode;
                foreach (DiscountItem item in result.DiscountItemCollection.OrderByDescending(x => x.DiscountStandard))
                    model.Items.Add(AutoMapper.Mapper.DynamicMap<DiscountItemModel>(item));
                return model;
            }
            return null;
        }

    }
}