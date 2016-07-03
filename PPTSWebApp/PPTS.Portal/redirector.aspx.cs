using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Library.Core;

namespace PPTS.Portal
{
    public partial class redirector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreRender(EventArgs e)
        {
            rd.Value = this.Request.QueryString.GetValue("rd", string.Empty);

            base.OnPreRender(e);
        }
    }
}