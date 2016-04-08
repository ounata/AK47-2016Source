using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data
{
    internal class TableAction
    {
        public TableAction()
        {
        }

        public TableAction(string tableName, Action<DataTable> action)
        {
            this.TableName = tableName;
            this.Action = action;
        }

        public string TableName
        {
            get;
            set;
        }

        public Action<DataTable> Action
        {
            get;
            set;
        }
    }

    internal class TableActionCollection : EditableDataObjectCollectionBase<TableAction>
    {
        public IEnumerable<TableAction> FindAllActions(string tableName)
        {
            return this.FindAll(ta => string.Compare(ta.TableName, tableName, true) == 0);
        }

        public string[] ToTableNames()
        {
            List<string> tableNames = new List<string>();

            foreach (TableAction item in this)
                tableNames.Add(item.TableName);

            return tableNames.ToArray();
        }
    }
}
