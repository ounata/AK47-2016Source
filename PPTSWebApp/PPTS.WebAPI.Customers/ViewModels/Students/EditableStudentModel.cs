using MCS.Library.Core;
using MCS.Library.Validation;
using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    public class EditableStudentModel
    {
        public StudentModel Customer
        {
            get;
            set;
        }

        public Parent Parent
        {
            get;
            set;
        }

        public CustomerStaffRelationCollection CustomerStaffRelations
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        public EditableStudentModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }

        public static EditableStudentModel Load(string id)
        {
            EditableStudentModel result = new EditableStudentModel();

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Customer));

            GenericCustomerAdapter<StudentModel, List<StudentModel>>.Instance.LoadInContext(id, customer => result.Customer = customer);

            GenericParentAdapter<ParentModel, List<ParentModel>>.Instance.LoadPrimaryParentInContext(id, parent => result.Parent = parent);

            CustomerStaffRelationAdapter.Instance.LoadByCustomerIDInContext(id, relations => result.CustomerStaffRelations = relations);

            PhoneAdapter.Instance.LoadByOwnerIDInContext(id, phones => result.Customer.FillFromPhones(phones));

            PhoneAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            return result;
        }
    }

    public class EditableStudentParentModel
    {
        public Customer Customer { get; set; }

        public ParentModel Parent { get; set; }

        public CustomerParentRelation CustomerParentRelation { get; set; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

        public EditableStudentParentModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }
    }

    [Serializable]
    public class CreatableParentModel
    {
        [ObjectValidator]
        public StudentModel Customer
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

        public CreatableParentModel()
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