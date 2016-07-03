using System;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using PPTS.Data.Common.Security;
using System.Linq;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Customers.Executors;
using PPTS.Data.Common;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("编辑业绩归属")]
    public class AccountEditChargeAllotExecutor : PPTSEditCustomerExecutorBase<ChargeAllotModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public AccountEditChargeAllotExecutor(ChargeAllotModel model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        private AccountChargeApply Apply
        {
            set;
            get;
        }

        private void Init(ChargeAllotModel allot)
        {
            this.Apply = AccountChargeApplyAdapter.Instance.LoadByApplyID(allot.ApplyID);
            this.Apply.FillModifier();
            this.Apply.AllotSubjects = ChargeApplyModel.ConvertSubjects(allot.Subjects);
            if (this.Apply.PayTime != DateTime.MinValue)
            {
                GlobalArgs args = ConfigsCache.GetGlobalArgs();
                if (args.IsClosedToAcccount(this.Apply.PayTime.Year, this.Apply.PayTime.Month))
                    throw new Exception("已过关帐日不可再编辑");
            }
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            this.Init(this.Model);
            AccountChargeApplyAdapter.Instance.UpdateInContext(this.Apply);
            AccountChargeAllotAdapter.Instance.DeleteInContext(builder => builder.AppendItem("ApplyID", this.Model.ApplyID));
            foreach (ChargeAllotItemModel itemModel in this.Model.Items)
            {
                if (!itemModel.TeacherID.IsNullOrEmpty())
                {
                    itemModel.AllotID = Guid.NewGuid().ToString().ToUpper();
                    AccountChargeAllotAdapter.Instance.UpdateInContext(itemModel);
                }
            }
        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
        }
    }
}