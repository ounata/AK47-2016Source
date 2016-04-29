using System;
using System.Runtime.Serialization;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    public class CustomerModel : IBasicCustomerInfo
    {
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
        /// 出生年月
        /// </summary>
        public DateTime Birthday
        {
            set;
            get;
        }

        /// <summary>
        /// 性别
        /// </summary>
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
        /// 家长姓名
        /// </summary>
        public string ParentName
        {
            set;
            get;
        }

        /// <summary>
        /// 家长与学员关系
        /// </summary>
        [ConstantCategory("C_CODE_ABBR_PARENTDICTIONARY")]
        public string ParentRole
        {
            set;
            get;
        }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber
        {
            set;
            get;
        }

        /// <summary>
        /// 在读学校ID
        /// </summary>
        public string SchoolID
        {
            set;
            get;
        }

        /// <summary>
        /// 在读学校
        /// </summary>
        public string SchoolName
        {
            set;
            get;
        }

        /// <summary>
        /// 是否潜客
        /// </summary>
        public bool IsPotential
        {
            set;
            get;
        }

        public static CustomerModel Load(string customerID)
        {
            CustomerModel model = new CustomerModel();
            Customer customer = CustomerAdapter.Instance.Load(customerID);
            if (customer != null)
            {
                model.CampusID = customer.CampusID;
                model.CampusName = customer.CampusName;
                model.CustomerID = customer.CustomerID;
                model.CustomerCode = customer.CustomerCode;
                model.CustomerName = customer.CustomerName;
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
                    model.CampusID = potential.OrgID;
                    model.CampusName = potential.OrgName;
                    model.CustomerID = potential.CustomerID;
                    model.CustomerCode = potential.CustomerCode;
                    model.CustomerName = potential.CustomerName;
                    model.Birthday = customer.Birthday;
                    model.Gender = potential.Gender;
                    model.Grade = potential.Grade;
                    model.SchoolID = potential.SchoolID;
                    model.IsPotential = true;
                }
            }
            Parent parent =  ParentAdapter.Instance.LoadPrimaryParentInContext(customerID);
            if (parent != null)
            {
                model.ParentName = parent.ParentName;
                Phone phone = PhoneAdapter.Instance.LoadPrimaryPhoneByOwnerID(parent.ParentID);
                if (phone != null)
                    model.PhoneNumber = phone.PhoneNumber;
                CustomerParentRelation relation = CustomerParentRelationAdapter.Instance.LoadPrimary(customerID, parent.ParentID);
                if (relation != null)
                    model.ParentRole = relation.ParentRole.ToString();
            }
            School school = SchoolAdapter.Instance.Load(model.SchoolID);
            if(school != null)
            {
                model.SchoolName = school.SchoolName;
            }
            return model;
        }    
    }
}