using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.OGUPermission;
using PPTS.Contracts.Customers.Models;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Common.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Authorization
{
    public class CustomerOrgAuthorizationAdapter : UpdatableAndLoadableAdapterBase<CustomerOrgAuthorization, CustomerOrgAuthorizationCollection>
    {
        private string connectionName = string.Empty;
        private CustomerOrgAuthorizationAdapter(string connName)
        {
            this.connectionName = connName;
        }

        public void DeleteOrgAuthorizations(string recordID, RecordType recordType, RelationType relationType)
        {
            this.Delete(builder => builder.AppendItem("OwnerID", recordID).AppendItem("OwnerType", (int)recordType).AppendItem("RelationType", (int)relationType));
        }

        public void DeleteOrgAuthorizationsInContext(string recordID, RecordType recordType, RelationType relationType)
        {
            this.DeleteInContext(builder => builder.AppendItem("OwnerID", recordID).AppendItem("OwnerType", (int)recordType).AppendItem("RelationType", (int)relationType));
        }

        public static CustomerOrgAuthorizationAdapter GetInstance(string connName)
        {
            return new CustomerOrgAuthorizationAdapter(connName);
        }

        public virtual void UpdateOrgAuthorizationsBase(string recordID, IOrganization org, RecordType recordType, RelationType relationType)
        {
            IOrganization xuekezu;
            IOrganization department;

            //DeleteOrgAuthorizations(recordID, recordType, relationType);
            xuekezu = org.GetParentOrganizationByType(DepartmentType.XueKeZu);
            xuekezu.IsNotNull(model => this.UpdateInContext(new CustomerOrgAuthorization()
            {
                ObjectID = model.ID,
                ObjectType = (OrgType)model.PPTSDepartmentType(),
                OwnerID = recordID,
                OwnerType = recordType,
                RelationType = relationType
            }));

            department = org.GetParentOrganizationByType(DepartmentType.Department);
            department.IsNotNull(model => this.Update(new CustomerOrgAuthorization()
            {
                ObjectID = model.ID,
                ObjectType = (OrgType)model.PPTSDepartmentType(),
                OwnerID = recordID,
                OwnerType = recordType,
                RelationType = relationType
            }));

            List<IOrganization> orgs = org.GetAllDataScopeParents().ToList();
            orgs.ForEach(orgmodel =>
            {
                this.Update(new CustomerOrgAuthorization
                {
                    ObjectID = orgmodel.ID,
                    ObjectType = (OrgType)orgmodel.PPTSDepartmentType(),
                    OwnerID = recordID,
                    OwnerType = recordType,
                    RelationType = relationType
                });
            });
        }

        public virtual void UpdateOrgAuthorizationsInContextBase(string recordID, IOrganization org, RecordType recordType, RelationType relationType)
        {
            IOrganization xuekezu;
            IOrganization department;

            //DeleteOrgAuthorizationsInContext(recordID, recordType, relationType);

            xuekezu = org.GetParentOrganizationByType(DepartmentType.XueKeZu);
            xuekezu.IsNotNull(model => this.UpdateInContext(new CustomerOrgAuthorization()
            {
                ObjectID = model.ID,
                ObjectType = (OrgType)model.PPTSDepartmentType(),
                OwnerID = recordID,
                OwnerType = recordType,
                RelationType = relationType
            }));

            department = org.GetParentOrganizationByType(DepartmentType.Department);
            department.IsNotNull(model => this.UpdateInContext(new CustomerOrgAuthorization()
            {
                ObjectID = model.ID,
                ObjectType = (OrgType)model.PPTSDepartmentType(),
                OwnerID = recordID,
                OwnerType = recordType,
                RelationType = relationType
            }));

            List<IOrganization> orgs = org.GetAllDataScopeParents().ToList();
            orgs.ForEach(orgmodel =>
            {
                this.UpdateInContext(new CustomerOrgAuthorization
                {
                    ObjectID = orgmodel.ID,
                    ObjectType = (OrgType)orgmodel.PPTSDepartmentType(),
                    OwnerID = recordID,
                    OwnerType = recordType,
                    RelationType = relationType
                });
            });
        }

        public virtual void UpdateOrgAuthorizations(string recordID, IOrganization org, RecordType recordType, RelationType relationType)
        {
            UpdateOrgAuthorizationsBase(recordID, org, recordType, relationType);
            CreateCopyTask(recordID, recordType, relationType);
        }

        public virtual void UpdateOrgAuthorizationsInContext(string recordID, IOrganization org, RecordType recordType, RelationType relationType)
        {
            UpdateOrgAuthorizationsInContextBase(recordID, org, recordType, relationType);
            CreateCopyTaskInContext(recordID, recordType, relationType);
        }

        public virtual void UpdateOrgAuthorizations(CourseOrgAuthorization model, RecordType recordType)
        {
            IOrganization org = OGUExtensions.GetOrganizationByID(model.ObjectID);
            org.NullCheck("org");
            UpdateOrgAuthorizations(model.OwnerID, org, recordType, model.RelationType);
        }

        public virtual void UpdateOrgAuthorizationsInContext(CourseOrgAuthorization model, RecordType recordType)
        {
            IOrganization org = OGUExtensions.GetOrganizationByID(model.ObjectID);
            org.NullCheck("org");
            UpdateOrgAuthorizationsInContext(model.OwnerID, org, recordType, model.RelationType);
        }

        public virtual void CreateCopyTask(string recordID, RecordType recordType, RelationType relationType)
        {
            CustomerScopeAuthorizationTask.Instance.CustomerOrgAuthorizationToOrderByTask(new CustomerScopeAuthorizationModel() { OwnerID = recordID, OwnerType = recordType, RelationType = relationType });
            CustomerScopeAuthorizationTask.Instance.CustomerOrgAuthorizationToSearchByTask(new CustomerScopeAuthorizationModel() { OwnerID = recordID, OwnerType = recordType, RelationType = relationType });
        }

        public virtual void CreateCopyTaskInContext(string recordID, RecordType recordType, RelationType relationType)
        {
            this.GetSqlContext().AfterActions.Add(() =>
            {
                CustomerScopeAuthorizationTask.Instance.CustomerOrgAuthorizationToOrderByTask(new CustomerScopeAuthorizationModel() { OwnerID = recordID, OwnerType = recordType, RelationType = relationType });
                CustomerScopeAuthorizationTask.Instance.CustomerOrgAuthorizationToSearchByTask(new CustomerScopeAuthorizationModel() { OwnerID = recordID, OwnerType = recordType, RelationType = relationType });
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
