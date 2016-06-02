using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.RefundAlerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerRefundAlertDataSource : GenericCustomerDataSource<RefundAlertQueryModel, RefundAlertQueryCollection>
    {
        public static readonly new CustomerRefundAlertDataSource Instance = new CustomerRefundAlertDataSource();

        public PagedQueryResult<RefundAlertQueryModel, RefundAlertQueryCollection> LoadCustomerStopAlerts(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = " * ";
            string from = " CM.[CustomerRefundAlerts] ";
            PagedQueryResult<RefundAlertQueryModel, RefundAlertQueryCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}
