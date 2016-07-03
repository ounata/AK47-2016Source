using System;
using System.Linq;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.Students;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("添加学员家长")]
    public class AddStudentParentExecutor : PPTSEditCustomerExecutorBase<AddStudentParentModel>
    {
        public AddStudentParentExecutor(AddStudentParentModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.Customer.NullCheck("Customer");
            model.Parent.NullCheck("Parent");
        }

        protected override void Validate()
        {
            base.Validate();
            CustomerParentRelationCollection relations = CustomerParentRelationAdapter.Instance.Load(this.Model.Customer.CustomerID);
            if (relations != null && relations.Count > 0)
            {
                if (relations.Where(r => r.ParentID == this.Model.Parent.ParentID).Count() > 0)
                {
                    throw new Exception("已与该家长存在亲属关系");
                }
            }
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            // 亲属关系
            CustomerParentRelation relation = this.Model.ToRelation().FillCreator();
            CustomerParentRelationCollection relations = CustomerParentRelationAdapter.Instance.Load(this.Model.Customer.CustomerID);
            if (relations.Count > 0)
            {
                if (relations.Where(r => r.IsPrimary).Count() > 0)
                {
                    relation.IsPrimary = false;
                }
            }
            CustomerParentRelationAdapter.Instance.UpdateInContext(relation);
            // 全文检索
            CustomerFulltextInfo customerFullText = CustomerFulltextInfo.Create(this.Model.Customer.CustomerID, CustomerFulltextInfo.PotentialCustomersType, this.Model.Customer.Status);
            customerFullText.CustomerSearchContent = this.Model.Customer.ToSearchContent();
            customerFullText.ParentSearchContent = this.Model.Parent.ToSearchContent();
            CustomerFulltextInfoAdapter.Instance.UpdateInContext(customerFullText);

            CustomerFulltextInfo parentFullText = CustomerFulltextInfo.Create(this.Model.Parent.ParentID, CustomerFulltextInfo.ParentsType);
            parentFullText.CustomerSearchContent = this.Model.Customer.ToSearchContent();
            parentFullText.ParentSearchContent = this.Model.Parent.ToSearchContent();
            CustomerFulltextInfoAdapter.Instance.UpdateInContext(parentFullText);
        }

        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);

            context.Logs.ForEach(log => log.ResourceID = this.Model.Customer.CustomerID);
        }
    }
}