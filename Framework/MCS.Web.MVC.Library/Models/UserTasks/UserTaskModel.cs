using MCS.Library.Data.DataObjects;
using MCS.Library.SOA.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models.UserTasks
{
    [Serializable]
    [DataContract]
    public class UserTaskModel : UserTask
    {
    }

    [Serializable]
    [DataContract]
    public class UserTaskModelCollection : EditableDataObjectCollectionBase<UserTaskModel>
    {
    }
}
