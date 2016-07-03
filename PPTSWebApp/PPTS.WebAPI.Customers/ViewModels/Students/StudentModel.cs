using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    [Serializable]
    public class StudentModel : Customer, IContactPhoneNumbers
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
        [NoMapping]
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

        /// <summary>
        /// 归属教师姓名
        /// </summary>
        [NoMapping]
        [DataMember]
        public string BelongTeacherNames { get; set; }

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