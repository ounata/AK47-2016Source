using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PermissionCenter.Tables
{
    /// <summary>
    /// DataTable创建器的虚基类
    /// </summary>
    public abstract class TableBuilderBase
    {
        /// <summary>
        /// 创建一个DataTable
        /// </summary>
        /// <returns></returns>
        protected abstract DataTable CreateTable();

        /// <summary>
        /// 将对象的属性填充到DataTable中
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="table"></param>
        protected abstract void FillPropertiesToTable(SCObjectAndRelation obj, DataRow row);

        /// <summary>
        /// 得到保留的（占用的）属性名称
        /// </summary>
        /// <returns></returns>
        protected abstract IList<string> GetReservedProperies();

        /// <summary>
        /// 得到需要忽略的属性名称
        /// </summary>
        /// <returns></returns>
        protected abstract IList<string> GetIgnoredProperies();


        /// <summary>
        /// 将SchemaObject转换为DataTable
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable Convert(SCObjectAndRelationCollection relations)
        {
            DataTable table = CreateTable();

            table.AddExtraPropertyColumn();

            string[] ignoredProperties = GetMergedProperties(this.GetReservedProperies(), this.GetIgnoredProperies()).ToArray();

            relations.FillDetails();

            foreach (SCObjectAndRelation obj in relations)
            {
                DataRow row = table.NewRow();
                FillPropertiesToTable(obj, row);

                obj.FillExtraPropertyValue(row, ignoredProperties);

                table.Rows.Add(row);
            }

            return table;
        }

        private static IList<string> GetMergedProperties(params IEnumerable<string>[] propertySets)
        {
            List<string> result = new List<string>();

            foreach (IEnumerable<string> set in propertySets)
                set.ForEach(s => result.Add(s));

            return result;
        }
    }
}