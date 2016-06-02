using PPTS.Contracts.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Orders.Operations
{
    [ServiceContract]
    public interface IAssetQueryService
    {
        /// <summary>
        /// 通过账户ID获取资产统计信息
        /// </summary>
        /// <param name="accountID">账户ID</param>
        /// <returns></returns>
        [OperationContract]
        AssetStatisticQueryResult QueryAssetStatisticByAccountID(string accountID);

        /// <summary>
        /// 通过学员ID获取资产统计信息
        /// </summary>
        /// <param name="customerID">学员ID</param>
        /// <returns></returns>
        [OperationContract]
        AssetStatisticQueryResult QueryAssetStatisticByCustomerID(string customerID);

        [OperationContract]
        RefundConsumptionValueQueryResult QueryConsumptionValue(RefundConsumptionValueQueryCriteriaModel criteria);

        [OperationContract]
        RefundReallowanceMoneyQueryResult QueryReallowanceMoney(RefundReallowanceMoneyQueryCriteriaModel criteria);
    }
}
