using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Orders.Operations
{
    [ServiceContract]
    public interface IOrderTransactionService
    {
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        [OperationContract]
        void ResetOrderStatus(string processID, string orderId, int status);

        /// <summary>
        /// 提交订单后同步资产
        /// </summary>
        /// <param name="assets"></param>
        [OperationContract]
        void SyncAsset(string processID, AssetCollection assets);
    }
}
