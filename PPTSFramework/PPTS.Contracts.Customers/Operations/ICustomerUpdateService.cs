using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Operations
{
    public interface ICustomerUpdateService
    {
        /// <summary>
        /// 扣除综合服务费
        /// </summary>
        /// <param name="expenses"></param>
        void DeductExpenses(List<CustomerExpenseRelation> expenses);
    }
}
