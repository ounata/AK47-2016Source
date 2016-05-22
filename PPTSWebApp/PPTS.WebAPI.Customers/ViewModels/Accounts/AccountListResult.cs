using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MCS.Library.Data.DataObjects;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 账户查询结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class AccountListResult
    {
        /// <summary>
        /// 学员信息
        /// </summary>
        [DataMember()]
        public CustomerModel Customer
        {
            set;
            get;
        }

        private List<AccountModel> _items = new List<AccountModel>();
        /// <summary>
        /// 账户列表
        /// </summary>
        [DataMember]
        public List<AccountModel> Items
        {
            get
            {
                return _items;
            }
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        public static AccountListResult Load(string customerID)
        {
            AccountListResult result = new AccountListResult();
            result.Customer = CustomerModel.Load(customerID);
            result.Items.AddRange(AccountModel.LoadByCustomerID(customerID));

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(AccountModel));
            return result;
        }
    }
}