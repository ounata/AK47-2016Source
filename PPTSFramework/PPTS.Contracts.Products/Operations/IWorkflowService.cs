using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Products.Operations
{
    [ServiceContract]
    public interface IWorkflowService
    {
        /// <summary>
        /// 折扣审批完成时调用的服务
        /// </summary>
        /// <param name="DiscountID"></param>
        [OperationContract]
        void DiscountProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

        /// <summary>
        /// 折扣审批作废时调用的服务
        /// </summary>
        /// <param name="DiscountID"></param>
        [OperationContract]
        void DiscountProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName);


        /// <summary>
        /// 买赠审批完成时调用的服务
        /// </summary>
        /// <param name="PresentID"></param>
        [OperationContract]
        void PresentProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

        /// <summary>
        /// 买赠审批作废时调用的服务
        /// </summary>
        /// <param name="PresentID"></param>
        [OperationContract]
        void PresentProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

        /// <summary>
        /// 添加产品 审批完成
        /// </summary>
        /// <param name="CurrentResourceID"></param>
        /// <param name="CurrentUserID"></param>
        /// <param name="CurrentUserName"></param>
        [OperationContract]
        void AddProductProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

        /// <summary>
        /// 添加产品 审批作废
        /// </summary>
        /// <param name="CurrentResourceID"></param>
        /// <param name="CurrentUserID"></param>
        /// <param name="CurrentUserName"></param>
        [OperationContract]
        void AddProductProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName);

    }
}
