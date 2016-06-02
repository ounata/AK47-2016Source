using MCS.Library.OGUPermission;
using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 收费界面显示结果模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class ChargeApplyResult
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

        /// <summary>
        /// 断言结果
        /// </summary>
        [DataMember]
        public AssertResult Assert
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
        public static ChargeApplyResult LoadByCustomerID(string customerID, IUser user)
        {
            ChargeApplyResult result = new ChargeApplyResult();
            result.Customer = CustomerModel.Load(customerID);
            result.Discount = DiscountModel.LoadByCampusID(result.Customer.CampusID);
            result.Apply = ChargeApplyModel.LoadByCustomer(result.Customer);
            result.Apply.Init(result.Customer, result.Discount, user.GetCurrentJob().JobType);
            result.Assert = Validate(result.Customer, user);

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(ChargeApplyModel)
                , typeof(ChargeAllotItemModel));
            return result;
        }

        public static ChargeApplyResult LoadByApplyID(string applyID, IUser user)
        {
            ChargeApplyResult result = new ChargeApplyResult();
            result.Apply = ChargeApplyModel.LoadByApplyID(applyID);
            result.Discount = DiscountModel.LoadByCampusID(result.Apply.CampusID);
            result.Customer = CustomerModel.Load(result.Apply.CustomerID);
            result.Apply.Init(result.Customer, result.Discount, user.GetCurrentJob().JobType);
            result.Assert = Validate(result.Customer, user);

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(ChargeApplyModel)
                , typeof(ChargeAllotItemModel)
                , typeof(ChargePaymentItemModel));
            return result;
        }

        public static ChargeApplyResult LoadByApplyID4Allot(string applyID)
        {
            ChargeApplyResult result = new ChargeApplyResult();
            result.Apply = ChargeApplyModel.LoadByApplyID4Allot(applyID);
            result.Customer = CustomerModel.Load(result.Apply.CustomerID);
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(ChargeApplyModel)
                , typeof(ChargeAllotItemModel));
            return result;
        }

        public static ChargeApplyResult LoadByApplyID4Payment(string applyID)
        {
            ChargeApplyResult result = new ChargeApplyResult();
            result.Apply = ChargeApplyModel.LoadByApplyID4Payment(applyID);
            result.Customer = CustomerModel.Load(result.Apply.CustomerID);
            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(ChargeApplyModel)
                , typeof(ChargePaymentItemModel));
            return result;
        }

        public static AssertResult Validate(string customerID, IUser user)
        {
            CustomerModel customer = CustomerModel.Load(customerID);
            if (customer == null)
                return new AssertResult(false, "不存在该记录");
            if (string.IsNullOrEmpty(customer.CampusID))
                return new AssertResult(false, "潜客必须分配校区才能充值");

            return Validate(customer, user);
        }

        public static AssertResult Validate(CustomerModel customer, IUser user)
        {
            PPTSJob job = user.GetCurrentJob();
            if (customer.IsPotential)
            {
                if (job.JobType != JobTypeDefine.Consultant)
                    return new AssertResult(false, "只有咨询师才可以给潜客充值");
                if (customer != null && customer.Status == CustomerStatus.Formal)
                    return new AssertResult(false, "该潜客已经转为正式学员，请到学员列表进行充值");
            }
            else
            {
                if (job.JobType != JobTypeDefine.Consultant && job.JobType != JobTypeDefine.Educator)
                    return new AssertResult(false, "只有咨询师和学管师才可以给学员充值");
            }
            CustomerStaffRelationCollection collection = CustomerStaffRelationAdapter.Instance.LoadByCustomerID(customer.CustomerID);
            if (job.JobType == JobTypeDefine.Consultant)
            {
                CustomerStaffRelation relation = collection.Find(x => x.RelationType == CustomerRelationType.Consultant);
                if (relation == null)
                    return new AssertResult(false, "请先给该学员分配咨询师再充值");
                else if (job.ID != relation.StaffJobID)
                    return new AssertResult(false, "只有该学员的咨询师才可以充值");
            }
            else if (job.JobType == JobTypeDefine.Educator)
            {
                CustomerStaffRelation relation = collection.Find(x => x.RelationType == CustomerRelationType.Educator);
                if (relation == null)
                    return new AssertResult(false, "请先给该学员分配学管师再充值");
                else if (job.ID != relation.StaffJobID)
                    return new AssertResult(false, "只有该学员的学管师才可以充值");
            }
            return new AssertResult();
        }
    }
}