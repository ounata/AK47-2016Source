using MCS.Library.Data.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;

namespace PPTS.Data.Products.Adapters
{
    /// <summary>
    /// 买赠 相关的Adapter的基类
    /// </summary>
    public class PresentAdapter : UpdatableAndLoadableAdapterBase<Present, PresentCollection> 
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSProductConnectionName;
        }

        public static PresentAdapter Instance = new PresentAdapter();

        public PresentCollection LoadByOrgId(string orgId)
        {
            return this.Load(builder => builder.AppendItem("OwnOrgID", orgId));
        }

        public void LoadByOrgIdInContext(string orgId,Action<PresentCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("OwnOrgID", orgId)), action);
        }
        

    }
}
