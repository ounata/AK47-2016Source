using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
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
    /// 充值列表
    /// </summary>
    [Serializable]
    [DataContract]
    public class ChargeApplyListResult
    {
        /// <summary>
        /// 学员信息
        /// </summary>
        [DataMember]
        public CustomerModel Customer
        {
            set;
            get;
        }

        private List<ChargeApplyModel> _items = new List<ChargeApplyModel>();
        /// <summary>
        /// 缴费申请列表
        /// </summary>
        [DataMember]
        public List<ChargeApplyModel> Items
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

        [DataMember]
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        /// <summary>
        /// 根据学员获取
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static ChargeApplyListResult Load(string customerID)
        {
            ChargeApplyListResult result = new ChargeApplyListResult();
            result.Customer = CustomerModel.Load(customerID);
            foreach (AccountChargeApply apply in AccountChargeApplyAdapter.Instance.LoadCollectionByCustomerID(customerID))
            {
                ChargeApplyModel model = AutoMapper.Mapper.DynamicMap<ChargeApplyModel>(apply);
                result.Items.Add(model);
            }
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(ChargeApplyModel));
            return result;
        }
    }
}