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
    public interface IProductQueryService
    {
        /// <summary>
        /// 通过产品ID集合获得产品视图信息
        /// </summary>
        /// <param name="ProductIDs">产品ID集合</param>
        /// <returns></returns>
        [OperationContract]
        ProductViewQueryResult QueryProductViewsByIDs(string[] ProductIDs);
    }
}
