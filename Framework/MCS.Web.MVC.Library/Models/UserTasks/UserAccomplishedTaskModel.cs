using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.UserTasks
{
    /// <summary>
    /// 已办的实体
    /// </summary>
    [Serializable]
    [DataContract]
    public class UserAccomplishedTaskModel : UserTask
    {
        /// <summary>
        /// 当前办理人
        /// </summary>
        [DataMember]
        public string CurrentUsers
        {
            get;
            set;
        }

        /// <summary>
        /// 当前流程状态
        /// </summary>
        [DataMember]
        public WfProcessStatus ProcessStatus
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class UserAccomplishedTaskModelCollection : EditableDataObjectCollectionBase<UserAccomplishedTaskModel>
    {
    }
}
