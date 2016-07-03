using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Assignment
{
    ///排课周视图
    public class AssignWeekViewModel
    {
        public IEnumerable<Assign> Result { get; private set; }
        public Dictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; private set; }

        public string Msg { get; private set; }

        public void LoadDataByStudent(StudentAssignQM qm)
        {
            //时间要日期不要小时分秒
            qm.StartTime = qm.StartTime.Date;
            qm.EndTime = qm.EndTime.Date;
            AssignCollection ac = AssignsAdapter.Instance.LoadCollection(AssignTypeDefine.ByStudent, qm.CustomerID, qm.StartTime, qm.EndTime, false);
            //只加载状态为 排定、完成、异常 且 课时类型为班组和一对一的排课数据
            var acLst = from r in ac
                        where (r.AssignStatus == AssignStatusDefine.Assigned 
                        || r.AssignStatus == AssignStatusDefine.Finished 
                        || r.AssignStatus == AssignStatusDefine.Exception)
                        && (r.CategoryType == ((int)CategoryType.CalssGroup).ToString() || r.CategoryType == ((int)CategoryType.OneToOne).ToString())
                        select r;

            if (acLst != null && !string.IsNullOrEmpty(qm.AssignStatus) && qm.AssignStatus != "-1")
                acLst = acLst.Where(p => p.AssignStatus == (AssignStatusDefine)Convert.ToInt32(qm.AssignStatus));

            if (acLst != null && !string.IsNullOrEmpty(qm.CategoryType) && qm.CategoryType != "-1")
                acLst = acLst.Where(p => p.CategoryType == qm.CategoryType);

            if (acLst != null && !string.IsNullOrEmpty(qm.Grade))
                acLst = acLst.Where(p => p.Grade == qm.Grade);

            if (acLst != null && !string.IsNullOrEmpty(qm.TeacherName))
                acLst = acLst.Where(p => p.TeacherName.Contains(qm.TeacherName));

            Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            ///处理下课时类型，剔除掉不要的项
            new OrderCommonHelper().GetProductCategoryType(Dictionaries);

            this.Result = acLst;
        }

        public void LoadDataByTeacher(TeacherAssignQM qm)
        {
            qm.StartTime = qm.StartTime.Date;
            qm.EndTime = qm.EndTime.Date;

            AssignCollection ac = AssignsAdapter.Instance.LoadCollection(AssignTypeDefine.ByTeacher, qm.TeacherJobID, qm.StartTime, qm.EndTime, false);

            var acLst = from r in ac
                        where (r.AssignStatus == AssignStatusDefine.Assigned 
                        || r.AssignStatus == AssignStatusDefine.Finished 
                        || r.AssignStatus == AssignStatusDefine.Exception)
                        && (r.CategoryType == ((int)CategoryType.CalssGroup).ToString() || r.CategoryType == ((int)CategoryType.OneToOne).ToString())
                        select r;

            if (acLst != null && !string.IsNullOrEmpty(qm.AssignStatus) && qm.AssignStatus != "-1")
                acLst = acLst.Where(p => p.AssignStatus == (AssignStatusDefine)Convert.ToInt32(qm.AssignStatus));

            if (acLst != null && !string.IsNullOrEmpty(qm.CategoryType) && qm.CategoryType != "-1")
                acLst = acLst.Where(p => p.CategoryType == qm.CategoryType);

            if (acLst != null && !string.IsNullOrEmpty(qm.Grade))
                acLst = acLst.Where(p => p.Grade == qm.Grade);

            if (acLst != null && !string.IsNullOrEmpty(qm.CustomerName))
                acLst = acLst.Where(p => p.CustomerName.Contains(qm.CustomerName));

            Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            ///处理下课时类型，剔除掉不要的项
            new OrderCommonHelper().GetProductCategoryType(Dictionaries);

            this.Result = acLst;
        }
    }
}