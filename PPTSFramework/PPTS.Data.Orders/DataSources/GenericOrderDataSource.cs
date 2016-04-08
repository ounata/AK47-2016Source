using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;

namespace PPTS.Data.Orders.DataSources
{
    public class GenericOrderDataSource<T, TCollection> : ObjectDataSourceQueryAdapterBase<T, TCollection>
     where T : new()
     where TCollection : EditableDataObjectCollectionBase<T>, new()
    {
        public static readonly GenericOrderDataSource<T, TCollection> Instance = new GenericOrderDataSource<T, TCollection>();

        private GenericOrderDataSource()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSOrderConnectionName;
        }
    }

}
