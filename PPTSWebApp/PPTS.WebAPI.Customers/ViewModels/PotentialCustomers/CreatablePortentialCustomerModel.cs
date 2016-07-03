using System.Collections.Generic;
using MCS.Library.Core;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    public class CreatablePortentialCustomerModel
    {
        [ObjectValidator]
        public PotentialCustomerModel Customer { get; set; }

        [ObjectValidator]
        public ParentModel Parent { get; set; }

        /// <summary>
        /// 学生对家长的亲属关系(C_CODE_ABBR_CHILDMALEDICTIONARY,C_CODE_ABBR_CHILDFEMALEDICTIONARY)
        /// </summary>
        [ConstantCategory("C_CODE_ABBR_CHILDMALEDICTIONARY")]
        [ConstantCategory("C_CODE_ABBR_CHILDFEMALEDICTIONARY")]
        public string CustomerRole { get; set; }

        /// <summary>
        /// 家长对学生的亲属关系(C_CODE_ABBR_PARENTMALEDICTIONARY,C_CODE_ABBR_PARENTFEMALEDICTIONARY)
        /// </summary>
        [ConstantCategory("C_CODE_ABBR_PARENTMALEDICTIONARY")]
        [ConstantCategory("C_CODE_ABBR_PARENTFEMALEDICTIONARY")]
        public string ParentRole { get; set; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

        public CreatablePortentialCustomerModel()
        {
            this.Customer = new PotentialCustomerModel { CustomerID = UuidHelper.NewUuidString(), IDType = IDTypeDefine.IDCard, VipType = VipTypeDefine.NoVip };
            this.Customer.PrimaryPhone = string.Empty.ToPhone(this.Customer.CustomerID, true);
            this.Customer.SecondaryPhone = string.Empty.ToPhone(this.Customer.CustomerID, false);

            this.Parent = new ParentModel { ParentID = UuidHelper.NewUuidString(), IDType = IDTypeDefine.IDCard };
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }

        public CustomerParentRelation ToRelation()
        {
            CustomerParentRelation relation = new CustomerParentRelation
            {
                CustomerID = this.Customer.CustomerID,
                ParentID = this.Parent.ParentID,
                CustomerRole = this.CustomerRole,
                ParentRole = this.ParentRole,
                IsPrimary = true
            };

            return relation;
        }
    }
}