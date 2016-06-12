using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Security.Adapters
{
    public partial class SchemaObjectAdapter
    {
        /// <summary>
        /// 加载指定的成员
        /// </summary>
        /// <param name="containerSchemaType"></param>
        /// <param name="containerID"></param>
        /// <param name="status"></param>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        public SchemaObjectCollection LoadMembers(string containerSchemaType, string containerID, SchemaObjectStatus status, DateTime timePoint)
        {
            containerSchemaType.CheckStringIsNullOrEmpty("schemaType");
            containerID.CheckStringIsNullOrEmpty("containerID");

            WhereSqlClauseBuilder condition = new WhereSqlClauseBuilder();

            condition.AppendItem("O.Status", (int)status);
            condition.AppendItem("M.ContainerSchemaType", containerSchemaType).AppendItem("M.Status", (int)status);
            condition.AppendItem("M.ContainerID", containerID);

            var timeCondition = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder(timePoint, "O.");
            var timeCondition2 = VersionStrategyQuerySqlBuilder.Instance.TimePointToBuilder(timePoint, "M.");

            ConnectiveSqlClauseCollection conditions = new ConnectiveSqlClauseCollection(condition, timeCondition, timeCondition2);

            string sql = string.Format("SELECT O.* FROM SC.SchemaObject O INNER JOIN SC.SchemaMembers M ON O.ID = M.MemberID WHERE {0}",
                conditions.ToSqlString(TSqlBuilder.Instance));

            DataTable table = DbHelper.RunSqlReturnDS(sql, this.GetConnectionName()).Tables[0];

            SchemaObjectCollection result = new SchemaObjectCollection();

            result.LoadFromDataView(table.DefaultView);

            return result;
        }
    }
}
