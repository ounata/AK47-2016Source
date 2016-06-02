using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products.Entities
{
    /// <summary>
    /// 买赠折扣生效折扣表视图
    /// </summary>
    [Serializable]
    [ORTableMapping("PM.v_PresentPermissions_Current")]
    [DataContract]
    public class PresentPermissionView
    {
        public PresentPermissionView()
        {
        }

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
        /// 生效日期
        /// </summary>
        [ORFieldMapping("StartDate")]
        [DataMember]
        public DateTime StartDate
        { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [ORFieldMapping("EndDate")]
        [DataMember]
        public DateTime EndDate
        { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
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
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class PresentPermissionViewCollection : EditableDataObjectCollectionBase<PresentPermissionView>
    {
    }
}
