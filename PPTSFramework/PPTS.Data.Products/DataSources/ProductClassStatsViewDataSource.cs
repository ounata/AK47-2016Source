using MCS.Library.Data.Adapters;
using PPTS.Data.Products.Entities;
using PPTS.Data.Products.Adapters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.DataObjects;

namespace PPTS.Data.Products.DataSources
{
    public class ProductClassStatsViewDataSource : ObjectDataSourceQueryAdapterBase<OrderClassGroupProductView, OrderClassGroupProductViewCollection>
    {
        public static readonly ProductClassStatsViewDataSource Instance = new ProductClassStatsViewDataSource();

        public string[] CampusIDs { set; get; }

        private ProductClassStatsViewDataSource()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSProductConnectionName;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            base.OnBuildQueryCondition(qc);

            if (CampusIDs != null && CampusIDs.Length > 0)
            {
                var queryTableName = MCS.Library.Data.Mapping.ORMapping.GetMappingInfo<OrderClassGroupProductView>().GetQueryTableName();
                var where = string.Format("exists ({0} and ProductID = {1}.ProductID )", ProductPermissionAdapter.Instance.GetProductIdsByCampusIdsSQL(CampusIDs), queryTableName);
                qc.WhereClause = qc.WhereClause + (string.IsNullOrWhiteSpace(qc.WhereClause) ? where : " and " + where);
            }

        }
    }
}
