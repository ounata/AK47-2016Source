using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Expression;

namespace MCS.Library.SOA.DataObjects.Workflow
{
	/// <summary>
	/// 流程条件中用户自定义函数的接口实现
	/// </summary>
	public interface IWfCalculateUserFunction
	{
        /// <summary>
        /// 回调计算函数
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="arrParams"></param>
        /// <param name="callerContext">是WfConditionDescriptor类型</param>
        /// <returns></returns>
		object CalculateUserFunction(string funcName, ParamObjectCollection arrParams, object callerContext);
	}
}
