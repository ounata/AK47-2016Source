using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("编辑潜在客户")]
    public class EditPotentialCustomerExecutor : PPTSEditCustomerExecutorBase<EditablePotentialCustomerModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public EditPotentialCustomerExecutor(EditablePotentialCustomerModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.Customer.NullCheck("Customer");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            this.Model.Customer.FillModifier();

            PotentialCustomerAdapter.Instance.UpdateInContext(this.Model.Customer);

            // 学员学校信息
            if (!string.IsNullOrEmpty(this.Model.Customer.SchoolID))
            {
                CustomerSchoolRelation schoolRelation = new CustomerSchoolRelation()
                {
                    SchoolID = this.Model.Customer.SchoolID,
                    CustomerID = this.Model.Customer.CustomerID
                };
                schoolRelation.FillCreator().FillModifier();
                CustomerSchoolRelationAdapter.Instance.UpdateInContext(schoolRelation);
            }

            PhoneAdapter.Instance.UpdateByOwnerIDInContext(this.Model.Customer.CustomerID, this.Model.Customer.ToPhones(this.Model.Customer.CustomerID));

            CustomerFulltextInfo fullText = CustomerFulltextInfo.Create(this.Model.Customer.CustomerID, CustomerFulltextInfo.PotentialCustomersType, this.Model.Customer.Status);

            fullText.CustomerSearchContent = this.Model.Customer.ToSearchContent();

            CustomerFulltextInfoAdapter.Instance.UpdateCustomerSearchContentInContext(fullText);
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