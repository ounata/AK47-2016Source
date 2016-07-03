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
    public class PhoneAdapter : VersionedCustomerAdapterBase<Phone, PhoneCollection>
    {
        public static readonly PhoneAdapter Instance = new PhoneAdapter();

        private PhoneAdapter()
        {
        }

        public PhoneCollection LoadByOwnerID(string ownerID)
        {
            return this.Load(builder => builder.AppendItem("OwnerID", ownerID), DateTime.MinValue);
        }

        /// <summary>
        /// 得到某个owner的主要电话
        /// </summary>
        /// <param name="ownerID"></param>
        /// <returns></returns>
        public Phone LoadPrimaryPhoneByOwnerID(string ownerID)
        {
            return this.Load(builder => builder.AppendItem("OwnerID", ownerID).AppendItem("IsPrimary", 1), DateTime.MinValue).SingleOrDefault();
        }

        public void LoadByOwnerIDInContext(string ownerID, Action<PhoneCollection> action)
        {
            this.LoadInContext(new WhereLoadingCondition(builder => builder.AppendItem("OwnerID", ownerID)), action, DateTime.MinValue, this.GetQueryMappingInfo().GetQueryTableName());
        }

        public Phone LoadByPhoneNumber(string phoneNumber)
        {
            return this.Load(builder => builder.AppendItem("PhoneNumber", phoneNumber), DateTime.MinValue).FirstOrDefault();
        }

        public void UpdateByOwnerIDInContext(string ownerID, IEnumerable<Phone> phones)
        {
            ownerID.CheckStringIsNullOrEmpty("ownerID");
            phones.NullCheck("phones");

            this.UpdateCollectionInContext(new InSqlClauseBuilder("OwnerID").AppendItem(ownerID), phones);
        }        
    }
}
