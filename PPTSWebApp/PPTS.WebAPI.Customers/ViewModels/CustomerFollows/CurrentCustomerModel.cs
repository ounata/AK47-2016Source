using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerFollows
{
    /// <summary>
    /// 当前学员实体类
    /// </summary>
    [Serializable]
    public class CurrentCustomerModel
    {
        /// <summary>
        /// 所在校区ID
        /// </summary>
        [DataMember]
        public string OrgID
        {
            get;
            set;
        }

        /// <summary>
        /// 所在校区名称
        /// </summary>
        [DataMember]
        public string OrgName
        {
            get;
            set;
        }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        [DataMember]
        public OrgTypeDefine OrgType
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
        public CustomerStatus Status
        {
            set;
            get;
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
        
        public static CurrentCustomerModel Load(string customerID)
        {
            CurrentCustomerModel model = new CurrentCustomerModel();
            Customer customer = CustomerAdapter.Instance.Load(customerID);
            if (customer != null)
            {
                model.OrgID = customer.CampusID;
                model.OrgName = customer.CampusName;
                model.OrgType = OrgTypeDefine.Campus;
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
                    //model.OrgType = potential.OrgType;
                    model.OrgType = OrgTypeDefine.Campus;
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
            Parent parent = ParentAdapter.Instance.LoadPrimaryParentInContext(customerID);
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
            if (school != null)
            {
                model.SchoolName = school.SchoolName;
            }
            return model;
        }
    }
}
