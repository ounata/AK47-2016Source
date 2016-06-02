#region
// -------------------------------------------------
// Assembly	：	DeluxeWorks.Library
// FileName	：	EnvironmentHelper.cs
// Remark	：	处理应用环境问题的类 
// -------------------------------------------------
// VERSION  	AUTHOR		DATE			CONTENT
// 1.0		    沈峥	    20070430		创建
// -------------------------------------------------
#endregion
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;

namespace MCS.Library.Core
{
    /// <summary>
    /// Instance mode
    /// </summary>
    public enum InstanceMode
    {
        /// <summary>
        /// Windows应用
        /// </summary>
        Windows,

        /// <summary>
        /// Asp.net应用，能够获得HttpContext
        /// </summary>
        Web
    }

    /// <summary>
    /// 操作系统或运行环境的类型
    /// </summary>
    public enum EnvironmentSystemType
    {
        /// <summary>
        /// Win32系统
        /// </summary>
        Windows32 = 4,

        /// <summary>
        /// Win64系统
        /// </summary>
        Windows64 = 8,

        /// <summary>
        /// Win128系统
        /// </summary>
        Windows128 = 16
    }

    /// <summary>
    /// 处理应用环境问题的类
    /// </summary>
    /// <remarks>处理应用环境问题的类
    /// </remarks>
    public static class EnvironmentHelper
    {
        private const string RegistryRoot = "SOFTWARE\\AK47\\Variables";

        #region Private field

        private static string shortDomainName;
        private static string domainDnsName;
        private static string computerNetBIOSName;

        #endregion

        #region Constructor
        /// <summary>
        /// EnvironmentHelper的构造函数
        /// </summary>
        /// <remarks>EnvironmentHelper的构造函数,该构造函数不带任何参数。
        /// </remarks>
        static EnvironmentHelper()
        {
            EnvironmentHelper.shortDomainName = GetShortDomainName();
            EnvironmentHelper.domainDnsName = GetDomainDnsName();
            EnvironmentHelper.computerNetBIOSName = GetComputerNetBIOSName();
        }
        #endregion

        #region InterOP
        internal enum COMPUTER_NAME_FORMAT
        {
            ComputerNameNetBIOS,
            ComputerNameDnsHostname,
            ComputerNameDnsDomain,
            ComputerNameDnsFullyQualified,
            ComputerNamePhysicalNetBIOS,
            ComputerNamePhysicalDnsHostname,
            ComputerNamePhysicalDnsDomain,
            ComputerNamePhysicalDnsFullyQualified,
            ComputerNameMax,
        }

        internal enum EXTENDED_NAME_FORMAT
        {
            NameUnknown = 0,
            NameFullyQualifiedDN = 1,
            NameSamCompatible = 2,
            NameDisplay = 3,
            NameUniqueId = 6,
            NameCanonical = 7,
            NameUserPrincipal = 8,
            NameCanonicalEx = 9,
            NameServicePrincipal = 10,
            NameDnsDomain = 12,
        }

        /*
        ComputerNameDnsDomain:			oa.hgzs.ain.cn
        ComputerNameDnsFullyQualified:	mcs-shenzheng.oa.hgzs.ain.cn
        ComputerNameDnsHostname:		mcs-shenzheng
        ComputerNameNetBIOS:			MCS-SHENZHENG
        ComputerNamePhysicalDnsDomain:	oa.hgzs.ain.cn
        ComputerNamePhysicalDnsFullyQualified:
                                        mcs-shenzheng.oa.hgzs.ain.cn
        ComputerNamePhysicalDnsHostname:
                                        mcs-shenzheng
        ComputerNamePhysicalNetBIOS:	MCS-SHENZHENG

        NameCanonical:			oa.hgzs.ain.cn/Computers/MCS-SHENZHENG
        NameCanonicalEx:		oa.hgzs.ain.cn/Computers
        MCS-SHENZHENG
        NameDisplay:			MCS-SHENZHENG$
        NameDnsDomain:		
        NameFullyQualifiedDN:	CN=MCS-SHENZHENG,CN=Computers,DC=oa,DC=hgzs,DC=ain,DC=cn
        NameSamCompatible:		OA\MCS-SHENZHENG$
        NameServicePrincipal:		
        NameUniqueId:			{848ed57e-afd5-49e9-8d9c-cdb12bd6cf9a}
        NameUnknown:		
        NameUserPrincipal:
        */
        #endregion InterOP

