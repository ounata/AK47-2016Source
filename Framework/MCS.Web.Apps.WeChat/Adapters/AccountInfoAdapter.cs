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
    public class AccountInfoAdapter : UpdatableAndLoadableAdapterBase<AccountInfo, AccountInfoCollection>
    {
        public static readonly AccountInfoAdapter Instance = new AccountInfoAdapter();

        private AccountInfoAdapter()
        {
        }

        public AccountInfoCollection LoadAll()
        {
            return base.Load(new WhereLoadingCondition(builder => builder.AppendItem("1", 1)));
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.WeChatInfoDBConnectionName;
        }
    }
}
