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
        /// <param name="productIDs">产品ID集合</param>
        /// <returns></returns>
        [OperationContract]
        ProductViewQueryResult QueryProductViewsByIDs(string[] productIDs);

        /// <summary>
        /// 是否允许手工确认
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsAssetConfirm(string productId);

        /// <summary>
        /// 是否 存在校区 在 产品列表中
        /// </summary>
        /// <param name="campusIds"></param>
        /// <param name="productIds"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsExistsCampusInProduct(string[] campusIds, string[] productIds);

    }
}
