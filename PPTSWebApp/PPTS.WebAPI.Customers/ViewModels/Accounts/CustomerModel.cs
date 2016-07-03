using System;
using System.Runtime.Serialization;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers;
using MCS.Library.Principal;
using MCS.Library.OGUPermission;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 学员信息模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class CustomerModel : IBasicCustomerInfo
    {
        [DataMember]
        public string OrgID
        {
            set;
            get;
        }

        [DataMember]
        public string OrgName
        {
            set;
            get;
        }

        [DataMember]
        public OrgTypeDefine OrgType
        {
            set;
            get;
        }

        /// <summary>
        /// 所在校区ID
        /// </summary>
        [DataMember]
        public string CampusID
        {
            get;
            set;
        }

        /// <summary>
        /// 所在校区名称
        /// </summary>
        [DataMember]
        public string CampusName
        {
            get;
            set;
        }

        /// <summary>
        /// 学员ID
        /// </summary>
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 学员编码
        /// </summary>
        [DataMember]
        public string CustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 学员名称
        /// </summary>
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 客户状态
        /// </summary>
        [DataMember]
        public CustomerStatus Status
        {
            set;
            get;
        }

        /// <summary>
        /// 出生年月
        /// </summary>
        [DataMember]
        public DateTime Birthday
        {
            set;
            get;
        }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_GENDER")]
        public GenderType Gender
        {
            set;
            get;
        }
        
        /// <summary>
        /// 当前年级
        /// </summary>
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_GRADE")]
        public string Grade
        {
            get;
            set;
        }

        /// <summary>
        /// 家长ID
        /// </summary>
        public string ParentID
        {
            set;
            get;
        }

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
        /// 家长与学员关系
        /// </summary>
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_PARENTDICTIONARY")]
        public string ParentRole
        {
            set;
            get;
        }

        /// <summary>
        /// 家长身份证号
        /// </summary>
        [DataMember]
        public string ParentIDNumber
        {
            set;
            get;
        }

        /// <summary>
        /// 电话号码
        /// </summary>
        [DataMember]
        public string PhoneNumber
        {
            set;
            get;
        }

        /// <summary>
        /// 在读学校ID
        /// </summary>
        [DataMember]
        public string SchoolID
        {
            set;
            get;
        }

        /// <summary>
        /// 在读学校
        /// </summary>
        [DataMember]
        public string SchoolName
        {
            set;
            get;
        }

        /// <summary>
        /// 是否潜客
        /// </summary>
        [DataMember]
        public bool IsPotential
        {
            set;
            get;
        }

        public static CustomerModel Load(string customerID, bool? isPotential)
        {
            CustomerModel model = new CustomerModel();
            Customer customer = CustomerAdapter.Instance.Load(customerID);
            if (customer != null)
            {
                model.OrgID = customer.CampusID;
                model.OrgName = customer.CampusName;
                model.OrgType = OrgTypeDefine.Campus;
                model.CampusID = customer.CampusID;
                model.CampusName = customer.CampusName;
                model.CustomerID = customer.CustomerID;
                model.CustomerCode = customer.CustomerCode;
                model.CustomerName = customer.CustomerName;
                model.Status = customer.Status;
                model.Birthday = customer.Birthday;
                model.Gender = customer.Gender;
                model.Grade = customer.Grade;
                model.SchoolID = customer.SchoolID;
            }
            else
            {
                PotentialCustomer potential = PotentialCustomerAdapter.Instance.Load(customerID);
                if (potential != null)
                {
                    model.OrgID = potential.OrgID;
                    model.OrgName = potential.OrgName;
                    model.OrgType = potential.OrgType;
                    model.CampusID = potential.CampusID;
                    model.CampusName = potential.CampusName;
                    model.CustomerID = potential.CustomerID;
                    model.CustomerCode = potential.CustomerCode;
                    model.CustomerName = potential.CustomerName;
                    model.Status = potential.Status;
                    model.Birthday = potential.Birthday;
                    model.Gender = potential.Gender;
                    model.Grade = potential.Grade;
                    model.SchoolID = potential.SchoolID;
                    model.IsPotential = true;
                }
            }
            CustomerParentRelation relation = CustomerParentRelationAdapter.Instance.LoadPrimary(customerID);
            if (relation != null)
            {
                model.ParentRole = relation.ParentRole;
                Parent parent = ParentAdapter.Instance.Load(relation.ParentID);
                if (parent != null)
                {
                    model.ParentID = parent.ParentID;
                    model.ParentName = parent.ParentName;
                    model.ParentIDNumber = parent.IDNumber;
                }
                Phone phone = PhoneAdapter.Instance.LoadPrimaryPhoneByOwnerID(relation.ParentID);
                if (phone != null)
                {
                    model.PhoneNumber = CommonHelper.FilterPhoneNumber(phone.PhoneNumber, DeluxeIdentity.CurrentUser);
                }
            }
            School school = SchoolAdapter.Instance.Load(model.SchoolID);
            if (school != null)
            {
                model.SchoolName = school.SchoolName;
            }
            return model;
        }

        public static CustomerModel Load(string customerID)
        {
            return Load(customerID, null);
        }

        public static CustomerModel LoadBy(string customerCode)
        {
            CustomerModel model = new CustomerModel();
            Customer customer = CustomerAdapter.Instance.LoadByCustomerCode(customerCode);
            if (customer != null)
            {
                model.CampusID = customer.CampusID;
                model.CampusName = customer.CampusName;
                model.CustomerID = customer.CustomerID;
                model.CustomerCode = customer.CustomerCode;
                model.CustomerName = customer.CustomerName;
                model.Status = customer.Status;
                model.Birthday = customer.Birthday;
                model.Gender = customer.Gender;
                model.Grade = customer.Grade;
                model.SchoolID = customer.SchoolID;
            }
            return model;
        }
    }
}