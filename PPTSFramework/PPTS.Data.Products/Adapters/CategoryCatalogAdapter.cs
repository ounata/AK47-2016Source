using MCS.Library.Data.Adapters;
using PPTS.Data.Common;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;

namespace PPTS.Data.Products.Adapters
{
    /// <summary>
    /// 产品分类 相关的Adapter的基类
    /// </summary>
    public class CategoryCatalogAdapter : UpdatableAndLoadableAdapterBase<CategoryCatalog, CategoryCatalogCollection> 
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSProductConnectionName;
        }

        public static CategoryCatalogAdapter Instance = new CategoryCatalogAdapter();

        public CategoryCatalogCollection LoadByCategoryType(CategoryType type)
        {
            return this.Load(builder => builder.AppendItem("CategoryType", (int)type));
        }

        public void LoadByCategoryTypeInContext(CategoryType type, Action<CategoryCatalogCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("CategoryType", type)), action);
        }

        public CategoryCatalog LoadByCatalog(string catalog)
        {
            return Load(b => b.AppendItem("Catalog", catalog)).FirstOrNull();
        }

    }
}
