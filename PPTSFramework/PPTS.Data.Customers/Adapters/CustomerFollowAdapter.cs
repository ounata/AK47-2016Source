using System.Linq;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.Builder;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerFollowAdapter : CustomerAdapterBase<CustomerFollow, CustomerFollowCollection>
    {
        public static readonly CustomerFollowAdapter Instance = new CustomerFollowAdapter();

        private CustomerFollowAdapter()
        {
        }

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="followid"></param>
        /// <returns></returns>
        public CustomerFollow Load(string followid)
        {
            return this.Load(builder => builder.AppendItem("FollowID", followid)).SingleOrDefault();
        }

        /// <summary>
        /// 返回最近的一条跟进记录
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public CustomerFollow LoadPreviousFollow(string customerID)
        {
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("CustomerID", customerID);

            string sql = string.Format("SELECT TOP 1 * FROM {0} WHERE {1} ORDER BY FollowTime DESC",
                this.GetTableName(),
                builder.ToSqlString(TSqlBuilder.Instance));

            return this.QueryData(sql).FirstOrDefault();
        }
    }
}