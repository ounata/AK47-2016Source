using PPTS.Contracts.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Operations
{
    /// <summary>
    /// 账户接口相关操作
    /// </summary>
    [ServiceContract]
    public interface IAccountQueryService
    {
        [OperationContract]
        AccountCollectionQueryResult QueryAccountCollectionByCustomerID(String customerID);

        [OperationContract]
        AccountChargeCollectionQueryResult QueryAccountChargeCollectionByCustomerID(string customerID);
    }

}
