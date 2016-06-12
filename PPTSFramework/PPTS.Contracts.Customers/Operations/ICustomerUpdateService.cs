using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Operations
{
    [ServiceContract]
    public interface ICustomerUpdateService
    {
        [OperationContract]
        /// <summary>
        /// 扣除综合服务费
        /// </summary>
        /// <param name="expenses"></param>
        void DeductExpenses(List<CustomerExpenseRelation> expenses);
    }
}
