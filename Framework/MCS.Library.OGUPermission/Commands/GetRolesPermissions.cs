using MCS.Library.Core;
using MCS.Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.OGUPermission.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class GetRolesPermissions : CommandBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public GetRolesPermissions(string name)
			: base(name)
		{
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argument"></param>
        public override void Execute(string argument)
        {
            string[] args = argument.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            ExceptionHelper.FalseThrow(args.Length > 1, "查询人员的角色对应的权限必须提供应用的CodeName和角色的CodeName");

            IApplication app = AppAdminMechanism.Instance.GetAllApplications().Find(a => a.CodeName == args[0]);

            (app != null).FalseThrow("不能找到CodeName为{0}的应用", args[0]);

            PermissionCollection permissions = AppAdminMechanism.Instance.GetRolesPermissions(app, args[1]);

            permissions.ForEach(permission => OutputHelper.OutputPermissionInfo(permission));
        }

        /// <summary>
		/// 
		/// </summary>
		public override string HelperString
        {
            get
            {
                return "getRolesPermissions {appCodeName} {roleCodeNames}";
            }
        }
    }
}
