using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using PPTS.Data.Common;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Entities;
using System;
using System.Runtime.Serialization;
using System.Text;

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
        public Phone PrimaryPhone { get; set; }
        /// <summary>
        /// 辅助联系方式
        /// </summary>
        [DataMember]
        public Phone SecondaryPhone { get; set; }
        /// <summary>
        /// 建档部门
        /// </summary>
        [DataMember]
        public string CreatorOrgName { get; set; }
        /// <summary>
        /// 所在学校
        /// </summary>
        [DataMember]
        public string SchoolName { get; set; }

        public string ToSearchContent()
        {
            StringBuilder strB = new StringBuilder();

            this.CustomerName.IsNotEmpty(s => strB.AppendWithSplitChars(s));
            this.CustomerCode.IsNotEmpty(s => strB.AppendWithSplitChars(s));

            this.PrimaryPhone.IsNotNull(p => p.IsValidNumber((pi, phoneType) => strB.AppendWithSplitChars(pi.ToPhoneNumber())));
            this.SecondaryPhone.IsNotNull(p => p.IsValidNumber((pi, phoneType) => strB.AppendWithSplitChars(pi.ToPhoneNumber())));

            return strB.ToString();
        }

        public static PPTSJobCollection GetStaffJobs(string staffOA)
        {
            PPTSJobCollection jobs = new PPTSJobCollection();
            if (!string.IsNullOrEmpty(staffOA))
            {
                IUser user = OGUExtensions.GetUserByOAName(staffOA);
                if (user != null)
                    jobs = user.Jobs();
            }
            return jobs;
        }

    }

}