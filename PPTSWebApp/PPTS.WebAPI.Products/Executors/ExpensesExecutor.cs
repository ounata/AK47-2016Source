using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.Net.SNTP;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using PPTS.WebAPI.Products.ViewModels.ServiceFees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.Executors
{
    [DataExecutorDescription("添加和编辑综合服务费")]
    public class ExpensesExecutor : PPTSEditProductExecutorBase<EditExpensesModel>
    {
        public ExpensesExecutor(EditExpensesModel model) : base(model, null)
        {
            model.expense.NullCheck("model");
        }
        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            DateTime now = SNTPClient.AdjustedLocalTime;
            string[] campus = Model.expense.CampusIDs.Split(',');
            string[] campuNames = Model.expense.CampusNames.Split(',');
            if (string.IsNullOrEmpty(Model.expense.ExpenseID))
            {
                for (int i=0;i<campus.Length;i++)
                {
                    bool isExist = ExpensePermissionAdapter.Instance.Exists(action => 
                    action.AppendItem("CampusID", campus[i]));
                    if (isExist)
                    {
                        throw new ApplicationException(string.Format("校区:{0} 已存在综合服务费!", campuNames[i]));
                    }
                }
                Model.expense.ExpenseID = UuidHelper.NewUuidString();
                Model.expense.CreateTime = now;
                Model.expense.CreatorID = DeluxeIdentity.CurrentUser.ID;
                Model.expense.CreatorName = DeluxeIdentity.CurrentUser.Name;

            }
            else
            {
                Model.expense.ModifyTime = now;
                Model.expense.ModifierID = DeluxeIdentity.CurrentUser.ID;
                Model.expense.ModifierName = DeluxeIdentity.CurrentUser.Name;
            }

            ExpenseAdapter.Instance.UpdateInContext(Model.expense);
            ExpensePermissionAdapter.Instance.DeleteInContext(x => x.AppendItem("ExpenseID", Model.expense.ExpenseID));
            foreach (string c in campus)
            {
                ExpensePermission ePermission = new ExpensePermission();
                ePermission.CampusID = c;
                ePermission.CreateTime = now;
                ePermission.CreatorID = DeluxeIdentity.CurrentUser.ID;
                ePermission.CreatorName = DeluxeIdentity.CurrentUser.Name;
                ePermission.ExpenseID = Model.expense.ExpenseID;
                ExpensePermissionAdapter.Instance.UpdateInContext(ePermission);
            }

        }
        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            #region 生成数据权限范围数据
            //PPTS.Data.Common.Authorization.ScopeAuthorization<Expense>
            //   .GetInstance(PPTS.Data.Products.ConnectionDefine.PPTSProductConnectionName)
            //   .UpdateAuthInContext(DeluxeIdentity.CurrentUser.GetCurrentJob()
            //   , DeluxeIdentity.CurrentUser.GetCurrentJob().Organization()
            //   , this.Model.expense.ExpenseID
            //   , PPTS.Data.Common.Authorization.RelationType.Owner);
            #endregion 生成数据权限范围数据

            return base.DoOperation(context);
        }
    }
}