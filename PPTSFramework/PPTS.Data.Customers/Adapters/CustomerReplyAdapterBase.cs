using MCS.Library.Data.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerReplyAdapterBase<T, TCollection> : UpdatableAndLoadableAdapterBase<T, TCollection> where TCollection : IList<T>, new()
    {
        public readonly static CustomerReplyAdapterBase<T, TCollection> Instance = new CustomerReplyAdapterBase<T, TCollection>();
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }

        public T Load(string replyID)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(replyID), "ReplyID")).SingleOrDefault();
        }
    }
}
