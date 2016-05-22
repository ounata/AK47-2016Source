using MCS.Library.Core;
using MCS.Library.Data.Executors;
using System;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerScores;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("编辑成绩")]
    public class EditCustomerScoresExecutor : PPTSEditCustomerExecutorBase<CustomerScoresModel>
    {
        public EditCustomerScoresExecutor(CustomerScoresModel model)
            : base(model, null)
        {
            model.NullCheck("model");
            model.Customer.NullCheck("Customer");
            model.Score.NullCheck("Score");
            model.ScoreItems.NullCheck("ScoreItems");
        }

        protected override void Validate()
        {
            base.Validate();
        }

        private void InitData(CustomerScoresModel model)
        {
            model.Customer = CustomerAdapter.Instance.Load(model.Customer.CustomerID);
            model.Score.FillModifier();
            foreach (CustomerScoreItem item in model.ScoreItems)
            {
                if (String.IsNullOrEmpty(item.ItemID))
                {
                    item.ItemID = UuidHelper.NewUuidString();
                    item.ScoreID = model.Score.ScoreID;
                }
            }
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            InitData(Model);
            CustomerScoreAdapter.Instance.UpdateInContext(Model.Score);
            foreach (CustomerScoreItem item in Model.ScoreItems)
            {
                CustomerScoreItemAdapter.Instance.UpdateInContext(item);
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