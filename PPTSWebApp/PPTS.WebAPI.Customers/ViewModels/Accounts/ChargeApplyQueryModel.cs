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

        /// <summary>
        /// 是否潜客
        /// </summary>
        [DataMember]
        public bool IsPotential
        {
            get
            {
                if (this.PayStatus == PayStatusDefine.Unpay && this.ChargeType == ChargeTypeDefine.New)
                    return true;
                return false;
            }
        }
    }

    [Serializable]
    [DataContract]
    public class ChargeApplyQueryModelCollection : EditableDataObjectCollectionBase<ChargeApplyQueryModel>
    {

    }
}