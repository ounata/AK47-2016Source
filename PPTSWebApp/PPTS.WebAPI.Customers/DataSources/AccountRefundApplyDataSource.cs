using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.WebAPI.Customers.ViewModels.Accounts;

namespace PPTS.WebAPI.Customers.DataSources
{
    /// <summary>
    /// 缴费单数据源类
    /// </summary>
    public class AccountRefundApplyDataSource : GenericCustomerDataSource<RefundApplyQueryModel, RefundApplyQueryModelCollection>
    {
        public static readonly new AccountRefundApplyDataSource Instance = new AccountRefundApplyDataSource();

        private AccountRefundApplyDataSource()
        {
        }

        public PagedQueryResult<RefundApplyQueryModel, RefundApplyQueryModelCollection> QueryResult(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            var select = @"a.*";
            var from = @" CM.AccountRefundApplies a
                          left  join CM.CustomersFulltext d on a.CustomerID = d.OwnerID ";
            PagedQueryResult<RefundApplyQueryModel, RefundApplyQueryModelCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}
