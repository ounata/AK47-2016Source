using MCS.Library.Core;
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
    public class ConditionalGroupAdapter : UpdatableAndLoadableAdapterBase<ConditionalGroup, ConditionalGroupCollection>
    {
        public static readonly ConditionalGroupAdapter Instance = new ConditionalGroupAdapter();

        private ConditionalGroupAdapter()
        {
        }

        public ConditionalGroup Load(string groupID)
        {
            groupID.CheckStringIsNullOrEmpty("groupID");

            return base.LoadByInBuilder(new InLoadingCondition(builder =>
                builder.AppendItem(groupID), "GroupID")).FirstOrDefault();
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.WeChatInfoDBConnectionName;
        }

        public ConditionalGroupCollection LoadAll()
        {
            return base.Load(new WhereLoadingCondition(builder => builder.AppendItem("1", 1)));
        }
    }
}
