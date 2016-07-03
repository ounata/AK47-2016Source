using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 学员监护人
    /// </summary>
    public class CustomerGuardian
    {
        /// <summary>
        /// 详细地址描述
        /// </summary>
        public string AddressDetail
        {
            get;
            set;
        }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// 证件类型(C_CODE_ABBR_BO_Customer_CertificateType)
        /// </summary>
        public string IDType
        {
            get;
            set;
        }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string IDNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 宅电
        /// </summary>
        public string HomePhone { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone { get; set; }

        public CustomerGuardian(string customerID)
        {
            CustomerParentRelationCollection retCPRA = CustomerParentRelationAdapter.Instance.Load(customerID);
            if (retCPRA == null)
                return;
            CustomerParentRelation primaryGuardian = retCPRA.Where(p => p.IsPrimary).FirstOrDefault();
            if (primaryGuardian == null)
                return;
            Parent pt = ParentAdapter.Instance.Load(primaryGuardian.ParentID);
            if (pt == null)
                return;

            AddressDetail = pt.AddressDetail;
            IDNumber = pt.IDNumber;
            ConstantEntity ce = ConstantAdapter.Instance.Get(ConstantCategoryConsts.CertificateType, ((int)pt.IDType).ToString());
            if (ce != null)
                IDType = ce.Value;
            Email = pt.Email;

            PhoneCollection pCollection = PhoneAdapter.Instance.LoadByOwnerID(pt.ParentID);
            if (pCollection == null)
                return;
            var homePhone = pCollection.Where(p => p.PhoneType == Data.Customers.PhoneTypeDefine.HomePhone && p.IsPrimary).FirstOrDefault();
            if (homePhone != null)
                HomePhone = homePhone.PhoneNumber;

            var mobilePhone = pCollection.Where(p => p.PhoneType == Data.Customers.PhoneTypeDefine.MobilePhone && p.IsPrimary).FirstOrDefault();
            if (mobilePhone != null)
                MobilePhone = mobilePhone.PhoneNumber;
        }
    }
}