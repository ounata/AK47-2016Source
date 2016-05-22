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
    [DataContract]
    public class ExpenseResult
    {
        /// <summary>
        /// 学员信息
        /// </summary>
        [DataMember]
        public CustomerModel Customer
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣编码
        /// </summary>
        [DataMember]
        public List<ExpenseModel> Items
        {
            get;
            set;
        }

        [DataMember]
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        public static ExpenseResult Load(string customerID)
        {
            ExpenseResult result = new ExpenseResult();
            result.Customer = CustomerModel.Load(customerID, false);
            result.Items = ExpenseModel.Load(customerID);

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(ExpenseModel));
            return result;
        }
    }
}