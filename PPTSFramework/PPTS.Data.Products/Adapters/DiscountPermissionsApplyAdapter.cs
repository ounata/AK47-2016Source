using MCS.Library.Data.Builder;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Adapters
{
    public class DiscountPermissionsApplyAdapter : ProductAdapterBase<DiscountPermissionsApply, DiscountPermissionsApplieCollection>
    {
        public static DiscountPermissionsApplyAdapter Instance = new DiscountPermissionsApplyAdapter();

        public DiscountPermissionsApplieCollection LoadCollectionByOrgIDs(string OrgIDs)
        {
            return this.Load(builder => builder.AppendItem("CampusID", OrgIDs, "in", true));
        }

        public DiscountPermissionsApplieCollection LoadApprovingCollectionByOrgIDs(string OrgIDs)
        {
            string sqlStr = PrepareLoadApprovingCollectionByOrgIDs(OrgIDs);
            return this.QueryData(sqlStr);
        }

        private string PrepareLoadApprovingCollectionByOrgIDs(string OrgIDs)
        {
            WhereSqlClauseBuilder bduilder = new WhereSqlClauseBuilder();
            bduilder.AppendItem("CampusID", OrgIDs, "in", true);
            return string.Format(@"   select * from [PM].[DiscountPermissionsApplies] 
                                      where {0}
                                      and exists(
                                        select * from[PM].[Discounts] d  where  d.DiscountID = [DiscountPermissionsApplies].DiscountID and d.DiscountStatus = {1}
                                      ) ", bduilder.ToSqlString(TSqlBuilder.Instance), (int)DiscountStatusDefine.Approving);
        }
    }
}
