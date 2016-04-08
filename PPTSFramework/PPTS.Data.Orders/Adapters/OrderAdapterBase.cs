using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;

namespace PPTS.Data.Orders.Adapters
{
    /// <summary>
    /// 订单相关的Adapter的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public abstract class OrderAdapterBase<T, TCollection> : UpdatableAndLoadableAdapterBase<T, TCollection> where TCollection : IList<T>, new()
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSOrderConnectionName;
        }
    }
}
