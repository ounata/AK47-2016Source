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
    public class InLoadingCondition : LoadingActionConditionBase<InSqlClauseBuilder>
    {
        /// <summary>
        /// 
        /// </summary>
        public InLoadingCondition() : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inAction"></param>
        /// <param name="dataField"></param>
        public InLoadingCondition(Action<InSqlClauseBuilder> inAction, string dataField = null)
            : base(inAction)
        {
            this.DataField = dataField;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inAction"></param>
        /// <param name="orderByAction"></param>
        /// <param name="dataField"></param>
        public InLoadingCondition(Action<InSqlClauseBuilder> inAction, Action<OrderBySqlClauseBuilder> orderByAction, string dataField = null)
            : base(inAction, orderByAction)
        {
            this.DataField = dataField;
        }

        /// <summary>
        /// In子句对应到字段名
        /// </summary>
        public string DataField
        {
            get;
            set;
        }
    }
}
