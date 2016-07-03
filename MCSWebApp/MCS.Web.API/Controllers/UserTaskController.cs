using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.DataSources;
using MCS.Web.MVC.Library.Filters;
using MCS.Web.MVC.Library.Models;
using MCS.Web.MVC.Library.Models.UserTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MCS.Web.API.Controllers
{
    [ApiPassportAuthentication]
    public class UserTaskController : ApiController
    {
        #region api/usertask/queryUserTasksAndCount

        /// <summary>
        /// 查询待办任务(用于首页)
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        [HttpPost]
        public UserTaskCountAndSimpleListModel QueryUserTasksAndCount(UserTaskSearchParams searchParams)
        {
            return UserTaskModelHelper.Instance.QueryTaskCountAndSimpleList(DeluxeIdentity.CurrentUser.ID, searchParams);
        }

        #endregion

        #region api/usertask/queryUserTasks

        /// <summary>
        /// 查询待办任务
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回待办列表</returns>
        [HttpPost]
        public PagedQueryResult<UserTaskModel, UserTaskModelCollection> QueryUserTasks(UserTaskQueryCriteria criteria)
        {
            criteria.Status = TaskStatus.Ban;
            return UserTaskDataSource.Instance.LoadUserTasks(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/usertask/queryUserCompletedTasks

        /// <summary>
        /// 查询已办任务
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回待办列表</returns>
        [HttpPost]
        public PagedQueryResult<UserAccomplishedTaskModel, UserAccomplishedTaskModelCollection> QueryUserCompletedTasks(UserTaskQueryCriteria criteria)
        {
            return UserAccomplishedTaskDataSource.Instance.LoadUserTasks(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/usertask/queryUserNotifies

        /// <summary>
        /// 查询通知消息
        /// </summary>
        /// <param name="criteria">查询条件</param>
        /// <returns>返回待办列表</returns>
        [HttpPost]
        public PagedQueryResult<UserTaskModel, UserTaskModelCollection> QueryUserNotifies(UserTaskQueryCriteria criteria)
        {
            criteria.Status = TaskStatus.Yue;
            return UserTaskDataSource.Instance.LoadUserTasks(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion
    }
}