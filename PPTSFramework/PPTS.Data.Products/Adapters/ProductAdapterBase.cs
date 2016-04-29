using MCS.Library.Data.Adapters;
using System.Collections.Generic;

namespace PPTS.Data.Products.Adapters
{
    /// <summary>
    /// 产品信息相关的Adapter的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public abstract class ProductAdapterBase<T, TCollection> : UpdatableAndLoadableAdapterBase<T, TCollection> 
        where TCollection : IList<T>, new()
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSProductConnectionName;
        }
    }
}
