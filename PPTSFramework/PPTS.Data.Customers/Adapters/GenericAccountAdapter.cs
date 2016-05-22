using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class GenericAccountAdapter<T, TCollection> : VersionedCustomerAdapterBase<T, TCollection>
        where T : Account
        where TCollection : IList<T>, new()
    {
        public static readonly GenericAccountAdapter<T, TCollection> Instance = new GenericAccountAdapter<T, TCollection>();

        protected GenericAccountAdapter()
        {
        }

        /// <summary>
        /// 根据账户ID获取账户信息
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public T LoadByAccountID(string accountID)
        {
            return this.Load(builder => builder.AppendItem("AccountID", accountID), DateTime.MinValue).SingleOrDefault();
        }
        public void LoadInContextByAccountID(string accountID, Action<T> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(accountID), "AccountID"),
                collection => action(collection.SingleOrDefault()), DateTime.MinValue);
        }
        
        protected override void BeforeInnerUpdateInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
        }

        protected override void BeforeInnerUpdate(T data, Dictionary<string, object> context)
        {
        }
    }
}
