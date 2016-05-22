using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using PPTS.Data.Products.DataSources;
using PPTS.Data.Products.Entities;
using PPTS.WebAPI.Products.ViewModels.ExtraGift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.DataSources
{
    public class PresentDataSource : GenericProductDataSource<Present, PresentCollection>
    {
        public static readonly new PresentDataSource Instance = new PresentDataSource();

        public PresentDataSource()
        {
        }

        public PagedQueryResult<Present, PresentCollection> Load(IPageRequestParams prp, PresentsQueryCriteriaModel condition, IEnumerable<IOrderByRequestItem> orderByBuilder) {
            var PresentsBuilder = ConditionMapping.GetConnectiveClauseBuilder(condition, new AdjustConditionValueDelegate(PresentsQueryCriteriaModel.PresentsAdjustConditionValueDelegate));
            var PresentPermissionsBuilder = ConditionMapping.GetWhereSqlClauseBuilder(condition, new AdjustConditionValueDelegate(PresentsQueryCriteriaModel.PresentPermissionsAdjustConditionValueDelegate));
            string PresentsWhere = PresentsBuilder.ToSqlString(TSqlBuilder.Instance);
            string PresentPermissionsWhere = string.Empty;
            if (condition.CheckPresentPermissionsAdjustCondition()) {
                PresentPermissionsWhere = condition.CampusStatus == "1" ? string.Format(" and (select * from [PM].[v_PresentPermissions_Current] pp  where pp.PresentID = p.PresentID and {0}) ", PresentPermissionsBuilder.ToSqlString(TSqlBuilder.Instance)) : string.Format(" and  exists (select * from [PM].[PresentPermissions] pp  where pp.PresentID = p.PresentID and {0}) ", PresentPermissionsBuilder.ToSqlString(TSqlBuilder.Instance));
            }  
            string sqlWhere = string.Format("{0}{1}", string.IsNullOrEmpty(PresentsWhere) ? " 1 = 1 " : PresentsWhere, PresentPermissionsWhere);

            var result = Query(prp, sqlWhere, " PresentID desc ");

            return result;
        }

    }
}