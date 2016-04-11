﻿using System.Collections.Generic;
using MCS.Library.Core;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class CreatablePortentialCustomerModel
    {
        [ObjectValidator]
        public PotentialCustomerModel Customer
        {
            get;
            set;
        }

        [ObjectValidator]
        public ParentModel PrimaryParent
        {
            get;
            set;
        }

        /// <summary>
        /// 学生对家长的亲属关系(C_CODE_ABBR_CHILDMALEDICTIONARY,C_CODE_ABBR_CHILDFEMALEDICTIONARY)
        /// </summary>
        [ConstantCategory("C_CODE_ABBR_CHILDMALEDICTIONARY")]
        [ConstantCategory("C_CODE_ABBR_CHILDFEMALEDICTIONARY")]
        public int CustomerRole
        {
            get;
            set;
        }

        /// <summary>
        /// 家长对学生的亲属关系(C_CODE_ABBR_PARENTMALEDICTIONARY,C_CODE_ABBR_PARENTFEMALEDICTIONARY)
        /// </summary>
        [ConstantCategory("C_CODE_ABBR_PARENTMALEDICTIONARY")]
        [ConstantCategory("C_CODE_ABBR_PARENTFEMALEDICTIONARY")]
        public int ParentRole
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        public CreatablePortentialCustomerModel()
        {
            this.Customer = new PotentialCustomerModel { CustomerID = UuidHelper.NewUuidString(), IDType = IDTypeDefine.IDCard,  VipType = VipTypeDefine.NoVip };
            this.PrimaryParent = new ParentModel { ParentID = UuidHelper.NewUuidString(), IDType = IDTypeDefine.IDCard };
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }

        public CustomerParentRelation ToRelation()
        {
            return new CustomerParentRelation
            {
                CustomerID = this.Customer.CustomerID,
                ParentID = this.PrimaryParent.ParentID,
                CustomerRole = this.CustomerRole,
                ParentRole = this.ParentRole,
                IsPrimary = true
            };
        }
    }
}