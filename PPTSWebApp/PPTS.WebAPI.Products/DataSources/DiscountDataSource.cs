using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using PPTS.Data.Products;
using PPTS.Data.Products.DataSources;
using PPTS.Data.Products.Entities;
using PPTS.WebAPI.Products.ViewModels.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.DataSources
{
    public class DiscountDataSource : GenericProductDataSource<Discount, DiscountCollection>
    {
        public static readonly new DiscountDataSource Instance = new DiscountDataSource();

        public DiscountDataSource()
        {
        }

        public PagedQueryResult<Discount, DiscountCollection> Load(IPageRequestParams prp, DiscountQueryCriteriaModel condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            var DiscountsBuilder = ConditionMapping.GetConnectiveClauseBuilder(condition,true, new AdjustConditionValueDelegate(DiscountQueryCriteriaModel.DiscountsAdjustConditionValueDelegate));
            var DiscountPermissionsBuilder = ConditionMapping.GetConnectiveClauseBuilder(condition, new AdjustConditionValueDelegate(DiscountQueryCriteriaModel.DiscountPermissionsAdjustConditionValueDelegate));
            string PresentsWhere = DiscountsBuilder.ToSqlString(TSqlBuilder.Instance);            
            string PresentPermissionsWhere = string.Empty;
            if (condition.CheckPresentPermissionsAdjustCondition())
            {
                PresentPermissionsWhere = condition.CampusStatus == CampusUseInfoDefine.DQ ? string.Format(" and exists (select * from [PM].[v_DiscountPermissions_Current] dp  where dp.DiscountID = [Discounts].DiscountID and {0}) ", DiscountPermissionsBuilder.ToSqlString(TSqlBuilder.Instance)) : string.Format(" and  exists (select * from [PM].[DiscountPermissions] dp  where dp.DiscountID = [Discounts].DiscountID and {0}) ", DiscountPermissionsBuilder.ToSqlString(TSqlBuilder.Instance));
            }
            string sqlWhere = string.Format("{0}{1}", string.IsNullOrEmpty(PresentsWhere) ? " 1 = 1 " : PresentsWhere, PresentPermissionsWhere);
            sqlWhere = sqlWhere + string.Format(" and  DiscountStatus != {0} ", ((int)DiscountStatusDefine.Deleted).ToString());

            var result = Query(prp, sqlWhere, " DiscountID desc ");

            return result;
        }

    }
}