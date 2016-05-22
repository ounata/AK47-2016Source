using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Adapters
{
    public class PPTSUserAdpter : UpdatableAndLoadableAdapterBase<PPTSUser, PPTSUserCollection>
    {
        public static readonly PPTSUserAdpter Instance = new PPTSUserAdpter();
        private PPTSUserAdpter()
        {

        }

        public PPTSUser LoadByOAName(string oaname)
        {
            string eduValue = string.Format("{0}@21edu.com", oaname);
            string xuedaValue = string.Format("{0}@xueda.com", oaname);
            //WhereSqlClauseBuilder connectiveSql = new WhereSqlClauseBuilder(LogicOperatorDefine.Or);
            //connectiveSql.AppendItem("CodeName", eduValue);
            //connectiveSql.AppendItem("CodeName", xuedaValue);
            return this.Load(builder => { builder.LogicOperator = LogicOperatorDefine.Or; builder.AppendItem("CodeName", eduValue).AppendItem("CodeName", xuedaValue); }).FirstOrDefault();
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSPermissionCenterConnectionName;
        }
    }
}
