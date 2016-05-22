using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVerifies
{
    [Serializable]
    public class ConfirmDoorQueryModel: CustomerVerify
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

        /// <summary>
        /// 年级
        /// </summary>
        [DataMember]
        public string Grade { get; set; }

        /// <summary>
        /// 计划上门时间
        /// </summary>
        [DataMember]
        public DateTime PlanTime { get; set; }

        /// <summary>
        /// 咨询师
        /// </summary>
        [DataMember]
        public string StaffName { get; set; }
    }

    public class ConfirmDoorsQueryCollection : EditableDataObjectCollectionBase<ConfirmDoorQueryModel>
    {
        public ConfirmDoorsQueryCollection()
        {

        }
    }
}
