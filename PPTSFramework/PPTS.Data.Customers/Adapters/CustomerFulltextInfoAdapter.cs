using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;

namespace PPTS.Data.Customers.Adapters
{
    public class CustomerFulltextInfoAdapter : CustomerAdapterBase<CustomerFulltextInfo, CustomerFulltextInfoCollection>
    {
        public static readonly CustomerFulltextInfoAdapter Instance = new CustomerFulltextInfoAdapter();

        private CustomerFulltextInfoAdapter()
        {
        }

        public CustomerFulltextInfo LoadByOwnerID(string ownerID)
        {
            ownerID.NullCheck("ownerID");

            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(ownerID), "OwnerID")).SingleOrDefault();
        }

        /// <summary>
        /// 仅仅更新客户的搜索信息
        /// </summary>
        /// <param name="cfi"></param>
        public void UpdateCustomerSearchContentInContext(CustomerFulltextInfo cfi)
        {
            this.UpdateInContext(cfi, "ParentSearchContent");
            //this.DoUpdateInContext(cfi, (data, sqlContext, context) =>
            //{
            //    string sql = ORMapping.GetUpdateSql(cfi, TSqlBuilder.Instance, "ParentSearchContent");

            //    sqlContext.AppendSqlInContext(TSqlBuilder.Instance, sql);
            //},
            //(data, sqlContext, context) =>
            //{
            //    string sql = ORMapping.GetInsertSql(cfi, TSqlBuilder.Instance, "ParentSearchContent");

            //    sqlContext.AppendSqlInContext(TSqlBuilder.Instance, sql);
            //});
        }

        /// <summary>
        /// 仅仅更新家长的搜索信息
        /// </summary>
        /// <param name="cfi"></param>
        public void UpdateParentSearchContentInContext(CustomerFulltextInfo cfi)
        {
            this.UpdateInContext(cfi, "CustomerSearchContent");
            //this.DoUpdateInContext(cfi, (data, sqlContext, context) =>
            //{
            //    string sql = ORMapping.GetUpdateSql(cfi, TSqlBuilder.Instance, "CustomerSearchContent");

            //    sqlContext.AppendSqlInContext(TSqlBuilder.Instance, sql);
            //},
            //(data, sqlContext, context) =>
            //{
            //    string sql = ORMapping.GetInsertSql(cfi, TSqlBuilder.Instance, "CustomerSearchContent");

            //    sqlContext.AppendSqlInContext(TSqlBuilder.Instance, sql);
            //});
        }
    }
}
