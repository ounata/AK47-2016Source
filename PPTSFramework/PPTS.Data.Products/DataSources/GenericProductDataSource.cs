
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;


namespace PPTS.Data.Products.DataSources
{
    /// <summary>
    /// 查询DataSource的通用类，可以直接使用Instance实例。已经重载了获取数据库连接名称的方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public class GenericProductDataSource<T, TCollection> : ObjectDataSourceQueryAdapterBase<T, TCollection>
        where T : new()
        where TCollection : EditableDataObjectCollectionBase<T>, new()
    {
        public static readonly GenericProductDataSource<T, TCollection> Instance = new GenericProductDataSource<T, TCollection>();

        private GenericProductDataSource()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSProductConnectionName;
        }
    }
}
