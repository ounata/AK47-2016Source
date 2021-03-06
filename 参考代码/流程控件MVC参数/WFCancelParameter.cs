﻿using CIIC.HSR.TSP.WF.UI.Control.Controls;
using CIIC.HSR.TSP.WF.UI.Control.ModelBinding;
using MCS.Library.WF.Contracts.Ogu;
using MCS.Library.WF.Contracts.Proxies;
using MCS.Library.WF.Contracts.Workflow.Runtime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CIIC.HSR.TSP.WF.UI.Control.Interfaces
{
	/// <summary>
	/// 废弃流程参数
	/// </summary>
	[ModelBinder(typeof(WFCancelParameterBinder))]
	public class WFCancelParameter : WFParameterWithResponseBase
	{
		protected override void InternalExecute(ResponseData response)
		{
            base.DefaultFillEmailCollector();
			response.ProcessInfo = WfClientProcessRuntimeServiceProxy.Instance.Cancel(this.ProcessId, this.RuntimeContext);
		}

        protected override bool CanOperate()
        {
            return HttpContext.Current.Request.GetWFContext().CanCancel();
        }
	}
}
