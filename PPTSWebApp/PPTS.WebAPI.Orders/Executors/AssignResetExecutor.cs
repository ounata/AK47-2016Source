using MCS.Library.Data.Executors;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Executors;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Common.Security;
using PPTS.WebAPI.Orders.ViewModels.Assignment;
using System.Linq;
using System.Collections.Generic;
using PPTS.Data.Customers.Entities;
using System;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("调整排课")]
    public class AssignResetExecutor : PPTSEditAssignExecutorBase<IList<AssignResetQM>>
    {
        public AssignResetExecutor(IList<AssignResetQM> model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            ///需要执行的判断
            ///1. 冻结状态学员不允许调整排课
            ///2. 只有排定 异常 状态的排课可以调课
            ///3. 学员排课及教师排课冲突检查
            base.PrepareData(context);

            var c = from s in this.Model
                    group s.CustomerID by s into g
                    select g.Key;

            foreach (var v in c)
            {
                ///1. 学员是否被冻结
                Customer cust = PPTS.WebAPI.Orders.Service.CustomerService.GetCustomerByCustomerId(v.CustomerID);
                if (cust.Locked)
                {
                    throw new Exception(string.Format("{0} 学员被冻结，不能调整课时", cust.CustomerName));
                }
            }

            ///调整排课，包含只有排定  异常  状态的排课可以调课
            IEnumerable<Assign> ac = this.GetAllowResetAssign();

            ///学员排课及教师排课冲突检查  如果出现冲突，则抛出异常，事务回滚
            foreach (var m in ac)
            {
                AssignsAdapter.Instance.CheckConflictAssignInContext(m);
            }
            foreach (var m in ac)
            {
                AssignsAdapter.Instance.UpdateInContext(m);
            }
        }

        private IEnumerable<Assign> GetAllowResetAssign()
        {
            IList<string> assignIDs = this.Model.Select(p => p.AssignID).ToList();
            AssignCollection ac = AssignsAdapter.Instance.LoadCollection(assignIDs);

            IEnumerable<Assign> assignCollection = null;
            ///挑选允许调整的记录
            if (ac != null)
            {
                assignCollection = ac.Where(p => (p.AssignStatus == Data.Orders.AssignStatusDefine.Assigned || p.AssignStatus == Data.Orders.AssignStatusDefine.Exception));
            }
            //创建新的记录
            foreach (var m in assignCollection)
            {
                m.AssignStatus = Data.Orders.AssignStatusDefine.Assigned;
                m.FillModifier();
                var mm = this.Model.Where(p => p.AssignID == m.AssignID).FirstOrDefault();
                if (mm == null)
                    continue;
                System.TimeSpan ts = m.EndTime.Subtract(m.StartTime);
                m.StartTime = mm.ReDate.Date.AddHours(System.Convert.ToDouble(mm.ReHour)).AddMinutes(System.Convert.ToDouble(mm.ReMinute));
                m.EndTime = m.StartTime.Add(ts);
            }
            return assignCollection;
        }

        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
            //context.Logs.ForEach(log => log.ResourceID = this.Model);
        }


    }
}