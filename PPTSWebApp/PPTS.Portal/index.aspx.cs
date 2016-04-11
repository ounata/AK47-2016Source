using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;

namespace PPTS.Portal
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            //this.portalParameters.Value = JsonConvert.SerializeObject(PreparePortalParameters());
            base.OnPreRender(e);
        }

        private static Dictionary<string, object> PreparePortalParameters()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters["displayName"] = DeluxeIdentity.CurrentUser.DisplayName;
            parameters["allJobs"] = DeluxeIdentity.CurrentUser.Jobs();
            parameters["allRoles"] = DeluxeIdentity.CurrentUser.PPTSRoles();

            return parameters;
        }
    }
}