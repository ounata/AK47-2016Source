using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects;
using MCS.Web.Apps.WeChat.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Web.Apps.WeChat.Adapters
{
	public class WeChatGroupAdapter : WeChatObjectAdapterBase<WeChatGroup, WeChatGroupCollection>
	{
		public static readonly WeChatGroupAdapter Instance = new WeChatGroupAdapter();

		private WeChatGroupAdapter()
		{
		}

		public WeChatGroup Load(string accountID, int groupID)
		{
			accountID.CheckStringIsNullOrEmpty("accountID");

			return Load(builder =>
				{
					builder.AppendItem("AccountID", accountID);
					builder.AppendItem("GroupID", groupID);
				}).FirstOrDefault();
		}

        public WeChatGroupCollection LoadAll()
        {
            return base.Load(new WhereLoadingCondition(builder => builder.AppendItem("1", 1)));
        }
    }
}
