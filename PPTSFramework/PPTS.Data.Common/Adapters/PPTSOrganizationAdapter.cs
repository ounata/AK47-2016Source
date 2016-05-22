using MCS.Library.Data.Adapters;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Builder;
using MCS.Library.Core;
using MCS.Library.Data;

namespace PPTS.Data.Common.Adapters
{
    public class PPTSOrganizationAdapter : UpdatableAndLoadableAdapterBase<PPTSOrganization, PPTSOrganizationCollection>
    {
        public static readonly PPTSOrganizationAdapter Instance = new PPTSOrganizationAdapter();
        private PPTSOrganizationAdapter()
        {

        }

        protected override int InnerDelete(PPTSOrganization data, Dictionary<string, object> context)
        {
            return 0;
            //return base.InnerDelete(data, context);
        }

        protected override string InnerDeleteInContext(PPTSOrganization data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            return null;
            //return base.InnerDeleteInContext(data, sqlContext, context);
        }

        public PPTSOrganization Load(string ID)
        {
            return this.Load(builder => builder.AppendItem("ID", ID)).SingleOrDefault();
        }

        public PPTSOrganization LoadByName(string name)
        {
            return this.Load(builder => builder.AppendItem("Name", name)).SingleOrDefault();
        }

        public PPTSOrganization LoadByShortName(string shortName)
        {
            return this.Load(builder => builder.AppendItem("ShortName", shortName)).SingleOrDefault();
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSPermissionCenterConnectionName;
        }
    }
}
