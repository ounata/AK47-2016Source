using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Mapping;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Customers.Models;
using PPTS.Contracts.Customers.Operations;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
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
            return CustomerAdapter.Instance.Load(customerID);
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public PrimaryParentQueryResult QueryPrimaryParentByCustomerID(string customerID)
        {
            return new PrimaryParentQueryResult() { Parent = ParentAdapter.Instance.LoadPrimaryParentInContext(customerID) };
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public CustomerCollectionQueryResult QueryCustomerCollectionByCustomerIDs(params string[] customerIDs)
        {
            customerIDs.NullCheck("CustomerIDs");
            (customerIDs.Length > 0).FalseThrow("CustomerIDs length is 0!");
            CustomerCollectionQueryResult queryResult = new CustomerCollectionQueryResult();
            queryResult.CustomerCollection = new List<CustomerQueryResult>();

            #region 多次查询部分
            //foreach (string customerID in customerIDs)
            //{
            //    WhereLoadingCondition whereCondition = new WhereLoadingCondition(builder => builder.AppendItem("CustomerID", customerID));
            //    CustomerQueryResult queryResultChild = new CustomerQueryResult();
            //    CustomerAdapter.Instance.LoadInContext(whereCondition, action => queryresult_child.Customer = action.SingleOrDefault(), tableName: "Customer" + customerID);
            //    CustomerStaffRelationAdapter.Instance.LoadInContext(whereCondition, action => queryresult_child.CustomerStaffRelationCollection = action.ToList(), DateTime.MinValue, tableName: "CustomerStaff" + customerID);
            //    queryResult.CustomerCollection.Add(queryResultChild);
            //}
            #endregion 多次查询部分

            CustomerCollection customerCollection = null;
            CustomerStaffRelationCollection customerStaffRelationCollection = null;
            #region 客户部分信息查询条件存在问题，屏蔽执行方法

            InLoadingCondition in_condition = new InLoadingCondition(builder => builder.AppendItem<string>(customerIDs).DataField = "CustomerID");
            CustomerAdapter.Instance.LoadByInBuilderInContext(in_condition, action => customerCollection = action, DateTime.MinValue);
            CustomerStaffRelationAdapter.Instance.LoadByInBuilderInContext(in_condition, action => customerStaffRelationCollection = action, DateTime.MinValue);


            using (DbContext context = CustomerAdapter.Instance.GetDbContext())
            {
                context.ExecuteDataSetSqlInContext();
            }

            foreach (string customerID in customerIDs)
            {
                CustomerQueryResult queryresult_child = new CustomerQueryResult();
                queryresult_child.Customer = customerCollection.SingleOrDefault(result => result.CustomerID == customerID);
                queryresult_child.CustomerStaffRelationCollection = customerStaffRelationCollection.Where(result => result.CustomerID == customerID).ToList();
                queryResult.CustomerCollection.Add(queryresult_child);
            }

            #endregion 客户部分信息查询条件存在问题，屏蔽执行方法
            return queryResult;
        }


        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public TeacherRelationByCustomerQueryResult QueryCustomerTeacherRelationByCustomerID(string customerID)
        {
            customerID.NullCheck("customerID");
            TeacherRelationByCustomerQueryResult result = new TeacherRelationByCustomerQueryResult();
            CustomerTeacherRelationCollection relations = CustomerTeacherRelationAdapter.Instance.Load(builder => builder.AppendItem("CustomerID", customerID), DateTime.MinValue);
            string[] customerIDs = new string[] { customerID };
            string[] teacherJobIDs = relations.Select<CustomerTeacherRelation, string>(model => model.TeacherJobID).ToArray();
            Dictionary<string, object> relationModels = QueryCustomerTeacherRelationModel(customerIDs, teacherJobIDs);
            List<Customer> customerCollection = relationModels["CustomerCollection"] as List<Customer>;
            customerCollection.IsNull(() => customerCollection = new List<Customer>());
            List<TeacherJobModel> teacherJobCollection = relationModels["TeacherJobCollection"] as List<TeacherJobModel>;
            teacherJobCollection.IsNull(() => teacherJobCollection = new List<TeacherJobModel>());
            result.Customer = customerCollection.FirstOrDefault();
            result.TeacherJobCollection = teacherJobCollection;
            return result;
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public CustomerRelationByTeacherQueryResult QueryCustomerTeacherRelationByTeacherJobID(string teacherJobID)
        {
            teacherJobID.NullCheck("teacherJobID");
            CustomerRelationByTeacherQueryResult result = new CustomerRelationByTeacherQueryResult();
            CustomerTeacherRelationCollection relations = CustomerTeacherRelationAdapter.Instance.Load(builder => builder.AppendItem("TeacherJobID", teacherJobID), DateTime.MinValue);
            string[] customerIDs = relations.Select<CustomerTeacherRelation, string>(model => model.CustomerID).ToArray();
            string[] teacherJobIDs = new string[] { teacherJobID };
            Dictionary<string, object> relationModels = QueryCustomerTeacherRelationModel(customerIDs, teacherJobIDs);
            List<Customer> customerCollection = relationModels["CustomerCollection"] as List<Customer>;
            customerCollection.IsNull(() => customerCollection = new List<Customer>());
            List<TeacherJobModel> teacherJobCollection = relationModels["TeacherJobCollection"] as List<TeacherJobModel>;
            teacherJobCollection.IsNull(() => teacherJobCollection = new List<TeacherJobModel>());
            result.CustomerCollection = customerCollection;
            result.TeacherJob = teacherJobCollection.FirstOrDefault();
            return result;
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]

        public CustomerExpenseCollectionQueryResult QueryCustomerExpenseByCustomerID(string customerID)
        {
            customerID.NullCheck("customerID");
            CustomerExpenseCollectionQueryResult result = new CustomerExpenseCollectionQueryResult();
            result.CustomerExpenseRelationCollection = CustomerExpenseRelationAdapter.Instance.Load(builder => builder.AppendItem("CustomerID", customerID)).ToList();
            return result;
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]

        public CustomerExpenseCollectionQueryResult QueryCustomerExpenseByCustomerIDs(params string[] customerIDs)
        {
            customerIDs.NullCheck("customerIDs");
            CustomerExpenseCollectionQueryResult result = new CustomerExpenseCollectionQueryResult();
            result.CustomerExpenseRelationCollection = CustomerExpenseRelationAdapter.Instance.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(customerIDs).DataField = "CustomerID")).ToList();
            return result;
        }

        /// <summary>
        /// 封装获取教师关系信息方法
        /// </summary>
        /// <param name="customerIDs">学员ID集合</param>
        /// <param name="teacherJobIDs">教师岗位ID集合</param>
        /// <param name="customerCollection">学员集合信息</param>
        /// <param name="teacherJobCollection">教师岗位集合模型</param>
        private Dictionary<string, object> QueryCustomerTeacherRelationModel(string[] customerIDs, string[] teacherJobIDs)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            List<Customer> customerCollection = new List<Customer>();
            List<TeacherJobModel> teacherJobCollection = new List<TeacherJobModel>();
            if (customerIDs != null && customerIDs.Length > 0)
            {
                customerCollection = CustomerAdapter.Instance.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(customerIDs), "CustomerID"), DateTime.MinValue).ToList();
            }
            if (teacherJobIDs != null && teacherJobIDs.Length > 0)
            {
                TeacherJobViewCollection jobCollection = TeacherJobViewAdapter.Instance.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(teacherJobIDs), "JobID"));
                string[] teacherIDs = jobCollection.Select<TeacherJobView, string>(model => model.TeacherID).Distinct().ToArray();
                TeacherTeachingCollection teacherTeachings = TeacherTeachingAdapter.Instance.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(teacherIDs), "TeacherID"));
                jobCollection.ForEach((action) => { TeacherJobModel model = new TeacherJobModel() { TeacherJob = action, TeacherTeachingCollection = teacherTeachings.Where(where => where.TeacherID == action.TeacherID).ToList() }; teacherJobCollection.Add(model); });
            }
            result.Add("CustomerCollection", customerCollection);
            result.Add("TeacherJobCollection", teacherJobCollection);
            return result;
        }


        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public CustomerExpenseRelation GetCustomerExpenseByOrderId(string orderID)
        {
            return Data.Customers.Adapters.CustomerExpenseRelationAdapter.Instance.Load(orderID);
        }
    }
}
