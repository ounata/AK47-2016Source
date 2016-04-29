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
	public class TeacherTeachingAdapter : UpdatableAndLoadableAdapterBase<TeacherTeaching, TeacherTeachingCollection>
	{
		public static readonly TeacherTeachingAdapter Instance = new TeacherTeachingAdapter();

		private TeacherTeachingAdapter()
		{
		}
                
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSMetaDataConnectionName;
        }
	}
}