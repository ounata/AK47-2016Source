using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Builder;

namespace PPTS.Data.Common.DataSources
{
    public class UserAndJobDataSource : ObjectDataSourceQueryAdapterBase<UserAndJob, UserAndJobCollection>
    {
        private IOrganization _Root = null;
        private JobTypeDefine _TargetJobType = JobTypeDefine.Unknown;

        public UserAndJobDataSource(IOrganization root, JobTypeDefine targetJobType)
        {
            root.NullCheck("root");

            this._Root = root;
            this._TargetJobType = targetJobType;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = "U.ID AS UserID, U.DisplayName AS UserName, U.CodeName AS UserCodeName, G.ID AS JobID, G.Name AS JobName";
            qc.FromClause = string.Format("[SC].[SchemaRelationObjectsSnapshot_Current] R INNER JOIN {0} G ON G.ID = R.ObjectID INNER JOIN SC.UserAndContainerSnapshot_Current UC ON G.ID = UC.ContainerID INNER JOIN SC.SchemaUserSnapshot_Current U ON U.ID = UC.UserID",
                GetTableNamae(this._TargetJobType));

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("R.FullPath", this._Root.FullPath + "\\%", "LIKE");

            qc.WhereClause = builder.ToSqlString(TSqlBuilder.Instance);
            qc.OrderByClause = "U.DisplayName";
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSPermissionCenterConnectionName;
        }

        private static string GetTableNamae(JobTypeDefine targetType)
        {
            EnumItemDescriptionAttribute attr = EnumItemDescriptionAttribute.GetAttribute(targetType);

            string tableName = string.Empty;

            if (attr != null)
                tableName = attr.Category;

            tableName.IsNotEmpty().FalseThrow("不能根据部门类型{0}找到对应的表名", targetType);

            return tableName;
        }
    }
}
