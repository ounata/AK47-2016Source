using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.Services.UnionPay.Extensions
{
    public static class Extensions
    {
        public static object ToSelfType(this string s, Type t)
        {
            if(t == typeof(string))
            {
                return s;
            }
            return t.GetMethod("Parse", new[] { typeof(string) }).Invoke(null, new object[] { s });
        }
    }
}