using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Adapters
{
    public class DebookOrderItemViewAdapter : DebookOrderAdapterBase<DebookOrderItemView, DebookOrderItemViewCollection>
    {
        public static readonly DebookOrderItemViewAdapter Instance = new DebookOrderItemViewAdapter();
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSOrderConnectionName;
        }

        public DebookOrderItemView LoadByDebookId(string debookId)
        {
            return Load(b => b.AppendItem("DebookID", debookId)).FirstOrDefault();
        }
    }
}
