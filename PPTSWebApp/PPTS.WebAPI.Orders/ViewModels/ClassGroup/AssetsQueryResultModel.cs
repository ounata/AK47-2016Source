using MCS.Library.Data;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    /// <summary>
    /// 资产视图查询返回模型
    /// </summary>
    public class AssetsQueryResultModel
    {
        public PagedQueryResult<AssetView, AssetViewCollection> QueryResult { get; set; }
    }

}