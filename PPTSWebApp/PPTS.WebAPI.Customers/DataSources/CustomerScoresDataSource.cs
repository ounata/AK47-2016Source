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
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers;
using PPTS.Data.Common;
using MCS.Library.OGUPermission;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerScoresDataSource : GenericCustomerDataSource<CustomerScoresSearchModel, CustomerScoresSearchModelCollection>
    {
        public static readonly new CustomerScoresDataSource Instance = new CustomerScoresDataSource();

        private CustomerScoresDataSource()
        {
        }

        public PagedQueryResult<CustomerScoresSearchModel, CustomerScoresSearchModelCollection> LoadCustomerScores(IPageRequestParams prp, CustomerScoresQueryCriteriaModel condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            ConnectiveSqlClauseCollection sqlCollection = new ConnectiveSqlClauseCollection();
            sqlCollection.Add(ConditionMapping.GetConnectiveClauseBuilder(condition));

            string customerSqlBuilder = sqlCollection.ToSqlString(TSqlBuilder.Instance);
            string sqlBuilder = BuildQueryCondition(condition);

            sqlBuilder = string.IsNullOrEmpty(customerSqlBuilder) ? string.Format(" 1=1 {0}", sqlBuilder) : string.Format("{0}{1}", customerSqlBuilder, sqlBuilder);

            var result = Query(prp, sqlBuilder, orderByBuilder);

            return result;
        }

        private string BuildQueryCondition(CustomerScoresQueryCriteriaModel condition)
        {
            // string sqlBuilder = string.Format(@" and CustomerScores.CreateTime >= '{0}' ", TimeZoneContext.Current.ConvertTimeToUtc(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"))));
            string sqlBuilder = string.Empty;

            #region 学员姓名
            string customerNameBuilder = string.Empty;
            if (!string.IsNullOrEmpty(condition.CustomerName))
            {
                customerNameBuilder = string.Format(
                    @" and exists(select CustomerID from CM.Customers Customers where Customers.CustomerID = CustomerScores.CustomerID and CustomerName like '%{0}%') ", condition.CustomerName.EncodeString());
            }
            #endregion

            #region 咨询师、学管师姓名
            string staffNameBuilder = string.Empty;
            if (!string.IsNullOrEmpty(condition.ConsultantName))
            {
                staffNameBuilder = string.Format(
                   @" and exists(select CustomerID from CM.CustomerStaffRelations_Current Staff where Staff.CustomerID = CustomerScores.CustomerID and staff.RelationType = {0} and staff.StaffName like '%{1}%') ", (byte)CustomerRelationType.Consultant, condition.ConsultantName.EncodeString());
            }
            if (!string.IsNullOrEmpty(condition.EducatorName))
            {
                staffNameBuilder += string.Format(
                    @" and exists(select CustomerID from CM.CustomerStaffRelations_Current Staff where Staff.CustomerID = CustomerScores.CustomerID and staff.RelationType = {0} and staff.StaffName like '%{1}%') ", (byte)CustomerRelationType.Educator, condition.EducatorName.EncodeString());
            }
            if (!string.IsNullOrEmpty(condition.StaffOA))
            {
                IUser user = OGUExtensions.GetUserByOAName(condition.StaffOA);
                string uid = user == null ? "0" : user.ID;
                staffNameBuilder += string.Format(
                    @" and exists(select CustomerID from CM.CustomerStaffRelations_Current Staff where Staff.CustomerID = CustomerScores.CustomerID and staff.RelationType in ({0},{1}) and staff.StaffID = '{2}') ", (byte)CustomerRelationType.Consultant, (byte)CustomerRelationType.Educator, uid);
            }
            if (!string.IsNullOrEmpty(condition.TeacherOA))
            {
                IUser user = OGUExtensions.GetUserByOAName(condition.TeacherOA);
                string uid = user == null ? "0" : user.ID;
                staffNameBuilder += string.Format(@" and TeacherID='{0}'", uid);
            }
            #endregion

            sqlBuilder += customerNameBuilder + staffNameBuilder;

            return sqlBuilder;
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
                    item.CustomerStatus = customer == null ? "" : ((byte)customer.Status).ToString();
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
                    teacher = teachers.Find(t => t.TeacherID == item.TeacherID);
                    item.TeacherOACode = teacher == null ? "" : teacher.TeacherOACode;
                }
            });

        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = @" CustomerScores.CampusName, CustomerScores.CustomerID, CustomerScores.ScoreID, CustomerScores.ScoreType, CustomerScores.ScoreGrade,
	                           CustomerScores.StudyYear, CustomerScores.StudyTerm, CustomerScores.StudyStage, /* CustomerScores.GradeRank, CustomerScores.ClassRank,*/
	                           CustomerScores.ClassPeoples, CustomerScores.AdmissionType, CustomerScores.IsKeyCollege, CustomerScores.ExamineMonth, CustomerScores.CreateTime,
                               CustomerScoreItems.ItemID, CustomerScoreItems.SortNo, CustomerScoreItems.Subject, CustomerScoreItems.TeacherID, 
                               CustomerScoreItems.TeacherName, CustomerScoreItems.PaperScore, CustomerScoreItems.RealScore, CustomerScoreItems.GradeRank, 
                               CustomerScoreItems.ClassRank, CustomerScoreItems.Satisficing, CustomerScoreItems.IsStudyHere ";
            qc.FromClause = @" CM.CustomerScores CustomerScores inner join CM.CustomerScoreItems CustomerScoreItems on CustomerScores.ScoreID = CustomerScoreItems.ScoreID ";

            #region 数据权限加工
            qc.WhereClause = Data.Common.Authorization.ScopeAuthorization<CustomerScore>
                .GetInstance(ConnectionDefine.PPTSCustomerConnectionName)
                .ReadAuthExistsBuilder("CustomerScores", qc.WhereClause).ToSqlString(TSqlBuilder.Instance);
            #endregion

            base.OnBuildQueryCondition(qc);
        }

    }
}