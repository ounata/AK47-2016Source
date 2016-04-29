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

            phoneNumbers.PrimaryPhone.IsValidNumber((phone, phoneType) =>
                { phone.OwnerID = ownerID; phone.IsPrimary = true; phone.ItemID = 0; phones.Add(phone.FillPhoneInfo(phoneType)); });

            phoneNumbers.SecondaryPhone.IsValidNumber((phone, phoneType) =>
                { phone.OwnerID = ownerID; phone.ItemID = 1; phones.Add(phone.FillPhoneInfo(phoneType)); });

            return phones;
        }

        public static void FillFromPhones(this IContactPhoneNumbers phoneNumbers, PhoneCollection phones)
        {
            phoneNumbers.NullCheck("phoneNumbers");
            phones.NullCheck("phones");

            phones.Find(phone => phone.IsPrimary).IsNotNull((phone => phoneNumbers.PrimaryPhone = phone));
            phones.Find(phone => phone.IsPrimary == false).IsNotNull(phone => phoneNumbers.SecondaryPhone = phone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Phone IsValidNumber(this Phone phone, Action<Phone, PhoneTypeDefine> action)
        {
            if (phone != null)
            {
                if (phone.PhoneNumber.IsNotEmpty())
                {
                    PhoneTypeDefine phoneType = phone.ToPhoneNumber().ToPhoneType();

                    if (phoneType != PhoneTypeDefine.Unknown)
                        action(phone, phoneType);
                }
            }

            return phone;
        }

        public static Phone ToPhone(this string phoneNumber, string ownerID, bool isPrimary)
        {
            PhoneTypeDefine phoneType = phoneNumber.ToPhoneType();

            Phone phone = new Phone()
            {
                OwnerID = ownerID,
                ItemID = isPrimary ? 0 : 1,
                PhoneType = phoneType,
                IsPrimary = isPrimary
            };

            FillPhone(phone, phoneType, phoneNumber);

            return phone;
        }

        public static string GetStaffName(this CustomerStaffRelation relation)
        {
            return relation != null ? relation.StaffName : "";
        }

        public static string GetStaffName(this CustomerStaffRelationCollection relations, CustomerRelationType relationType)
        {
            return relations.Find(relation => relation.RelationType == relationType).GetStaffName();
        }

        private static Phone FillPhoneInfo(this Phone phone, PhoneTypeDefine phoneType)
        {
            if (phone != null && phoneType != PhoneTypeDefine.Unknown)
            {
                phone.PhoneType = phoneType;

                FillPhone(phone, phoneType, phone.PhoneNumber);
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
