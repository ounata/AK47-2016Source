#region
// -------------------------------------------------
// Assembly	��	DeluxeWorks.Library.Passport
// FileName	��	DeluxePrincipal.cs
// Remark	��	
// -------------------------------------------------
// VERSION  	AUTHOR		DATE			CONTENT
// 1.0
// 1.1          ����ǿ      2008-12-2       ���ע��
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
    /// ͨ��DeluxeWorks��֤���ƣ����������û���ݼ�����Ȩ��Ϣ��ʵ����ϵͳ��IPrincipal�ӿڡ�
    /// </summary>
    public class DeluxePrincipal : IGenericTokenPrincipal
    {
        private DeluxeIdentity userIdentity = null;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="identity"></param>
        public DeluxePrincipal(DeluxeIdentity identity)
        {
            this.userIdentity = identity;
        }

        /// <summary>
        /// �ӵ�ǰ�߳���ȡ���û��ĵ�¼��Ȩ����Ϣ(Principal)
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
        /// �Ƿ񾭹���֤
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

        #region IPrincipal ��Ա
        /// <summary>
        /// �û���ݱ�ʶ
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return this.userIdentity;
            }
        }

        /// <summary>
        /// �жϵ�ǰ�û��Ƿ�����ĳһ��ɫ
        /// </summary>
        /// <param name="roleDesp">��ɫ������(Ӧ�õ�����1:��ɫ����11,��ɫ����12,...;Ӧ������2:��ɫ����21,��ɫ����22,...)</param>
        /// <returns>�û��Ƿ�����ĳһ��ɫ</returns>
        public bool IsInRole(string roleDesp)
        {
            return IsInRole(this.userIdentity.User, roleDesp);
        }

        /// <summary>
        /// �ж�ĳ���û��Ƿ�����ĳһ��ɫ
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleDesp">��ɫ������(Ӧ�õ�����1:��ɫ����11,��ɫ����12,...;Ӧ������2:��ɫ����21,��ɫ����22,...)</param>
        /// <returns>�û��Ƿ�����ĳһ��ɫ</returns>
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
        /// ����ĳһ��ɫ��Ȩ�ޣ�������������������Ӧ�úͽ�ɫ��Ȩ�ޣ���Ϊ�������ݵ��ص�����
        /// </summary>
        /// <param name="roleDesp">��ɫ������(Ӧ�õ�����1:��ɫ����11,��ɫ����12,...;Ӧ������2:��ɫ����21,��ɫ����22,...)</param>
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
        /// �����Web App(��HttpContext��)����ͨ������CookieʧЧ�ķ�ʽ����������е�Principal
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
        /// ���������õ�һ���ɫ����
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
        /// �õ�Ʊ����ص�Token����
        /// </summary>
        /// <returns></returns>
        public GenericTicketTokenContainer GetGenericTicketTokenContainer()
        {
            GenericTicketTokenContainer container = new GenericTicketTokenContainer();

            container.CopyFrom(this.Identity as DeluxeIdentity);

            return container;
        }

        /// <summary>
        /// ���ݵ�ǰ���󴴽�Principal
        /// </summary>
        /// <param name="autoRedirect">�Ƿ��Զ���ת����¼ҳ</param>
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
                //�Ƿ�ʹ�ò����ʻ�
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
        /// ������֤�������û���
        /// </summary>
        /// <param name="autoRedirect"></param>
        /// <param name="ticket"><see cref="ITicket"/> ����</param>
        /// <returns>�û���</returns>
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
