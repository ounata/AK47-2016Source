using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Builder;
using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.UserTasks
{
    /// <summary>
    /// 待办Model的查询Helper
    /// </summary>
    public class UserTaskModelHelper
    {
        public static readonly UserTaskModelHelper Instance = new UserTaskModelHelper();

        private UserTaskModelHelper()
        {
        }

        /// <summary>
        /// 根据UserID查询用户的待办个数和摘要列表。同时传递originalServerTag。如果originalServerTag和目前Server端的ServerTag不匹配，
        /// 则重新查询，否则不执行查询且返回数据
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public UserTaskCountAndSimpleListModel QueryTaskCountAndSimpleList(string userID, UserTaskSearchParams searchParams)
        {
            userID.CheckStringIsNullOrEmpty("userID");

            UserTaskCountAndSimpleListModel model = new UserTaskCountAndSimpleListModel();

            model.ServerTag = GetServerTag(userID);

            if (model.ServerTag != searchParams.OriginalServerTag)
            {
                model.CountData = UserTaskAdapter.Instance.GetUserTaskCount(userID);

                OrderBySqlClauseBuilder orderBuilder = new OrderBySqlClauseBuilder().AppendItem("DELIVER_TIME", FieldSortDirection.Descending);

                model.TaskData = UserTaskAdapter.Instance.LoadUserTasks("WF.USER_TASK", CreateTaskBuilder(userID), orderBuilder, searchParams.Top, true);
                model.NotifyData = UserTaskAdapter.Instance.LoadUserTasks("WF.USER_TASK", CreateNotifyBuilder(userID), orderBuilder, searchParams.Top, true);
            }

            return model;
        }

        private static WhereSqlClauseBuilder CreateTaskBuilder(string userID)
        {
            return new WhereSqlClauseBuilder().AppendItem("SEND_TO_USER", userID).AppendItem("STATUS", "1");
        }

        private static WhereSqlClauseBuilder CreateNotifyBuilder(string userID)
        {
            return new WhereSqlClauseBuilder().AppendItem("SEND_TO_USER", userID).AppendItem("STATUS", "2");
        }

        private static string GetServerTag(string userID)
        {
            return UserTaskChangingCache.Instance.GetOrAddNewValue(userID, (cache, key) =>
            {
                UdpNotifierCacheDependency dependency = new UdpNotifierCacheDependency();

                string serverTag = Guid.NewGuid().ToString();
                cache.Add(userID, serverTag, dependency);

                return serverTag;
            });
        }
    }
}
