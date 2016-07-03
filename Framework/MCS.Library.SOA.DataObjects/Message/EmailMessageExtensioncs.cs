using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using MCS.Library.Core;

namespace MCS.Library.SOA.DataObjects
{
    public static class EmailMessageExtensioncs
    {
        public static MailAddress ToMailAddress(this EmailAddress ea, bool enableFake = false)
        {
            MailAddress result = null;

            if (ea != null)
            {
                EmailMessageSettings settings = EmailMessageSettings.GetConfig();

                string address = ea.Address;

                if (enableFake && settings.EnableFakeTarget && settings.FakeTarget.IsNotEmpty())
                    address = settings.FakeTarget;

                result = new MailAddress(address, ea.DisplayName);
            }

            return result;
        }

        /// <summary>
        /// 将Encoding转换成名称，如果为空，则返回空串
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToDescription(this Encoding encoding)
        {
            string result = string.Empty;

            if (encoding != null)
                result = encoding.BodyName;

            return result;
        }

        /// <summary>
        /// 将名称转换成Encoding
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="encodingDesp"></param>
        /// <returns></returns>
        public static Encoding FromDescription(this Encoding encoding, string encodingDesp)
        {
            Encoding result = Encoding.UTF8;

            if (encodingDesp.IsNotEmpty())
                result = Encoding.GetEncoding(encodingDesp);

            return result;
        }
    }
}
