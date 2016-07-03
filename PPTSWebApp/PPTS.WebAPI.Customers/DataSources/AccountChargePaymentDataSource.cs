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
    public class AccountChargePaymentDataSource : GenericCustomerDataSource<ChargePaymentQueryModel, ChargePaymentQueryModelCollection>
    {
        public static readonly new AccountChargePaymentDataSource Instance = new AccountChargePaymentDataSource();

        private AccountChargePaymentDataSource()
        {
        }
        
        public PagedQueryResult<ChargePaymentQueryModel, ChargePaymentQueryModelCollection> QueryResult(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            var select = @"a.CampusID,a.CampusName,a.CustomerID,a.CustomerCode,a.CustomerName,a.CustomerGrade,a.ApplyID,a.ApplyNo,a.ApplyTime,a.ApplierName,a.ChargeType,a.ChargeMoney
                            ,b.PayID,b.PayNo,b.PayTime,b.PayStatus,b.PayType,b.PayTicket,b.PayMoney,b.PayeeName,b.Payer,b.CheckStatus";
            var from = @" CM.AccountChargeApplies a
                          inner join CM.AccountChargePayments b on a.ApplyID = b.ApplyID
                          left join CM.CustomersFulltext d on a.CustomerID = d.OwnerID
                          left join CM.PotentialCustomersFulltext e on a.CustomerID=e.OwnerID";

            PagedQueryResult<ChargePaymentQueryModel, ChargePaymentQueryModelCollection> result;

            string searchText = (condition as ChargePaymentQueryCriteriaModel).SearchText;
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
                    where += "(";
                    where += string.Format("EXISTS(select top 1 1 from CM.CustomersFulltext d where d.OwnerID = a.CustomerID and (contains(d.CustomerSearchContent,'{0}') or contains(d.ParentSearchContent,'{0}')))", searchText);
                    where += " or ";
                    where += string.Format("EXISTS(select top 1 1 from CM.PotentialCustomersFulltext e where e.OwnerID = a.CustomerID and (contains(e.CustomerSearchContent,'{0}') or contains(e.ParentSearchContent,'{0}')))", searchText);
                    where += ")";
                }
                result = Query(prp, select, from, where, orderByBuilder);
            }
            return result;
        }
    }
}
