using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using PPTS.Data.Common.Adapters;
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
    public class AssignByStudentModel : AssignBaseModel
    {
        ///学员对应的资产列表
        public AssetViewCollection AssignExtension { get; private set; }
        
        ///学员对应的教师集合
        public TeacherModel Teacher { get; private set; }

        public string Msg { get; private set; }

        public void LoadData(StudentAssignQM qm)
        {
            this.Msg = "ok";
            if (string.IsNullOrEmpty(qm.CustomerID))
            {
                this.Msg = "学员ID为空，加载数据失败！";
                return;
            }
            ///学员ID
            string customerID = qm.CustomerID;
           ///加载学员已经有的排课条件
            this.AssignCondition = AssignConditionAdapter.Instance.LoadCollection(AssignTypeDefine.ByStudent, customerID, string.Empty);
            //this.AssignCondition.Insert(0, new AssignCondition() { ConditionID = "100", ConditionName4Customer = "新建", ConditionName4Teacher = "新建" });
           ///加载学员的资产
            this.AssignExtension = AssetViewAdapter.Instance.LoadCollection(customerID);
            ///从服务中获取学员指定的教师列表
            ///当前操作人所属校区ID
            IOrganization org = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);
            if (org == null)
            {
                Teacher = new TeacherModel();
                return;       
            }
            var dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Data.Orders.Entities.Assign));
            string operaterCampusID = org.ID;
            this.Teacher = new OrderCommonHelper().GetTeacher(customerID, operaterCampusID, dictionaries);
           
            this.Assign.CampusID = org.ID;
            this.Assign.CampusName = org.Name;
        }
    }
}