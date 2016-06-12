using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Products.Entities
{
    [Serializable]
    [ORTableMapping("[PM].[Categories]")]
    [DataContract]
    public class CategoryEntity : IEntityWithCreator, IEntityWithModifier
    {
        /// <summary>
        /// 产品类别ID
        /// </summary>
        [ORFieldMapping("CategoryID", PrimaryKey =true)]
        [DataMember]
        public string CategoryID { set; get; }


        /// <summary>
        /// 产品类型编码
        /// </summary>		
        [ORFieldMapping("Category")]
        [DataMember]
        public string Category
        {
            get;
            set;
        }
        /// <summary>
        /// 产品类型名称
        /// </summary>		
        [ORFieldMapping("CategoryName")]
        [DataMember]
        public string CategoryName
        {
            get;
            set;
        }
        /// <summary>
        /// 产品类别枚举（1对1，班组，游学，无课收合作，其它）
        /// </summary>		
        [ORFieldMapping("CategoryType")]
        [DataMember]
        public string CategoryType
        {
            get;
            set;
        }
        /// <summary>
        /// 是否课程管理
        /// </summary>		
        [ORFieldMapping("HasCourse")]
        [DataMember]
        public int HasCourse
        {
            get;
            set;
        }
        /// <summary>
        /// 订购时是否允许录入数量
        /// </summary>		
        [ORFieldMapping("CanInput")]
        [DataMember]
        public int CanInput
        {
            get;
            set;
        }
        /// <summary>
        /// 是否启用
        /// </summary>		
        [ORFieldMapping("Enabled")]
        [DataMember]
        public int Enabled
        {
            get;
            set;
        }
        /// <summary>
        /// 显示顺序
        /// </summary>		
        [ORFieldMapping("SortNo")]
        [DataMember]
        public int SortNo
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
        public string CreatorID { set; get; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [ORFieldMapping("CreatorName")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorName { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [ORFieldMapping("ModifierID")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string ModifierID { set; get; }

        /// <summary>
        /// 最后修改人姓名
        /// </summary>
        [ORFieldMapping("ModifierName")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string ModifierName { set; get; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [ORFieldMapping("ModifyTime", UtcTimeToLocal = true)]
        [DataMember]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()", ForceUseDefaultExpression = true)]
        public DateTime ModifyTime { set; get; }

    }

    [Serializable]
    [DataContract]
    public class CategoryEntityCollection : EditableDataObjectCollectionBase<CategoryEntity>
    {
    }


}
