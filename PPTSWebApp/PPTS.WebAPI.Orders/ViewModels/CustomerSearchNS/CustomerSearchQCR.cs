using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCS.Library.Data;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Common.Entities;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.DataSources;

namespace PPTS.WebAPI.Orders.ViewModels.CustomerSearchNS
{
    public class CustomerSearchQCR
    {
        public PagedQueryResult<CustomerSearch, CustomerSearchCollection> QueryResult { get; private set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; private set; }
        public string Msg { get; private set; }

        public void LoadData(CustomerSearchQCM qCM)
        {
            this.Msg = "ok";
            ///当前操作人所属校区ID
            IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (org == null)
            {
                QueryResult = null;
                Dictionaries = null;
                this.Msg = "未能获取到当前用户所属校区，无法加载学员数据，请检查当前用户角色是否正确！";
                return;
            }
            qCM.CampusID = org.ID;
            QueryResult = GenericSearchDataSource<CustomerSearch, CustomerSearchCollection>.Instance.Query(qCM.PageParams, qCM, qCM.OrderBy);
            Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerSearch));
        }

    }
}