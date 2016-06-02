using MCS.Library.OGUPermission;
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
    public class ReturnApplyResult
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
        /// 申请信息
        /// </summary>
        public ReturnApplyModel Apply
        {
            set;
            get;
        }

        /// <summary>
        /// 服务费列表
        /// </summary>
        [DataMember]
        public List<ExpenseModel> Expenses
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

        public static ReturnApplyResult LoadByCustomerID(string customerID, IUser user)
        {
            ReturnApplyResult result = new ReturnApplyResult();
            result.Customer = CustomerModel.Load(customerID, false);
            result.Apply = ReturnApplyModel.Load(result.Customer);
            result.Expenses = ExpenseModel.Load(customerID);

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(ExpenseModel));
            return result;
        }
    }
}