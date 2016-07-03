using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Common.Security;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerServices
{
    [Serializable]
    public class CustomerServiceModel : CustomerService
    {
        [DataMember]
        public string OrgName { get; set; }
        

        /// <summary>
        /// 学员姓名
        /// </summary>
        [DataMember]
        public string CustomerName { get; set; }

        /// <summary>
        /// 家长姓名
        /// </summary>
        [DataMember]
        public string ParentName { get; set; }

        /// <summary>
        /// 当前年级
        /// </summary>
        [DataMember]
        [ConstantCategory("c_codE_ABBR_CUSTOMER_GRADE")]
        public string Grade { get; set; }

        /// <summary>
        /// 校区反馈
        /// </summary>
        [DataMember]
        public string SchoolMemo { get; set; }

        /// <summary>
        /// 录音状态
        /// </summary>
        [DataMember]
        public string VoiceStatus { get; set; }

        public void FillAccepter(IUser user)
        {
            this.AccepterID = user.ID;
            this.AccepterName = user.Name;
            this.AccepterJobID = user.GetCurrentJob().ID;
            this.AccepterJobName = user.GetCurrentJob().Name;
            this.AcceptTime = MCS.Library.Net.SNTP.SNTPClient.AdjustedTime;
        }
    }
    [Serializable]
    public class CustomerServiceModelCollection : EditableDataObjectCollectionBase<CustomerServiceModel>
    {
        public CustomerServiceModelCollection()
        {

        }
    }
}