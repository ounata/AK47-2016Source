using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Mapping;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Customers.Models;
using PPTS.Contracts.Customers.Operations;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PPTS.Services.Customers.Services
{
    public class CustomerQueryService : ICustomerQueryService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public Customer QueryCustomerByID(string customerID)
        {
            return new Customer()
            {
                CustomerID = UuidHelper.NewUuidString(),
                CustomerName = "何明宇"
            };
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public CustomerTeacherRelationQueryResult QueryCustomerTeacherRelationByCustomerID(CustomerTearcherRelationQueryModel QueryModel)
        {
            QueryModel.NullCheck("QueryModel");
            QueryModel.CustomerID.NullCheck("CustomerID");
            CustomerTeacherRelationQueryResult queryresult = new CustomerTeacherRelationQueryResult();
            #region 客户部分信息查询条件存在问题，屏蔽执行方法
            /*
            ConnectiveLoadingCondition condition = new ConnectiveLoadingCondition(ConditionMapping.GetConnectiveClauseBuilder(QueryModel));
            CustomerTeacherRelationAdapter.Instance.LoadByBuilderInContext(condition
                , action => queryresult.CustomerTeacherRelationCollection = action.ToList(), DateTime.MinValue);
            if (QueryModel.IsContainCustomerInfo)
            {
                CustomerAdapter.Instance.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("CustomerID", QueryModel.CustomerID)), action => queryresult.Customer = action.SingleOrDefault());
            }*/
            #endregion 客户部分信息查询条件存在问题，屏蔽执行方法

            using (DbContext context = CustomerTeacherRelationAdapter.Instance.GetDbContext())
            {
                context.ExecuteDataSetSqlInContext();
            }

            return queryresult;
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public CustomerCollectionQueryResult QueryCustomerCollectionByCustomerIDs(string[] CustomerIDs)
        {
            CustomerIDs.NullCheck("CustomerIDs");
            (CustomerIDs.Length > 0).FalseThrow("CustomerIDs length is 0!");
            CustomerCollectionQueryResult queryresult = new CustomerCollectionQueryResult();
            queryresult.CustomerCollection = new List<CustomerQueryResult>();

            #region 多次查询部分
            //foreach (string customerid in CustomerIDs)
            //{
            //    WhereLoadingCondition where_condition = new WhereLoadingCondition(builder => builder.AppendItem("CustomerID", customerid));
            //    CustomerQueryResult queryresult_child = new CustomerQueryResult();
            //    CustomerAdapter.Instance.LoadInContext(where_condition, action => queryresult_child.Customer = action.SingleOrDefault(), tableName: "Customer" + customerid);
            //    CustomerStaffRelationAdapter.Instance.LoadInContext(where_condition, action => queryresult_child.CustomerStaffRelationCollection = action.ToList(), DateTime.MinValue, tableName: "CustomerStaff" + customerid);
            //    queryresult.CustomerCollection.Add(queryresult_child);
            //}
            #endregion 多次查询部分

            CustomerCollection customercollection = null;
            CustomerStaffRelationCollection customerstaffrelationcollection = null;
            #region 客户部分信息查询条件存在问题，屏蔽执行方法
            
            InLoadingCondition in_condition = new InLoadingCondition(builder => builder.AppendItem<string>(CustomerIDs).DataField = "CustomerID");
            CustomerAdapter.Instance.LoadByInBuilderInContext(in_condition, action => customercollection = action,DateTime.MinValue);
            CustomerStaffRelationAdapter.Instance.LoadByInBuilderInContext(in_condition, action => customerstaffrelationcollection= action, DateTime.MinValue);
           

            using (DbContext context = CustomerAdapter.Instance.GetDbContext())
            {
                context.ExecuteDataSetSqlInContext();
            }

            foreach (string customerid in CustomerIDs)
            {
                CustomerQueryResult queryresult_child = new CustomerQueryResult();
                queryresult_child.Customer = customercollection.SingleOrDefault(result => result.CustomerID == customerid);
                queryresult_child.CustomerStaffRelationCollection = customerstaffrelationcollection.Where(result => result.CustomerID == customerid).ToList();
                queryresult.CustomerCollection.Add(queryresult_child);
            }
            
            #endregion 客户部分信息查询条件存在问题，屏蔽执行方法
            return queryresult;
        }
    }
}
