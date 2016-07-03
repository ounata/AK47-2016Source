using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
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

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {

            qc.SelectFields = @" a.* ";

            qc.FromClause = @" CM.[CustomerStopAlerts] a ";

            #region 数据权限加工

            qc.WhereClause = PPTS.Data.Common.Authorization.ScopeAuthorization<CustomerStopAlerts>

                .GetInstance(ConnectionDefine.PPTSCustomerConnectionName)

                .ReadAuthExistsBuilder("a", qc.WhereClause).ToSqlString(TSqlBuilder.Instance);

            #endregion

            base.OnBuildQueryCondition(qc);
        }

        public PagedQueryResult<StopAlertQueryModel, StopAlertQueryCollection> LoadCustomerStopAlerts(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = " * ";
            string from = " CM.[CustomerStopAlerts] ";
            PagedQueryResult<StopAlertQueryModel, StopAlertQueryCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }

    }
}
