using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class PhoneAdapter : CustomerAdapterBase<Phone, PhoneCollection>
    {
        public static readonly PhoneAdapter Instance = new PhoneAdapter();

        private PhoneAdapter()
        {
        }

        public PhoneCollection LoadByOwnerID(string ownerID)
        {
            return this.Load(builder => builder.AppendItem("OwnerID", ownerID));
        }

        public void LoadByOwnerIDInContext(string ownerID, Action<PhoneCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("OwnerID", ownerID)), action);
        }

        public void UpdateByOwnerIDInContext(string ownerID, PhoneCollection phones)
        {
            ownerID.CheckStringIsNullOrEmpty("ownerID");
            phones.NullCheck("phones");

            Dictionary<string, object> context = new Dictionary<string, object>();

            SqlContextItem sqlContext = this.GetSqlContext();

            this.DeleteInContext(builder => builder.AppendItem("OwnerID", ownerID));

            foreach (Phone phone in phones)
            {
                sqlContext.AppendSqlInContext(TSqlBuilder.Instance, TSqlBuilder.Instance.DBStatementSeperator);
                this.InnerInsertInContext(phone, sqlContext, context);
            }
        }
    }
}
