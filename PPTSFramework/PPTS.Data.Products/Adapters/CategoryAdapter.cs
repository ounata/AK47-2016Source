using MCS.Library.Data.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;

namespace PPTS.Data.Products.Adapters
{
    /// <summary>
    /// 类别 二级分类
    /// </summary>
    public class CategoryAdapter : UpdatableAndLoadableAdapterBase<CategoryEntity, CategoryEntityCollection>
    {
        public static CategoryAdapter Instance = new CategoryAdapter();
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSProductConnectionName;
        }

        public CategoryEntityCollection LoadAll() {
            return this.Load(b => { }, o => o.AppendItem("SortNo", MCS.Library.Data.FieldSortDirection.Ascending));
        }

    }
}
