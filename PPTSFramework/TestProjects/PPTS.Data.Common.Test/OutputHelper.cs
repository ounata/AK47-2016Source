using MCS.Library.Core;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Test
{
    public static class OutputHelper
    {
        public static void Output(this IEnumerable<PPTSJob> jobs)
        {
            jobs.ForEach(job => job.Output());
        }

        public static void Output(this PPTSJob job)
        {
            if (job != null)
                Console.WriteLine("ID: {0}, Name: {1}", job.ID, job.Name);
        }

        public static void Output(this IEnumerable<string> functions)
        {
            Console.WriteLine(string.Join(", ", functions.ToArray()));
        }

        public static void Output(this PPTSRole role)
        {
            if (role != null)
                Console.WriteLine("ID: {0}, Name: {1}", role.ID, role.Name);
        }

        public static void Output(this IEnumerable<PPTSRole> roles)
        {
            roles.ForEach(role => role.Output());
        }
    }
}
