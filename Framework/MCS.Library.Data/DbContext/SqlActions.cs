using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;

namespace MCS.Library.Data
{
    /// <summary>
    /// 在ExecuteSqlInContext执行SQL前后的附加操作
    /// </summary>
    public class SqlActions : EditableDataObjectCollectionBase<Action>
    {
        /// <summary>
        /// 执行操作
        /// </summary>
        internal void DoActions()
        {
            foreach (Action action in this)
            {
                if (action != null)
                    action();
            }
        }
    }
}
