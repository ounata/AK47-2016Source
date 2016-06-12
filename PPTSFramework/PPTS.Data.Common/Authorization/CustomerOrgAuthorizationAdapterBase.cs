using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Authorization
{
   public abstract class CustomerOrgAuthorizationAdapterBase : UpdatableAndLoadableAdapterBase<CustomerOrgAuthorization, CustomerOrgAuthorizationCollection>
    {
        public virtual void UpdateOrgAuthorizations(string recordID, IOrganization org, RecordType recordType, RelationType relationType)
        {
            IOrganization department;

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

        public virtual void UpdateOrgAuthorizationsInContext(string recordID, IOrganization org, RecordType recordType, RelationType relationType)
        {
            IOrganization department;
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

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSMetaDataConnectionName;
        }
    }
}