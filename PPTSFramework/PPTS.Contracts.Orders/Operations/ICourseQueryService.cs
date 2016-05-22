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
    public interface ICourseQueryService
    {
        [OperationContract]
        HasPeriodCourseQueryResult QueryPeriodCourseByCustomerID(string customerID,DateTime dateTime);

        [OperationContract]
        OrderInfoForRefundQueryResult QueryOrderInfoForRefundByAccountID(OrderInfoForRefundQueryModel model);
    }
}
