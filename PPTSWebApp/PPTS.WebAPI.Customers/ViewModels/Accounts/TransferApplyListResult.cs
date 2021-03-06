﻿using PPTS.Data.Common.Adapters;
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
    /// 转让列表
    /// </summary>
    [Serializable]
    [DataContract]
    public class TransferApplyListResult
    {
        /// <summary>
        /// 学员信息
        /// </summary>
        [DataMember]
        public CustomerModel Customer
        {
            get;
            private set;
        }

        private List<TransferApplyModel> _items = new List<TransferApplyModel>();

        /// <summary>
        /// 转让申请列表
        /// </summary>
        [DataMember]
        public List<TransferApplyModel> Items
        {
            get
            {
                return _items;
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
        /// <param name="customerID">学员ID</param>
        /// <returns></returns>
        public static TransferApplyListResult Load(string customerID)
        {
            TransferApplyListResult result = new TransferApplyListResult();
            result.Customer = CustomerModel.Load(customerID, false);
            foreach(AccountTransferApplyView  apply in AccountTransferApplyViewAdapter.Instance.LoadCollectionByCustomerID(customerID))
            {
                TransferApplyModel model = apply.ProjectedAs<TransferApplyModel>();
                result.Items.Add(model);
            }
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(ChargeApplyModel));
            return result;
        }
    }
}