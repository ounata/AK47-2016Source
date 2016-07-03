using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Adapters
{
    public class AssetConfirmAdapter : UpdatableAndLoadableAdapterBase<AssetConfirm, AssetConfirmCollection>
    {
        public static readonly AssetConfirmAdapter Instance = new AssetConfirmAdapter();
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSOrderConnectionName;
        }

        public AssetConfirmCollection Load(string itemID,string[] campusIds)
        {
            var whereSqlBuilder = new WhereSqlClauseBuilder();
            whereSqlBuilder.AppendItem("AssetRefID", itemID);
            if(campusIds != null)
            {
                whereSqlBuilder.AppendItem("CampusID", campusIds,"in",true);
            }

            var mainQueryTable = ORMapping.GetMappingInfo<Asset>().GetQueryTableName();
            var whereSql = string.Format(" exists ( select 1 from {1} where {0}.AssetID = {1}.AssetID and {2})", GetQueryTableName(), mainQueryTable, whereSqlBuilder.ToSqlString(TSqlBuilder.Instance));
            var fields = ORMapping.GetSelectFieldsNameSql<AssetConfirm>();
            var sql = string.Format("select {0} from {1} where {2}", fields, GetQueryTableName(), whereSql);
            return QueryData(sql);
        }

        public AssetConfirmCollection Load(string itemID)
        {
            return Load(itemID, null);
        }



    }
}
