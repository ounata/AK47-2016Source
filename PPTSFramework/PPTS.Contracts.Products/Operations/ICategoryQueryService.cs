using PPTS.Contracts.Products.Models;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Products.Operations
{
    [ServiceContract]
    public interface ICategoryQueryService
    {
        /// <summary>
        /// 获取 产品2级分类
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        CategoryQueryResult QueryCategory();
    }
}
