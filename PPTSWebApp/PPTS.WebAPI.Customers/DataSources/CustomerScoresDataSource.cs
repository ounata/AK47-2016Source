using System;
using System.Collections.Generic;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.Data.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Adapters;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Adapters;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerScoresDataSource : GenericCustomerDataSource<CustomerScoresSearchModel, CustomerScoresSearchModelCollection>
    {
        public static readonly new CustomerScoresDataSource Instance = new CustomerScoresDataSource();

        private CustomerScoresDataSource()
        {
        }

        protected override void OnAfterQuery(CustomerScoresSearchModelCollection result)
        {
            InLoadingCondition inBuilder = new InLoadingCondition(condition => result.ForEach(item => condition.AppendItem(item.CustomerID)), "CustomerID");
            CustomerCollection customers = GenericCustomerAdapter<Customer, CustomerCollection>.Instance.LoadByInBuilder(inBuilder, DateTime.MinValue);
            CustomerStaffRelationCollection relations = CustomerStaffRelationAdapter.Instance.LoadByInBuilder(inBuilder, DateTime.MinValue);
            TeacherJobViewCollection teachers = TeacherJobViewAdapter.Instance.LoadByInBuilder(new InLoadingCondition(builder => result.ForEach(item => builder.AppendItem(item.TeacherID)), "TeacherID"));

            Customer customer = null;
            CustomerStaffRelation relation_constantStaffName = null, relation_educatorName = null;
            TeacherJobView teacher = null;
            result.ForEach(item =>
            {
                if (customers != null && customers.Count > 0)
                {
                    customer = customers.Find(c => c.CustomerID == item.CustomerID);
                    item.CustomerName = customer == null ? "" : customer.CustomerName;
                    item.CustomerCode = customer == null ? "" : customer.CustomerCode;
                }
                if (relations != null && relations.Count > 0)
                {
                    relation_constantStaffName = relations.Find(r => r.CustomerID == item.CustomerID && r.RelationType == Data.Customers.CustomerRelationType.Consultant);
                    item.ConstantStaffName = relation_constantStaffName == null ? "" : relation_constantStaffName.StaffName;
                    relation_educatorName = relations.Find(r => r.CustomerID == item.CustomerID && r.RelationType == Data.Customers.CustomerRelationType.Educator);
                    item.EducatorName = relation_educatorName == null ? "" : relation_educatorName.StaffName;
                }
                if (teachers != null && teachers.Count > 0)
                {
                    teacher = teachers.Find(t=>t.TeacherID==item.TeacherID);
                    item.TeacherOACode = teacher == null ? "" : teacher.TeacherOACode;
                }
            });

        }

        public PagedQueryResult<CustomerScoresSearchModel, CustomerScoresSearchModelCollection> LoadCustomerScores(IPageRequestParams prp, CustomerScoresQueryCriteriaModel condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            WhereSqlClauseBuilder customerScoreBuilder = ConditionMapping.GetWhereSqlClauseBuilder(condition, new AdjustConditionValueDelegate(CustomerScoresQueryCriteriaModel.AdjustConditionValueDelegate_CustomerScore));
            WhereSqlClauseBuilder customerScoreItemsBuilder = ConditionMapping.GetWhereSqlClauseBuilder(condition, new AdjustConditionValueDelegate(CustomerScoresQueryCriteriaModel.AdjustConditionValueDelegate_CustomerScoreItems));
            WhereSqlClauseBuilder staffRelationBuilder = ConditionMapping.GetWhereSqlClauseBuilder(condition, new AdjustConditionValueDelegate(CustomerScoresQueryCriteriaModel.AdjustConditionValueDelegate_StaffRelations));
            WhereSqlClauseBuilder customerBuilder = ConditionMapping.GetWhereSqlClauseBuilder(condition, new AdjustConditionValueDelegate(CustomerScoresQueryCriteriaModel.AdjustConditionValueDelegate_Customers));

            string customerScoreWhere = customerScoreBuilder.ToSqlString(TSqlBuilder.Instance);
            string customerScoreItemsWhere = customerScoreItemsBuilder.ToSqlString(TSqlBuilder.Instance);
            string staffRelationsWhere = condition.CheckCondition_StaffRelations() ? "" : string.Format(" and exists(select CustomerID from CM.CustomerStaffRelations_Current Staff where Staff.CustomerID = CustomerScores.CustomerID and {0}) ", staffRelationBuilder.ToSqlString(TSqlBuilder.Instance));
            string customersWhere = condition.CheckCondition_Customers() ? "" : string.Format(" and exists(select CustomerID from CM.Customers Customers where Customers.CustomerID = CustomerScores.CustomerID and {0}) ", customerBuilder.ToSqlString(TSqlBuilder.Instance));

            string select = @" CustomerScores.CampusName, CustomerScores.CustomerID, CustomerScores.ScoreID, CustomerScores.ScoreType, CustomerScores.ScoreGrade,
	                           CustomerScores.StudyYear, CustomerScores.StudyTerm, CustomerScores.StudyStage, /* CustomerScores.GradeRank, CustomerScores.ClassRank,*/
	                           CustomerScores.ClassPeoples, CustomerScores.AdmissionType, CustomerScores.IsKeyCollege, CustomerScores.ExamineMonth, CustomerScores.CreateTime,
                               CustomerScoreItems.ItemID, CustomerScoreItems.SortNo, CustomerScoreItems.Subject, CustomerScoreItems.TeacherID, 
                               CustomerScoreItems.TeacherName, CustomerScoreItems.PaperScore, CustomerScoreItems.RealScore, CustomerScoreItems.GradeRank, 
                               CustomerScoreItems.ClassRank, CustomerScoreItems.Satisficing, CustomerScoreItems.IsStudyHere ";
            string from = @" CM.CustomerScores CustomerScores inner join CM.CustomerScoreItems CustomerScoreItems on CustomerScores.ScoreID = CustomerScoreItems.ScoreID ";

            string where = string.Format("{0}{1}{2}{3}",
                                        string.IsNullOrEmpty(customerScoreWhere) ? " 1 = 1 " : customerScoreWhere,
                                        string.IsNullOrEmpty(customerScoreItemsWhere) ? "" : " and " + customerScoreItemsWhere,
                                        staffRelationsWhere,
                                        customersWhere);
            var result = Query(prp, select, from, where, orderByBuilder);

            return result;
        }
    }
}