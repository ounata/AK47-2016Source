using MCS.Library.Data;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Entities;
using PPTS.WebAPI.Orders.ViewModels.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.DataSources
{
    public class AssetConsumeDataSource : GenericOrderDataSource<AssetConsumeViewModel, AssetConsumeViewCollectionModel>
    {
        public static readonly new AssetConsumeDataSource Instance = new AssetConsumeDataSource();

        public AssetConsumeDataSource()
        {
        }

        public PagedQueryResult<AssetConsumeViewModel, AssetConsumeViewCollectionModel> Load(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            var select = @" * ";
            var from = @" [OM].[v_AssetConsumes] ";
            return this.Query(prp,select,from, condition, orderByBuilder);
        }
    }
}