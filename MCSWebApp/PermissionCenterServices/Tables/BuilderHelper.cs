using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MCS.Library.Core;
using MCS.Library.SOA.DataObjects.Security;
using MCS.Library.SOA.DataObjects.Schemas.SchemaProperties;
using System.Xml;

namespace PermissionCenter.Tables
{
    internal static class BuilderHelper
    {
        public const string ExtraPropertyColumnName = "EXTRA_PROPERTIES";

        public static void AddExtraPropertyColumn(this DataTable table)
        {
            if (table.Columns.Contains(ExtraPropertyColumnName) == false)
                table.Columns.Add(ExtraPropertyColumnName, typeof(string));
        }

        public static void FillExtraPropertyValue(this SCObjectAndRelation obj, DataRow row, params string[] ignoredProperties)
        {
            FillExtraPropertyValue(obj.Detail, row, ignoredProperties);
        }

        public static void FillExtraPropertyValue(this SchemaObjectBase obj, DataRow row, params string[] ignoredProperties)
        {
            XmlDocument xmlDoc = XmlHelper.CreateDomDocument("<Object/>");

            foreach (SchemaPropertyValue pv in obj.Properties)
            {
                if (ignoredProperties.NotExists(s => string.Compare(s, pv.Definition.Name, true) == 0))
                    XmlHelper.AppendAttr(xmlDoc.DocumentElement, pv.Definition.Name, pv.StringValue);
            }

            row[ExtraPropertyColumnName] = xmlDoc.OuterXml;
        }
    }
}