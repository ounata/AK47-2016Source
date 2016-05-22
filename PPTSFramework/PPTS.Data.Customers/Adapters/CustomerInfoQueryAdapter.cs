using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    /// <summary>
    /// 客户信息的查询类。这个查询会跨越学员和潜客。先从学员中查询，然后从潜客中查询。学员信息优先。
    /// </summary>
    public class CustomerInfoQueryAdapter
    {
        public static readonly CustomerInfoQueryAdapter Instance = new CustomerInfoQueryAdapter();

        private CustomerInfoQueryAdapter()
        {
        }

        /// <summary>
        /// 根据客户的ID在潜客和客户中查询客户信息。以客户信息优先
        /// </summary>
        /// <param name="customerIDs"></param>
        /// <returns></returns>
        public BasicCustomerInfoCollection LoadCustomersBasicInfoByIDs(params string[] customerIDs)
        {
            customerIDs.NullCheck("customerIDs");

            ORMappingItemCollection mapping = ORMapping.GetMappingInfo(typeof(BasicCustomerInfo));

            string select = string.Join(",", ORMapping.GetSelectFieldsName(mapping));

            InSqlClauseBuilder builder = new InSqlClauseBuilder("CustomerID");

            builder.AppendItem(customerIDs);

            BasicCustomerInfoCollection result = null;

            if (builder.IsEmpty == false)
            {
                BasicCustomerInfoCollection dataInCustomers = this.Query<BasicCustomerInfo, BasicCustomerInfoCollection>(
                    mapping,
                    select,
                    ORMapping.GetMappingInfo<Customer>().GetQueryTableName(),
                    builder);

                BasicCustomerInfoCollection dataInPotentialCustomers = this.Query<BasicCustomerInfo, BasicCustomerInfoCollection>(
                    mapping,
                    select,
                    ORMapping.GetMappingInfo<PotentialCustomer>().GetQueryTableName(),
                    builder);

                dataInPotentialCustomers.ForEach(c => dataInCustomers.AddNotExistsItem(c));
                result = dataInCustomers;
            }
            else
                result = new BasicCustomerInfoCollection();

            return result;
        }

        /// <summary>
        /// 根据客户的ID在潜客和客户中查询客户信息。以客户信息优先。通过上下文查询
        /// </summary>
        /// <param name="action"></param>
        /// <param name="customerIDs"></param>
        public void LoadCustomersBasicInfoByIDsInContext(Action<BasicCustomerInfoCollection> action, params string[] customerIDs)
        {
            customerIDs.NullCheck("customerIDs");

            ORMappingItemCollection mapping = ORMapping.GetMappingInfo(typeof(BasicCustomerInfo));

            string select = string.Join(",", ORMapping.GetSelectFieldsName(mapping));

            InSqlClauseBuilder builder = new InSqlClauseBuilder("CustomerID");

            builder.AppendItem(customerIDs);

            BasicCustomerInfoCollection dataInCustomers = null;

            if (builder.IsEmpty == false)
            {
                this.QueryInContext<BasicCustomerInfo, BasicCustomerInfoCollection>(
                    mapping,
                    select,
                    ORMapping.GetMappingInfo<Customer>().GetQueryTableName(),
                    builder,
                    (customers) => dataInCustomers = customers);

                this.QueryInContext<BasicCustomerInfo, BasicCustomerInfoCollection>(
                    mapping,
                    select,
                    ORMapping.GetMappingInfo<PotentialCustomer>().GetQueryTableName(),
                    builder,
                    (customers) =>
                    {
                        customers.ForEach(c => dataInCustomers.AddNotExistsItem(c));
                        action(dataInCustomers);
                    });
            }
        }

        /// <summary>
        /// 查询客户、主要联系人和电话
        /// </summary>
        /// <param name="customerIDs"></param>
        /// <returns></returns>
        public CustomerParentPhoneCollection LoadCustomerParentPhoneByIDs(params string[] customerIDs)
        {
            ORMappingItemCollection mapping = ORMapping.GetMappingInfo(typeof(CustomerParentPhone));

            InSqlClauseBuilder builder = new InSqlClauseBuilder("CustomerID");

            builder.AppendItem(customerIDs);

            CustomerParentPhoneCollection result = null;

            if (builder.IsEmpty == false)
            {
                CustomerParentPhoneCollection dataInCustomers = this.Query<CustomerParentPhone, CustomerParentPhoneCollection>(
                    mapping,
                    "*",
                    "CM.CustomerParentPhone_Current",
                    builder);

                CustomerParentPhoneCollection dataInPotentialCustomers = this.Query<CustomerParentPhone, CustomerParentPhoneCollection>(
                    mapping,
                    "*",
                    "CM.PotentialCustomerParentPhone_Current",
                    builder);

                dataInPotentialCustomers.ForEach(c => dataInCustomers.AddNotExistsItem(c, (inner) => inner.CustomerID == c.CustomerID));

                result = dataInCustomers;
            }
            else
                result = new CustomerParentPhoneCollection();

            return result;
        }

        /// <summary>
        /// 在上下文中查询客户、主要联系人和电话
        /// </summary>
        /// <param name="action"></param>
        /// <param name="customerIDs"></param>
        public void LoadCustomerParentPhoneByIDsInContext(Action<CustomerParentPhoneCollection> action, params string[] customerIDs)
        {
            ORMappingItemCollection mapping = ORMapping.GetMappingInfo(typeof(CustomerParentPhone));

            InSqlClauseBuilder builder = new InSqlClauseBuilder("CustomerID");

            builder.AppendItem(customerIDs);

            CustomerParentPhoneCollection dataInCustomers = null;

            if (builder.IsEmpty == false)
            {
                this.QueryInContext<CustomerParentPhone, CustomerParentPhoneCollection>(
                    mapping,
                    "*",
                    "CM.CustomerParentPhone_Current",
                    builder,
                    (customers) => dataInCustomers = customers);

                this.QueryInContext<CustomerParentPhone, CustomerParentPhoneCollection>(
                    mapping,
                    "*",
                    "CM.PotentialCustomerParentPhone_Current",
                    builder,
                    (customers) =>
                    {
                        customers.ForEach(c => dataInCustomers.AddNotExistsItem(c, (inner) => inner.CustomerID == c.CustomerID));
                        action(dataInCustomers);
                    });
            }
        }

        /// <summary>
        /// 返回DBContext
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext()
        {
            return DbContext.GetContext(this.GetConnectionName());
        }

        private TCollection Query<T, TCollection>(ORMappingItemCollection mapping, string select, string tableName, IConnectiveSqlClause conditionBuilder)
            where T : new()
            where TCollection : IList<T>, new()
        {
            string sql = string.Format("SELECT {0} FROM {1} WHERE {2}",
                select, tableName, conditionBuilder.ToSqlString(TSqlBuilder.Instance));

            return QueryInContextBuilder<T, TCollection>.Instance.QueryData(mapping, sql, this.GetConnectionName(), (row) => new T());
        }

        private void QueryInContext<T, TCollection>(ORMappingItemCollection mapping, string select, string tableName, IConnectiveSqlClause conditionBuilder, Action<TCollection> action)
            where T : new()
            where TCollection : IList<T>, new()
        {
            string sql = string.Format("SELECT {0} FROM {1} WHERE {2}",
                select, tableName, conditionBuilder.ToSqlString(TSqlBuilder.Instance));

            QueryInContextBuilder<T, TCollection>.Instance.RegisterQueryData(this.GetSqlContext(), tableName, mapping, sql, action, (row) => new T());
        }

        public SqlContextItem GetSqlContext()
        {
            return SqlContext.GetContext(this.GetConnectionName());
        }

        protected virtual string GetConnectionName()
        {
            return ConnectionDefine.PPTSCustomerConnectionName;
        }
    }
}
