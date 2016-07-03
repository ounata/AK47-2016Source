using System;
using System.Linq;
using System.Collections.Generic;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;

namespace PPTS.Data.Customers.Adapters
{
    public class AccountRecordAdapter : CustomerAdapterBase<AccountRecord, AccountRecordCollection>
    {
        public static readonly AccountRecordAdapter Instance = new AccountRecordAdapter();

    }
}
