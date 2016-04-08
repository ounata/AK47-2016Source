using System.Linq;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerFollowAdapter : CustomerAdapterBase<CustomerFollow, CustomerFollowCollection>
	{
		public static readonly CustomerFollowAdapter Instance = new CustomerFollowAdapter();

		private CustomerFollowAdapter()
		{
		}

		/// <summary>
		/// 加载操作
		/// </summary>
        /// <param name="followid"></param>
		/// <returns></returns>
		public CustomerFollow Load(string followid)
		{
            return this.Load(builder => builder.AppendItem("FollowID", followid)).SingleOrDefault();
		}
	}
}