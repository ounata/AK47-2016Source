using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Operations
{
    [ServiceContract]
    public interface IFinancialChargeService
    {
        /// <summary>
        /// 收入（每天）
        /// </summary>
        [OperationContract]
        void SendFinancialIncome();

        /// <summary>
        /// 退费（每天）
        /// </summary>
        [OperationContract]
        void SendFinancialRefound();
    }
}
