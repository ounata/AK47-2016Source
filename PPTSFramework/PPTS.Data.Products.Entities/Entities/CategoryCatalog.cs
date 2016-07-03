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
    /// 产品分类 
    /// </summary>
    [Serializable]
    [ORTableMapping("PM.v_CategoryCatalogs")]
    [DataContract]
    public class CategoryCatalog
    {
        /// <summary>
        /// 产品类别ID
        /// </summary>
        [ORFieldMapping("CategoryID")]
        [DataMember]
        public string CategoryID { set; get; }

        /// <summary>
        /// 产品分类ID
        /// </summary>
        [ORFieldMapping("CatalogID")]
        [DataMember]
        public string CatalogID { set; get; }

        /// <summary>
        /// 产品分类编码
        /// </summary>
        [ORFieldMapping("Catalog")]
        [DataMember]
        public string Catalog { set; get; }

        /// <summary>
        /// 产品分类名称
        /// </summary>
        [ORFieldMapping("CatalogName")]
        [DataMember]
        public string CatalogName { set; get; }

        /// <summary>
        /// 产品类型编码
        /// </summary>
        [ORFieldMapping("Category")]
        [DataMember]
        public string Category { set; get; }

        /// <summary>
        /// 是否有合作
        /// </summary>
        [ORFieldMapping("HasPartner")]
        [DataMember]
        public int HasPartner { set; get; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [ORFieldMapping("Eanbled")]
        [DataMember]
        public int Eanbled { set; get; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [ORFieldMapping("SortNo")]
        [DataMember]
        public int SortNo { set; get; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
        [DataMember]
        public string CreatorID { set; get; }

        /// <summary>
        /// 创建人
        /// </summary>
        [ORFieldMapping("CreatorName")]
        [DataMember]
        public string CreatorName { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime")]
        [DataMember]
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [ORFieldMapping("ModifierID")]
        [DataMember]
        public string ModifierID { set; get; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [ORFieldMapping("ModifierName")]
        [DataMember]
        public string ModifierName { set; get; }

        /// <summary>
        /// ModifyTime
        /// </summary>
        [ORFieldMapping("ModifyTime")]
        [DataMember]
        public DateTime ModifyTime { set; get; }
        


    }


    [Serializable]
    [DataContract]
    public class CategoryCatalogCollection : EditableDataObjectCollectionBase<CategoryCatalog>
    {
        public CategoryCatalogCollection Where(Func<CategoryCatalog,bool> fun)
        {
            var result = new CategoryCatalogCollection();
            this.ForEach(m => { if (fun(m)) { result.Add(m); } });
            return result;
        }
    }


}
