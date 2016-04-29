using System.Collections.Generic;
using MCS.Library.Core;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerFollows
{
    public class CreatableFollowModel
    {
        [ObjectValidator]
        public CustomerFollow Follow
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

        ///// <summary>
        ///// 跟进对象
        ///// </summary>
        //[NoMapping]
        //[ConstantCategory("C_CODE_ABBR_Customer_CRM_SaleContactTarget")]
        //public int FollowsObject
        //{
        //    get;
        //    set;
        //}

        public CreatableFollowModel(string customerId, bool isPotential)
        {
            this.Follow = new CustomerFollow { FollowID = UuidHelper.NewUuidString(), CustomerID = customerId, IsPotential = isPotential, FollowStage = SalesStageType.NotInvited };
            this.FollowItems = new List<CustomerFollowItem>();
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }
    }
}