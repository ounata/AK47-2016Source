﻿using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    /// <summary>
    /// 客户信息相关的Adapter的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public abstract class AccountAdapterBase<T, TCollection> : CustomerAdapterBase<T, TCollection> where TCollection : IList<T>, new()
    {
    }
}
