#region
// -------------------------------------------------
// Assembly	：	DeluxeWorks.Library.Passport
// FileName	：	DeluxePrincipal.cs
// Remark	：	
// -------------------------------------------------
// VERSION  	AUTHOR		DATE			CONTENT
// 1.0
// 1.1          胡自强      2008-12-2       添加注释
// -------------------------------------------------
#endregion
using MCS.Library.Caching;
using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using MCS.Library.Passport;
using MCS.Library.Passport.Properties;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

namespace MCS.Library.Principal
{
    /// <summary>
    /// 通过DeluxeWorks认证机制，所产生的用户身份及其授权信息，实现了系统的IPrincipal接口。
    /// </summary>
    public class DeluxePrincipal : IGenericTokenPrincipal
    {
        private DeluxeIdentity userIdentity = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="identity"></param>
        public DeluxePrincipal(DeluxeIdentity identity)
        {
            this.userIdentity = identity;
        }

        /// <summary>
        /// 从当前线程中取得用户的登录和权限信息(Principal)
        /// </summary>
        public static DeluxePrincipal Current
        {
            get
            {
                DeluxePrincipal result = GetPrincipal();

                ExceptionHelper.FalseThrow<AuthenticateException>(result != null,
                    Resource.CanNotGetPrincipal);

                return result;
            }
        }

