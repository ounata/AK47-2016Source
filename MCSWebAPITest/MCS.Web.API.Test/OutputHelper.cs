using MCS.Library.Core;
using MCS.Library.OGUPermission;
using MCS.Web.MVC.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.API.Test
{
    static class OutputHelper
    {
        public static void Output(this IOguObject oguObj)
        {
            Console.WriteLine("ID: {0}, Name: {1}, Type: {2}, FullPath: {3}",
                oguObj.ID, oguObj.Name, oguObj.ObjectType, oguObj.FullPath);
        }

        public static void Output(this IEnumerable<IOguObject> oguObjs)
        {
            oguObjs.ForEach(obj => obj.Output());
        }
    }
}
