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
    public class WeChatAppMessageAdapter : UpdatableAndLoadableAdapterBase<WeChatAppMessage, WeChatAppMessageCollection>
    {
        public static readonly WeChatAppMessageAdapter Instance = new WeChatAppMessageAdapter();

        private WeChatAppMessageAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.WeChatInfoDBConnectionName;
        }
    }
}
