using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.DataSources
{

    public class GenericMetaDataSource<T, TCollection> : ObjectDataSourceQueryAdapterBase<T, TCollection>
  where T : new()
  where TCollection : EditableDataObjectCollectionBase<T>, new()
    {
        public static readonly GenericMetaDataSource<T, TCollection> Instance = new GenericMetaDataSource<T, TCollection>();

        private GenericMetaDataSource()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSMetaConnectionName;
        }
    }
}
