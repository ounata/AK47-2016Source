using MCS.Library.Data.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class LoadingActionConditionBase<TBuilder> where TBuilder : IConnectiveSqlClause
    {
        /// <summary>
        /// 
        /// </summary>
        public LoadingActionConditionBase()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builderAction"></param>
        public LoadingActionConditionBase(Action<TBuilder> builderAction)
        {
            this.BuilderAction = builderAction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builderAction"></param>
        /// <param name="orderByAction"></param>
        public LoadingActionConditionBase(Action<TBuilder> builderAction, Action<OrderBySqlClauseBuilder> orderByAction)
        {
            this.BuilderAction = builderAction;
            this.OrderByAction = orderByAction;
        }

        /// <summary>
        /// 
        /// </summary>
        public Action<TBuilder> BuilderAction
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Action<OrderBySqlClauseBuilder> OrderByAction
        {
            get;
            set;
        }
    }
}
