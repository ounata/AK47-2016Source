using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.Data.DataObjects;

namespace MCS.Library.SOA.DataObjects.Workflow
{
    /// <summary>
    /// 用户设置的委托待办信息
    /// </summary>
    [Serializable]
    [XElementSerializable]
    [ORTableMapping("WF.DELEGATIONS")]
    [TenantRelativeObject]
    public class WfDelegation
    {
        private bool _Enabled = true;

        /// <summary>
        /// 是否启用
        /// </summary>
        [ORFieldMapping("ENABLED")]
        public bool Enabled
        {
            get { return this._Enabled; }
            set { this._Enabled = value; }
        }

        /// <summary>
        /// 委托人的用户ID
        /// </summary>
        [ORFieldMapping("SOURCE_USER_ID", PrimaryKey = true)]
        public string SourceUserID
        {
            get;
            set;
        }

        /// <summary>
        /// 被委托人的用户ID
        /// </summary>
        [ORFieldMapping("DESTINATION_USER_ID", PrimaryKey = true)]
        public string DestinationUserID
        {
            get;
            set;
        }

        /// <summary>
		/// 应用名称
		/// </summary>
		[ORFieldMapping("APPLICATION_NAME", PrimaryKey = true)]
        public string ApplicationName
        {
            get;
            set;
        }

        /// <summary>
		/// 应用模块名称
		/// </summary>
		[ORFieldMapping("PROGRAM_NAME", PrimaryKey = true)]
        public string ProgramName
        {
            get;
            set;
        }

        /// <summary>
        /// 委托人的用户名称
        /// </summary>
        [ORFieldMapping("SOURCE_USER_NAME")]
        public string SourceUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 被委托人的用户名称
        /// </summary>
        [ORFieldMapping("DESTINATION_USER_NAME")]
        public string DestinationUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        [ORFieldMapping("START_TIME", UtcTimeToLocal = true)]
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        [ORFieldMapping("END_TIME", UtcTimeToLocal = true)]
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 传入的应用类型是否匹配
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="programName"></param>
        /// <returns></returns>
        public bool Matched(string applicationName, string programName)
        {
            return StringMatched(applicationName, this.ApplicationName) &&
                StringMatched(programName, this.ProgramName);
        }

        private static bool StringMatched(string param, string template)
        {
            bool result = false;

            if (template.IsNullOrEmpty())
                result = true;
            else
            {
                if (param.IsNullOrEmpty())
                    param = string.Empty;

                result = string.Compare(param, template, true) == 0;
            }

            return result;
        }
    }

    /// <summary>
    /// 用户委托待办信息列表
    /// </summary>
    [Serializable]
    [XElementSerializable]
    public class WfDelegationCollection : EditableDataObjectCollectionBase<WfDelegation>
    {
    }
}
