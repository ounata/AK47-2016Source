using MCS.Library.Data.Adapters;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Authorization
{
    public abstract class RecordOrgAuthorizationAdapterBase : UpdatableAndLoadableAdapterBase<RecordOrgAuthorization, RecordOrgAuthorizationCollection>
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSMetaDataConnectionName;
        }
    }
}

