using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class ParentAdapter : GenericParentAdapter<Parent, ParentCollection>
    {
        public new static readonly ParentAdapter Instance = new ParentAdapter();

        private ParentAdapter()
        {
        }
    }
}