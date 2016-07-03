using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.Assignment;
using PPTS.Data.Orders.Adapters;
using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Common.Security;
using MCS.Library.Data;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Products;
using PPTS.Data.Common;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("复制排课")]
    public class AssignCopyExecutor : PPTSEditAssignExecutorBase<AssignCopyQM>
    {
        public string Msg { get; private set; }

        public AssignCopyExecutor(AssignCopyQM model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        public List<string> CustomerIDTask { get; private set; }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            ///需要执行的判断
            ///1. 检查学员是否被冻结
            ///2. 检查课表是否允许复制（排课是否可以复制标志）（另外班组排课的课表默认不允许复制）
            ///3. 如课表允许复制，则排定、异常、已上状态的课表能被复制  类型必须为一对一
            ///4. 当前排课的数量 不大于 未排课课时数量（课时总数量减去已排课数量）
            ///5. 学员排课及教师排课冲突检查
            ///6. 插入排课记录及更新资产表已排课数量

            base.PrepareData(context);

            this.Msg = "ok";

            ///获取允许复制的排课记录（2  3 规则）,并且形成新的实体 
            IEnumerable<Assign> ac = this.GetAllowCopyAssign();
            if (ac == null || ac.Count() == 0)
            {
                this.Msg = "该时间段区间的课表，不允许复制！";
                return;
            }
            var custs = from cs in ac
                        group cs.CustomerID by new { cs.CustomerID, cs.CustomerName, cs.CustomerCode } into g
                        select g;

            ///1. 学员是否被冻结
            IList<string> custID = new List<string>();
            foreach (var c in custs)
            {
                Customer cust = PPTS.WebAPI.Orders.Service.CustomerService.GetCustomerByCustomerId(c.Key.CustomerID);
                if (cust.Locked)
                    Msg += string.Format("{0}{1}学员被冻结，不允许复制课表<br>", c.Key.CustomerName, c.Key.CustomerID);
                else
                    custID.Add(c.Key.CustomerID);
            }
            if (custID.Count() == 0)
            {
                this.Msg = "学员被冻结，课表不允许复制";
                return;
            }
            ///学员排课及教师排课冲突检查  如果出现冲突，则抛出异常，事务回滚
            foreach (var m in ac)
                AssignsAdapter.Instance.CheckConflictAssignInContext(m);

            this.CustomerIDTask = ac.Select(p => p.CustomerID).Distinct().ToList();

            ///排课可能来自不同的资产，所以要分组处理
            IEnumerable<IGrouping<string, Assign>> result = ac.GroupBy(p => p.AssetID);
            foreach (IGrouping<string, Assign> g in result)
            {
                IEnumerable<Assign> assigns = g.ToList<Assign>();
                decimal assignedAmount = assigns.Sum(p => p.Amount);
                ///检查资产表，未排课课时数量要不小于当前排课的数量,否则，引发异常，事务回滚
                GenericAssetAdapter<Asset, AssetCollection>.Instance.CheckUnAssignedAmountInContext(g.Key, assignedAmount);
            }
            ///保存新的排课记录
            foreach (var m in ac)
            {
                AssignsAdapter.Instance.UpdateInContext(m);
            }
            ///复制课表中，可能有来自不同资产的排课，所以要分组处理
            foreach (IGrouping<string, Assign> g in result)
            {
                IEnumerable<Assign> assigns = g.ToList<Assign>();
                decimal assignedAmount = assigns.Sum(p => p.Amount);

                ///更新资产表中已排课数量
                Asset at = GenericAssetAdapter<Asset, AssetCollection>.Instance.Load(g.Key);
                at.AssignedAmount += assignedAmount;
                GenericAssetAdapter<Asset, AssetCollection>.Instance.UpdateInContext(at);
            }
        }

        private IEnumerable<Assign> GetAllowCopyAssign()
        {
            if (!string.IsNullOrEmpty(this.Model.CustomerID) && !string.IsNullOrEmpty(this.Model.TeacherID))
                return null;

            ///计算复制源与目标间隔天数
            TimeSpan ts = this.Model.DestDateStart.Subtract(this.Model.SrcDateStart);
            int days = ts.Days;

            AssignCollection ac = null;
            if (!string.IsNullOrEmpty(this.Model.CustomerID))
                ac = AssignsAdapter.Instance.LoadCollection(Data.Orders.AssignTypeDefine.ByStudent, this.Model.CustomerID, this.Model.SrcDateStart.Date, this.Model.SrcDateEnd.AddDays(1).Date, false);
            else
                ac = AssignsAdapter.Instance.LoadCollection(Data.Orders.AssignTypeDefine.ByTeacher, this.Model.TeacherJobID, this.Model.SrcDateStart.Date, this.Model.SrcDateEnd.AddDays(1).Date, false);
            IEnumerable<Assign> assignCollection = null;
            ///挑选允许复制的记录
            if (ac != null)
            {
                assignCollection = ac.Where(p => p.CopyAllowed && p.CategoryType == ((int)CategoryType.OneToOne).ToString()
                && (p.AssignStatus == Data.Orders.AssignStatusDefine.Assigned
                || p.AssignStatus == Data.Orders.AssignStatusDefine.Exception
                || p.AssignStatus == Data.Orders.AssignStatusDefine.Finished));
            }
            //创建新的记录
            foreach (var m in assignCollection)
            {
                m.AssignID = UuidHelper.NewUuidString();
                m.AssignTime = DateTime.MinValue;
                m.StartTime = m.StartTime.AddDays(days);
                m.EndTime = m.EndTime.AddDays(days);
                m.AssignStatus = Data.Orders.AssignStatusDefine.Assigned;
                m.AssignSource = Data.Orders.AssignSourceDefine.Manual;
                m.ConfirmStatus = Data.Orders.ConfirmStatusDefine.Unconfirmed;
                m.FillCreator();
                m.FillModifier();
                m.CreateTime = DateTime.MinValue;
            }
            return assignCollection;
        }


        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }



        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
            //context.Logs.ForEach(log => log.ResourceID = this.Model.AssetID);
        }
    }
}