using MCS.Library.OGUPermission;
using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    /// <summary>
    /// 转学界面显示结果模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class StudentTransferApplyResult
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
        /// 转让申请
        /// </summary>
        [DataMember]
        public StudentTransferApplyModel Apply
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
        public static StudentTransferApplyResult LoadByCustomerID(string customerID, IUser user)
        {
            StudentTransferApplyResult result = new StudentTransferApplyResult();
            result.Customer = CustomerModel.Load(customerID, false);
            result.Apply = StudentTransferApplyModel.LoadByCustomer(result.Customer);
            result.Assert = Validate(result.Customer.CustomerID, user);

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(StudentTransferApplyModel));
            return result;
        }

        public static StudentTransferApplyResult LoadByApplyID(string applyID, IUser user)
        {
            StudentTransferApplyResult result = new StudentTransferApplyResult();
            result.Apply = StudentTransferApplyModel.LoadByApplyID(applyID);
            result.Customer = CustomerModel.Load(result.Apply.CustomerID, false);

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerModel)
                , typeof(StudentTransferApplyModel));
            return result;
        }

        public static AssertResult Validate(string customerID, IUser user)
        {
            return new AssertResult();

            PPTSJob job = user.GetCurrentJob();
            if (job.JobType != JobTypeDefine.Educator)
                return new AssertResult(false, "只有学管师才可以提交转学申请");

            CustomerStaffRelationCollection collection = CustomerStaffRelationAdapter.Instance.LoadByCustomerID(customerID);
            if (job.JobType == JobTypeDefine.Educator)
            {
                CustomerStaffRelation relation = collection.Find(x => x.RelationType == CustomerRelationType.Educator);
                if (relation == null)
                    return new AssertResult(false, "请先给该学员分配学管师再提交转学申请");
                else if (job.ID != relation.StaffJobID)
                    return new AssertResult(false, "只有该学员的学管师才可以提交转学申请");
            }
            return new AssertResult();
        }
    }
}