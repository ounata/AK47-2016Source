using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
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
        public Phone PrimaryPhone
        {
            get;
            set;
        }

        /// <summary>
        /// 辅助联系方式
        /// </summary>
        [NoMapping]
        [DataMember]
        public Phone SecondaryPhone
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
                string referralStaff = this.ReferralStaffName;

                if (this.ReferralStaffJobName.IsNotEmpty())
                {
                    referralStaff += "(" + this.ReferralStaffJobName;

                    if (this.ReferralStaffOACode.IsNotEmpty())
                        referralStaff += " " + this.ReferralStaffOACode;

                    referralStaff += ")";
                }
                return referralStaff;
            }
        }

        public string ToSearchContent()
        {
            StringBuilder strB = new StringBuilder();

            this.CustomerName.IsNotEmpty(s => strB.AppendWithSplitChars(s));
            this.CustomerCode.IsNotEmpty(s => strB.AppendWithSplitChars(s));

            this.PrimaryPhone.IsNotNull(p => p.IsValidNumber((pi, phoneType) => strB.AppendWithSplitChars(pi.ToPhoneNumber())));
            this.SecondaryPhone.IsNotNull(p => p.IsValidNumber((pi, phoneType) => strB.AppendWithSplitChars(pi.ToPhoneNumber())));

            return strB.ToString();
        }
    }
}