using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerStaffRelationAdapter : VersionedCustomerAdapterBase<CustomerStaffRelation, CustomerStaffRelationCollection>
    {
        public static CustomerStaffRelationAdapter Instance = new CustomerStaffRelationAdapter();

        private CustomerStaffRelationAdapter()
        {
        }

        /// <summary>
        /// 根据客户ID信息进行加载
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public CustomerStaffRelationCollection LoadByCustomerID(string customerID)
        {
            customerID.CheckStringIsNullOrEmpty("customerID");

            return this.Load(builder => builder.AppendItem("CustomerID", customerID), DateTime.MinValue);
        }

        /// <summary>
        /// 根据客户ID信息进行加载
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public CustomerStaffRelation LoadByCustomerID(string customerID, CustomerRelationType relationType)
        {
            customerID.CheckStringIsNullOrEmpty("customerID");

            return this.Load(builder => builder.AppendItem("CustomerID", customerID).AppendItem("RelationType", (int)relationType), DateTime.MinValue).FirstOrDefault();
        }

        /// <summary>
        /// 在上下文中根据客户ID信息进行加载
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="action"></param>
        public void LoadByCustomerIDInContext(string customerID, Action<CustomerStaffRelationCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("CustomerID", customerID)), action, DateTime.MinValue, this.GetMappingInfo().QueryTableName);
        }
        public CustomerStaffRelation GetCustomerStaffRelation(string customerID, string relationType)
        {
            customerID.CheckStringIsNullOrEmpty("customerID");
            relationType.CheckStringIsNullOrEmpty("relationType");

            return this.Load(builder => builder.AppendItem("CustomerID", customerID).AppendItem("RelationType", relationType), DateTime.MinValue).FirstOrDefault();

            //WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            //builder.AppendItem("customerId", customerId);
            //builder.AppendItem("relationType", relationType);
            //string sql = string.Format("SELECT TOP 1 * FROM {0} WHERE {1} ORDER BY CreateTime DESC",
            //    GetTableName(), builder.ToSqlString(TSqlBuilder.Instance));
            //return this.QueryData(GetMappingInfo(), sql).SingleOrDefault();
            //this.GetQueryMappingInfo().GetQueryTableName();
        }
    }
}
