using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerFollowItemAdapter : CustomerAdapterBase<CustomerFollow, CustomerFollowCollection>
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
		public CustomerFollow Load(string itemid)
		{
            return this.Load(builder => builder.AppendItem("ItemID", itemid)).SingleOrDefault();
		}
	}
}