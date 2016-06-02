using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.StopAlerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerStopAlertDataSource : GenericCustomerDataSource<StopAlertQueryModel, StopAlertQueryCollection>
    {
        public static readonly new CustomerStopAlertDataSource Instance = new CustomerStopAlertDataSource();

        public PagedQueryResult<StopAlertQueryModel, StopAlertQueryCollection> LoadCustomerStopAlerts(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = " * ";
            string from = " CM.[CustomerStopAlerts] ";
            PagedQueryResult<StopAlertQueryModel, StopAlertQueryCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }

    }
}
