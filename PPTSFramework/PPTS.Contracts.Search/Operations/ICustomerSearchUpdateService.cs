using PPTS.Contracts.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Search.Operations
{
    [ServiceContract]
    public interface ICustomerSearchUpdateService
    {
        /// <summary>
        /// 客户信息更新
        /// </summary>
        /// <param name="model">客户信息模型</param>
        [OperationContract]
        void UpdateByCustomerInfo(CustomerSearchUpdateModel model);

    }
}
