using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
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
    public class ParentModel : Parent, IContactPhoneNumbers
    {
        [NoMapping]
        [DataMember]
        public Phone PrimaryPhone
        {
            get;
            set;
        }

        [NoMapping]
        [DataMember]
        public Phone SecondaryPhone
        {
            get;
            set;
        }

        public string ToSearchContent()
        {
            StringBuilder strB = new StringBuilder();

            this.ParentName.IsNotEmpty(s => strB.AppendWithSplitChars(s));
            this.ParentCode.IsNotEmpty(s => strB.AppendWithSplitChars(s));

            this.PrimaryPhone.IsNotNull(p => p.IsValidNumber((pi, phoneType) => strB.AppendWithSplitChars(pi.ToPhoneNumber())));
            this.SecondaryPhone.IsNotNull(p => p.IsValidNumber((pi, phoneType) => strB.AppendWithSplitChars(pi.ToPhoneNumber())));

            return strB.ToString();
        }
    }

    [Serializable]
    public class ParentModelCollection : EditableDataObjectCollectionBase<ParentModel>
    {
    }

    [Serializable]
    public class CreatablePotentialCustomerParentModel
    {
        [ObjectValidator]
        public PotentialCustomerModel Customer
        {
            get;
            set;
        }

        [ObjectValidator]
        public ParentModel Parent
        {
            get;
            set;
        }

        /// <summary>
        /// 学生对家长的亲属关系(C_CODE_ABBR_CHILDMALEDICTIONARY,C_CODE_ABBR_CHILDFEMALEDICTIONARY)
        /// </summary>
        [ConstantCategory("C_CODE_ABBR_CHILDMALEDICTIONARY")]
        [ConstantCategory("C_CODE_ABBR_CHILDFEMALEDICTIONARY")]
        public string CustomerRole
        {
            get;
            set;
        }

        /// <summary>
        /// 家长对学生的亲属关系(C_CODE_ABBR_PARENTMALEDICTIONARY,C_CODE_ABBR_PARENTFEMALEDICTIONARY)
        /// </summary>
        [ConstantCategory("C_CODE_ABBR_PARENTMALEDICTIONARY")]
        [ConstantCategory("C_CODE_ABBR_PARENTFEMALEDICTIONARY")]
        public string ParentRole
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        public CreatablePotentialCustomerParentModel()
        {
            this.Parent = new ParentModel { ParentID = UuidHelper.NewUuidString(), IDType = IDTypeDefine.IDCard };
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }

        public CustomerParentRelation ToRelation(bool isPrimary)
        {
            CustomerParentRelation relation = new CustomerParentRelation
            {
                CustomerID = this.Customer.CustomerID,
                ParentID = this.Parent.ParentID,
                CustomerRole = this.CustomerRole,
                ParentRole = this.ParentRole,
                IsPrimary = isPrimary
            };

            return relation;
        }
    }
}