using MCS.Library.Core;
using MCS.Library.Data.Executors;
using System;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerScores;
using PPTS.Data.Customers;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("批量录入成绩")]
    public class AddBatchScoresExecutor : PPTSEditCustomerExecutorBase<CustomerScoresBatchSearchModelCollection>
    {
        public AddBatchScoresExecutor(CustomerScoresBatchSearchModelCollection model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void Validate()
        {
            base.Validate();
            // 学年度、学期、考试类型、年级每个学员只能添加一条，重复提示：已添加过该成绩，请勿重复添加。
            Model.ForEach(item=> {
                CustomerScore customerScore = item.Scores;
                CustomerScoreCollection scores = CustomerScoreAdapter.Instance.Load(where =>
                                           where.AppendItem("StudyYear", customerScore.StudyYear)
                                                .AppendItem("StudyTerm", customerScore.StudyTerm)
                                                .AppendItem("ScoreType", customerScore.ScoreType)
                                                .AppendItem("ScoreGrade", customerScore.ScoreGrade)
                                                .AppendItem("IsAllAdded", (byte)ScoreIsAllAdded.Yes));
                if (scores != null && scores.Count > 0)
                {
                    Customer customer = CustomerAdapter.Instance.Load(customerScore.CustomerID);
                    throw new ApplicationException(customer.CustomerName + " 已全部添加过该成绩，请勿重复添加");
                }
            });
        }

        private void InitData(CustomerScoresBatchSearchModelCollection model)
        {
            model.ForEach(item =>
            {
                CustomerScore customerScore = item.Scores;
                CustomerScoreItemCollection customerScoreItems = item.ScoreItems;
                Customer customer = CustomerAdapter.Instance.Load(customerScore.CustomerID);
                if (string.IsNullOrEmpty(customerScore.ScoreID))
                {
                    customerScore.ScoreID = UuidHelper.NewUuidString();
                }
                customerScore.FillCreator();
                customerScore.FillModifier();
                customerScore.CampusID = customer.CampusID;
                customerScore.CampusName = customer.CampusName;
                customerScoreItems.ForEach(scoreItem =>
                {
                    if (string.IsNullOrEmpty(scoreItem.ScoreID))
                    {
                        scoreItem.ItemID = UuidHelper.NewUuidString();
                        scoreItem.ScoreID = customerScore.ScoreID;
                    }
                });
            });
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            InitData(this.Model);
            Model.ForEach(item =>
            {
                CustomerScoreAdapter.Instance.UpdateInContext(item.Scores);
                foreach (CustomerScoreItem scoreItem in item.ScoreItems)
                {
                    CustomerScoreItemAdapter.Instance.UpdateInContext(scoreItem);
                }
            });
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