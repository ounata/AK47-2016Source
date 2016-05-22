using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 缴费申请单模型
    /// </summary>
    [Serializable]
    public class ChargeApplyQueryModel : AccountChargeApply
    {
        /// <summary>
        /// 家长姓名
        /// </summary>
        [DataMember]
        public string ParentName
        {
            set;
            get;
        }

        /// <summary>
        /// 家长电话
        /// </summary>
        [DataMember]
        public string PhoneNumber
        {
            set;
            get;
        }

        /// <summary>
        /// 是否能够审核
        /// </summary>
        [DataMember]
        public bool CanAudit
        {
            get
            {
                return  this.PayStatus == PayStatusDefine.Paid
                    && this.AuditStatus == ChargeAuditStatus.UnAudit;
            }
        }

    }

    [Serializable]
    [DataContract]
    public class ChargeApplyQueryModelCollection : EditableDataObjectCollectionBase<ChargeApplyQueryModel>
    {

    }
}