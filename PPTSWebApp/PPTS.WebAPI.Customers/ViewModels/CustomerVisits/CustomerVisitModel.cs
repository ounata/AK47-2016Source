using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Common.Security;
using MCS.Library.OGUPermission;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVisits
{
    public class CustomerVisitModel: CustomerVisit
    {
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
        /// 学员编号
        /// </summary>
        [DataMember]
        public string CustomerCode { get; set; }

        [DataMember]
        public int SelectType { get; set; }

        public void FillAccepter(IUser user)
        {
            this.VisitorID = user.ID;
            this.VisitorName = user.Name;
            this.VisitorJobID = user.GetCurrentJob().ID;
            this.VisitorJobName = user.GetCurrentJob().Name;
        }
    }

    [Serializable]
    public class CustomerVisitModelCollection : EditableDataObjectCollectionBase<CustomerVisitModel>
    {
        public CustomerVisitModelCollection()
        {

        }
    }
}