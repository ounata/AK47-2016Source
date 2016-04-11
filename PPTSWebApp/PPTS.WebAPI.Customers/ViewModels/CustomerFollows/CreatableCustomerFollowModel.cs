using System.Collections.Generic;
using MCS.Library.Core;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerFollows
{
    public class CreatableCustomerFollowModel
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

        public CreatableCustomerFollowModel()
        {
            this.Follow = new CustomerFollow { FollowID = UuidHelper.NewUuidString() };
            this.FollowItems = new List<CustomerFollowItem>();
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }
    }
}