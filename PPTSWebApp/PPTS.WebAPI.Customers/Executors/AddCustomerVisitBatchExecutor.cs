using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerServices;
using PPTS.WebAPI.Customers.ViewModels.CustomerVisits;
using System;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("批量增加回访")]
    public class AddCustomerVisitBatchExecutor : PPTSEditCustomerExecutorBase<CreatableVisitBatchModel>
    {
        /// <summary>
        /// 批量添加回访
        /// </summary>
        /// <param name="model"></param>
        public AddCustomerVisitBatchExecutor(CreatableVisitBatchModel model)
            : base(model, null)
        {
            model.NullCheck("model");

            model.CustomerVisits.NullCheck("CustomerVisits");
        }

        private void InitData(CreatableVisitBatchModel model)
        {
            foreach (CustomerVisit item in model.CustomerVisits)
            {
                item.VisitID = UuidHelper.NewUuidString();
                item.CampusID = this.Model.CampusID;
                item.CampusName = this.Model.CampusName;

                ///回访人相关
                item.VisitorID = this.Model.CurrID;
                item.VisitorJobID = this.Model.CurrJobID;
                item.VisitorName = this.Model.CurrName;
                item.VisitorJobName = this.Model.CurrJobName;

                ///创建人相关
                item.CreatorID = this.Model.CurrID;
                item.CreatorName = this.Model.CurrName;
                item.CreateTime = MCS.Library.Net.SNTP.SNTPClient.AdjustedTime;

                ///编辑人相关
                item.ModifierID = this.Model.CurrID;
                item.ModifierName = this.Model.CurrName;
                item.ModifyTime = MCS.Library.Net.SNTP.SNTPClient.AdjustedTime;

            }
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            InitData(Model);
            foreach (CustomerVisit item in Model.CustomerVisits)
            {
                CustomerVisitAdapter.Instance.UpdateInContext(item);
            }
        }
    }
}