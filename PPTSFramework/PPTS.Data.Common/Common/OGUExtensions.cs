using MCS.Library.OGUPermission;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTS.Data.Common.Security;
using MCS.Library.Core;

namespace PPTS.Data.Common
{
    /// <summary>
    /// 权限管理体系扩展常用方法
    /// </summary>
    public static class OGUExtensions
    {
        /// <summary>
        /// 通过机构IDs返回机构信息
        /// </summary>
        /// <param name="IDs">机构IDs集合</param>
        /// <returns></returns>
        public static OguObjectCollection<IOrganization> GetOrganizationByIDs(params string[] IDs)
        {
            return OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, IDs);
            //PPTSOrganization organization = PPTSOrganizationAdapter.Instance.Load(ID);
            //return organization.ToBaseOrganization();
        }

        /// <summary>
        /// 通过机构ID返回机构信息
        /// </summary>
        /// <param name="ID">机构ID</param>
        /// <returns></returns>
        public static IOrganization GetOrganizationByID(string ID)
        {
            return OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, ID).FirstOrDefault();
            //PPTSOrganization organization = PPTSOrganizationAdapter.Instance.Load(ID);
            //return organization.ToBaseOrganization();
        }

        /// <summary>
        /// 通过机构名称返回机构信息
        /// </summary>
        /// <param name="name">机构名称</param>
        /// <returns></returns>
        public static IOrganization GetOrganizationByName(string name)
        {
            PPTSOrganization organization = PPTSOrganizationAdapter.Instance.LoadByName(name);
            return organization.ToBaseOrganization();
        }

        /// <summary>
        /// 通过机构简称返回机构信息
        /// </summary>
        /// <param name="shortName">机构简称</param>
        /// <returns></returns>
        public static IOrganization GetOrganizationByShortName(string shortName)
        {
            PPTSOrganization organization = PPTSOrganizationAdapter.Instance.LoadByShortName(shortName);
            return organization.ToBaseOrganization();
        }

        public static IUser GetUserByOAName(string oaname)
        {
            oaname.NullCheck("oaname");
            return OguMechanismFactory.GetMechanism().GetObjects<IUser>(SearchOUIDType.LogOnName, oaname).FirstOrDefault();
        }
    }
}
