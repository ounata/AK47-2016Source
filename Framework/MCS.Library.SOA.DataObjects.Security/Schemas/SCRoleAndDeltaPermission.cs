using MCS.Library.SOA.DataObjects.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Security
{
    public class SchemaObjectAndDeltaChildrenBase<T> where T : SchemaObjectBase
    {
        public SchemaObjectAndDeltaChildrenBase(T container, DeltaSchemaObjectCollection delta)
        {
            this.Container = container;
            this.Delta = delta;
        }

        public T Container
        {
            get;
            internal set;
        }

        public DeltaSchemaObjectCollection Delta
        {
            get;
            internal set;
        }
    }

    /// <summary>
    /// 角色和变化的权限
    /// </summary>
    public class SCRoleAndDeltaPermission : SchemaObjectAndDeltaChildrenBase<SCRole>
    {
        public SCRoleAndDeltaPermission(SCRole role, DeltaSchemaObjectCollection delta) :
            base(role, delta)
        {
        }
    }

    /// <summary>
    /// 角色和变化的群组
    /// </summary>
    public class SCRoleAndDeltaGroup : SchemaObjectAndDeltaChildrenBase<SCRole>
    {
        public SCRoleAndDeltaGroup(SCRole role, DeltaSchemaObjectCollection delta) :
            base(role, delta)
        {
        }
    }
}
