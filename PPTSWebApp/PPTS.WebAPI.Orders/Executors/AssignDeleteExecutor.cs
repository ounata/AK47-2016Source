using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("删除课表")]
    public class AssignDeleteExecutor : PPTSEditAssignExecutorBase<AssignCollection>
    {
        public AssignDeleteExecutor(AssignCollection model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        public List<string> CustomerIDTask { get; private set; }
        public string Msg { get; set; }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            ///只能删除已上状态课时
            ///过结账日后，只能删除当月课表，不能删除上月课表
            ///删除后，已上课时数减去删除的课时数量，剩余课时数加上删除的课时数量
            ///设置排课状态为 无效   设置确认状态为 已删除 
            base.PrepareData(context);

            if (this.Model.Count == 0)
                return;
            this.Model[0].FillModifier();

            IList<string> aIDs = this.Model.Select(p => p.AssignID).ToList();
            AssignCollection acCollection = AssignsAdapter.Instance.LoadCollection(aIDs);
            //只能删除已上状态课时
            //判断结账日接口调用，过了为true,没有过false
            IList<Assign> retValue = new List<Assign>();
            GlobalArgs ca = ConfigsCache.GetGlobalArgs();
            foreach (var v in acCollection)
            {
                if ((v.AssignStatus != Data.Orders.AssignStatusDefine.Finished || ca.IsClosedToAcccount(v.StartTime)))
                    continue;
                retValue.Add(v);
            }
            if (retValue.Count() == 0)
                return;
            this.CustomerIDTask = retValue.Select(p => p.CustomerID).Distinct().ToList();
            ///排课可能来自相同的资产，所以要分组处理
            IEnumerable<IGrouping<string, Assign>> result = retValue.GroupBy(p => p.AssetID);
            foreach (IGrouping<string, Assign> g in result)
            {
                IEnumerable<Assign> assigns = g.ToList<Assign>();
                decimal assignedAmount = assigns.Sum(p => p.Amount);
                Asset at = GenericAssetAdapter<Asset, AssetCollection>.Instance.Load(g.Key);
                if (at == null)
                    continue;
                foreach (Assign assign in assigns)
                {
                    assign.FillModifier();

                    assign.AssignStatus = AssignStatusDefine.Invalid;
                    assign.ConfirmStatus = ConfirmStatusDefine.Deleted;
                    ///更新排课状态
                    AssignsAdapter.Instance.ConfirmAssignInContext(assign);
                    AssetConfirm assetConfirm = this.GenerateAssetConfirm(at, assign);
                    //为确认课时表，计算剩余课时数量,增加课时量
                    at.Amount += assign.Amount;
                    assetConfirm.FillCreator();
                    ///更新确认课时表
                    AssetConfirmAdapter.Instance.UpdateInContext(assetConfirm);

                    at.ConfirmedMoney -= assign.Amount * assign.ConfirmPrice; //确认金额减少
                }
                //已上课时数减去删除的课时数量，剩余课时数加上删除的课时数量
                at.ConfirmedAmount -= assignedAmount;//已经确认的课时量要减去
                at.Amount += assignedAmount; //还原剩余课时量
                GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateInContext(at);
            }
        }


        private AssetConfirm GenerateAssetConfirm(Asset asset, Assign assign)
        {
            AssetConfirm assetConfirm = new AssetConfirm();

            assetConfirm.ConfirmID = UuidHelper.NewUuidString();

            assetConfirm.CampusID = assign.CampusID;  //校区ID
            assetConfirm.CampusName = assign.CampusName;

            assetConfirm.ConfirmerID = assign.ModifierID;
            assetConfirm.ConfirmerName = assign.ModifierName;
            assetConfirm.ConfirmTime = DateTime.Now;
            assetConfirm.ConfirmStatus = assign.ConfirmStatus;

            assetConfirm.CustomerID = assign.CustomerID;
            assetConfirm.AccountID = assign.AccountID;
            assetConfirm.CustomerCode = assign.CustomerCode;
            assetConfirm.CustomerName = assign.CustomerName;

            assetConfirm.TeacherID = assign.TeacherID;
            assetConfirm.TeacherName = assign.TeacherName;
            assetConfirm.TeacherJobID = assign.TeacherJobID;

            assetConfirm.ConsultantID = assign.ConsultantID;
            assetConfirm.ConsultantName = assign.ConsultantName;
            assetConfirm.ConsultantJobID = assign.ConsultantJobID;

            assetConfirm.EducatorID = assign.EducatorID;
            assetConfirm.EducatorName = assign.EducatorName;
            assetConfirm.EducatorJobID = assign.EducatorJobID;

            assetConfirm.DurationValue = assign.DurationValue;
            assetConfirm.Amount = assign.Amount;
            assetConfirm.StartTime = assign.StartTime;
            assetConfirm.EndTime = assign.EndTime;

            assetConfirm.AssetID = assign.AssetID;
            assetConfirm.AssetCode = assign.AssetCode;
            assetConfirm.AssetType = asset.AssetType;
            assetConfirm.AssetRefType = asset.AssetRefType;
            assetConfirm.AssetRefPID = asset.AssetRefPID;
            assetConfirm.AssetRefID = asset.AssetRefID;
            assetConfirm.Price = asset.Price;///确认价格
            assetConfirm.AssetMoney = assign.ConfirmPrice * asset.Amount; //上次资产剩余价值
            assetConfirm.ConfirmFlag = ConfirmFlagDefine.Cancel;   //确认标志（1-收入确认，-1收入取消）
            assetConfirm.ConfirmMoney = asset.Price * assign.Amount;   //确认金额
            //assetConfirm.ConfirmMemo  //确认说明
            IUser user = DeluxeIdentity.CurrentUser;
            assetConfirm.ConfirmerJobID = user.GetCurrentJob().ID;  //确认人岗位ID
            assetConfirm.ConfirmerJobID = user.GetCurrentJob().Name; //assetConfirm.ConfirmerJobName  //确认人岗位名称
            assetConfirm.ConfirmerJobType = ((int)user.GetCurrentJob().JobType).ToString(); //确认人岗位类型代码

            return assetConfirm;
        }

        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }

        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
            //context.Logs.ForEach(log => log.ResourceID = this.Model);
        }

    }
}