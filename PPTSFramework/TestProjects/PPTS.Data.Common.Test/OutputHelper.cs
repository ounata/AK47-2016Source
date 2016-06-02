using MCS.Library.Core;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Entities;
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
        public static void Output(this IEnumerable<UserAndJob> ujs)
        {
            ujs.ForEach(uj => uj.Output());
        }

        public static void Output(this UserAndJob uj)
        {
            Console.WriteLine("Job ID: {0}, Job Name: {1}, User ID: {2}, User Name: {3}",
                uj.JobID, uj.JobName, uj.UserID, uj.UserName);
        }

        public static void Output(this IEnumerable<PPTSJob> jobs)
        {
            jobs.ForEach(job => job.Output());
        }

        public static void Output(this PPTSJob job)
        {
            if (job != null)
                Console.WriteLine("ID: {0}, Name: {1}, Type: {2}", job.ID, job.Name, job.JobType);
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

        public static void Output(this IEnumerable<IOguObject> objs)
        {
            foreach (IOguObject obj in objs)
            {
                Console.WriteLine("ID: {0}, Name: {1}", obj.ID, obj.Name);
            }
        }
    }
}
