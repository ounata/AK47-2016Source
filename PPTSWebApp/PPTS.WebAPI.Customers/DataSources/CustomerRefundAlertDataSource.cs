using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
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

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = @" a.* ";

            qc.FromClause = @" CM.[CustomerRefundAlerts] a ";

            #region 数据权限加工

            qc.WhereClause = PPTS.Data.Common.Authorization.ScopeAuthorization<CustomerRefundAlerts>

                .GetInstance(ConnectionDefine.PPTSCustomerConnectionName)

                .ReadAuthExistsBuilder("a", qc.WhereClause).ToSqlString(TSqlBuilder.Instance);

            #endregion

            base.OnBuildQueryCondition(qc);
        }

        public PagedQueryResult<RefundAlertQueryModel, RefundAlertQueryCollection> LoadCustomerRefundAlerts(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            string select = " * ";
            string from = " CM.[CustomerRefundAlerts] ";
            PagedQueryResult<RefundAlertQueryModel, RefundAlertQueryCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}
