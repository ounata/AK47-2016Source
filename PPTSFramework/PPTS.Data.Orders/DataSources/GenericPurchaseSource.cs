using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;


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
    }
}
