using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;

namespace MCS.Web.MVC.Library.DataSources
{
    public class GenericUserTaskDataSource<T, TCollection> : ObjectDataSourceQueryAdapterBase<T, TCollection>
        where T : new()
        where TCollection : EditableDataObjectCollectionBase<T>, new()
    {
        public static readonly GenericUserTaskDataSource<T, TCollection> Instance = new GenericUserTaskDataSource<T, TCollection>();

        protected GenericUserTaskDataSource()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.SearchConnectionName;
        }
    }
}