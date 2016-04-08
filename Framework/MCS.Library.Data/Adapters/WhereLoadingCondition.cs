using MCS.Library.Data.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Adapters
{
    /// <summary>
    /// 加载数据时封装的条件（WhereBuilder）
    /// </summary>
    public class WhereLoadingCondition : LoadingActionConditionBase<WhereSqlClauseBuilder>
    {
        /// <summary>
        /// 
        /// </summary>
        public WhereLoadingCondition() : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereAction"></param>
        public WhereLoadingCondition(Action<WhereSqlClauseBuilder> whereAction)
            : base(whereAction)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereAction"></param>
        /// <param name="orderByAction"></param>
        public WhereLoadingCondition(Action<WhereSqlClauseBuilder> whereAction, Action<OrderBySqlClauseBuilder> orderByAction)
            : base(whereAction, orderByAction)
        {
        }
    }
}
