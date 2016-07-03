using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Orders.Operations
{
    [ServiceContract]
    public interface IWorkflowService
    {

        /// <summary>
        /// 退费审批通过
        /// </summary>
        /// <param name="CurrentResourceID"></param>
        /// <param name="CurrentUserID"></param>
        /// <param name="CurrentUserName"></param>
        [OperationContract]
        void SubmitOrderProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

        /// <summary>
        /// 退费审批取消
        /// </summary>
        /// <param name="CurrentResourceID"></param>
        /// <param name="CurrentUserID"></param>
        /// <param name="CurrentUserName"></param>
        [OperationContract]
        void SubmitOrderProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

        /// <summary>
        /// 退订订单审批通过
        /// </summary>
        /// <param name="CurrentResourceID"></param>
        /// <param name="CurrentUserID"></param>
        /// <param name="CurrentUserName"></param>
        [OperationContract]
        void DebookOrderProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

        /// <summary>
        /// 退订订单审批取消
        /// </summary>
        /// <param name="CurrentResourceID"></param>
        /// <param name="CurrentUserID"></param>
        /// <param name="CurrentUserName"></param>
        [OperationContract]
        void DebookOrderProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

    }
}
