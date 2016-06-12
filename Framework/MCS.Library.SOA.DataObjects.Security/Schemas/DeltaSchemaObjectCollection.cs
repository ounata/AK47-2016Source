using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Security
{
    /// <summary>
    /// 变化的SchemaObject对象
    /// </summary>
    public class DeltaSchemaObjectCollection : DeltaDataCollectionBase<SchemaObjectCollection>
    {
        public override SchemaObjectCollection CreateNewInnerCollecction()
        {
            return new SchemaObjectCollection();
        }

        protected override DeltaDataCollectionBase CreateNewInstance()
        {
            return new DeltaSchemaObjectCollection();
        }
    }
}
