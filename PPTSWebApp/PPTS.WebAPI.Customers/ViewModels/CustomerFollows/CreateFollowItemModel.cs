using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerFollows
{
    public class CreateFollowItemModel
    {
        /// <summary>
        /// 跟进ID
        /// </summary>
        [NoMapping]
        [DataMember]
        public string FollowID
        {
            set;
            get;
        }
    }
}
