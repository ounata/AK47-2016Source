using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.SOA.DataObjects;
using MCS.Web.Apps.WeChat.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Web.Apps.WeChat.Adapters
{
    public class GroupAndMemberAdapter : UpdatableAndLoadableAdapterBase<GroupAndMember, GroupAndMemberCollection>
    {
        public static readonly GroupAndMemberAdapter Instance = new GroupAndMemberAdapter();

        private GroupAndMemberAdapter()
        {
        }

        public GroupAndMemberCollection LoadByGroupID(string groupID)
        {
            return base.LoadByInBuilder(new InLoadingCondition(builder => builder.AppendItem("groupID"), "GroupID"));
        }

        public void DeleteByGroupID(string groupID)
        {
            base.Delete(p => p.AppendItem("GroupID", groupID));
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.WeChatInfoDBConnectionName;
        }
    }
}
