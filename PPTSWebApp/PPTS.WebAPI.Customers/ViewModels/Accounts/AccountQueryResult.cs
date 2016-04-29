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
    public class AccountQueryResult
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

        public static AccountQueryResult Load(string customerID)
        {
            AccountQueryResult result = new AccountQueryResult();
            result.Customer = CustomerModel.Load(customerID);
            AccountCollection accounts = AccountAdapter.Instance.LoadCollectionByCustomerID(customerID);
            foreach (Account account in accounts)
            {
                result.Items.Add(AccountModel.Load(account));
            }
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(AccountQueryResult));
            return result;
        }
    }
}