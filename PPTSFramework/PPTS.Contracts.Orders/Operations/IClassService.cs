using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Orders.Operations
{
    [ServiceContract]
    public interface IClassService
    {
        /// <summary>
        /// 同步班级数量到产品
        /// </summary>
        /// <param name="productID"></param>
        [OperationContract]
        void SyncClassCountToProduct(string productID);

        /// <summary>
        /// 确认课时
        /// </summary>
        /// <param name="ConfirmTime"></param>
        [OperationContract]
        void ConfirmClassLesson(DateTime ConfirmTime);

        /// <summary>
        /// 作业确认课时
        /// </summary>
        [OperationContract]
        void Job_ConfirmClassLesson();

        /// <summary>
        /// 初始化数据
        /// </summary>
        [OperationContract]
        void Job_InitClassCountToProduct();
    }
}
