using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Executors;
using PPTS.Data.Orders.Adapters;
using PPTS.WebAPI.Orders.ViewModels.Assignment;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders;
using MCS.Library.Core;
using MCS.Library.Data;
using PPTS.Data.Common.Security;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("增加排课")]
    public class AssignAddExecutor : PPTSEditAssignExecutorBase<AssignSuperModel>
    {
        public AssignAddExecutor(AssignSuperModel model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            ///需要执行的判断
            ///1. 学员是否被冻结
            ///2. 检查资产表，未排课课时数量要不小于当前排课的数量
            ///3. 学员排课及教师排课冲突检查
            ///4. 资产表已排课数据增加排定的课时数
            base.PrepareData(context);

            Assign an = GetAssign(this.Model);
            an.FillCreator();
            an.FillModifier();            
            an.AssignID = UuidHelper.NewUuidString();
            this.Model.AssignID = an.AssignID;
            an.AssignStatus = AssignStatusDefine.Assigned;
            an.AssignSource = AssignSourceDefine.Manual;
            an.ConfirmStatus = ConfirmStatusDefine.Unconfirmed;

            ///学员排课及教师排课冲突检查,如果有冲突，引发异常，事务回滚
            AssignsAdapter.Instance.CheckConflictAssignInContext(an);

            ///检查资产表，未排课课时数量要不小于当前排课的数量,否则，引发异常，事务回滚
            GenericAssetAdapter<Asset, AssetCollection>.Instance.CheckUnAssignedAmountInContext(this.Model.AssetID, this.Model.Amount);

            //新增排课条件           
            if (string.IsNullOrEmpty(this.Model.ConditionID) || this.Model.ConditionID.Trim() == "-1")
            {
                AssignCondition ac = this.GetAssignCondition(this.Model);
                ac.ConditionID = UuidHelper.NewUuidString();
                ac.ConditionName4Customer = string.Format("{0}-{1}-{2}-{3}", this.Model.AssetCode, this.Model.SubjectName, this.Model.TeacherName, this.Model.GradeName);
                ac.ConditionName4Teacher  = string.Format("{0}-{1}-{2}-{3}", this.Model.AssetCode, this.Model.SubjectName, this.Model.CustomerName, this.Model.GradeName);
                ac.FillModifier();
                ac.FillCreator();
                AssignConditionAdapter.Instance.UpdateInContext(ac);
            }
            // 插入排课信息          
            AssignsAdapter.Instance.UpdateInContext(an);
            ///更新资产表排课数量       
            Asset at = GenericAssetAdapter<Asset, AssetCollection>.Instance.Load(this.Model.AssetID);
            at.AssignedAmount += this.Model.Amount;
            GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateInContext(at);
        }


        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }

        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
            context.Logs.ForEach(log => log.ResourceID = this.Model.AssetID);
        }

        private AssignCondition GetAssignCondition(AssignSuperModel ae)
        {
            AssignCondition ac = new AssignCondition();
            Type acType = typeof(AssignCondition);
            PropertyInfo[] acPropertyInfo = acType.GetProperties();
            Type aeType = typeof(AssignSuperModel);
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
        private Assign GetAssign(AssignSuperModel ae)
        {
            Assign ac = new Assign();
            Type acType = typeof(Assign);
            PropertyInfo[] acPropertyInfo = acType.GetProperties();
            Type aeType = typeof(AssignSuperModel);
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
    }
}