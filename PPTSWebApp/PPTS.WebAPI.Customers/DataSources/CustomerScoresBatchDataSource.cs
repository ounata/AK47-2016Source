﻿using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using PPTS.WebAPI.Customers.ViewModels.CustomerScores;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.Adapters;
using System;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Adapters;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerScoresBatchDataSource : GenericCustomerDataSource<CustomerScoresBatchSearchModel, CustomerScoresBatchSearchModelCollection>
    {
        public static readonly new CustomerScoresBatchDataSource Instance = new CustomerScoresBatchDataSource();

        private CustomerScoresBatchDataSource()
        {
        }

        protected override void OnAfterQuery(CustomerScoresBatchSearchModelCollection result)
        {


        }

        public PagedQueryResult<CustomerScoresBatchSearchModel, CustomerScoresBatchSearchModelCollection> LoadCustomerScores_Batch(IPageRequestParams prp, CustomerScoresQueryCriteriaModel condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            WhereSqlClauseBuilder customerScoreBuilder = ConditionMapping.GetWhereSqlClauseBuilder(condition, new AdjustConditionValueDelegate(CustomerScoresQueryCriteriaModel.AdjustConditionValueDelegate_CustomerScore));
            string customerScoreWhere = customerScoreBuilder.ToSqlString(TSqlBuilder.Instance);
            customerScoreWhere = string.Format(" and not exists(select CustomerID from CM.CustomerScores customerScores where customers.CustomerID = customerScores.CustomerID and IsAllAdded = 1 {0})", string.IsNullOrEmpty(customerScoreBuilder.ToSqlString(TSqlBuilder.Instance)) ? "" : " and " + customerScoreBuilder.ToSqlString(TSqlBuilder.Instance));

            string select = @" customers.CustomerID,customers.CustomerName ";
            string from = @" CM.Customers_Current customers ";
            string where = string.Format(@" {0}{1}",
                                            string.IsNullOrEmpty(condition.ScoreGrade) ? "1=1" : "customers.Grade=" + condition.ScoreGrade,
                                            customerScoreWhere);
            var result = Query(prp, select, from, where, orderByBuilder);

            CustomerScoresBatchSearchModelCollection data = result.PagedData;

            data.ForEach(item =>
            {
                CustomerScoreAdapter.Instance.LoadInContext(new WhereLoadingCondition(
                   builder =>
                   {
                       builder.AppendItem("CustomerID", item.CustomerID)
                              .AppendItem("StudyYear", condition.StudyYear)
                              .AppendItem("StudyTerm", condition.StudyTerm)
                              .AppendItem("StudyStage", condition.StudyStage)
                              .AppendItem("ScoreGrade", condition.ScoreGrade)
                              .AppendItem("ScoreType", condition.ScoreType);
                   }), collection =>
                   {
                       collection.ForEach(score =>
                       {
                           data.ForEach(o =>
                           {
                               if (o.CustomerID == score.CustomerID)
                               {
                                   o.Scores = score;
                               }
                           });
                       });
                   });
            });
            CustomerScoreAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            data.ForEach(item =>
            {
                if (item.Scores != null)
                {
                    CustomerScoreItemAdapter.Instance.LoadInContext(new WhereLoadingCondition(
                    builder =>
                    {
                        builder.AppendItem("ScoreID", item.Scores.ScoreID);
                    }), collection =>
                    {
                        collection.ForEach(scoreItem =>
                        {
                            data.ForEach(s =>
                            {
                                if (s.Scores != null)
                                {
                                    if (s.Scores.CustomerID == s.CustomerID && s.Scores.ScoreID == scoreItem.ScoreID)
                                    {
                                        if (s.ScoreItems == null)
                                        {
                                            s.ScoreItems = new CustomerScoreItemCollection();
                                        }
                                        s.ScoreItems.Add(scoreItem);
                                    }
                                }
                            });
                        });
                    });
                }
            });
            CustomerScoreItemAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            TeacherSearchCollection teachers = null;
            data.ForEach(item =>
            {
                bool isTeacher = DeluxeIdentity.CurrentUser.GetCurrentJob().JobType == Data.Common.JobTypeDefine.Teacher;
                if (isTeacher)
                {
                    teachers = TeacherSearchAdapter.Instance.Load(item.CustomerID, DeluxeIdentity.CurrentUser.ID);
                }
                else
                {
                    teachers = TeacherSearchAdapter.Instance.Load(item.CustomerID);
                }
                item.Teachers = teachers;
            });

            return result;
        }
    }
}