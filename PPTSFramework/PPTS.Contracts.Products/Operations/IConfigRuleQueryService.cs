using PPTS.Contracts.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Products.Operations
{
    /// <summary>
    /// 配置规则
    /// </summary>
    [ServiceContract]
    public interface IConfigRuleQueryService
    {
        /// <summary>
        /// 通过校区ID获得对应的折扣配置信息
        /// </summary>
        /// <param name="campusID">校区ID</param>
        /// <returns></returns>
        [OperationContract]
        DiscountQueryResult QueryDiscountByCampusID(string campusID);

        /// <summary>
        /// 通过折扣ID获得对应的折扣配置信息
        /// </summary>
        /// <param name="discountID">折扣ID</param>
        /// <returns></returns>
        [OperationContract]
        DiscountQueryResult QueryDiscountByDiscountID(string discountID);

        /// <summary>
        ///  通过校区ID获得对应的服务费信息
        /// </summary>
        /// <param name="campusID">校区ID</param>
        /// <returns></returns>
        [OperationContract]
        ExpenseQueryResult QueryExpenseByCampusID(string campusID);

        /// <summary>
        ///  通过校区ID获得对应的买赠折扣配置信息
        /// </summary>
        /// <param name="campusID">校区ID</param>
        /// <returns></returns>
        [OperationContract]
        PresentQueryResult QueryPresentByCampusID(string campusID);
    }
}