        #region Public
        /// <summary>
        /// 获取当前电脑的NetBIOS名称
        /// </summary>
        public static string ComputerNetBIOSName
        {
            get
            {
                return EnvironmentHelper.computerNetBIOSName;
            }
        }

        /// <summary>
        /// 是否使用web.config作为配置文件
        /// </summary>
        public static bool IsUsingWebConfig
        {
            get
            {
                return HostingEnvironment.IsHosted;
            }
        }

        /// <summary>
        /// 获取运行环境的系统类型（32,64...）
        /// </summary>
        public static EnvironmentSystemType SystemType
        {
            get
            {
                EnvironmentSystemType result = EnvironmentSystemType.Windows32;

                switch (IntPtr.Size)
                {
                    case 4:
                        result = EnvironmentSystemType.Windows32;
                        break;
                    case 8:
                        result = EnvironmentSystemType.Windows64;
                        break;
                    case 16:
                        result = EnvironmentSystemType.Windows128;
                        break;
                }

                return result;
            }
        }

        /// <summary>
        /// 当前应用是否为web应用的属性 ( Windows / Web)
        /// </summary>
        /// <remarks>该属性是只读的。
        /// <seealso cref="MCS.Library.Configuration.ConfigurationBroker"/>
        /// <seealso cref="MCS.Library.Configuration.MetaConfigurationSourceInstanceElement"/>
        /// <seealso cref="MCS.Library.Configuration.MetaConfigurationSourceMappingElement"/>
        /// </remarks>
        public static InstanceMode Mode
        {
            get
            {
                if (EnvironmentHelper.CheckIsWebApplication())
                    return InstanceMode.Web;
                else
                    return InstanceMode.Windows;
            }
        }

        /// <summary>
        /// 如果机器在域上注册，返回短域名
        /// </summary>
        /// <remarks>改属性只读。如果机器在域上的短名称是oa\hb2004-db，那么ShortDomainName就是oa。如果没有加入域，则返回空串</remarks>
        public static string ShortDomainName
        {
            get
            {
                return EnvironmentHelper.shortDomainName;
            }
        }

        /// <summary>
        /// 如果机器在域上注册，返回长域名
        /// </summary>
        /// <remarks>改属性只读。域的长名称如oa.hgzs.ain.cn。如果没有加入域，则返回空串</remarks>
        public static string DomainDnsName
        {
            get
            {
                return EnvironmentHelper.domainDnsName;
            }
        }

        /// <summary>
        /// 替代字符串中的环境变量（先从注册表中查找，然后再从系统环境变量中查找
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReplaceVariablesInString(string filePath)
        {
            string result = ExceptionHelper.DoSilentFunc(() => ReplaceRegistryVariablesInString(filePath), filePath);
            result = ExceptionHelper.DoSilentFunc(() => ReplaceEnvironmentVariablesInString(result), result);

            return result;
        }

        /// <summary>
        /// 替代字符串中的环境变量（从系统环境变量获取值）
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReplaceEnvironmentVariablesInString(string filePath)
        {
            string result = filePath;

            EnumerationVariables(filePath, (sourceValue, variableName) =>
            {
                string variableValue = Environment.GetEnvironmentVariable(variableName);

                if (variableValue != null)
                    result = result.Replace(sourceValue, variableValue);

                return true;
            });

            return result;
        }

        /// <summary>
        /// 替代字符串中的环境变量（从注册表中获取值）
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReplaceRegistryVariablesInString(string filePath)
        {
            string result = filePath;

            EnumerationVariables(filePath, (sourceValue, variableName) =>
            {
                using (RegistryKey subKey = Registry.LocalMachine.OpenSubKey(RegistryRoot))
                {
                    object regValue = subKey.GetValue(variableName, string.Empty);

                    if (regValue is string)
                    {
                        string variableValue = (string)regValue;

                        if (variableValue.IsNotEmpty())
                            result = result.Replace(sourceValue, variableValue);
                    }
                }

                return true;
            });

            return result;
        }

        /// <summary>
        /// 得到当前环境的描述信息
        /// </summary>
        /// <returns></returns>
        public static string GetEnvironmentInfo()
        {
            StringBuilder strB = new StringBuilder(1024);

            using (StringWriter writer = new StringWriter(strB))
            {
                if (HostingEnvironment.IsHosted)
                    WriteHostInfo(writer);

                if (Mode == InstanceMode.Web)
                    WriteHttpContextInfo(writer);

                WriteProcessInfo(writer);
            }

            return strB.ToString();
        }

