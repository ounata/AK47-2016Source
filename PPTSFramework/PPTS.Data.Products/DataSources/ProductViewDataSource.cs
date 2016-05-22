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
    public class ProductViewDataSource : ObjectDataSourceQueryAdapterBase<ProductView, ProductViewCollection>
    {
        public static readonly ProductViewDataSource Instance = new ProductViewDataSource();

        public string[] CampusIDs { set; get; }

        private ProductViewDataSource()
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
                var where = string.Format("exists ({0} and ProductID = {1}.ProductID )", ProductPermissionAdapter.Instance.GetProductIdsByCampusIdsSQL(CampusIDs), ProductViewAdapter.Instance.TableName);
                qc.WhereClause = qc.WhereClause + (string.IsNullOrWhiteSpace(qc.WhereClause) ? where : " and " + where);
            }
            
        }

    }
}
