using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;

namespace PPTS.Data.Customers.DataSources
{
    /// <summary>
    /// 客户子系统查询DataSource的通用类，可以直接使用Instance实例。已经重载了获取数据库连接名称的方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public class GenericSearchDataSource<T, TCollection> : ObjectDataSourceQueryAdapterBase<T, TCollection>
        where T : new()
        where TCollection : EditableDataObjectCollectionBase<T>, new()
    {
        public static readonly GenericSearchDataSource<T, TCollection> Instance = new GenericSearchDataSource<T, TCollection>();

        protected GenericSearchDataSource()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSSearchConnectionName;
        }


    }
}
