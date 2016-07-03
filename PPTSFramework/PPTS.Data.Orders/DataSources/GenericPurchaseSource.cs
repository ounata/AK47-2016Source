using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Passport;
using PPTS.Data.Orders.Entities;

namespace PPTS.Data.Orders.DataSources
{
    public class GenericPurchaseSource<T, TCollection> : ObjectDataSourceQueryAdapterBase<T, TCollection>
     where T : new()
     where TCollection : EditableDataObjectCollectionBase<T>, new()
    {
        public static readonly GenericPurchaseSource<T, TCollection> Instance = new GenericPurchaseSource<T, TCollection>();

        private GenericPurchaseSource()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSOrderConnectionName;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {

            #region 数据权限加工

            if (RolesDefineConfig.GetConfig().Enabled)
            {
                var queryTable = MCS.Library.Data.Mapping.ORMapping.GetMappingInfo<T>().GetQueryTableName();

                qc.WhereClause = PPTS.Data.Common.Authorization.ScopeAuthorization<T>
                    .GetInstance(ConnectionDefine.PPTSOrderConnectionName)
                    .ReadAuthExistsBuilder(queryTable, qc.WhereClause).ToSqlString(TSqlBuilder.Instance);
            }

            #endregion


            base.OnBuildQueryCondition(qc);
        }

    }
}
