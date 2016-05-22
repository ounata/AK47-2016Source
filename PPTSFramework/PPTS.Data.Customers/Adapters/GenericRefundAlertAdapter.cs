using MCS.Library.Data.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class GenericRefundAlertAdapter<T, TCollection> : CustomerAdapterBase<T, TCollection>
               where T : CustomerRefundAlerts
        where TCollection : IList<T>, new()
    {
        public static readonly GenericRefundAlertAdapter<T, TCollection> Instance = new GenericRefundAlertAdapter<T, TCollection>();

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public CustomerRefundAlerts Load(string alertID)
        {
            return this.Load(builder => builder.AppendItem("AlertID", alertID)).SingleOrDefault();
        }

        public void LoadInContext(string alertID, Action<T> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(alertID), "AlertID"),
                collection => action(collection.SingleOrDefault()));
        }
    }
}
