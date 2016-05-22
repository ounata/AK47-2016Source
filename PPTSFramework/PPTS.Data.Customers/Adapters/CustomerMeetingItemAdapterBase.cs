using MCS.Library.Data.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerMeetingItemAdapterBase<T, TCollection> : UpdatableAndLoadableAdapterBase<T, TCollection> where TCollection : IList<T>, new()
    {
        public readonly static CustomerMeetingItemAdapterBase<T, TCollection> Instance = new CustomerMeetingItemAdapterBase<T, TCollection>();
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }
        public TCollection LoadItems(string meetingID)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(meetingID), "meetingID"));
        }
        public T Load(string ItemID)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(ItemID), "ItemID")).SingleOrDefault();
        }
    }
}
