using MCS.Library.SOA.DataObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Security
{
    /// <summary>
    /// 需要导入的角色和权限集合
    /// </summary>
    public class SCRolesAndPermissions
    {
        public SCRolesAndPermissions(SCRoleCollection roles, SCPermissionCollection permissions)
        {
            this.Roles = roles;
            this.Permissions = permissions;
        }

        public SCRoleCollection Roles
        {
            get;
            internal set;
        }

        public SCPermissionCollection Permissions
        {
            get;
            internal set;
        }
    }
}
