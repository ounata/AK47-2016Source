using System;
using System.Linq;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.Students;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("编辑学员家长")]
    public class EditableStudentParentExecutor : PPTSEditCustomerExecutorBase<EditableStudentParentModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public EditableStudentParentExecutor(EditableStudentParentModel model)
            : base(model, null)
        {
            model.NullCheck("model");
            model.Parent.NullCheck("Parent");
        }

        protected override void Validate()
        {
            base.Validate();
            string error = string.Empty;
            // 判断家长手机号是否重复
            string ownerID = string.Empty;
            // 主要联系方式
            Phone phone = null, primaryPhone = null, secondaryPhone = null;
            if (this.Model.Parent.PrimaryPhone != null)
            {
                primaryPhone = PhoneAdapter.Instance.LoadByPhoneNumber(this.Model.Parent.PrimaryPhone.PhoneNumber);
                if (primaryPhone != null)
                {
                    if (primaryPhone.OwnerID == this.Model.Parent.ParentID)
                    {
                        primaryPhone = null;
                    }
                    else
                    {
                        phone = primaryPhone;
                        ownerID = primaryPhone.OwnerID;
                    }
                }
            }
            // 辅助联系方式
            if (string.IsNullOrEmpty(ownerID))
            {
                if (this.Model.Parent.SecondaryPhone != null)
                {
                    secondaryPhone = PhoneAdapter.Instance.LoadByPhoneNumber(this.Model.Parent.SecondaryPhone.PhoneNumber);
                    if (secondaryPhone != null)
                    {
                        if (secondaryPhone.OwnerID == this.Model.Parent.ParentID)
                        {
                            secondaryPhone = null;
                        }
                        else
                        {
                            phone = secondaryPhone;
                            ownerID = secondaryPhone.OwnerID;
                        }
                    }
                }
            }
            // 家长身份证号码
            Parent parent = null;
            if (string.IsNullOrEmpty(ownerID))
            {
                if (this.Model.Parent.IDType == IDTypeDefine.IDCard && !string.IsNullOrEmpty(this.Model.Parent.IDNumber))
                {
                    parent = ParentAdapter.Instance.Load(builder => builder.AppendItem("IDType", this.Model.Parent.IDType).AppendItem("IDNumber", this.Model.Parent.IDNumber), DateTime.MinValue).SingleOrDefault();
                    if (parent != null && parent.ParentID == this.Model.Parent.ParentID)
                        parent = null;
                    else
                        ownerID = parent.ParentID;
                }
            }
            if (!string.IsNullOrEmpty(ownerID))
            {
                CustomerParentRelation relation = CustomerParentRelationAdapter.Instance.Load(builder => builder.AppendItem("ParentID", ownerID), DateTime.MinValue).FirstOrDefault();
                try
                {
                    Customer customer = CustomerAdapter.Instance.Load(relation.CustomerID);
                    CustomerStaffRelationCollection staffRelations = CustomerStaffRelationAdapter.Instance.LoadByCustomerID(relation.CustomerID);
                    var creator = staffRelations.GetStaff(CustomerRelationType.Creator);
                    error = string.Format(
                        @"该{0}号码{1}已存在于系统中，与该号码相同的客户是：
                        <br />客户：{2}（{3}）
                        <br />建档人：{4}（{5}）
                        <br />当前所属部门/ 校区：{6}
                        <br />当前归属咨询师：{7}
                        <br />接触方式：{8}
                        <br />当前归属座席：{9} ",
                        phone != null ? "手机" : "身份证",
                        phone != null ? phone.PhoneNumber : this.Model.Parent.IDNumber,
                        customer.CustomerName, customer.CustomerCode, customer.CreatorName,
                        creator == null ? "" : creator.StaffJobName, 
                        customer.CampusName,
                        staffRelations.GetStaffName(CustomerRelationType.Consultant),
                        !string.IsNullOrEmpty(customer.ContactType) ? ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Customer))["C_CODE_ABBR_Customer_CRM_NewContactType"].Where(c => c.Key == Convert.ToString(customer.ContactType)).FirstOrDefault().Value : "",
                        staffRelations.GetStaff(CustomerRelationType.Callcenter));
                }
                catch
                {
                    error = string.Format(@"该{0}号码{1}已存在于系统中", phone != null ? "手机" : "身份证", phone != null ? phone.PhoneNumber : this.Model.Parent.IDNumber);
                }
                throw new ApplicationException(error);
            }
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            // 更新家长信息
            this.Model.Parent.FillModifier();
            ParentAdapter.Instance.UpdateInContext(this.Model.Parent);
            // 当前为主监护人，则更新其他家长主监护人信息为false
            if (this.Model.CustomerParentRelation.IsPrimary)
            {
                CustomerParentRelationCollection relations = CustomerParentRelationAdapter.Instance.Load(this.Model.Customer.CustomerID);
                foreach (CustomerParentRelation item in relations)
                {
                    if (item.ParentID != this.Model.CustomerParentRelation.ParentID)
                    {
                        item.IsPrimary = false;
                        CustomerParentRelationAdapter.Instance.UpdateInContext(item);
                    }
                }
            }
            CustomerParentRelationAdapter.Instance.UpdateInContext(this.Model.CustomerParentRelation);
            // 更新家长手机信息
            PhoneAdapter.Instance.UpdateByOwnerIDInContext(this.Model.Parent.ParentID, this.Model.Parent.ToPhones(this.Model.Parent.ParentID));
            // 全文检索
            CustomerFulltextInfo parentFullText = CustomerFulltextInfo.Create(this.Model.Parent.ParentID, CustomerFulltextInfo.ParentsType);
            parentFullText.ParentSearchContent = this.Model.Parent.ToSearchContent();
            CustomerFulltextInfoAdapter.Instance.UpdateParentSearchContentInContext(parentFullText);
        }

        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);

            context.Logs.ForEach(log => log.ResourceID = this.Model.Parent.ParentID);
        }
    }
}