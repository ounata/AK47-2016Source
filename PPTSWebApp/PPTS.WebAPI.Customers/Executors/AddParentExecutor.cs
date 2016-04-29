using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Linq;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("添加孩子家长")]
    public class AddParentExecutor : PPTSEditCustomerExecutorBase<CreatablePortentialCustomerModel>
    {
        public AddParentExecutor(CreatablePortentialCustomerModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.Customer.NullCheck("Customer");
            model.PrimaryParent.NullCheck("PrimaryParent");
        }

        protected override void Validate()
        {
            base.Validate();

        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            CustomerParentRelation relation = this.Model.ToRelation().FillCreator();

            CustomerParentRelationCollection collection = CustomerParentRelationAdapter.Instance.Load(action =>
            {
                action.AppendItem("CustomerID", this.Model.Customer.CustomerID);
            }, DateTime.MinValue);

            if (collection.Count > 0)
            {
                if (collection.Where(r => r.ParentID == this.Model.PrimaryParent.ParentID).Count() > 0)
                    return;
                if (collection.Where(r => r.IsPrimary == true).Count() > 0)
                    relation.IsPrimary = false;
            }

            CustomerParentRelationAdapter.Instance.UpdateInContext(relation);

            CustomerFulltextInfo customerFullText = CustomerFulltextInfo.Create(this.Model.Customer.CustomerID, CustomerFulltextInfo.PotentialCustomersType, this.Model.Customer.Status);
            customerFullText.CustomerSearchContent = this.Model.Customer.ToSearchContent();
            customerFullText.ParentSearchContent = this.Model.PrimaryParent.ToSearchContent();
            CustomerFulltextInfoAdapter.Instance.UpdateInContext(customerFullText);

            CustomerFulltextInfo parentFullText = CustomerFulltextInfo.Create(this.Model.PrimaryParent.ParentID, CustomerFulltextInfo.ParentsType);
            parentFullText.CustomerSearchContent = this.Model.Customer.ToSearchContent();
            parentFullText.ParentSearchContent = this.Model.PrimaryParent.ToSearchContent();
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