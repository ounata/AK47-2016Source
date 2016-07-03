using System;
using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects;

namespace MCS.Web.MVC.Library.Models.UserTasks
{
    [Serializable]
    public class UserTaskQueryCriteria
    {
        [ConditionMapping("TASK_TITLE", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string Title { get; set; }

        [ConditionMapping("DELIVER_TIME", UtcTimeToLocal = true, Operation = ">=")]
        public DateTime DeliverTimeStart { get; set; }

        [ConditionMapping("DELIVER_TIME", UtcTimeToLocal = true, Operation = "<", AdjustDays = 1)]
        public DateTime DeliverTimeEnd { get; set; }

        [ConditionMapping("SEND_TO_USER")]
        public string SendToUser { get; set; }

        [ConditionMapping("STATUS")]
        public TaskStatus Status { get; set; }

        [NoMapping]
        public PageRequestParams PageParams
        {
            get;
            set;
        }

        [NoMapping]
        public OrderByRequestItem[] OrderBy
        {
            get;
            set;
        }
    }
}
