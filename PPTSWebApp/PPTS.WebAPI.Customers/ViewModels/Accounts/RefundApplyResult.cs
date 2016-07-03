using MCS.Library.OGUPermission;
using MCS.Web.MVC.Library.Models.Workflow;
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
    /// 退费申请单模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class RefundApplyResult
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
        /// 退费申请
        /// </summary>
        [DataMember]
        public RefundApplyModel Apply
        {
            set;
            get;
        }

        /// <summary>
        /// 账户列表
        /// </summary>
        [DataMember]
        public List<AccountModel> Accounts
        {
            get;
            set;
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
        /// 客户端流程信息
        /// </summary>
        [DataMember]
        public WfClientProcess ClientProcess
        {
            get;
            set;
        }

        /// <summary>
        /// 根据学员获取
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static RefundApplyResult LoadByCustomerID(string customerID, IUser user)
        {
            RefundApplyResult result = new RefundApplyResult();
            result.Customer = CustomerModel.Load(customerID, false);
            result.Accounts = AccountModel.Load4RefundByCustomerID(result.Customer.CustomerID);
            result.Apply = RefundApplyModel.LoadByCustomerID(result.Customer);
            result.Assert = Validate(result.Customer, user);

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(RefundApplyModel)
                , typeof(ChargeAllotItemModel));
            return result;
        }

        public static RefundApplyResult LoadByApplyID(string applyID, IUser user)
        {
            RefundApplyResult result = new RefundApplyResult();
            result.Apply = RefundApplyModel.LoadByApplyID(applyID);
            result.Customer = CustomerModel.Load(result.Apply.CustomerID, false);

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(RefundApplyModel)
                , typeof(ChargeAllotItemModel));
            return result;
        }

        public static AssertResult Validate(string customerID, IUser user)
        {
            return Validate(CustomerModel.Load(customerID), user);
        }

        public static AssertResult Validate(CustomerModel customer, IUser user)
        {
#if DEBUG
            return new AssertResult();
#else
            PPTSJob job = user.GetCurrentJob();

            if (job.JobType != JobTypeDefine.Consultant && job.JobType != JobTypeDefine.Educator)
                return new AssertResult(false, "只有咨询师或学管师才可以给学员提交退费申请");

            CustomerStaffRelationCollection collection = CustomerStaffRelationAdapter.Instance.LoadByCustomerID(customer.CustomerID);
            if (job.JobType == JobTypeDefine.Consultant)
            {
                CustomerStaffRelation relation = collection.Find(x => x.RelationType == CustomerRelationType.Consultant);
                if (relation == null)
                    return new AssertResult(false, "请先给该学员分配咨询师再退费");
                else if (job.ID != relation.StaffJobID)
                    return new AssertResult(false, "只有该学员的咨询师才可以提交退费申请");
            }
            else if (job.JobType == JobTypeDefine.Educator)
            {
                CustomerStaffRelation relation = collection.Find(x => x.RelationType == CustomerRelationType.Educator);
                if (relation == null)
                    return new AssertResult(false, "请先给该学员分配学管师再退费");
                else if (job.ID != relation.StaffJobID)
                    return new AssertResult(false, "只有该学员的学管师才可以提交退费申请");
            }
            return new AssertResult();
#endif
        }
    }
}