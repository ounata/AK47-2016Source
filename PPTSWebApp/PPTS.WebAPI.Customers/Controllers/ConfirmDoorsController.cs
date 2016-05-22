using MCS.Library.Data;
using MCS.Library.Principal;
using MCS.Web.MVC.Library.Filters;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.DataSources;
using PPTS.WebAPI.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using PPTS.WebAPI.Customers.ViewModels.CustomerVerifies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PPTS.WebAPI.Customers.Controllers
{
    [ApiPassportAuthentication]
    public class ConfirmDoorsController : ApiController
    {
        #region api/customerVerifies/getAllConfirmDoors

        /// <summary>
        /// 上门确认记录查询，第一次。第一页，下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回带字典的跟进记录列表</returns>
        [HttpPost]
        public ConfirmDoorQueryResult GetAllConfirmDoors(ConfirmDoorCriteriaModel criteria)
        {
            return new ConfirmDoorQueryResult
            {
                QueryResult = ConfirmDoorDataSource.Instance.LoadConfirmDoor(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerVerify))
            };
        }

        /// <s6ummary>
        /// 上门确认记录查询，翻页或排序。不下载字典
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回不带字典的跟进记录列表</returns>
        [HttpPost]
        public PagedQueryResult<ConfirmDoorQueryModel, ConfirmDoorsQueryCollection> GetPagedConfirmDoors(ConfirmDoorCriteriaModel criteria)
        {
            return ConfirmDoorDataSource.Instance.LoadConfirmDoor(criteria.PageParams, criteria, criteria.OrderBy);
        }



        #endregion

        #region api/customerfollows/createConfirmDoors

        /// <summary>
        /// 新建上门记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ConfirmDoorCreateModel CreateConfirmDoor()
        {
            return ConfirmDoorCreateModel.CreateConfirmDoor();
        }

        [HttpPost]
        public void CreateConfirmDoor(ConfirmDoorCreateModel model)
        {
            model.ConfirmDoor.CreatorID = DeluxeIdentity.CurrentUser.ID;
            model.ConfirmDoor.CreatorName = DeluxeIdentity.CurrentUser.DisplayName;
            model.ConfirmDoor.VerifierID = DeluxeIdentity.CurrentUser.ID;
            model.ConfirmDoor.VerifierJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
            model.ConfirmDoor.VerifierJobName = DeluxeIdentity.CurrentUser.GetCurrentJob().Name;

            model.ConfirmDoor.IsInvited = Convert.ToInt32(CreatableFollowModel.ExistCustomerFollow(model.ConfirmDoor.CustomerID));
            model.ConfirmDoor.VerifierName = DeluxeIdentity.CurrentUser.DisplayName;
            AddConfirmDoorExecutor executor = new AddConfirmDoorExecutor(model);
            executor.Execute();
        }

        #endregion
    }
}