using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    [Serializable]
    public class PotentialCustomerModel : PotentialCustomer, IContactPhoneNumbers
    {
        /// <summary>
        /// 主要联系方式
        /// </summary>
        [NoMapping]
        [DataMember]
        public string PrimaryPhone
        {
            get;
            set;
        }

        /// <summary>
        /// 辅助联系方式
        /// </summary>
        [NoMapping]
        [DataMember]
        public string SecondaryPhone
        {
            get;
            set;
        }

        /// <summary>
        /// 转介绍员工信息
        /// </summary>
        [NoMapping]
        [DataMember]
        public string ReferralStaffInfo
        {
            get
            {
                var referralStaff = this.ReferralStaffName;
                if (!String.IsNullOrEmpty(this.ReferralStaffJobName))
                {
                    referralStaff += "(" + this.ReferralStaffJobName;
                    if (!String.IsNullOrEmpty(this.ReferralStaffCode))
                    {
                        referralStaff += " " + this.ReferralStaffCode;
                    }
                    referralStaff += ")";
                }
                return referralStaff;
            }
        }
    }
}