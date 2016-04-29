using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Orders.Entities;

namespace PPTS.Data.Orders.Adapters
{
    /// <summary>
    /// 购物车 相关的Adapter的基类
    /// </summary>
    public class ShoppingCartAdapter : UpdatableAndLoadableAdapterBase<ShoppingCart, ShoppingCartCollection> 
    {
        public static readonly ShoppingCartAdapter Instance = new ShoppingCartAdapter();
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSOrderConnectionName;
        }
    }
}
