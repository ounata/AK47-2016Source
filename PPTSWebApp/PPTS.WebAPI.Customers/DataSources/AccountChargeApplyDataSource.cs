using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.WebAPI.Customers.ViewModels.Accounts;
using MCS.Library.Data.Mapping;
using MCS.Library.Data.Builder;
using System;
using MCS.Library.Core;
using PPTS.Data.Common;

namespace PPTS.WebAPI.Customers.DataSources
{
    /// <summary>
    /// 缴费单数据源类
    /// </summary>
    public class AccountChargeApplyDataSource : GenericCustomerDataSource<ChargeApplyQueryModel, ChargeApplyQueryModelCollection>
    {
        public static readonly new AccountChargeApplyDataSource Instance = new AccountChargeApplyDataSource();

        private AccountChargeApplyDataSource()
        {
        }

        private string EncodeText(string text)
        {
            if (text != null)
                return text.Replace("'", "''");
            return text;
        }

        public PagedQueryResult<ChargeApplyQueryModel, ChargeApplyQueryModelCollection> QueryResult(IPageRequestParams prp, object condition, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            var select = @"a.*";
            var from = @" CM.AccountChargeApplies a";

            PagedQueryResult<ChargeApplyQueryModel, ChargeApplyQueryModelCollection> result;

            string where = null;
            var whereBuilder = ConditionMapping.GetConnectiveClauseBuilder(condition);
            if (whereBuilder != null && TSqlBuilder.Instance != null)
                where = whereBuilder.ToSqlString(TSqlBuilder.Instance);
            where = string.IsNullOrEmpty(where) ? "1>0" : where;

            ChargeApplyQueryCriteriaModel criterial = (condition as ChargeApplyQueryCriteriaModel);
            if (!string.IsNullOrEmpty(criterial.SearchText))
            {
                string searchText = this.EncodeText(criterial.SearchText);
                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.CustomersFulltext d where d.OwnerID = a.CustomerID and (contains(d.CustomerSearchContent,'{0}') or contains(d.ParentSearchContent,'{0}')))", searchText);
                where += " or ";
                where += string.Format("EXISTS(select top 1 1 from CM.PotentialCustomersFulltext e where e.OwnerID = a.CustomerID and (contains(e.CustomerSearchContent,'{0}') or contains(e.ParentSearchContent,'{0}')))", searchText);
                where += ")";
            }
            if (CommonHelper.IsValidDbDate(criterial.CustomerCreateTimeStart) && CommonHelper.IsValidDbDate(criterial.CustomerCreateTimeStart))
            {
                string date = TimeZoneContext.Current.ConvertTimeToUtc(criterial.CustomerCreateTimeStart).ToString("yyyy-MM-dd HH:mm:ss");
                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.PotentialCustomers_Current e where e.CustomerID = a.CustomerID and e.CreateTime>='{0}')", date);
                where += ")";
            }
            if (CommonHelper.IsValidDbDate(criterial.CustomerCreateTimeEnd) && CommonHelper.IsValidDbDate(criterial.CustomerCreateTimeEnd))
            {
                string date = TimeZoneContext.Current.ConvertTimeToUtc(criterial.CustomerCreateTimeEnd.AddDays(1)).ToString("yyyy-MM-dd HH:mm:ss");
                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.PotentialCustomers_Current e where e.CustomerID = a.CustomerID and e.CreateTime<'{0}')", date);
                where += ")";
            }
            if (criterial.CustomerCreatorJobTypes != null && criterial.CustomerCreatorJobTypes.Length != 0)
            {
                string inStr = string.Empty;
                foreach (string s in criterial.CustomerCreatorJobTypes)
                    inStr += "\'" + s + "\',";
                inStr = inStr.TrimEnd(',');

                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.PotentialCustomers_Current e where e.CustomerID = a.CustomerID and e.CreatorJobType in ({0}))", inStr);
                where += ")";
            }
            if (criterial.SourceMainTypes != null && criterial.SourceMainTypes.Length != 0)
            {
                string inStr = string.Empty;
                foreach (string s in criterial.SourceMainTypes)
                    inStr += "\'" + s + "\',";
                inStr = inStr.TrimEnd(',');

                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.PotentialCustomers_Current e where e.CustomerID = a.CustomerID and e.SourceMainType in ({0}))", inStr);
                where += ")";
            }
            if (criterial.SourceSubTypes != null && criterial.SourceSubTypes.Length != 0)
            {
                string inStr = string.Empty;
                foreach (string s in criterial.SourceSubTypes)
                    inStr += "\'" + s + "\',";
                inStr = inStr.TrimEnd(',');

                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.PotentialCustomers_Current e where e.CustomerID = a.CustomerID and e.SourceSubType in ({0}))", inStr);
                where += ")";
            }
            if (criterial.BelongRelationTypes != null && criterial.BelongRelationTypes.Length != 0)
            {
                string inStr = string.Empty;
                foreach (string s in criterial.BelongRelationTypes)
                    inStr += "\'" + s + "\',";
                inStr = inStr.TrimEnd(',');

                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.CustomerStaffRelations_Current d where d.CustomerID = a.CustomerID and d.RelationType in ({0}))", inStr);
                where += ")";
            }
            if (!string.IsNullOrEmpty(criterial.BelongerName))
            {
                string belongerName = this.EncodeText(criterial.BelongerName);
                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.CustomerStaffRelations_Current d where d.CustomerID = a.CustomerID and d.StaffName like '%{0}%')", belongerName);
                where += ")";
            }
            if (!string.IsNullOrEmpty(criterial.CustomerCreatorName))
            {
                string customerCreatorName = this.EncodeText(criterial.CustomerCreatorName);
                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from CM.PotentialCustomers_Current e where e.CustomerID = a.CustomerID and e.CreatorName='{0}')", customerCreatorName);
                where += ")";
            }
            if (!string.IsNullOrEmpty(criterial.QueryDeptID))
            {
                where += " and (";
                where += string.Format("EXISTS(select top 1 1 from MT.CustomerOrgAuthorizations d where d.OwnerID = a.CustomerID and d.ObjectID='{0}')", criterial.QueryDeptID);
                where += ")";
            }
            result = Query(prp, select, from, where, orderByBuilder);
            return result;
        }
    }
}
