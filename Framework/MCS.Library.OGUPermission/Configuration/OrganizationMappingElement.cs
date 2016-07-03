using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.OGUPermission
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class OrganizationMappingElement : ConfigurationElement
    {
        internal OrganizationMappingElement()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("sourcePath", IsRequired = true, IsKey = true)]
        public string SourcePath
        {
            get
            {
                return (string)this["sourcePath"];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("destinationPath", IsRequired = true)]
        public string DestinationPath
        {
            get
            {
                return (string)this["destinationPath"];
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ConfigurationCollection(typeof(OrganizationMappingElement))]
    public sealed class OrganizationMappingElementCollection : ConfigurationElementCollection
    {
        private const int DefalutTopOULevel = 3;

        internal static bool needReloadData = true;

        private OrganizationMappingElementCollection()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int Level
        {
            get
            {
                //int nResult = DefalutTopOULevel;

                int nResult = (int)base["level"];

                return nResult;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="serializeCollectionKey"></param>
        protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
        {
            lock (typeof(OrganizationMappingElementCollection))
            {
                if (needReloadData)
                {
                    if (this.Properties.Contains("level"))
                        this.Properties.Remove("level");

                    string levelValue = reader.GetAttribute("level");

                    if (string.IsNullOrEmpty(levelValue))
                        levelValue = DefalutTopOULevel.ToString();

                    ConfigurationProperty property = new ConfigurationProperty("level", typeof(int), levelValue, ConfigurationPropertyOptions.None);

                    this.Properties.Add(property);

                    needReloadData = false;
                }
            }

            base.DeserializeElement(reader, serializeCollectionKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        /// <returns></returns>
        public bool GetMappedPath(string sourcePath, out string destPath)
        {
            bool result = false;

            destPath = sourcePath;

            OrganizationMappingElement elem = FindNearestElement(sourcePath);

            if (elem != null)
            {
                destPath = elem.DestinationPath;
                result = true;
            }

            return result;
        }

        private OrganizationMappingElement FindNearestElement(string sourcePath)
        {
            int matchLengh = -1;
            OrganizationMappingElement result = null;

            foreach (OrganizationMappingElement elem in this)
            {
                if (sourcePath.IndexOf(elem.SourcePath, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (elem.SourcePath.Length > matchLengh)
                    {
                        result = elem;
                        matchLengh = elem.SourcePath.Length;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((OrganizationMappingElement)element).SourcePath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new OrganizationMappingElement();
        }
    }
}
