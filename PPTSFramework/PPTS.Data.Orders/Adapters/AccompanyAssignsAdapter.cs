using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Adapters
{
   public class AccompanyAssignsAdapter : OrderAdapterBase<AccompanyAssign, AccompanyAssignCollection>
    {
        public static readonly AccompanyAssignsAdapter Instance = new AccompanyAssignsAdapter();
        private AccompanyAssignsAdapter()
        { }

    }
}
