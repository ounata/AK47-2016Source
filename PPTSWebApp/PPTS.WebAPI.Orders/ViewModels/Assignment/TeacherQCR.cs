using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Assignment
{
    /// 按教师排课，获取教师列表，查询结果数据模型
    public class TeacherQCR
    {
        public PagedQueryResult<TeacherJobView, TeacherJobViewCollection> QueryResult { get; private set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; private set; }

        public string Msg { get; private set; }

        public void LoadData(TeacherQCM qcm)
        {
            this.Msg = "ok";
            IOrganization organ = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (organ == null)
            {
                this.Msg = "未能获取到当前用户所属校区，无法加载教师数据，请检查当前用户角色是否正确！";
                return;
            }
            qcm.CampusID = organ.ID;

            string partWhere = string.Empty;
            if (!string.IsNullOrEmpty(qcm.SubjectMemo))
                partWhere += "and " + TeacherJobViewAdapter.Instance.GetSql("Subject", qcm.SubjectMemo);
            if (!string.IsNullOrEmpty(qcm.GradeMemo))
                partWhere += "and " + TeacherJobViewAdapter.Instance.GetSql("Grade", qcm.GradeMemo);

            ConnectiveSqlClauseCollection cscc = ConditionMapping.GetConnectiveClauseBuilder(qcm);
            string sql = cscc.ToSqlString(TSqlBuilder.Instance);

            sql = sql + partWhere;

            QueryResult = GenericMetaDataSource<TeacherJobView, TeacherJobViewCollection>.Instance.Query(qcm.PageParams, sql, qcm.OrderBy);
            Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(TeacherJobView));
        }

        public void LoadDataPaged(TeacherQCM qcm)
        {
            IOrganization organ = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            qcm.CampusID = organ.ID;
            string partWhere = string.Empty;
            if (!string.IsNullOrEmpty(qcm.SubjectMemo))
                partWhere += "and " + TeacherJobViewAdapter.Instance.GetSql("Subject", qcm.SubjectMemo);
            if (!string.IsNullOrEmpty(qcm.GradeMemo))
                partWhere += "and " + TeacherJobViewAdapter.Instance.GetSql("Grade", qcm.GradeMemo);

            ConnectiveSqlClauseCollection cscc = ConditionMapping.GetConnectiveClauseBuilder(qcm);
            string sql = cscc.ToSqlString(TSqlBuilder.Instance);

            sql = sql + partWhere;
            QueryResult = GenericMetaDataSource<TeacherJobView, TeacherJobViewCollection>.Instance.Query(qcm.PageParams, sql, qcm.OrderBy);
        }
    }
}