using MCS.Library.Data.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerRefundAlertAdapter : CustomerAdapterBase<CustomerRefundAlerts, CustomerRefundAlertsCollection>
    {
        public static readonly CustomerRefundAlertAdapter Instance = new CustomerRefundAlertAdapter();

        public CustomerRefundAlertAdapter()
        {

        }

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public CustomerRefundAlerts Load(string alertID)
        {
            return this.Load(builder => builder.AppendItem("AlertID", alertID)).SingleOrDefault();
        }

        public void LoadInContext(string alertID, Action<CustomerRefundAlerts> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(alertID), "AlertID"),
                collection => action(collection.SingleOrDefault()));
        }
    }
}
