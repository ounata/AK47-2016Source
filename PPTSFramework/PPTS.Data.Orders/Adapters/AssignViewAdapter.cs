using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Adapters
{
    public class AssignViewAdapter : OrderAdapterBase<AssignView, AssignViewCollection>
    {
        public static readonly AssignViewAdapter Instance = new AssignViewAdapter();

        private AssignViewAdapter()
        {
        }


    }
}
