using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.Students;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("编辑学员家长")]
    public class EditableStudentParentExecutor : PPTSEditCustomerExecutorBase<EditableStudentParentModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public EditableStudentParentExecutor(EditableStudentParentModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.Parent.NullCheck("Parent");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            this.Model.Parent.FillModifier();

            ParentAdapter.Instance.UpdateInContext(this.Model.Parent);

            CustomerParentRelationAdapter.Instance.UpdateInContext(this.Model.CustomerParentRelation);

            PhoneAdapter.Instance.UpdateByOwnerIDInContext(this.Model.Parent.ParentID, this.Model.Parent.ToPhones(this.Model.Parent.ParentID));

            CustomerFulltextInfo parentFullText = CustomerFulltextInfo.Create(this.Model.Parent.ParentID, CustomerFulltextInfo.ParentsType);

            parentFullText.ParentSearchContent = this.Model.Parent.ToSearchContent();

            CustomerFulltextInfoAdapter.Instance.UpdateParentSearchContentInContext(parentFullText);
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

            context.Logs.ForEach(log => log.ResourceID = this.Model.Parent.ParentID);
        }
    }
}