        /// <summary>
        /// 获取HttpRequest的客户端的IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            IPAddress ip = null;

            if (CheckIsWebApplication())
            {
                ip = HttpContext.Current.Request.ServerVariables.GetValue("HTTP_X_FORWARDED_FOR", string.Empty).GetFirstIPAddress();

                if (ip == null)
                {
                    ip = HttpContext.Current.Request.ServerVariables.GetValue("REMOTE_ADDR", string.Empty).GetFirstIPAddress();

                    if (ip == null)
                        ip = HttpContext.Current.Request.UserHostAddress.GetFirstIPAddress();
                }
            }

            return (ip != null) ? ip.ToString() : string.Empty;
        }
        #endregion Public

        #region Private helper method
        private static void EnumerationVariables(string filePath, Func<string, string, bool> func)
        {
            func.NullCheck("func");

            Regex r = new Regex(@"%\S+?%");

            foreach (Match m in r.Matches(filePath))
            {
                string variableName = m.Value.Trim('%');

                if (func(m.Value, variableName) == false)
                    break;
            }
        }

        private static void WriteProcessInfo(TextWriter writer)
        {
            try
            {
                Process process = Process.GetCurrentProcess();

                writer.WriteLine("MachineName: {0}, Process ID: {1}, Name: {2}", GetComputerNetBIOSName(), process.Id, process.ProcessName);
            }
            catch (System.Exception)
            {
            }
        }

        private static void WriteHostInfo(TextWriter writer)
        {
            writer.WriteLine("Host Info:");
            if (HostingEnvironment.ApplicationHost != null)
                writer.WriteLine("Site ID: {0}", HostingEnvironment.ApplicationHost.GetSiteID());

            writer.WriteLine("Site Name: {0}", HostingEnvironment.SiteName);
            writer.WriteLine("Application Virtual Path: {0}", HostingEnvironment.ApplicationVirtualPath);
            writer.WriteLine("Application ID: {0}", HostingEnvironment.ApplicationID);
        }

        private static void WriteHttpContextInfo(TextWriter writer)
        {
            writer.WriteLine("Request Info:");
            HttpRequest request = HttpContext.Current.Request;

            writer.WriteLine("Url: {0}", request.Url.ToString());

            if (request.UrlReferrer != null)
                writer.WriteLine("Url Referrer: {0}", request.UrlReferrer.ToString());

            writer.WriteLine("User Agent: {0}", request.UserAgent);
            writer.WriteLine("User Host Address: {0}", request.UserHostAddress);
        }

        private static bool CheckIsWebApplication()
        {
            bool isWebApp = false;

            AppDomain domain = AppDomain.CurrentDomain;
            try
            {
                if (domain.ShadowCopyFiles)
                    isWebApp = (HttpContext.Current != null);
            }
            catch (System.Exception)
            {
            }

            return isWebApp;
        }

        private static string GetShortDomainName()
        {
            string machineName = InnerGetComputerObjectName(EXTENDED_NAME_FORMAT.NameSamCompatible);

            string[] nameParts = machineName.Split('\\', '/');

            return nameParts[0];
        }

        private static string GetDomainDnsName()
        {
            return InnerGetComputerName(COMPUTER_NAME_FORMAT.ComputerNamePhysicalDnsDomain);
        }

        private static string GetComputerNetBIOSName()
        {
            return InnerGetComputerName(COMPUTER_NAME_FORMAT.ComputerNameNetBIOS);
        }

        private static string InnerGetComputerObjectName(EXTENDED_NAME_FORMAT nameFormat)
        {
            StringBuilder strB = new StringBuilder(1024);

            ulong nSize = (ulong)strB.Capacity;

            NativeMethods.GetComputerObjectName(nameFormat, strB, ref nSize);

            return strB.ToString();
        }

        private static string InnerGetComputerName(COMPUTER_NAME_FORMAT nameType)
        {
            StringBuilder strB = new StringBuilder(1024);

            uint nSize = (uint)strB.Capacity;

            NativeMethods.GetComputerNameEx(nameType, strB, ref nSize);

            return strB.ToString();
        }
        #endregion
    }
}
