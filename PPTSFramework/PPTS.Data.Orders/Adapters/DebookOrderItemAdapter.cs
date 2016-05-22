using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data;


namespace PPTS.Data.Orders.Adapters
{
    /// <summary>
    /// 退订 相关的Adapter的基类
    /// </summary>
    public class DebookOrderItemAdapter : DebookOrderAdapterBase<DebookOrderItem, DebookOrderItemCollection>
    {
        public static readonly DebookOrderItemAdapter Instance = new DebookOrderItemAdapter();
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSOrderConnectionName;
        }


        public void AddOrderItemInContext(string debookId, DebookOrderItem data)
        {
            data.DebookID = debookId;
            UpdateInContext(data);
        }

    }
}
