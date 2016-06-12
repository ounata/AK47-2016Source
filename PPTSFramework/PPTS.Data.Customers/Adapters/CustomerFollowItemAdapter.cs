using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Entities;
using System;
using System.Linq;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerFollowItemAdapter : CustomerAdapterBase<CustomerFollowItem, CustomerFollowItemCollection>
	{
		public static readonly CustomerFollowItemAdapter Instance = new CustomerFollowItemAdapter();

		private CustomerFollowItemAdapter()
		{
		}

		/// <summary>
		/// 加载操作
		/// </summary>
        /// <param name="itemid"></param>
		/// <returns></returns>
		public CustomerFollowItem Load(string itemid)
		{
            return this.Load(builder => builder.AppendItem("ItemID", itemid)).SingleOrDefault();
		}

        public CustomerFollowItemCollection LoadCollectionByCustomerID(string followID)
        {
            return this.Load(builder => builder.AppendItem("FollowID", followID));
        }

    }
}