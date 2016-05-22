using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.DataSources
{
    public class GenericCommonDataSource<T, TCollection> : ObjectDataSourceQueryAdapterBase<T, TCollection>
        where T : new()
        where TCollection : EditableDataObjectCollectionBase<T>, new()
    {
        public static readonly GenericCommonDataSource<T, TCollection> Instance = new GenericCommonDataSource<T, TCollection>();

        private GenericCommonDataSource()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSMetaDataConnectionName;
        }
    }
}
