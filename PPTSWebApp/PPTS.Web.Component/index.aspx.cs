using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using MCS.Library.Configuration;
using MCS.Library.Core;

namespace PPTS.Web.Component
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
                parameters["jobs"] = jobs;
                parameters["roles"] = DeluxeIdentity.CurrentUser.PPTSRoles();
                if (jobs.Count > 0)
                {
                    parameters["orgId"] = jobs[0].GetParentOrganizationByType(DepartmentType.HQ).ID;
                }
            }

            Dictionary<string, Uri> webAPIs = new Dictionary<string, Uri>();

            UriSettings.GetConfig().GetUrlsInGroup("pptsWebAPIs").ForEach(u => webAPIs[u.Key] = u.Value.Uri);

            parameters["pptsWebAPIs"] = webAPIs;

            return parameters;
        }
    }
}