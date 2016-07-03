﻿using MCS.Library.Data.Adapters;
using PPTS.Data.Common.Entities;
using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data;
using PPTS.Contracts.Customers.Models;
using PPTS.Contracts.Orders.Models;
using PPTS.Data.Common.Service;

namespace PPTS.Data.Common.Authorization
{
    public class OwnerRelationAuthorizationAdapter : UpdatableAndLoadableAdapterBase<OwnerRelationAuthorization, OwnerRelationAuthorizationCollection>
    {
        private string connectionName = string.Empty;
        private OwnerRelationAuthorizationAdapter(string connName)
        {
            this.connectionName = connName;
        }

        public static OwnerRelationAuthorizationAdapter GetInstance(string connName)
        {
            return new OwnerRelationAuthorizationAdapter(connName);
        }

        public void DeleteRelationAuthorizationsBase(string recordID, RecordType recordType, RelationType relationType)
        {
            this.Delete(builder => builder.AppendItem("OwnerID", recordID).AppendItem("OwnerType", (int)recordType).AppendItem("ObjectType", (int)relationType));
        }

        public void DeleteRelationAuthorizations(string recordID, RecordType recordType, RelationType relationType)
        {
            DeleteRelationAuthorizationsBase(recordID, recordType, relationType);
            CreateCopyTask(recordID, recordType, relationType);
        }

        public void DeleteRelationAuthorizationsInContextBase(string recordID, RecordType recordType, RelationType relationType)
        {
            this.DeleteInContext(builder => builder.AppendItem("OwnerID", recordID).AppendItem("OwnerType", (int)recordType).AppendItem("ObjectType", (int)relationType));
        }

        public void DeleteRelationAuthorizationsInContext(string recordID, RecordType recordType, RelationType relationType)
        {
            DeleteRelationAuthorizationsInContextBase(recordID, recordType, relationType);
            CreateCopyTaskInContext(recordID, recordType, relationType);
        }

        public virtual void UpdateRelationAuthorizations(OwnerRelationAuthorization data)
        {
            //this.Delete(builder => builder.AppendItem("OwnerID", data.OwnerID).AppendItem("OwnerType", (int)data.OwnerType).AppendItem("ObjectType", (int)data.ObjectType));
            this.Update(data);
            CreateCopyTask(data.OwnerID, data.OwnerType, data.ObjectType);
        }

        public virtual void UpdateRelationAuthorizationsInContext(OwnerRelationAuthorization data)
        {
            //this.DeleteInContext(builder => builder.AppendItem("OwnerID", data.OwnerID).AppendItem("OwnerType", (int)data.OwnerType).AppendItem("ObjectType", (int)data.ObjectType));
            this.UpdateInContext(data);
            CreateCopyTaskInContext(data.OwnerID, data.OwnerType, data.ObjectType);
        }

        public virtual void CreateCopyTask(string recordID, RecordType recordType, RelationType relationType)
        {
            if (ConnectionDefine.PPTSCustomerConnectionName == this.connectionName)
                CustomerScopeAuthorizationTask.Instance.OwnerRelationAuthorizationToSearchByTask(new CustomerScopeAuthorizationModel() { OwnerID = recordID, OwnerType = recordType, RelationType = relationType });
            else if (ConnectionDefine.PPTSOrderConnectionName == this.connectionName)
                OrderScopeAuthorzationTask.Instance.OwnerRelationAuthorizationToSearchByTask(new OrderScopeAuthorizationModel() { OwnerID = recordID, OwnerType = recordType, RelationType = relationType });
        }

        public virtual void CreateCopyTaskInContext(string recordID, RecordType recordType, RelationType relationType)
        {
            this.GetSqlContext().AfterActions.Add(() =>
            {
                if (ConnectionDefine.PPTSCustomerConnectionName == this.connectionName)
                    CustomerScopeAuthorizationTask.Instance.OwnerRelationAuthorizationToSearchByTask(new CustomerScopeAuthorizationModel() { OwnerID = recordID, OwnerType = recordType, RelationType = relationType });
                else if (ConnectionDefine.PPTSOrderConnectionName == this.connectionName)
                    OrderScopeAuthorzationTask.Instance.OwnerRelationAuthorizationToSearchByTask(new OrderScopeAuthorizationModel() { OwnerID = recordID, OwnerType = recordType, RelationType = relationType });
            });
        }


        /// <summary>
        /// 得到连接串的名称
        /// </summary>
        /// <returns></returns>
        protected override string GetConnectionName()
        {
            string result = this.connectionName;
            if (result.IsNullOrEmpty())
                result = ConnectionDefine.PPTSMetaDataConnectionName;
            return result;
        }
    }
}
