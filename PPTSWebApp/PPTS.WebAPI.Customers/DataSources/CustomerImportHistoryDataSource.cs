using System.Collections.Generic;
using MCS.Library.Data;
using PPTS.Data.Customers.DataSources;
using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Principal;

namespace PPTS.WebAPI.Customers.DataSources
{
    public class CustomerImportHistoryDataSource : GenericCustomerDataSource<UserOperationLog, UserOperationLogCollection>
    {
        public static readonly new CustomerImportHistoryDataSource Instance = new CustomerImportHistoryDataSource();

        private CustomerImportHistoryDataSource()
        {
        }

        public PagedQueryResult<UserOperationLog, UserOperationLogCollection> LoadImportCustomerHistory(IPageRequestParams prp, object criteria, IEnumerable<IOrderByRequestItem> orderByBuilder)
        {
            var result = Query(prp, "", orderByBuilder);

            return result;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.SelectFields = @" ACTIVITY_NAME, OPERATOR_NAME, OPERATE_DATETIME, OPERATE_NAME, OPERATE_DESCRIPTION ";
            qc.FromClause = @"  WF.USER_OPERATION_LOG ";
            qc.WhereClause = string.Format(@" OPERATOR_ID = {0} and RESOURCE_ID=N'IMPORT-CUSTOMERS-F-CM-AC-002'", DeluxeIdentity.CurrentUser.ID);
            base.OnBuildQueryCondition(qc);
        }

        protected override void OnAfterQuery(UserOperationLogCollection result)
        {
        }
    }
}