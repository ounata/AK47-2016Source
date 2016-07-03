using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTS.Data.Common.Entities;

namespace PPTS.Data.Common.Adapters
{
    public class MutexSettingAdapter : UpdatableAndLoadableAdapterBase<MutexSetting, MutexSettingCollection>
    {
        public static readonly MutexSettingAdapter Instance = new MutexSettingAdapter();

        private MutexSettingAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSMetaDataConnectionName;
        }

        public MutexSettingCollection LoadAll()
        {
            return this.Load(builder => builder.AppendItem("", "1=1", "", true));
        }
    }
}