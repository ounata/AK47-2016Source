using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;

namespace PPTS.WebAPI.Customers.Executors
{
    /// <summary>
    /// 
    /// </summary>
    [DataExecutorDescription("增加潜在客户")]
    public class AddPotentialCustomerExecutor : PPTSEditCustomerExecutorBase<CreatablePortentialCustomerModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public AddPotentialCustomerExecutor(CreatablePortentialCustomerModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.Customer.NullCheck("Customer");
            model.PrimaryParent.NullCheck("PrimaryParent");
        }

        protected override void Validate()
        {
            base.Validate();
            //this.Model.Customer.PrimaryPhone.ToPhoneNumber()
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            Parent parent = ParentAdapter.Instance.Load(this.Model.PrimaryParent.ParentID);
            if (parent == null)
            {
                Model.PrimaryParent.FillCreator();
                ParentAdapter.Instance.UpdateInContext(this.Model.PrimaryParent);
                PhoneAdapter.Instance.UpdateByOwnerIDInContext(this.Model.PrimaryParent.ParentID, this.Model.PrimaryParent.ToPhones(this.Model.PrimaryParent.ParentID).FillCreatorList());
            }
            this.Model.Customer.FillCreator();
            PotentialCustomerAdapter.Instance.UpdateInContext(this.Model.Customer);

            CustomerParentRelation relation = this.Model.ToRelation().FillCreator();

            CustomerParentRelationAdapter.Instance.UpdateInContext(relation);

            PhoneAdapter.Instance.UpdateByOwnerIDInContext(this.Model.Customer.CustomerID, this.Model.Customer.ToPhones(this.Model.Customer.CustomerID).FillCreatorList());

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