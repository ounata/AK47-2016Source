using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using MCS.Web.MVC.Library.ApiCore;
using MCS.Web.MVC.Library.Models.Workflow;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common
{
    public static class CommonHelper
    {
        /// <summary>
        /// 过滤电话号码
        /// </summary>
        /// <param name="phoneNumber">电话号码</param>
        /// <param name="user">当前用户</param>
        /// <returns></returns>
        public static string FilterPhoneNumber(string phoneNumber, IUser user)
        {
            if (!string.IsNullOrEmpty(phoneNumber) && phoneNumber.Length > 3)
            {
                if (!user.GetCurrentJob().Functions.Contains(CommonFunctions.PhoneNumberFunction))
                {
                    return phoneNumber.Substring(0, 3).PadRight(phoneNumber.Length, '*');
                }
            }
            return phoneNumber;
        }

        static DateTime _minDate = DateTime.Parse("1900-01-01");
        /// <summary>
        /// 是否是合法的数据库日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsValidDbDate(DateTime date)
        {
            return date >= _minDate;
        }
    }

    /// <summary>
    /// 常用的权限点
    /// </summary>
    public static class CommonFunctions
    {
        /// <summary>
        /// 联系电话查看权限点
        /// </summary>
        public const string PhoneNumberFunction = "查看联系电话";
    }
}
