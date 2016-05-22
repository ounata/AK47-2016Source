using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PPTS.Data.Common.Security;

namespace PPTS.ExtServices.LeYu.Models.CallingOrgCenterConfig
{
    public class CallingOrgCenterCofigModel
    {
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string JobName
        { get; set; }

        /// <summary>
        /// 呼叫中心类型
        /// </summary>
        public string CallingType
        { get; set; }

        /// <summary>
        /// 优先级0，1，2，3
        /// 0-joblevel=13,1-joblevel=14,2-joblevel其他值，2，4，20，22等，3-没有
        /// </summary>
        public string SortId
        { get; set; }
    }

    public class JobModel
    {
        public CallingOrgCenterCofigModel CofigModel { get; set; }
        public PPTSJob Job { get; set; }
    }
}