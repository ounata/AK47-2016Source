#region
// -------------------------------------------------
// Assembly	£º	DeluxeWorks.Library.OGUPermission
// FileName	£º	OguPermissionSection.cs
// Remark	£º	»ú¹¹ÈËÔ±ºÍÊÚÈ¨¹ÜÀíÏà¹ØÅäÖÃÐÅÏ¢
// -------------------------------------------------
// VERSION  	AUTHOR		DATE			CONTENT
// 1.0		    Éòá¿	    20070430		´´½¨
// -------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using MCS.Library.Configuration;
using MCS.Library.Core;

namespace MCS.Library.OGUPermission
{
    /// <summary>
    /// 组织人员与授权的访问接口的配置信息类
    /// </summary>
    public sealed class OguPermissionSettings : ConfigurationSection
    {
        /// <summary>
        /// 得到配置节的信息
        /// </summary>
        /// <returns></returns>
        public static OguPermissionSettings GetConfig()
        {
            OguPermissionSettings settings = (OguPermissionSettings)ConfigurationBroker.GetSection("oguPermissionSettings");

            ConfigurationExceptionHelper.CheckSectionNotNull(settings, "oguPermissionSettings");

            return settings;
        }

        private OguPermissionSettings()
        {
        }

        /// <summary>
        /// 顶级部分映射
        /// </summary>
        [ConfigurationProperty("topOUMapping")]
        public OrganizationMappingElementCollection TopOUMapping
        {
            get
            {
                return (OrganizationMappingElementCollection)this["topOUMapping"];
            }
        }

        /// <summary>
        /// 根路径
        /// </summary>
        [ConfigurationProperty("rootOUPath", DefaultValue = "")]
        public string RootOUPath
        {
            get
            {
                return (string)this["rootOUPath"];
            }
        }

        /// <summary>
        /// 组织人员访问的Web Service地址
        /// </summary>
        public Uri OguServiceAddress
        {
            get
            {
                return Paths["oguServiceAddress"].Uri;
            }
        }

        /// <summary>
        /// 角色中包含的人员，是否依赖于FullPath（FullPath改变，角色是否失效）
        /// </summary>
        [ConfigurationProperty("roleRelatedUserParentDept", DefaultValue = true)]
        public bool RoleRelatedUserParentDept
        {
            get
            {
                return (bool)this["roleRelatedUserParentDept"];
            }
        }

        /// <summary>
        /// 授权信息的Web Service的地址
        /// </summary>
        public Uri AppAdminServiceAddress
        {
            get
            {
                return Paths["appAdminServiceAddress"].Uri;
            }
        }

        /// <summary>
        /// Web Service访问的超时时间
        /// </summary>
        [ConfigurationProperty("timeout", DefaultValue = "00:01:30", IsRequired = false)]
        public TimeSpan Timeout
        {
            get
            {
                return (TimeSpan)this["timeout"];
            }
        }

        /// <summary>
        /// 是否使用本地缓存
        /// </summary>
        [ConfigurationProperty("useLocalCache", DefaultValue = true, IsRequired = false)]
        public bool UseLocalCache
        {
            get
            {
                return (bool)this["useLocalCache"];
            }
        }

        /// <summary>
        /// 是否使用服务端缓存
        /// </summary>
        [ConfigurationProperty("useServerCache", DefaultValue = true, IsRequired = false)]
        public bool UseServerCache
        {
            get
            {
                return (bool)this["useServerCache"];
            }
        }

        [ConfigurationProperty("paths", IsRequired = true)]
        private UriConfigurationCollection Paths
        {
            get
            {
                return (UriConfigurationCollection)this["paths"];
            }
        }

        /// <summary>
        /// 创建组织人员访问类的工厂类
        /// </summary>
        public IOrganizationMechanism OguFactory
        {
            get
            {
                return (IOrganizationMechanism)TypeFactories["oguFactory"].CreateInstance();
            }
        }

        /// <summary>
        /// 创建授权信息访问类的工厂类
        /// </summary>
        public IPermissionMechanism PermissionFactory
        {
            get
            {
                return (IPermissionMechanism)TypeFactories["permissionFactory"].CreateInstance();
            }
        }

        /// <summary>
        /// 组织对象的实现类
        /// </summary>
        public IOguImplInterface OguObjectImpls
        {
            get
            {
                IOguImplInterface result = null;

                if (TypeFactories.ContainsKey("oguObjectImpls"))
                    result = (IOguImplInterface)TypeFactories["oguObjectImpls"].CreateInstance();
                else
                    result = OguAdminMechanism.Instance;

                return result;
            }
        }

        /// <summary>
        /// 授权对象的实现类
        /// </summary>
        public IPermissionImplInterface PermissionObjectImpls
        {
            get
            {
                IPermissionImplInterface result = null;

                if (TypeFactories.ContainsKey("permissionObjectImpls"))
                    result = (IPermissionImplInterface)TypeFactories["permissionObjectImpls"].CreateInstance();
                else
                    result = AppAdminMechanism.Instance;

                return result;
            }
        }

        /// <summary>
        /// 组织对象的实现类的工厂类
        /// </summary>
        public IOguObjectFactory OguObjectFactory
        {
            get
            {
                IOguObjectFactory factory = null;

                if (TypeFactories.ContainsKey("oguObjectFactory"))
                    factory = (IOguObjectFactory)TypeFactories["oguObjectFactory"].CreateInstance();
                else
                    factory = DefaultOguObjectFactory.Instance;

                return factory;
            }
        }

        /// <summary>
        /// 授权对象实现类的工厂类
        /// </summary>
        public IPermissionObjectFactory PermissionObjectFactory
        {
            get
            {
                IPermissionObjectFactory factory = null;

                if (TypeFactories.ContainsKey("permissionObjectFactory"))
                    factory = (IPermissionObjectFactory)TypeFactories["permissionObjectFactory"].CreateInstance();
                else
                    factory = DefaultPermissionObjectFactory.Instance;

                return factory;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="serializeCollectionKey"></param>
        protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
        {
            OrganizationMappingElementCollection.needReloadData = true;
            base.DeserializeElement(reader, serializeCollectionKey);
        }

        [ConfigurationProperty("typeFactories", IsRequired = true)]
        private TypeConfigurationCollection TypeFactories
        {
            get
            {
                return (TypeConfigurationCollection)this["typeFactories"];
            }
        }

        /// <summary>
        /// 连接串的映射
        /// </summary>
        [ConfigurationProperty("connectionMappings", IsRequired = false)]
        public OguConnectionMappingElementCollection ConnectionMappings
        {
            get
            {
                return (OguConnectionMappingElementCollection)this["connectionMappings"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            return true;
        }
    }
}
