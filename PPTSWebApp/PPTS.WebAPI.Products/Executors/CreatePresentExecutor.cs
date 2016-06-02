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
using PPTS.WebAPI.Products.ViewModels.Presents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.Executors
{
    [DataExecutorDescription("新增买赠表")]
    public class CreatePresentExecutor : PPTSEditProductExecutorBase<CreatePresentModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public CreatePresentExecutor(CreatePresentModel model)
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
            CheckResultModel chk = Model.CheckCreatePresent();
            if (chk.Sucess)
            {
                var branch = CreateJob.GetParentOrganizationByType(DepartmentType.Branch);
                if (branch == null)
                    throw new Exception("该岗位没有分公司！");
                //买赠表
                Model.Present.PresentID = UuidHelper.NewUuidString();
                Model.Present.PresentCode = PPTS.Data.Products.Helper.GetPresentCode(CreateJob.GetParentOrganizationByType(DepartmentType.HQ).GetFirstInitial());
                Model.Present.PresentName = PPTS.Data.Products.Helper.GetPresentName(branch.Name);
                Model.Present.PresentStatus = Data.Products.PresentStatusDefine.Approving;
                Model.Present.FillCreator();
                Model.Present.FillModifier();
                //Model.Present.ApproverID = CreateUser.ID;
                //Model.Present.ApproverName = CreateUser.Name;
                //Model.Present.ApproverJobID = CreateJob.ID;
                //Model.Present.ApproverJobName = CreateJob.Name;
                //Model.Present.ApproveTime = SNTPClient.AdjustedTime;
                Model.Present.SubmitterID = CreateUser.ID;
                Model.Present.SubmitterName = CreateUser.Name;
                Model.Present.SubmitterJobId = CreateJob.ID;
                Model.Present.SubmitterJobName = CreateJob.Name;
                Model.Present.SubmitTime = SNTPClient.AdjustedTime;
                Model.Present.BranchID = CreateJob.GetParentOrganizationByType(DepartmentType.Branch).ID;
                Model.Present.BranchName = CreateJob.GetParentOrganizationByType(DepartmentType.Branch).Name;
                PresentAdapter.Instance.Update(Model.Present);

                //买赠表数据档
                foreach (var item in Model.PresentItemCollection)
                {
                    if (item.PresentStandard == 0 && item.PresentValue == 0)
                        continue;
                    item.PresentID = Model.Present.PresentID;
                    PresentItemAdapter.Instance.UpdateInContext(item);
                }
                //买赠表  校区 关系提交
                foreach (var item in Model.PresentPermissionsApplieCollection)
                {
                    item.FillCreator();
                    item.PresentID = Model.Present.PresentID;
                    PresentPermissionsApplyAdapter.Instance.UpdateInContext(item);
                }
            }
            else {
                throw new Exception(chk.Message);
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