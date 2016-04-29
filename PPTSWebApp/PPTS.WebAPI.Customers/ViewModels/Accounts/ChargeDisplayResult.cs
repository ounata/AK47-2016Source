using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 收费界面显示结果模型
    /// </summary>
    public class ChargeDisplayResult
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

        /// <summary>
        /// 缴费申请
        /// </summary>
        [DataMember]
        public ChargeApplyModel Apply
        {
            set;
            get;
        }

        /// <summary>
        /// 折扣信息
        /// </summary>
        [DataMember]
        public DiscountModel Discount
        {
            set;
            get;
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
        public static ChargeDisplayResult LoadByCustomerID(string customerID, JobTypeDefine applierJobType)
        {
            ChargeDisplayResult result = new ChargeDisplayResult();
            result.Customer = CustomerModel.Load(customerID);
            result.Discount = DiscountModel.LoadByCampusID(result.Customer.CampusID);
            result.Apply = ChargeApplyModel.LoadByCustomerID(result.Customer, applierJobType);
            result.Apply.ThisDiscountID = result.Discount.DiscountID;

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(ChargeApplyModel)
                , typeof(ChargeAllotItemModel));
            return result;
        }

        public static ChargeDisplayResult LoadByApplyID(string applyID)
        {
            ChargeDisplayResult result = new ChargeDisplayResult();
            result.Apply = ChargeApplyModel.LoadByApplyID(applyID);
            result.Customer = CustomerModel.Load(result.Apply.CustomerID);            
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)                
                , typeof(ChargeApplyModel)
                , typeof(ChargeAllotItemModel)
                , typeof(ChargePaymentItemModel));
            return result;
        }

        public static ChargeDisplayResult LoadByApplyID4Allot(string applyID)
        {
            ChargeDisplayResult result = new ChargeDisplayResult();
            result.Apply = ChargeApplyModel.LoadByApplyID4Allot(applyID);
            result.Customer = CustomerModel.Load(result.Apply.CustomerID);
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(ChargeApplyModel)
                , typeof(ChargeAllotItemModel));
            return result;
        }

        public static ChargeDisplayResult LoadByApplyID4Payment(string applyID)
        {
            ChargeDisplayResult result = new ChargeDisplayResult();
            result.Apply = ChargeApplyModel.LoadByApplyID4Payment(applyID);
            result.Customer = CustomerModel.Load(result.Apply.CustomerID);
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(ChargeApplyModel)
                , typeof(ChargePaymentItemModel));
            return result;
        }
    }
}