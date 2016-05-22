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
    public class AccountChargePaymentDataSource : GenericCustomerDataSource<ChargePaymentQueryModel, ChargePaymentQueryModelCollection>
    {
        public static readonly new AccountChargePaymentDataSource Instance = new AccountChargePaymentDataSource();

        private AccountChargePaymentDataSource()
        {
        }
        
        public PagedQueryResult<ChargePaymentQueryModel, ChargePaymentQueryModelCollection> QueryResult(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            var select = @"a.CampusID,a.CampusName,a.CustomerID,a.CustomerCode,a.CustomerName,a.CustomerGrade
                            ,a.ApplyID,a.ApplyNo,a.ApplyTime,a.ApplierName,a.ChargeType,a.ChargeMoney
                            ,b.PayID,b.PayNo,b.PayTime,b.PayStatus,b.PayType,b.PayTicket,b.PayMoney,b.PayeeName,b.Payer
                            ,b.CheckStatus";
            var from = @" CM.AccountChargeApplies a
                          inner join CM.AccountChargePayments b on a.ApplyID = b.ApplyID
                          left  join CM.CustomersFulltext d on a.CustomerID = d.OwnerID ";
            PagedQueryResult<ChargePaymentQueryModel, ChargePaymentQueryModelCollection> result = Query(prp, select, from, condition, orderByBuilder);
            return result;
        }
    }
}
