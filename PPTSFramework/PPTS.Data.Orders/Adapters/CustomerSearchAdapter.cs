using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Adapters;
using PPTS.Data.Orders.Entities;

namespace PPTS.Data.Orders.Adapters
{
    public class CustomerSearchAdapter : SearchAdapterBase<CustomerSearch,CustomerSearchCollection>
    {
        public static readonly CustomerSearchAdapter Instance = new CustomerSearchAdapter();

        private CustomerSearchAdapter()
        {
        }

        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="asset"></param>
        /*
		public void Insert(Asset asset)
		{
			this.InnerInsert(asset, new Dictionary<string, object>());
		}
		*/

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="assetid"></param>
        /// <returns></returns>
        public CustomerSearch Load(string customerSearchID)
        {
            return this.Load(builder => builder.AppendItem("CustomerSearch", customerSearchID)).SingleOrDefault();
        }

      
    }
}
