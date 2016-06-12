using System.Collections.Generic;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.Mapping;
using System.Runtime.Serialization;
using PPTS.Data.Customers;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.Adapters;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerFollows
{
    public class ViewFollowModel
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

        public CustomerFollow Follow
        {
            get;
            set;
        }

        public CustomerFollowItemCollection FollowItems
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        public ViewFollowModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }

        public static ViewFollowModel LoadFollowModel(string followId)
        {
            ViewFollowModel model = new ViewFollowModel();
            model.Follow = CustomerFollowAdapter.Instance.Load(followId);
            #region Add FollowItems

            if (followId != null)
            {
                CustomerFollowItemCollection followItem = CustomerFollowItemAdapter.Instance.LoadCollectionByCustomerID(followId);
                //if (followItem != null && followItem.Count > 0)
                //{
                //    foreach (CustomerFollowItem item in followItem)
                //    {
                //        model.FollowItems.Add(item);
                //    }
                //}
                model.FollowItems = followItem;
            }

            #endregion
            
            model.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerFollow), typeof(ViewFollowModel));
            model.PreviousFollowStage = CustomerFollowAdapter.Instance.LoadPreviousFollow(model.Follow.CustomerID).FollowStage;
            return model;
        }
    }
}