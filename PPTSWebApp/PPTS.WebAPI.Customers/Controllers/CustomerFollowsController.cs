using System.Web.Http;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Customers.DataSources;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using MCS.Library.Data;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.DataSources;
using System.Collections.Generic;
using MCS.Library.Core;
using MCS.Web.MVC.Library.Filters;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using PPTS.Web.MVC.Library.Filters;

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
    public class CustomerFollowsController : ApiController
    {
        #region api/customerfollows/getallfollows

        /// <summary>
        /// 跟进记录查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的跟进记录列表</returns>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:跟进管理（学员视图-跟进记录跟进记录详情）,跟进管理（学员视图-跟进记录跟进记录详情）-本部门,跟进管理（学员视图-跟进记录跟进记录详情）-本校区,跟进管理（学员视图-跟进记录跟进记录详情）-本分公司,跟进管理（跟进记录详情）-全国")]
        public FollowQueryResult GetAllFollows(FollowQueryCriteriaModel criteria)
        {
            if (criteria.QueryDepts != null && criteria.QueryDepts.Length != 0)
            {
                //获取部门ID
                criteria.QueryDeptID = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Department).ID;
            }
            return new FollowQueryResult
            {
                QueryResult = CustomerFollowDataSource.Instance.LoadCustomerFollow(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerFollow))
            };
        }

        /// <s6ummary>
        /// 跟进记录查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的跟进记录列表</returns>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:跟进管理（学员视图-跟进记录跟进记录详情）,跟进管理（学员视图-跟进记录跟进记录详情）-本部门,跟进管理（学员视图-跟进记录跟进记录详情）-本校区,跟进管理（学员视图-跟进记录跟进记录详情）-本分公司,跟进管理（跟进记录详情）-全国")]
        public PagedQueryResult<FollowQueryModel, CustomerFollowQueryCollection> GetPagedFollows(FollowQueryCriteriaModel criteria)
        {
            return CustomerFollowDataSource.Instance.LoadCustomerFollow(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/customerfollows/createfollow
        /// <summary>
        /// 新建跟踪记录
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="isPotential"></param>
        /// <returns></returns>
        [HttpGet]
        [PPTSJobFunctionAuthorize("PPTS:新增跟进记录")]
        public CreatableFollowModel CreateFollow(string customerId, bool isPotential)
        {
            return CreatableFollowModel.CreateFollow(customerId, isPotential);
        }

        /// <summary>
        /// 提交新建的跟进记录
        /// </summary>
        /// <param name="model">跟进记录实体类</param>
        [HttpPost]
        [PPTSJobFunctionAuthorize("PPTS:新增跟进记录")]
        public void CreateFollow(CreatableFollowModel model)
        {
            #region 写入权限验证
            PPTS.Data.Common.Authorization.ScopeAuthorization<CustomerFollow>
               .GetInstance(Data.Customers.ConnectionDefine.PPTSCustomerConnectionName).CheckEditAuth("", model.Follow.CustomerID);
            #endregion

            model.Follow.CreatorID = DeluxeIdentity.CurrentUser.ID;
            model.Follow.CreatorName = DeluxeIdentity.CurrentUser.DisplayName;
            model.Follow.FollowerJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
            model.Follow.FollowerJobName = DeluxeIdentity.CurrentUser.GetCurrentJob().Name;
            AddCustomerFollowExecutor executor = new AddCustomerFollowExecutor(model);
            executor.Execute();

            List<CustomerFollowItem> itemList = model.FollowItems;
            if (itemList != null && itemList.Count > 0)
            {
                foreach (CustomerFollowItem item in itemList)
                {
                    item.FollowID = model.Follow.FollowID;
                    item.ItemID = UuidHelper.NewUuidString();
                }
            }
            AddCustomerFollowItemExecutor itemExecutor = new AddCustomerFollowItemExecutor(model.FollowItems);
            itemExecutor.Execute();
        }

        #endregion

        #region api/customerfollows/viewfollow

        [HttpGet]
        [PPTSJobFunctionAuthorize("PPTS:跟进管理（学员视图-跟进记录跟进记录详情）,跟进管理（学员视图-跟进记录跟进记录详情）-本部门,跟进管理（学员视图-跟进记录跟进记录详情）-本校区,跟进管理（学员视图-跟进记录跟进记录详情）-本分公司,跟进管理（跟进记录详情）-全国")]
        public ViewFollowModel ViewFollow(string followId)
        {
            return ViewFollowModel.LoadFollowModel(followId);
        }

        #endregion
    }
}