using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

[assembly: WebResource("MCS.Web.Responsive.Library.Resources.Animations.js", "application/x-javascript")]

namespace MCS.Web.Responsive.Library.Script
{
    /// <summary>
    /// 与资源脚本MCS.Web.Library.Script.Resources.Animations.js相关联的类
    /// </summary>
    [ClientScriptResource(null, "MCS.Web.Responsive.Library.Resources.Animations.js")]
	[RequiredScript(typeof(ControlBaseScript))]
    public static class AnimationsScript
    {
    }
}
