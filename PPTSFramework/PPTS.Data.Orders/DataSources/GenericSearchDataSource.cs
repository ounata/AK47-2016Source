using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;

namespace PPTS.Data.Orders.DataSources
{
    public class GenericSearchDataSource<T, TCollection> : ObjectDataSourceQueryAdapterBase<T, TCollection>
      where T : new()
      where TCollection : EditableDataObjectCollectionBase<T>, new()
    {
        public static readonly GenericSearchDataSource<T, TCollection> Instance = new GenericSearchDataSource<T, TCollection>();

        private GenericSearchDataSource()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSSearchConnectionName;
        }
    }


}
