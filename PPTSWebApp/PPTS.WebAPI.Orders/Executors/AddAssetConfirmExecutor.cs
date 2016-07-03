using MCS.Library.Core;
using MCS.Library.Data.Executors;
using PPTS.Data.Orders.Executors;
using System;
using System.Linq;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Adapters;
using PPTS.WebAPI.Orders.ViewModels.Purchase;
using MCS.Library.Data;
using MCS.Library.Validation;
using System.Transactions;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using PPTS.Data.Common.Security;
using MCS.Library.Principal;
using PPTS.Data.Orders.Entities;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("确认非上课收入")]
    public class AddAssetConfirmExecutor : PPTSEditPurchaseExecutorBase<AssetConfirmModel>
    {
        public AddAssetConfirmExecutor(AssetConfirmModel model) : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            
            Model.FillAsset()
                .FillAssetConfirm();

            base.PrepareData(context);            
        }
        protected override void DoValidate(ValidationResults validationResults)
        {
            Model.Validate();
            base.DoValidate(validationResults);
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            AssetConfirmAdapter.Instance.UpdateInContext(Model);
            GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateInContext(Model.Asset);

            Data.Orders.ConnectionDefine.GetDbContext().DoAction(db => db.ExecuteTimePointSqlInContext());
            return null;
        }

    }
}