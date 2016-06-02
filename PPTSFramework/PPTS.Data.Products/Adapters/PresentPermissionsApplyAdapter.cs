using MCS.Library.Data.Builder;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Adapters
{
    public class PresentPermissionsApplyAdapter : ProductAdapterBase<PresentPermissionsApply, PresentPermissionsApplieCollection>
    {
        public static PresentPermissionsApplyAdapter Instance = new PresentPermissionsApplyAdapter();

        public PresentPermissionsApplieCollection LoadCollectionByOrgIDs(string OrgIDs)
        {
            return this.Load(builder => builder.AppendItem("CampusID", OrgIDs, "in", true));
        }

        public PresentPermissionsApplieCollection LoadApprovingCollectionByOrgIDs(string OrgIDs)
        {
            string sqlStr = PrepareLoadApprovingCollectionByOrgIDs(OrgIDs);
            return this.QueryData(sqlStr);
        }

        private string PrepareLoadApprovingCollectionByOrgIDs(string OrgIDs)
        {
            WhereSqlClauseBuilder bduilder = new WhereSqlClauseBuilder();
            bduilder.AppendItem("CampusID", OrgIDs, "in", true);
            return string.Format(@"   select * from [PM].[PresentPermissionsApplies] 
                                      where {0}
                                      and exists(
                                        select * from[PM].[Presents] p  where  p.PresentID = [PresentPermissionsApplies].PresentID and p.PresentStatus = {1}
                                      ) ", bduilder.ToSqlString(TSqlBuilder.Instance), (int)PresentStatusDefine.Approving);
        }
    }
}
