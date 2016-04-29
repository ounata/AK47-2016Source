using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Entities
{
    [Serializable]
    [ORTableMapping("MT.Configs")]
    public class ConfigEntity
    {
        [ORFieldMapping("ConfigKey", PrimaryKey = true)]
        public string ConfigKey
        {
            get;
            set;
        }

        [ORFieldMapping("ConfigValue")]
        public string ConfigValue
        {
            get;
            set;
        }

        [ORFieldMapping("Description")]
        public string Description
        {
            get;
            set;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ConfigEntityCollection : EditableDataObjectCollectionBase<ConfigEntity>
    {
    }
}
