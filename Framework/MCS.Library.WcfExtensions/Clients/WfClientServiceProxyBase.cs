﻿using MCS.Library.Core;
using MCS.Web.Library.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.WcfExtensions
{
    /// <summary>
    /// 客户端调用WCF的代理类的基类
    /// </summary>
    /// <typeparam name="TServiceContract"></typeparam>
    public abstract class WfClientServiceProxyBase<TServiceContract>
    {
        /// <summary>
        /// 得到Service的Channel
        /// </summary>
        /// <returns></returns>
        protected abstract WfClientChannelFactory<TServiceContract> GetService();

        /// <summary>
        /// 对服务进行一次SingleCall
        /// </summary>
        /// <param name="action"></param>
        public void SingleCall(Action<TServiceContract> action)
        {
            ServiceProxy.SingleCall<TServiceContract>(this.GetService(), action);
        }

        public TResult SingleCall<TResult>(Func<TServiceContract, TResult> func)
        {
            return ServiceProxy.SingleCall<TServiceContract, TResult>(this.GetService(), func);
        }

        public TResult SingleCallWithScalar<TResult>(Func<TServiceContract, string> func, TResult defaultValue = default(TResult))
        {
            string serializedData = this.SingleCall(func);

            Dictionary<string, object> data = JSONSerializerExecute.Deserialize<Dictionary<string, object>>(serializedData);

            return data.GetValue("d", defaultValue);
        }
    }
}
