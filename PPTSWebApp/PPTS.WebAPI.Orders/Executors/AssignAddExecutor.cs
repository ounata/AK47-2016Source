using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Executors;
using PPTS.Data.Orders.Adapters;
using PPTS.WebAPI.Orders.ViewModels.AssignConditions;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders;
using MCS.Library.Core;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("增加排课")]
    public class AssignAddExecutor : PPTSEditAssignExecutorBase<AssignExtension>
    {
        public AssignAddExecutor(AssignExtension model)
            : base(model, null)
        {
            //model.NullCheck("model");
            //model.Product.NullCheck("Product");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            ///需要执行的判断
            ///1. 学员是否被冻结
            ///2. 未排课课时数量要不小于当前排课的数量
            ///3. 学员排课及教师排课冲突检查

            base.PrepareData(context);
            //新增排课条件
            if (string.IsNullOrEmpty(this.Model.ConditionID) || this.Model.ConditionID.Trim() == "-1")
            {
                AssignCondition acObj = GetAssignCondition(this.Model);
                acObj.ConditionID = UuidHelper.NewUuidString();
                acObj.ConditionName = string.Format("{0}-{1}-{2}-{3}", this.Model.AssetCode, this.Model.SubjectName, this.Model.TeacherName, this.Model.GradeName);
                AssignConditionAdapter.Instance.UpdateInContext(acObj);
            }
            // 插入排课信息
            this.Model.AssignID = UuidHelper.NewUuidString();
            this.Model.AssignStatus = AssignStatusDefine.Assigned;
            this.Model.AssignSource = AssignSourceDefine.Manual;
            this.Model.ConfirmStatus = ConfirmStatusDefine.Unconfirmed;
            AssignsAdapter.Instance.UpdateInContext((Assign)this.Model);
            ///更新资产表中已排数量
            ///注意，要传入修改人ID，修改人姓名
            AssetAdapter.Instance.IncreaseAssignedAmountInContext(this.Model.AssetID, this.Model.Amount,string.Empty,string.Empty);

        }

        private AssignCondition GetAssignCondition(AssignExtension ae)
        {
            AssignCondition ac = new AssignCondition();
            Type acType = typeof(AssignCondition);
            PropertyInfo[] acPropertyInfo = acType.GetProperties();
            Type aeType = typeof(AssignExtension);
            PropertyInfo[] aePropertyInfo = aeType.GetProperties();
            foreach (PropertyInfo pInfo in acPropertyInfo)
            {
                PropertyInfo pi = aePropertyInfo.Where(p => p.Name == pInfo.Name).FirstOrDefault();
                if (pi != null)
                {
                    pInfo.SetValue(ac, pi.GetValue(ae), null);
                }
            }
            return ac;
        }

        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
            context.Logs.ForEach(log => log.ResourceID = this.Model.AssetID);
        }

    }
}