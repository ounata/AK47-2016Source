using System;
using System.Linq;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using PPTS.Data.Customers;
using PPTS.Data.Common.Adapters;

namespace PPTS.WebAPI.Customers.Executors
{
    /// <summary>
    /// 
    /// </summary>
    [DataExecutorDescription("新增潜在客户")]
    public class AddPotentialCustomerExecutor : PPTSEditCustomerExecutorBase<CreatablePortentialCustomerModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public AddPotentialCustomerExecutor(CreatablePortentialCustomerModel model)
            : base(model, null)
        {
            model.NullCheck("model");
            model.Customer.NullCheck("Customer");
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
                    parent = ParentAdapter.Instance.Load(builder => builder.AppendItem("IDType", (byte)this.Model.Parent.IDType).AppendItem("IDNumber", this.Model.Parent.IDNumber), DateTime.MinValue).FirstOrDefault();
                    if (parent != null)
                    {
                        if (parent.ParentID == this.Model.Parent.ParentID)
                            parent = null;
                        else
                            ownerID = parent.ParentID;
                    }
                }
            }
            if (!string.IsNullOrEmpty(ownerID))
            {
                CustomerParentRelation relation = CustomerParentRelationAdapter.Instance.Load(builder => builder.AppendItem("ParentID", ownerID), DateTime.MinValue).FirstOrDefault();
                try
                {
                    PotentialCustomer customer = PotentialCustomerAdapter.Instance.Load(relation.CustomerID);
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
            // 转介绍员工OA
            if (!string.IsNullOrEmpty(this.Model.Customer.ReferralStaffJobID))
            {
                PPTSJob referralJob = OGUExtensions.GetPPTSJobByJobID(this.Model.Customer.ReferralStaffJobID);
                if (referralJob == null)
                    throw new ApplicationException("输入的转介绍员工OA不正确！");
            }
            // 转介绍学员编号
            if (!string.IsNullOrEmpty(this.Model.Customer.ReferralCustomerCode))
            {
                Customer referralCustomer = CustomerAdapter.Instance.LoadByCustomerCode(this.Model.Customer.ReferralCustomerCode);
                if (referralCustomer == null)
                    throw new ApplicationException("输入的转介绍学员编号不正确！");
            }
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            // 更新家长信息
            Model.Parent.FillCreator().FillModifier();
            ParentAdapter.Instance.UpdateInContext(this.Model.Parent);

            // 更新学生信息
            this.Model.Customer.FillCreator().FillModifier();
            this.Model.Customer.Grade = this.Model.Customer.EntranceGrade;
            this.Model.Customer.CreatorJobType = DeluxeIdentity.CurrentUser.GetCurrentJob().JobType;
            if (!string.IsNullOrEmpty(this.Model.Customer.ReferralStaffJobID))
            {
                PPTSJob referralJob = OGUExtensions.GetPPTSJobByJobID(this.Model.Customer.ReferralStaffJobID);
                if (referralJob != null)
                {
                    IUser referralUser = OGUExtensions.GetUserByOAName(this.Model.Customer.ReferralStaffOACode);
                    this.Model.Customer.ReferralStaffID = referralUser == null ? "" : referralUser.ID;
                    this.Model.Customer.ReferralStaffName = referralUser == null ? "" : referralUser.Name;
                    this.Model.Customer.ReferralStaffJobName = referralJob.JobName;
                }
            }
            if (!string.IsNullOrEmpty(this.Model.Customer.ReferralCustomerCode))
            {
                Customer referralCustomer = CustomerAdapter.Instance.LoadByCustomerCode(this.Model.Customer.ReferralCustomerCode);
                this.Model.Customer.ReferralCustomerID = referralCustomer == null ? "" : referralCustomer.CustomerID;
                this.Model.Customer.ReferralCustomerName = referralCustomer == null ? "" : referralCustomer.CustomerName;
            }
            PotentialCustomerAdapter.Instance.UpdateInContext(this.Model.Customer);

            // 学员学校信息
            if (!string.IsNullOrEmpty(this.Model.Customer.SchoolID))
            {
                CustomerSchoolRelation schoolRelation = new CustomerSchoolRelation()
                {
                    SchoolID = this.Model.Customer.SchoolID,
                    CustomerID = this.Model.Customer.CustomerID
                };
                schoolRelation.FillCreator().FillModifier();
                CustomerSchoolRelationAdapter.Instance.UpdateInContext(schoolRelation);
            }

            // 更新亲属关系
            CustomerParentRelation relation = this.Model.ToRelation().FillCreator().FillModifier();
            CustomerParentRelationAdapter.Instance.UpdateInContext(relation);

            // 联系方式
            PhoneAdapter.Instance.UpdateByOwnerIDInContext(this.Model.Parent.ParentID, this.Model.Parent.ToPhones(this.Model.Parent.ParentID).FillCreatorList());
            PhoneAdapter.Instance.UpdateByOwnerIDInContext(this.Model.Customer.CustomerID, this.Model.Customer.ToPhones(this.Model.Customer.CustomerID).FillCreatorList());

            // 建档人信息
            CustomerStaffRelation staffRelation = new CustomerStaffRelation()
            {
                CustomerID = this.Model.Customer.CustomerID,
                RelationType = CustomerRelationType.Creator,
                StaffID = DeluxeIdentity.CurrentUser.ID,
                StaffName = DeluxeIdentity.CurrentUser.Name,
                StaffJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID,
                StaffJobName = DeluxeIdentity.CurrentUser.GetCurrentJob().JobName,
                StaffJobOrgID = DeluxeIdentity.CurrentUser.GetCurrentJob().Organization().ID,
                StaffJobOrgName = DeluxeIdentity.CurrentUser.GetCurrentJob().Organization().GetShortName()
            }.FillCreator();
            CustomerStaffRelationAdapter.Instance.UpdateInContext(staffRelation);

            // 归属关系
            switch (this.Model.Customer.CreatorJobType)
            {
                // 咨询关系-校教育咨询师
                case JobTypeDefine.Consultant:
                    staffRelation.RelationType = CustomerRelationType.Consultant;
                    break;
                /* 学管关系-校学习管理师建档，默认的当前归属人为空
                case JobTypeDefine.Educator:
                    staffRelation.RelationType = CustomerRelationType.Educator;
                    break;
                */
                // 电销关系-总坐席代表 分呼叫中心专员
                case JobTypeDefine.Callcenter:
                    staffRelation.RelationType = CustomerRelationType.Callcenter;
                    break;
                // 市场关系-校市场专员、分市场专员
                case JobTypeDefine.Marketing:
                    staffRelation.RelationType = CustomerRelationType.Market;
                    break;
            }
            if (staffRelation.RelationType != CustomerRelationType.Creator)
                CustomerStaffRelationAdapter.Instance.UpdateInContext(staffRelation);

            // 转介绍学员
            if (!string.IsNullOrEmpty(this.Model.Customer.ReferralCustomerCode))
            {
                Customer referralCustomer = CustomerAdapter.Instance.LoadByCustomerCode(this.Model.Customer.ReferralCustomerCode);
                referralCustomer.FillModifier();
                GenericCustomerAdapter<Customer, CustomerCollection>.Instance.UpdateReferralSummaryInContext(referralCustomer);
            }
            // 全文检索
            CustomerFulltextInfo customerFullText = CustomerFulltextInfo.Create(this.Model.Customer.CustomerID, CustomerFulltextInfo.PotentialCustomersType, this.Model.Customer.Status);
            customerFullText.FillCreator();
            customerFullText.CustomerSearchContent = this.Model.Customer.ToSearchContent();
            customerFullText.ParentSearchContent = this.Model.Parent.ToSearchContent();
            CustomerFulltextInfoAdapter.Instance.UpdateInContext(customerFullText);

            CustomerFulltextInfo parentFullText = CustomerFulltextInfo.Create(this.Model.Parent.ParentID, CustomerFulltextInfo.ParentsType);
            parentFullText.FillCreator();
            parentFullText.CustomerSearchContent = this.Model.Customer.ToSearchContent();
            parentFullText.ParentSearchContent = this.Model.Parent.ToSearchContent();
            CustomerFulltextInfoAdapter.Instance.UpdateInContext(parentFullText);
        }

        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            #region 生成数据权限范围数据
            PPTS.Data.Common.Authorization.ScopeAuthorization<PotentialCustomer>
               .GetInstance(PPTS.Data.Customers.ConnectionDefine.PPTSCustomerConnectionName)
               .UpdateAuthInContext(DeluxeIdentity.CurrentUser.GetCurrentJob()
               , DeluxeIdentity.CurrentUser.GetCurrentJob().Organization()
               , this.Model.Customer.CustomerID
               , PPTS.Data.Common.Authorization.RelationType.Owner);
            #endregion 生成数据权限范围数据

            return base.DoOperation(context);
        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);

            context.Logs.ForEach(log => log.ResourceID = this.Model.Customer.CustomerID);
        }
    }
}