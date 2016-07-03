using System;
using System.Runtime.Serialization;
using System.Text;
using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    [Serializable]
    public class ParentModel : Parent, IContactPhoneNumbers
    {
        [NoMapping]
        [DataMember]
        public Phone PrimaryPhone
        {
            get;
            set;
        }

        [NoMapping]
        [DataMember]
        public Phone SecondaryPhone
        {
            get;
            set;
        }

        public string ToSearchContent()
        {
            StringBuilder strB = new StringBuilder();

            this.ParentName.IsNotEmpty(s => strB.AppendWithSplitChars(s));
            this.ParentCode.IsNotEmpty(s => strB.AppendWithSplitChars(s));

            this.PrimaryPhone.IsNotNull(p => p.IsValidNumber((pi, phoneType) => strB.AppendWithSplitChars(pi.ToPhoneNumber())));
            this.SecondaryPhone.IsNotNull(p => p.IsValidNumber((pi, phoneType) => strB.AppendWithSplitChars(pi.ToPhoneNumber())));

            return strB.ToString();
        }
    }

    [Serializable]
    public class ParentModelCollection : EditableDataObjectCollectionBase<ParentModel>
    {
    }
    
}