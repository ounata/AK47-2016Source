using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.Students;
using System.Linq;

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
            model.PrimaryParent.NullCheck("PrimaryParent");
        }

        protected override void Validate()
        {
            base.Validate();
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            CustomerParentRelationCollection relations = CustomerParentRelationAdapter.Instance.Load(this.Model.Customer.CustomerID);

            bool isPrimary = false;
            if (relations.Count > 0)
            {
                if (relations.Where(r => r.ParentID == this.Model.PrimaryParent.ParentID).Count() > 0)
                    return;
                if (relations.Where(r => r.IsPrimary == true).Count() > 0)
                    isPrimary = false;
            }

            CustomerParentRelation relation = this.Model.ToRelation(isPrimary).FillCreator();
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