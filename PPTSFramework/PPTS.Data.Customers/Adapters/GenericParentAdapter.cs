using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Adapters;

namespace PPTS.Data.Customers.Adapters
{
    public class GenericParentAdapter<T, TCollection> : VersionedCustomerAdapterBase<T, TCollection>
        where T : Parent
        where TCollection : IList<T>, new()
    {
        public static readonly GenericParentAdapter<T, TCollection> Instance = new GenericParentAdapter<T, TCollection>();

        /// <summary>
        /// 加载操作
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public Parent Load(string parentID)
        {
            return this.Load(builder => builder.AppendItem("ParentID", parentID), DateTime.MinValue).SingleOrDefault();
        }

        /// <summary>
        /// 在上下文中得到某个客户的主要家长
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public void LoadPrimaryParentInContext(string customerID, Action<Parent> action)
        {
            customerID.CheckStringIsNullOrEmpty("customerID");
            action.NullCheck("action");

            this.RegisterQueryData(this.GetQueryMappingInfo().GetQueryTableName(),
                this.GetQueryMappingInfo(),
                this.PrepareLoadPrimaryParentSql(customerID),
                (parents) => action(parents.FirstOrDefault()));
        }

        /// <summary>
        /// 得到某个客户的主要家长
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public T LoadPrimaryParentInContext(string customerID)
        {
            string sql = this.PrepareLoadPrimaryParentSql(customerID);

            return this.QueryData(this.GetQueryMappingInfo(), sql).FirstOrDefault();
        }

        protected override void BeforeInnerUpdateInContext(T data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            if (data.ParentCode.IsNullOrEmpty())
                data.ParentCode = Helper.GetCustomerCode("P");
        }

        protected override void BeforeInnerUpdate(T data, Dictionary<string, object> context)
        {
            if (data.ParentCode.IsNullOrEmpty())
                data.ParentCode = Helper.GetCustomerCode("P");
        }

        private string PrepareLoadPrimaryParentSql(string customerID)
        {
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("CustomerID", customerID);
            builder.AppendItem("IsPrimary", 1);

            string sql = string.Format("SELECT P.* FROM {0} P INNER JOIN {1} R ON P.ParentID = R.ParentID WHERE {2}",
                this.GetQueryMappingInfo().GetQueryTableName(),
                CustomerParentRelationAdapter.Instance.GetQueryMappingInfo().GetQueryTableName(),
                builder.ToSqlString(TSqlBuilder.Instance));

            return sql;
        }

        public void LoadInContext(string parentID, Action<T> action)
        {
            this.LoadByInBuilderInContext(new InLoadingCondition(builder => builder.AppendItem(parentID), "ParentID"),
                collection => action(collection.SingleOrDefault()), DateTime.MinValue);
        }
    }
}
