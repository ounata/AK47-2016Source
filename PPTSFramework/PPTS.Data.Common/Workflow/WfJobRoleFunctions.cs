using MCS.Library.Core;
using MCS.Library.Expression;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Workflow;
using PPTS.Data.Common.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Workflow
{
    public class WfJobRoleFunctions : IWfCalculateUserFunction
    {
        public static readonly WfJobRoleFunctions Instance = new WfJobRoleFunctions();

        private WfJobRoleFunctions()
        {
        }

        public bool IsFunction(string funcName)
        {
            funcName.CheckStringIsNullOrEmpty("funcName");

            BuiltInFunctionInfoCollection funcsInfo = BuiltInFunctionHelper.GetBuiltInFunctionsInfo(this.GetType());

            return funcsInfo.Contains(funcName);
        }

        public object CalculateUserFunction(string funcName, ParamObjectCollection arrParams, object callerContext)
        {
            object result = null;

            if (WfJobRoleFunctions.Instance.IsFunction(funcName))
                result = BuiltInFunctionHelper.ExecuteFunction(funcName, this, arrParams, callerContext);

            return result;
        }

        #region BuiltInFunctions
        [BuiltInFunction("JobUsers", "岗位中的人员")]
        private IEnumerable<IUser> JobUsers(string orgID, string jobName, WfConditionDescriptor callerContext)
        {
            return PPTSOrganizationAdapter.Instance.GetUsersInJobsByOrganizationID(orgID, jobName);
        }

        [BuiltInFunction("CampusID", "校区ID")]
        private string CampusID(WfConditionDescriptor callerContext)
        {
            return callerContext.GetRuntimeParameterValue("CampusID", string.Empty);
        }

        [BuiltInFunction("BranchID", "分公司ID")]
        private string BranchID(WfConditionDescriptor callerContext)
        {
            return callerContext.GetRuntimeParameterValue("BranchID", string.Empty);
        }

        [BuiltInFunction("HQID", "总部ID")]
        private string HQID(WfConditionDescriptor callerContext)
        {
            return callerContext.GetRuntimeParameterValue("HQID", string.Empty);
        }

        [BuiltInFunction("RegionID", "大区ID")]
        private string RegionID(WfConditionDescriptor callerContext)
        {
            return callerContext.GetRuntimeParameterValue("RegionID", string.Empty);
        }
        #endregion BuiltInFunctions
    }
}
