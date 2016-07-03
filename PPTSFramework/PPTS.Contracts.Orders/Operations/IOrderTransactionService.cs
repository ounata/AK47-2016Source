using PPTS.Data.Common;
using PPTS.Data.Orders;
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
        /// 帐户扣钱后，订单进行处理
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="orderId"></param>
        /// <param name="processStatus"></param>
        /// <param name="order"></param>
        /// <param name="items"></param>
        /// <param name="assets"></param>
        /// <param name="assigns"></param>
        /// <param name="classLessonItems"></param>
        [OperationContract]
        void OrderProcess(string processID, string orderId, ProcessStatusDefine processStatus, Order order, List<OrderItem> items, List<Asset> assets, List<Assign> assigns, List<ClassLessonItem> classLessonItems);

        /// <summary>
        /// 帐户退钱后，订单进行处理
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="debookId"></param>
        /// <param name="orderStatus"></param>
        /// <param name="processStatus"></param>
        /// <param name="currentUserId"></param>
        /// <param name="currentUserName"></param>
        [OperationContract]
        void ModifyDebookOrderStatus(string processID, string debookId, OrderStatus orderStatus, ProcessStatusDefine processStatus,string currentUserId,string currentUserName);


    }
}
