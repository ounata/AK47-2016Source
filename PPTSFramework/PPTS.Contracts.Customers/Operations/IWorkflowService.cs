using PPTS.Data.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Operations
{
    [ServiceContract]
    public interface IWorkflowService
    {
        /// <summary>
        /// 解冻流程完成时调用的服务
        /// </summary>
        /// <param name="CustomerID"></param>
        [OperationContract]
        void ThawProcessCompleting(string CustomerID, ThawReasonType ThawReasonType);

        /// <summary>
        /// 解冻流程作废时调用的服务
        /// </summary>
        /// <param name="CustomerID"></param>
        [OperationContract]
        void ThawProcessCancelling(string CustomerID);

        /// <summary>
        /// 退费审批通过
        /// </summary>
        /// <param name="CurrentResourceID"></param>
        /// <param name="CurrentUserID"></param>
        /// <param name="CurrentUserName"></param>
        [OperationContract]
        void AccountRefundProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

        /// <summary>
        /// 退费审批取消
        /// </summary>
        /// <param name="CurrentResourceID"></param>
        /// <param name="CurrentUserID"></param>
        /// <param name="CurrentUserName"></param>
        [OperationContract]
        void AccountRefundProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

        /// <summary>
        /// 转让审批通过
        /// </summary>
        /// <param name="CurrentResourceID"></param>
        /// <param name="CurrentUserID"></param>
        /// <param name="CurrentUserName"></param>
        [OperationContract]
        void AccountTransferProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

        /// <summary>
        /// 转让审批取消
        /// </summary>
        /// <param name="CurrentResourceID"></param>
        /// <param name="CurrentUserID"></param>
        /// <param name="CurrentUserName"></param>
        [OperationContract]
        void AccountTransferProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

        /// <summary>
        /// 转学审批通过
        /// </summary>
        /// <param name="CurrentResourceID"></param>
        /// <param name="CurrentUserID"></param>
        /// <param name="CurrentUserName"></param>
        [OperationContract]
        void StudentTransferProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

        /// <summary>
        /// 转学审批取消
        /// </summary>
        /// <param name="CurrentResourceID"></param>
        /// <param name="CurrentUserID"></param>
        /// <param name="CurrentUserName"></param>
        [OperationContract]
        void StudentTransferProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName);
    }
}
