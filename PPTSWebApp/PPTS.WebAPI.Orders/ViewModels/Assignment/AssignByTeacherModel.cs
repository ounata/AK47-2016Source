using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Assignment
{
    public class AssignByTeacherModel : AssignBaseModel
    {
        public StudentModel Student { get; private set; }

        public string Msg { get; private set; }

        public void LoadData(TeacherAssignQM qm)
        {
            this.Msg = "ok";

            TeacherJobView tjv = TeacherJobViewAdapter.Instance.Load(qm.TeacherJobID);
            if (tjv == null)
            {
                this.Msg = string.Format("未能查找到岗位ID：{0}的教师信息", qm.TeacherJobID);
                return;
            }

            IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (org == null)
            {
                this.Msg = "未能获取当前用户所属校区，请确认角色是否正确！";
                return;
            }

            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            ///从服务中获取教师的学员列表，教师可能在多个校区，所以用校区ID过滤下学生
            this.Student = new OrderCommonHelper().GetStudent(qm.TeacherJobID, org.ID, dictionaries);

            ///获取教师的排课条件
            this.AssignCondition = AssignConditionAdapter.Instance.LoadCollection(AssignTypeDefine.ByTeacher, qm.TeacherID, qm.TeacherJobID);
            //this.AssignCondition.Insert(0, new AssignCondition() { ConditionID = "100", ConditionName4Customer = "新建", ConditionName4Teacher = "新建" });

            this.Assign.TeacherID = tjv.TeacherID;
            this.Assign.TeacherName = tjv.TeacherName;
            this.Assign.TeacherJobID = tjv.JobID;
            this.Assign.TeacherJobOrgID = tjv.JobOrgID;
            this.Assign.TeacherJobOrgName = tjv.JobOrgName;
            this.Assign.IsFullTimeTeacher = tjv.IsFullTime;
            this.Assign.CampusID = org.ID;
            this.Assign.CampusName = org.Name;
        }
    }
}