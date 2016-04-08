using MCS.Library.Data.Adapters;
using MCS.Library.Data.Test.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Test.Adapters
{
    public class TestObjectAdapter : UpdatableAndLoadableAdapterBase<TestObject, TestObjectCollection>
    {
        public static readonly TestObjectAdapter Instance = new TestObjectAdapter();

        private TestObjectAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return Common.ConnectionName;
        }
    }
}
