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
    public class AccountChargeApplyDataSource : GenericCustomerDataSource<ChargeApplyQueryModel, ChargeApplyQueryModelCollection>
    {
        public static readonly new AccountChargeApplyDataSource Instance = new AccountChargeApplyDataSource();

        private AccountChargeApplyDataSource()
        {
        }

        public PagedQueryResult<ChargeApplyQueryModel, ChargeApplyQueryModelCollection> QueryResult(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            var select = @"a.*,c.ParentName,c.PhoneNumber";
            var from = @" CM.AccountChargeApplies a
                          left join CM.CustomerParentPhone_Current c on a.CustomerID = c.CustomerID
                          left  join CM.CustomersFulltext d on a.CustomerID = d.OwnerID ";
            PagedQueryResult<ChargeApplyQueryModel, ChargeApplyQueryModelCollection> result = this.Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}
