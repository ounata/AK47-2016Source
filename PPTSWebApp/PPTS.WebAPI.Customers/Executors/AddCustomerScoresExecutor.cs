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
    [DataExecutorDescription("录入成绩")]
    public class AddCustomerScoresExecutor : PPTSEditCustomerExecutorBase<CustomerScoresModel>
    {
        public AddCustomerScoresExecutor(CustomerScoresModel model)
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
            // 学年度、学期、考试类型、年级每个学员只能添加一条，重复提示：已添加过该成绩，请勿重复添加。
            CustomerScoreCollection scores = CustomerScoreAdapter.Instance.Load(where =>
                                       where.AppendItem("CustomerID", Model.Score.CustomerID)
                                            .AppendItem("StudyYear", Model.Score.StudyYear)
                                            .AppendItem("StudyTerm", Model.Score.StudyTerm)
                                            .AppendItem("ScoreType", Model.Score.ScoreType)
                                            .AppendItem("ScoreGrade", Model.Score.ScoreGrade));
            if (scores != null && scores.Count > 0)
                throw new ApplicationException("已添加过该成绩，请勿重复添加");
        }

        private void InitData(CustomerScoresModel model)
        {
            model.Customer = CustomerAdapter.Instance.Load(model.Customer.CustomerID);
            model.Score.FillCreator();
            model.Score.FillModifier();
            model.Score.CustomerID = model.Customer.CustomerID;
            model.Score.CampusID = model.Customer.CampusID;
            model.Score.CampusName = model.Customer.CampusName;
            foreach (CustomerScoreItem item in model.ScoreItems)
            {
                item.ItemID = UuidHelper.NewUuidString();
                item.ScoreID = model.Score.ScoreID;
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