using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerFollows
{
    [Serializable]
    public class FollowQueryModel: CustomerFollow
    {
        /// <summary>
        /// 学员姓名
        /// </summary>
        [DataMember]
        public string CustomerName { get; set; }

        /// <summary>
        /// 学员编号
        /// </summary>
        [DataMember]
        public string CustomerCode { get; set; }

        /// <summary>
        /// 家长姓名
        /// </summary>
        [DataMember]
        public string ParentName { get; set; }
    }

    public class CustomerFollowQueryCollection : EditableDataObjectCollectionBase<FollowQueryModel>
    {
        public CustomerFollowQueryCollection()
        {

        }
    }
}
