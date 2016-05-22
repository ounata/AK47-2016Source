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
        [OperationContract]
        AssetStatisticQueryResult QueryAssetStatisticByAccountID(string accountID);
    }
}
