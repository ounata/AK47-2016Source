using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Products.Adapters;
using PPTS.WebAPI.Products.ViewModels.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.Executors
{
    [DataExecutorDescription("新增折扣表")]
    public class CreateDiscountExecutor : PPTSEditProductExecutorBase<CreateDiscountModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public CreateDiscountExecutor(CreateDiscountModel model)
            : base(model, null)
        {

        }

        #region 创建人：通过当前用户信息获取
        private PPTSJob _createJob = null;
        PPTSJob CreateJob
        {
            get
            {
                if (_createJob == null)
                {
                    _createJob = DeluxeIdentity.CurrentUser.GetCurrentJob();
                }
                return _createJob;
            }
        }

        private IUser _createUser;
        IUser CreateUser
        {
            get
            {
                if (_createUser == null)
                {
                    _createUser = DeluxeIdentity.CurrentUser;
                }
                return _createUser;
            }
        }
        #endregion

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            CheckResultModel chk = Model.CheckCreateDiscount();
            chk.Sucess.FalseThrow(chk.Message);
            //折扣表
            Model.Discount.DiscountID = UuidHelper.NewUuidString();
            Model.Discount.DiscountCode = PPTS.Data.Products.Helper.GetDiscountCode(CreateJob.GetParentOrganizationByType(DepartmentType.HQ).GetFirstInitial());
            Model.Discount.DiscountName = PPTS.Data.Products.Helper.GetDiscountName(CreateJob.GetParentOrganizationByType(DepartmentType.Branch).Name);
            Model.Discount.DiscountStatus = Data.Products.DiscountStatusDefine.Approving;
            Model.Discount.FillCreator();
            Model.Discount.FillModifier();
            //Model.Discount.ApproverID = CreateUser.ID;
            //Model.Discount.ApproverName = CreateUser.Name;
            //Model.Discount.ApproverJobID = CreateJob.ID;
            //Model.Discount.ApproverJobName = CreateJob.Name;
            //Model.Discount.ApproveTime = SNTPClient.AdjustedTime;
            Model.Discount.SubmitterID = CreateUser.ID;
            Model.Discount.SubmitterName = CreateUser.Name;
            Model.Discount.SubmitterJobId = CreateJob.ID;
            Model.Discount.SubmitterJobName = CreateJob.Name;
            Model.Discount.SubmitTime = SNTPClient.AdjustedTime;
            Model.Discount.BranchID = CreateJob.GetParentOrganizationByType(DepartmentType.Branch).ID;
            Model.Discount.BranchName = CreateJob.GetParentOrganizationByType(DepartmentType.Branch).Name;
            DiscountAdapter.Instance.UpdateInContext(Model.Discount);

            //折扣表数据档
            foreach (var item in Model.DiscountItemCollection)
            {
                if (item.DiscountStandard == 0 && item.DiscountValue == 0)
                    continue;
                item.DiscountID = Model.Discount.DiscountID;
                DiscountItemAdapter.Instance.UpdateInContext(item);
            }
            //折扣表  校区 关系提交
            foreach (var item in Model.DiscountPermissionsApplieCollection)
            {
                item.FillCreator();
                item.DiscountID = Model.Discount.DiscountID;
                DiscountPermissionsApplyAdapter.Instance.UpdateInContext(item);
            }
        }



        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
        }
    }
}