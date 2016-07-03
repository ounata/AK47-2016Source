using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.Passport;
using MCS.Web.MVC.Library.Models.UserTasks;
using System.Reflection;
using System.IO;

namespace PPTS.Portal
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            this.configData.Value = JsonConvert.SerializeObject(PreparePortalParameters());
            base.OnPreRender(e);
        }

        private static Dictionary<string, object> PreparePortalParameters()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (DeluxePrincipal.IsAuthenticated)
            {
                PPTSJobCollection jobs = DeluxeIdentity.CurrentUser.Jobs();
                parameters["userId"] = DeluxeIdentity.CurrentUser.ID;
                parameters["displayName"] = DeluxeIdentity.CurrentUser.DisplayName;
                parameters["logOnName"] = DeluxeIdentity.CurrentUser.LogOnName;
                parameters["jobs"] = jobs;
                parameters["roles"] = DeluxeIdentity.CurrentUser.PPTSRoles();
                if (jobs.Count > 0)
                {
                    var hq = jobs[0].GetParentOrganizationByType(DepartmentType.HQ);
                    var branch = jobs[0].GetParentOrganizationByType(DepartmentType.Branch);
                    var campus = jobs[0].GetParentOrganizationByType(DepartmentType.Campus);
                    parameters["orgId"] = hq != null ? hq.ID : "";
                    parameters["branchId"] = branch != null ? branch.ID : "";
                    parameters["campusId"] = campus != null ? campus.ID : "";
                }
            }

            Dictionary<string, Uri> webAPIs = new Dictionary<string, Uri>();

            UriSettings.GetConfig().GetUrlsInGroup("pptsWebAPIs").ForEach(u => webAPIs[u.Key] = u.Value.Uri);

            parameters["pptsWebAPIs"] = webAPIs;
            parameters["logoffUrl"] = PassportManager.GetLogOnOrLogOffUrl();
            parameters["userTasksAndCount"] = UserTaskModelHelper.Instance.QueryTaskCountAndSimpleList(DeluxeIdentity.CurrentUser.ID, new UserTaskSearchParams() { Top = 5 });

            parameters["roleCheckEnabled"] = RolesDefineConfig.GetConfig().Enabled;
            parameters["timestamp"] = File.GetCreationTime(Assembly.GetExecutingAssembly().Location).Ticks;

            return parameters;
        }
    }
}