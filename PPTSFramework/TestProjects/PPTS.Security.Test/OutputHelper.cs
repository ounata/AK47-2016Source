﻿using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Security.Test
{
    public static class OutputHelper
    {
        public static void OutputColumns(this DataTable table)
        {
            foreach (DataColumn column in table.Columns)
                Console.WriteLine(column.ColumnName);
        }

        public static void OutputRowsInfo(this DataTable table)
        {
            Console.WriteLine("Total rows: {0}", table.Rows.Count);
        }

        public static void Output(this IEnumerable<SCRole> roles)
        {
            roles.ForEach(r => Console.WriteLine("Role CodeName: {0}", r.CodeName));
        }

        public static void Output(this IEnumerable<SCPermission> permissions)
        {
            permissions.ForEach(p => Console.WriteLine("Permission CodeName: {0}", p.CodeName));
        }

        public static void OutputOrgRecursively(this SCOrganization parent)
        {
            SCRelationObject relation = parent.CurrentParentRelations.FirstOrDefault();
            string fullPath = relation != null ? relation.FullPath : string.Empty;

            Console.WriteLine("Name: {0}, FullPath: {1}", parent.Name, fullPath);

            foreach(SCBase child in parent.CurrentChildren)
            {
                if (child is SCOrganization)
                    OutputOrgRecursively((SCOrganization)child);
            }
        }
    }
}
