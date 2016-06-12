using System.Collections.Generic;
using MCS.Library.Core;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.Adapters;
using System.Runtime.Serialization;
using System;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Security;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerFollows
{
    public class CreatableFollowModel
    {
        /// <summary>
        /// 跟进前阶段
        /// </summary>
        [NoMapping]
        [DataMember]
        public SalesStageType PreviousFollowStage
        {
            get;
            set;
        }

        [ObjectValidator]
        public CustomerFollow Follow
        {
            get;
            set;
        }

        [ObjectValidator]
        public CurrentCustomerModel CurrentCustomer
        {
            get;
            set;
        }

        [ObjectValidator]
        public List<CustomerFollowItem> FollowItems
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        /// <summary>
        /// 科目
        /// </summary>
        [NoMapping]
        [ConstantCategory("c_codE_ABBR_BO_Product_TeacherSubject")]
        public int Subject
        {
            get;
            set;
        }

        public void InitVerifier(IUser user)
        {
            this.Follow.FollowerID = user.ID;
            this.Follow.FollowerName = user.Name;
            this.Follow.FollowerJobID = user.GetCurrentJob().ID;
            this.Follow.FollowerJobName = user.GetCurrentJob().Name;
        }

        public static CreatableFollowModel CreateFollow(string customerId, bool isPotential)
        {
            CreatableFollowModel model = new CreatableFollowModel();
            model.CurrentCustomer = CurrentCustomerModel.Load(customerId);
            model.Follow = new CustomerFollow { FollowID = UuidHelper.NewUuidString(), OrgID = model.CurrentCustomer.OrgID, OrgName = model.CurrentCustomer.OrgName, OrgType = model.CurrentCustomer.OrgType, CustomerID = customerId, IsPotential = isPotential, FollowStage = SalesStageType.NotInvited, FollowTime = DateTime.Now, CreateTime = DateTime.Now };
            model.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerFollow), typeof(CreatableFollowModel), typeof(List<CustomerFollowItem>));
            CustomerFollow followsObj = CustomerFollowAdapter.Instance.LoadPreviousFollow(customerId);
            if (followsObj != null)
                model.PreviousFollowStage = followsObj.FollowStage;
            else
                model.PreviousFollowStage = SalesStageType.NotInvited;

            return model;
        }

        /// <summary>
        /// 是否存在跟进记录
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool ExistCustomerFollow(string customerID)
        {
            CustomerFollow followModel = CustomerFollowAdapter.Instance.LoadPreviousFollow(customerID);
            return followModel == null;
        }
    }
}