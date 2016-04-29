using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using MCS.Library.Configuration;
using MCS.Library.Core;

namespace PPTS.Portal
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
      
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.portalParameters.Value = JsonConvert.SerializeObject(PreparePortalParameters());
            base.OnPreRender(e);
        }

        private static Dictionary<string, object> PreparePortalParameters()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (DeluxePrincipal.IsAuthenticated)
            {
                parameters["displayName"] = DeluxeIdentity.CurrentUser.DisplayName;
                parameters["jobs"] = DeluxeIdentity.CurrentUser.Jobs();
                parameters["roles"] = DeluxeIdentity.CurrentUser.PPTSRoles();
            }

            Dictionary<string, Uri> urls = new Dictionary<string, Uri>();

            UriSettings.GetConfig().GetUrlsInGroup("pptsWebAPI").ForEach(u => urls[u.Key] = u.Value.Uri);

            parameters["pptsWebAPI"] = urls;

            return parameters;
        }
    }
}