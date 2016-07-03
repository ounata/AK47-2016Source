using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Linq;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("批量导入客户资源")]
    public class CustomerImportExecutor : PPTSEditCustomerExecutorBase<CustomerImportModel>
    {
        public CustomerImportExecutor(CustomerImportModel model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void Validate()
        {
            base.Validate();
            Phone phone = PhoneAdapter.Instance.LoadByPhoneNumber(Model.PhoneNumber);
            if (phone != null)
                throw new ApplicationException(string.Format("{0}手机号码已存在", Model.PhoneNumber));
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            // 潜客信息
            PotentialCustomerModel customer = new PotentialCustomerModel
            {
                CustomerID = UuidHelper.NewUuidString(),
                CustomerName = Model.CustomerName,
                Gender = Model.CustomerGender == "男" ? GenderType.Male : (Model.CustomerGender == "女" ? GenderType.Female : GenderType.Unknown),
                SubjectType = CustomerImportModel.GetSubjectType(Model.SubjectType),
                SchoolID = CustomerImportModel.GetSchoolID(Model.SchoolName.EncodeString()),
                EntranceGrade = CustomerImportModel.GetGradeID(Model.Grade),
                CreatorJobType = DeluxeIdentity.CurrentUser.GetCurrentJob().JobType,
                SourceMainType = Model.SourceMainType,
                SourceSubType = Model.SourceSubType,
                OrgID = Model.OrgID,
                OrgName = Model.OrgName,
                IDType = IDTypeDefine.IDCard,
                VipType = VipTypeDefine.NoVip
            }.FillCreator().FillModifier();
            customer.Grade = customer.EntranceGrade;
            if (!string.IsNullOrEmpty(Model.OrgType))
                customer.OrgType = (OrgTypeDefine)Enum.Parse(typeof(OrgTypeDefine), Model.OrgType);
            customer.CampusID = customer.OrgType == OrgTypeDefine.Campus ? customer.OrgID : "";
            customer.CampusName = customer.OrgType == OrgTypeDefine.Campus ? customer.OrgName : "";
            PotentialCustomerAdapter.Instance.UpdateInContext(customer);

            // 家长信息
            ParentModel parent = new ParentModel
            {
                ParentID = UuidHelper.NewUuidString(),
                ParentName = Model.ParentName,
                Gender = Model.ParentGender == "男" ? GenderType.Male : (Model.ParentGender == "女" ? GenderType.Female : GenderType.Unknown),
                IDType = IDTypeDefine.IDCard
            }.FillCreator().FillModifier();
            ParentAdapter.Instance.UpdateInContext(parent);

            // 家长联系方式
            Phone phone = new Phone
            {
                OwnerID = parent.ParentID,
                PhoneNumber = Model.PhoneNumber,
                IsPrimary = true
            }.FillCreator();
            PhoneAdapter.Instance.UpdateInContext(phone);

            // 亲属关系
            CustomerParentRelation relation = new CustomerParentRelation
            {
                CustomerID = customer.CustomerID,
                ParentID = parent.ParentID,
                IsPrimary = true
            }.FillCreator().FillModifier();
            CustomerParentRelationAdapter.Instance.UpdateInContext(relation);

            // 学员学校信息
            if (!string.IsNullOrEmpty(customer.SchoolID))
            {
                CustomerSchoolRelation schoolRelation = new CustomerSchoolRelation()
                {
                    SchoolID = customer.SchoolID,
                    CustomerID = customer.CustomerID
                };
                schoolRelation.FillCreator().FillModifier();
                CustomerSchoolRelationAdapter.Instance.UpdateInContext(schoolRelation);
            }

            // 建档人信息
            CustomerStaffRelation staffRelation = new CustomerStaffRelation()
            {
                CustomerID = customer.CustomerID,
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
            switch (customer.CreatorJobType)
            {
                // 市场体系导入：分呼叫中心专员
                case JobTypeDefine.Callcenter:
                    staffRelation.RelationType = CustomerRelationType.Callcenter;
                    break;
                // 呼叫中心体系导入：校市场专员、分市场专员
                case JobTypeDefine.Marketing:
                    staffRelation.RelationType = CustomerRelationType.Market;
                    break;
            }
            if (staffRelation.RelationType != CustomerRelationType.Creator)
                CustomerStaffRelationAdapter.Instance.UpdateInContext(staffRelation);

            // 全文检索
            CustomerFulltextInfo customerFullText = CustomerFulltextInfo.Create(customer.CustomerID, CustomerFulltextInfo.PotentialCustomersType);
            customerFullText.FillCreator();
            customerFullText.CustomerSearchContent = customer.ToSearchContent();
            customerFullText.ParentSearchContent = parent.ToSearchContent();
            CustomerFulltextInfoAdapter.Instance.UpdateInContext(customerFullText);

            CustomerFulltextInfo parentFullText = CustomerFulltextInfo.Create(parent.ParentID, CustomerFulltextInfo.ParentsType);
            parentFullText.FillCreator();
            parentFullText.CustomerSearchContent = customer.ToSearchContent();
            parentFullText.ParentSearchContent = parent.ToSearchContent();
            CustomerFulltextInfoAdapter.Instance.UpdateInContext(parentFullText);
        }

        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }

        protected override void PersistOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            // base.PersistOperationLog(context);    
        }
    }
}