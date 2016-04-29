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
            T obj = null;
            this.LoadInContextByAccountID(accountID, account => obj = account);
            return obj;
        }
        public void LoadInContextByAccountID(string accountID, Action<T> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(accountID), "AccountID"),
                collection => action(collection.SingleOrDefault()), DateTime.MinValue);
        }

        /// <summary>
        /// 根据学员ID获取当前账号。
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public T LoadCurrentByCustomerID(string customerID)
        {
            T obj = null;
            this.LoadCurrentInContextByCustomerID(customerID, account => obj = account);
            return obj;
        }
        public void LoadCurrentInContextByCustomerID(string customerID, Action<T> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(customerID), "CustomerID"),
              collection => action(collection.Count == 0 ? null : collection[collection.Count - 1]), DateTime.MinValue);
        }

        protected override void BeforeInnerUpdateInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
        }

        protected override void BeforeInnerUpdate(T data, Dictionary<string, object> context)
        {
        }
    }
}
