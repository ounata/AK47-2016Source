using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Security.Adapters
{
    public partial class SchemaObjectAdapter
    {
        public void UpdateChildrenFullPath(string parentID = SCOrganization.RootOrganizationID)
        {
            SCRelationForFullPath parent = LoadRFPByID(parentID);

            InternalUpdateOneLevelChildrenFullPath(parent);
        }

        private void InternalUpdateOneLevelChildrenFullPath(SCRelationForFullPath parent)
        {
            if (parent != null)
            {
                if (parent.FullPath == null)
                    parent.FullPath = string.Empty;

                if (parent.GlobalSort == null)
                    parent.GlobalSort = string.Empty;

                SCRelationForFullPathCollection children = LoadChildRFP(parent.ObjectID);

                SCRelationForFullPathCollection needToUpdate = children.SetAndFilterUnmatched(parent.FullPath, parent.GlobalSort);

                this.UpdateRelations(needToUpdate);

                foreach(SCRelationForFullPath rfp in children)
                    InternalUpdateOneLevelChildrenFullPath(rfp);
            }
        }

        private void UpdateRelations(IEnumerable<SCRelationForFullPath> rfps)
        {

        }

        private static string BuildUpdateFullPathSql(SCRelationForFullPath rfp)
        {
            StringBuilder strB = new StringBuilder();

            strB.AppendFormat("UPDATE SC.SchemaRelationObjects SET FullPath = {0}, GlobalSort = {1} WHERE ParentID = {2} AND ObjectID = {3}",
                TSqlBuilder.Instance.CheckUnicodeQuotationMark(rfp.FullPath),
                TSqlBuilder.Instance.CheckUnicodeQuotationMark(rfp.GlobalSort),
                TSqlBuilder.Instance.CheckUnicodeQuotationMark(rfp.ParentID),
                TSqlBuilder.Instance.CheckUnicodeQuotationMark(rfp.ObjectID)
                );

            strB.AppendFormat(TSqlBuilder.Instance.DBStatementSeperator);

            strB.AppendFormat("UPDATE SC.SchemaRelationObjectsSnapshot SET FullPath = {0}, GlobalSort = {1} WHERE ParentID = {2} AND ObjectID = {3}", 
                TSqlBuilder.Instance.CheckUnicodeQuotationMark(rfp.FullPath),
                TSqlBuilder.Instance.CheckUnicodeQuotationMark(rfp.GlobalSort),
                TSqlBuilder.Instance.CheckUnicodeQuotationMark(rfp.ParentID),
                TSqlBuilder.Instance.CheckUnicodeQuotationMark(rfp.ObjectID)
                );

            return strB.ToString();
        }

        private SCRelationForFullPathCollection LoadChildRFP(string parentID)
        {
            if (parentID.IsNullOrEmpty())
                parentID = SCOrganization.RootOrganizationID;

            string sql = string.Format("SELECT ParentID, R.ObjectID, SC.Name, R.ChildSchemaType, R.InnerSort, R.FullPath, R.GlobalSort FROM {0} WHERE ParentID = {1}",
                "SC.SchemaRelationObjectsSnapshot_Current R INNER JOIN SC.SchemaObjectSnapshot_Current SC ON R.ObjectID = SC.ID",
                TSqlBuilder.Instance.CheckUnicodeQuotationMark(parentID));

            DataTable table = DbHelper.RunSqlReturnDS(sql, this.GetConnectionName()).Tables[0];

            SCRelationForFullPathCollection result = new SCRelationForFullPathCollection();

            foreach(DataRow row in table.Rows)
                result.Add(ORMapping.DataRowToObject(row, new SCRelationForFullPath()));

            return result;
        }

        private SCRelationForFullPath LoadRFPByID(string parentID)
        {
            if (parentID.IsNullOrEmpty())
                parentID = SCOrganization.RootOrganizationID;

            SCRelationForFullPath result = null;

            if (parentID == SCOrganization.RootOrganizationID)
            {
                result = new SCRelationForFullPath();

                result.ParentID = string.Empty;
                result.ObjectID = parentID;
                result.Name = SCOrganization.GetRoot().Name;
                result.InnerSort = 0;
                result.FullPath = string.Empty;
                result.GlobalSort = string.Empty;
            }
            else
            {
                string sql = string.Format("SELECT ParentID, R.ObjectID, SC.Name, R.ChildSchemaType, R.InnerSort, R.FullPath, R.GlobalSort FROM {0} WHERE ObjectID = {1} AND ChildSchemaType = 'Organizations'",
                    "SC.SchemaRelationObjectsSnapshot_Current R INNER JOIN SC.SchemaObjectSnapshot_Current SC ON R.ObjectID = SC.ID",
                    TSqlBuilder.Instance.CheckUnicodeQuotationMark(parentID));

                DataTable table = DbHelper.RunSqlReturnDS(sql, this.GetConnectionName()).Tables[0];

                if (table.Rows.Count > 0)
                {
                    result = new SCRelationForFullPath();

                    ORMapping.DataRowToObject(table.Rows[0], result);
                }
            }

            return result;
        }
    }
}
