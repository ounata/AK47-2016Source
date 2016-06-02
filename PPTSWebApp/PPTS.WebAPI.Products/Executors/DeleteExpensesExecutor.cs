using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Products.Adapters;
using PPTS.WebAPI.Products.ViewModels.ServiceFees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.Executors
{
    [DataExecutorDescription("删除综合服务费")]
    public class DeleteExpensesExecutor : PPTSEditProductExecutorBase<EditExpensesModel>
    {
        public DeleteExpensesExecutor(EditExpensesModel model) : base(model, null)
        {
            model.ExpenseIds.NullCheck("ExpenseIds");
        }
        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            foreach (string expenseId in Model.ExpenseIds)
            {
                ExpenseAdapter.Instance.DeleteInContext(action=>action.AppendItem("expenseId", expenseId));
                ExpensePermissionAdapter.Instance.DeleteInContext(action => action.AppendItem("expenseId", expenseId));
            }
        }
    }
}