        /// <summary>
        /// 是否经过认证
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                return GetPrincipal() != null;
            }
        }

        private static DeluxePrincipal GetPrincipal()
        {
            return PrincipaContextAccessor.GetPrincipal<DeluxePrincipal>();
        }

        #region IPrincipal 成员
        /// <summary>
        /// 用户身份标识
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return this.userIdentity;
            }
        }

        /// <summary>
        /// 判断当前用户是否属于某一角色
        /// </summary>
        /// <param name="roleDesp">角色的描述(应用的名称1:角色名称11,角色名称12,...;应用名称2:角色名称21,角色名称22,...)</param>
        /// <returns>用户是否属于某一角色</returns>
        public bool IsInRole(string roleDesp)
        {
            return IsInRole(this.userIdentity.User, roleDesp);
        }

        /// <summary>
        /// 判断某个用户是否属于某一角色
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleDesp">角色的描述(应用的名称1:角色名称11,角色名称12,...;应用名称2:角色名称21,角色名称22,...)</param>
        /// <returns>用户是否属于某一角色</returns>
        public static bool IsInRole(IUser user, string roleDesp)
        {
            bool result = false;

            if (user != null)
            {
                result = ParseRoleDescription(roleDesp,
                    (appName, roleName) => user.Roles.ContainsApp(appName) && user.Roles[appName].ContainsKey(roleName));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permissionDesp"></param>
        /// <returns></returns>
        public static bool HavePermissions(IUser user, string permissionDesp)
        {
            bool result = false;

            if (user != null)
            {
                result = ParseRoleDescription(permissionDesp,
                    (appName, roleName) => user.Permissions.ContainsApp(appName) && user.Permissions[appName].ContainsKey(roleName));
            }

            return result;
        }

        /// <summary>
        /// 分析某一角色（权限）的描述。将分析出的应用和角色（权限）作为参数传递到回调当中
        /// </summary>
        /// <param name="roleDesp">角色的描述(应用的名称1:角色名称11,角色名称12,...;应用名称2:角色名称21,角色名称22,...)</param>
        /// <param name="roleFunc"></param>
        /// <returns></returns>
        public static bool ParseRoleDescription(string roleDesp, Func<string, string, bool> roleFunc)
        {
            bool result = false;

            if (roleDesp.IsNotEmpty())
            {
                string[] appRoles = roleDesp.Split(';');

                for (int i = 0; i < appRoles.Length; i++)
                {
                    string[] oneAppRoles = appRoles[i].Split(':');

                    ExceptionHelper.FalseThrow<AuthenticateException>(oneAppRoles.Length == 2, Resource.InvalidAppRoleNameDescription);

                    string appName = oneAppRoles[0].Trim();

                    string[] roles = oneAppRoles[1].Split(',');

                    for (int j = 0; j < roles.Length; j++)
                    {
                        string roleName = roles[j].Trim();

                        result = roleFunc(appName, roleName);

                        if (result)
                            break;
                    }
                }
            }

            return result;
        }
  
        /// <summary>
        /// 如果是Web App(有HttpContext的)，则通过设置Cookie失效的方式来清除缓存中的Principal
        /// </summary>
        public void ClearCacheInWebApp()
        {
            if (EnvironmentHelper.Mode == InstanceMode.Web)
            {
                string cookieName = Common.C_SESSION_KEY_NAME;

                HttpCookie cookie = HttpContext.Current.Response.Cookies[cookieName];

                if (cookie != null)
                {
                    HttpContext.Current.Response.Cookies.Remove(cookieName);

                    cookie = new HttpCookie(cookieName, "Principal");
                    cookie.Expires = SNTPClient.AdjustedTime.AddDays(-1);

                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
            }
        }

        /// <summary>
        /// 根据描述得到一组角色集合
        /// </summary>
        /// <param name="rolesDesp"></param>
        /// <returns></returns>
        public static IRole[] GetRoles(string rolesDesp)
        {
            List<IRole> result = new List<IRole>();

            if (string.IsNullOrEmpty(rolesDesp) == false)
            {
                AppRolesDespsCollection allAppRolesDesps = GetAppRolesDesps(rolesDesp);

                ApplicationCollection apps = PermissionMechanismFactory.GetMechanism().GetApplications(allAppRolesDesps.GetAppNames());

                foreach (AppRolesDesps appRolesDesps in allAppRolesDesps)
                {
                    if (apps.ContainsKey(appRolesDesps.AppName))
                    {
                        IApplication app = apps[appRolesDesps.AppName];

                        foreach (string roleName in appRolesDesps.RolesDesp)
                        {
                            if (app.Roles.ContainsKey(roleName))
                            {
                                result.Add(app.Roles[roleName]);
                            }
                        }
                    }
                }
            }

            return result.ToArray();
        }

        private static AppRolesDespsCollection GetAppRolesDesps(string rolesDesp)
        {
            AppRolesDespsCollection appRolesDesps = new AppRolesDespsCollection();

            string[] appRoles = rolesDesp.Split(';');

            for (int i = 0; i < appRoles.Length; i++)
            {
                string[] oneAppRoles = appRoles[i].Split(':');

                ExceptionHelper.FalseThrow<AuthenticateException>(oneAppRoles.Length == 2, Resource.InvalidAppRoleNameDescription);

                string appName = oneAppRoles[0].Trim();

                string[] roles = oneAppRoles[1].Split(',');

                appRolesDesps.Add(new AppRolesDesps { AppName = appName, RolesDesp = roles });
            }

            return appRolesDesps;
        }

        #endregion

        private class AppRolesDesps
        {
            public string AppName;
            public string[] RolesDesp;
        }

        private class AppRolesDespsCollection : EditableDataObjectCollectionBase<AppRolesDesps>
        {
            public string[] GetAppNames()
            {
                List<string> result = new List<string>();

                foreach (AppRolesDesps item in this)
                    result.Add(item.AppName);

                return result.ToArray();
            }
        }

        /// <summary>
        /// 得到票据相关的Token容器
        /// </summary>
        /// <returns></returns>
        public GenericTicketTokenContainer GetGenericTicketTokenContainer()
        {
            GenericTicketTokenContainer container = new GenericTicketTokenContainer();

            container.CopyFrom(this.Identity as DeluxeIdentity);

            return container;
        }

        /// <summary>
        /// 根据当前请求创建Principal
        /// </summary>
        /// <param name="autoRedirect">是否自动跳转到登录页</param>
        /// <returns></returns>
        public static IPrincipal CreateByRequest(bool autoRedirect)
        {
            return DoAuthentication(autoRedirect);
        }

        private static IPrincipal DoAuthentication(bool autoRedirect)
        {
            IPrincipal principal = null;
            ITicket ticket;

            string logonName = InternalGetLogOnName(autoRedirect, out ticket);

            if (logonName.IsNotEmpty())
            {
                logonName = ImpersonateSettings.GetConfig().Impersonation[logonName];

                LogOnIdentity loi = new LogOnIdentity(logonName);

                if (ticket != null)
                    ticket.SignInInfo.UserID = loi.LogOnNameWithDomain;

                principal = SetPrincipal(loi.LogOnNameWithDomain, ticket);
            }

            return principal;
        }

        private static string InternalGetLogOnName(bool autoRedirect, out ITicket ticket)
        {
            string userID = string.Empty;
            ticket = null;

            if (ImpersonateSettings.GetConfig().EnableTestUser)
            {
                //是否使用测试帐户
                userID = HttpContext.Current.Request.Headers["testUserID"];

                if (userID.IsNotEmpty())
                    HttpContext.Current.Response.AppendHeader("testUserID", userID);
                else
                    userID = ImpersonateSettings.GetConfig().TestUserID;
            }

            if (userID.IsNullOrEmpty())
                userID = GetLogOnName(autoRedirect, out ticket);

            return userID;
        }

        /// <summary>
        /// 进行认证，返回用户名
        /// </summary>
        /// <param name="autoRedirect"></param>
        /// <param name="ticket"><see cref="ITicket"/> 对象。</param>
        /// <returns>用户名</returns>
        private static string GetLogOnName(bool autoRedirect, out ITicket ticket)
        {
            string userID = string.Empty;
            ticket = CheckAuthenticatedAndGetTicket(autoRedirect);

            if (ticket != null)
            {
                if (PassportClientSettings.GetConfig().IdentityWithoutDomainName)
                    userID = ticket.SignInInfo.UserID;
                else
                    userID = ticket.SignInInfo.UserIDWithDomain;
            }

            return userID;
        }

        private static ITicket CheckAuthenticatedAndGetTicket(bool autoRedirect)
        {
            AuthenticateDirElement aDir =
                AuthenticateDirSettings.GetConfig().AuthenticateDirs.GetMatchedElement<AuthenticateDirElement>();

            bool needRedirect = autoRedirect && (aDir == null || aDir.AutoRedirect);

            PassportManager.CheckAuthenticated(needRedirect);

            bool fromCookie = false;

            return PassportManager.GetTicket(out fromCookie);
        }

        private static IPrincipal SetPrincipal(string userID, ITicket ticket)
        {
            IPrincipal principal = GetPrincipalInSession(userID);

            if (principal == null)
            {
                LogOnIdentity loi = new LogOnIdentity(userID);

                string identityID = string.Empty;

                if (PassportClientSettings.GetConfig().IdentityWithoutDomainName)
                    identityID = loi.LogOnNameWithoutDomain;
                else
                    identityID = loi.LogOnName;

                principal = PrincipalSettings.GetConfig().GetPrincipalBuilder().CreatePrincipal(identityID, ticket);

                HttpCookie cookie = new HttpCookie(Common.GetPrincipalSessionKey());
                cookie.Expires = DateTime.MinValue;

                CookieCacheDependency cookieDependency = new CookieCacheDependency(cookie);

                SlidingTimeDependency slidingDependency =
                    new SlidingTimeDependency(Common.GetSessionTimeOut());

                PrincipalCache.Instance.Add(
                    Common.GetPrincipalSessionKey(),
                    principal,
                    new MixedDependency(cookieDependency, slidingDependency));
            }

            PrincipaContextAccessor.SetPrincipal(principal);

            return principal;
        }

        private static IPrincipal GetPrincipalInSession(string userID)
        {
            string strKey = Common.GetPrincipalSessionKey();
            IPrincipal principal;

            if (PrincipalCache.Instance.TryGetValue(strKey, out principal))
                if (string.Compare(principal.Identity.Name, userID, true) != 0)
                    principal = null;

            return principal;
        }
    }
}
