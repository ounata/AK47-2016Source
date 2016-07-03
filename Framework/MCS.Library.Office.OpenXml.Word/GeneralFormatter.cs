using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MCS.Library.Office.OpenXml.Word
{
    public static class GeneralFormatter
    {
        public static string ToString(object o, string formatStr)
        {
            string result = string.Empty;

            if (o != null)
            {
                Type type = o.GetType();

                MethodInfo method = type.GetMethod("ToString", new Type[] { typeof(string) });

                if (null == method)
                    result = o.ToString();
                else
                    result = method.Invoke(o, new object[] { formatStr }).ToString();
            }

            return result;
        }
    }
}
