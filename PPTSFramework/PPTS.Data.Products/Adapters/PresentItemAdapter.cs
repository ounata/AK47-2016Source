using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;

namespace PPTS.Data.Products.Adapters
{
    /// <summary>
    /// 买赠 相关的Adapter的基类
    /// </summary>
    public class PresentItemAdapter : UpdatableAndLoadableAdapterBase<PresentItem, PresentItemCollection> 
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSProductConnectionName;
        }

        public static PresentItemAdapter Instance = new PresentItemAdapter();

        public PresentItemCollection LoadByPresentIds(string []presentIds)
        {
            return this.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem(presentIds), "PresentID"));

        }

        

    }
}
