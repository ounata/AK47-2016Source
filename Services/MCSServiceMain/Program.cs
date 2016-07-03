using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Services
{
    class Program
    {
        public static int Main(string[] args)
        {
            int result = 0;

            try
            {
                StringDictionary arguments = ArgumentsParser.Parse(args);

                if (arguments.ContainsKey("i"))
                    InstallService();
                else
                if (arguments.ContainsKey("u"))
                    UninstallService();
                else
                    result = ServiceToRun.Main(args);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);

                result = -1;
            }

            return result;
        }

        private static void InstallService()
        {
            string[] paramArray = new string[] { Assembly.GetEntryAssembly().Location };

            ManagedInstallerClass.InstallHelper(paramArray);

            Console.WriteLine("Install Sucessed");
        }

        private static void UninstallService()
        {
            string[] paramArray = new string[] { "/u", Assembly.GetEntryAssembly().Location };

            ManagedInstallerClass.InstallHelper(paramArray);

            Console.WriteLine("Uninstall Sucessed");
        }
    }
}
