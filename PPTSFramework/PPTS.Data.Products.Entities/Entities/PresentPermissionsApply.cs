using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Entities
{
    [Serializable]
    [ORTableMapping("[PM].[PresentPermissionsApplies]")]
    [DataContract]
    public class PresentPermissionsApply : IEntityWithCreator
    {
        /// <summary>
        /// 校区ID
        /// </summary>
        [ORFieldMapping("CampusID", PrimaryKey = true)]
        [DataMember]
        public string CampusID
        {
            get;
            set;
        }

        /// <summary>
        /// 买赠ID
        /// </summary>
        [ORFieldMapping("PresentID", PrimaryKey = true)]
        [DataMember]
        public string PresentID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [ORFieldMapping("CreatorName")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class PresentPermissionsApplieCollection : EditableDataObjectCollectionBase<PresentPermissionsApply>
    {
    }
}
