using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
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
            DiscountPermissionView view = DiscountPermissionViewAdapter.Instance.LoadByCampusID(campusID);
            DiscountModel model = new DiscountModel();
            if (view != null)
            {
                model.DiscountID = view.DiscountID;
                DiscountItemCollection items = DiscountItemAdapter.Instance.LoadCollectionByDiscountID(model.DiscountID);
                foreach (DiscountItem item in items.OrderByDescending(x=>x.DiscountStandard))
                {
                    model.Items.Add(AutoMapper.Mapper.DynamicMap<DiscountItemModel>(item));
                }
            }
            return model;
        }
    }
}