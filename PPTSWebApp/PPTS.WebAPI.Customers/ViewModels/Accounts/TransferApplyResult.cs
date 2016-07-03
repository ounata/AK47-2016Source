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
    /// 转让界面显示结果模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class TransferApplyResult
    {
        /// <summary>
        /// 转出学员信息
        /// </summary>
        [DataMember]
        public CustomerModel Customer
        {
            set;
            get;
        }

        /// <summary>
        /// 转给学员信息
        /// </summary>
        [DataMember]
        public CustomerModel BizCustomer
        {
            set;
            get;
        }

        /// <summary>
        /// 转让申请
        /// </summary>
        [DataMember]
        public TransferApplyModel Apply
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
        public static TransferApplyResult LoadByCustomerID(string customerID, IUser user)
        {
            TransferApplyResult result = new TransferApplyResult();
            result.Customer = CustomerModel.Load(customerID, false);
            result.Accounts = AccountModel.Load4TransferByCustomerID(result.Customer.CustomerID);
            result.Apply = TransferApplyModel.LoadByCustomer(result.Customer);
            result.Assert = Validate(result.Customer.CustomerID, user);

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(TransferApplyModel));
            return result;
        }

        public static TransferApplyResult LoadByApplyID(string applyID, IUser user)
        {
            TransferApplyResult result = new TransferApplyResult();
            result.Apply = TransferApplyModel.LoadByApplyID(applyID);
            result.Customer = CustomerModel.Load(result.Apply.CustomerID, false);
            result.BizCustomer = CustomerModel.Load(result.Apply.BizCustomerID, false);

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(TransferApplyModel));
            return result;
        }

        public static AssertResult Validate(string customerID, IUser user)
        {
#if DEBUG
            return new AssertResult();
#else
            PPTSJob job = user.GetCurrentJob();
            if (job.JobType != JobTypeDefine.Educator)
                return new AssertResult(false, "只有学管师才可以提交转让申请");

            CustomerStaffRelationCollection collection = CustomerStaffRelationAdapter.Instance.LoadByCustomerID(customerID);
            if (job.JobType == JobTypeDefine.Educator)
            {
                CustomerStaffRelation relation = collection.Find(x => x.RelationType == CustomerRelationType.Educator);
                if (relation == null)
                    return new AssertResult(false, "请先给该学员分配学管师再转让");
                else if (job.ID != relation.StaffJobID)
                    return new AssertResult(false, "只有该学员的学管师才可以提交转让申请");
            }
            return new AssertResult();
#endif
        }
    }
}