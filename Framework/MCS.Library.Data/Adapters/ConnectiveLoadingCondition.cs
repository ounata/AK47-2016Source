using MCS.Library.Data.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data.Adapters
{
    /// <summary>
    /// 可连接子句的Builder
    /// </summary>
    public class ConnectiveLoadingCondition
    {
        /// <summary>
        /// 
        /// </summary>
        public ConnectiveLoadingCondition()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectiveBuilder"></param>
        public ConnectiveLoadingCondition(IConnectiveSqlClause connectiveBuilder)
        {
            this.ConnectiveBuilder = connectiveBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectiveBuilder"></param>
        /// <param name="orderByBuilder"></param>
        public ConnectiveLoadingCondition(IConnectiveSqlClause connectiveBuilder, OrderBySqlClauseBuilder orderByBuilder)
        {
            this.ConnectiveBuilder = connectiveBuilder;
            this.OrderByBuilder = orderByBuilder;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConnectiveSqlClause ConnectiveBuilder
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public OrderBySqlClauseBuilder OrderByBuilder
        {
            get;
            set;
        }
    }
}
