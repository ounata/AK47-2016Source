using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    /// <summary>
    /// 客户关系的扩展方法
    /// </summary>
    public static class Extensions
    {
        private static readonly char[] PhoneSplitter = new char[] { '-' };

        public static PhoneTypeDefine ToPhoneType(this string phoneNumber)
        {
            PhoneTypeDefine result = PhoneTypeDefine.Unknown;

            if (phoneNumber.IsNotEmpty())
            {
                phoneNumber = phoneNumber.Trim();

                Regex phoneRegex = new Regex("(^(\\d{3,4})-(\\d{7,8})-(\\d{1,5}))|(^(\\d{3,4})-(\\d{7,8})$)");
                Match macth = phoneRegex.Match(phoneNumber);

                if (macth.Success)
                    result = PhoneTypeDefine.HomePhone;
                else
                    result = PhoneTypeDefine.MobilePhone;
            }

            return result;
        }

        /// <summary>
        /// IContactPhoneNumbers转换为电话集合
        /// </summary>
        /// <param name="phoneNumbers"></param>
        /// <param name="ownerID"></param>
        /// <returns></returns>
        public static PhoneCollection ToPhones(this IContactPhoneNumbers phoneNumbers, string ownerID)
        {
            ownerID.CheckStringIsNullOrEmpty("ownerID");

            PhoneCollection phones = new PhoneCollection();

            phoneNumbers.PrimaryPhone.ToPhone(ownerID, true).IsNotNull(phone => phones.Add(phone));
            phoneNumbers.SecondaryPhone.ToPhone(ownerID, false).IsNotNull(phone => phones.Add(phone));

            return phones;
        }

        /// <summary>
        /// 从电话集合反推回phoneNumbers
        /// </summary>
        /// <param name="phoneNumbers"></param>
        /// <param name="phones"></param>
        public static void FillFromPhones(this IContactPhoneNumbers phoneNumbers, PhoneCollection phones)
        {
            phoneNumbers.NullCheck("phoneNumbers");
            phones.NullCheck("phones");

            phones.Find(phone => phone.IsPrimary).IsNotNull(phone => phoneNumbers.PrimaryPhone = phone.ToPhoneNumber());
            phones.Find(phone => phone.IsPrimary == false).IsNotNull(phone => phoneNumbers.SecondaryPhone = phone.ToPhoneNumber());
        }

        public static Phone ToPhone(this string phoneNumber, string ownerID, bool isPrimary)
        {
            Phone phone = null;

            PhoneTypeDefine phoneType = phoneNumber.ToPhoneType();

            if (phoneType != PhoneTypeDefine.Unknown)
            {
                phone = new Phone()
                {
                    PhoneID = UuidHelper.NewUuidString(),
                    OwnerID = ownerID,
                    PhoneType = phoneType,
                    IsPrimary = isPrimary
                };

                FillPhone(phone, phoneType, phoneNumber);
            }

            return phone;
        }

        private static void FillPhone(Phone phone, PhoneTypeDefine phoneType, string phoneNumber)
        {
            switch (phoneType)
            {
                case PhoneTypeDefine.MobilePhone:
                    phone.PhoneNumber = phoneNumber.Trim();
                    break;
                default:
                    FillPhoneByParts(phone, phoneNumber);
                    break;
            }
        }

        private static void FillPhoneByParts(Phone phone, string phoneNumber)
        {
            string[] parts = phoneNumber.Split(PhoneSplitter, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                phone.PhoneNumber = phoneNumber.Trim();
            }
            else
            if (parts.Length == 1)
            {
                phone.PhoneNumber = parts[0].Trim();
            }
            else
            if (parts.Length > 1)
            {
                phone.AreaNumber = parts[0].Trim();
                phone.PhoneNumber = parts[1].Trim();

                if (parts.Length > 2)
                    phone.Extension = parts[2].Trim();
            }
        }
    }
}
