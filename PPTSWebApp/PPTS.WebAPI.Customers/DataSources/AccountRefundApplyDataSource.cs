using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using MCS.Library.Data.Mapping;
using MCS.Library.Data.Builder;

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
            var from = @" CM.AccountRefundApplies a";

            PagedQueryResult<RefundApplyQueryModel, RefundApplyQueryModelCollection> result;

            string searchText = (condition as RefundApplyQueryCriteriaModel).SearchText;
            if (string.IsNullOrEmpty(searchText))
                result = Query(prp, select, from, condition, orderByBuilder);
            else
            {
                searchText = searchText.Replace("'", "''");

                string where = null;
                var whereBuilder = ConditionMapping.GetConnectiveClauseBuilder(condition);
                if (whereBuilder != null && TSqlBuilder.Instance != null)
                {
                    where = whereBuilder.ToSqlString(TSqlBuilder.Instance);
                    if (!string.IsNullOrEmpty(where))
                        where += " AND ";
                    where += string.Format("EXISTS(select top 1 1 from CM.CustomersFulltext d where d.OwnerID = a.CustomerID and (contains(d.CustomerSearchContent,'{0}') or contains(d.ParentSearchContent,'{0}')))", searchText);
                }
                result = Query(prp, select, from, where, orderByBuilder);
            }
            return result;
        }
    }
}
