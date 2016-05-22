using MCS.Library.Data.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerMeetingAdapterBase<T, TCollection> : UpdatableAndLoadableAdapterBase<T, TCollection> where TCollection : IList<T>, new()
    {
        public readonly static CustomerMeetingAdapterBase<T, TCollection> Instance = new CustomerMeetingAdapterBase<T, TCollection>();
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }

        public T Load(string meetingID)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(meetingID), "meetingID")).SingleOrDefault();
        }
    }
}
