using MCS.Library.Data.Adapters;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTS.Data.Common.Security;
using MCS.Library.OGUPermission;
using MCS.Library.Core;

namespace PPTS.Data.Common.Authorization
{
    public abstract class CourseOrgAuthorizationAdapterBase : UpdatableAndLoadableAdapterBase<CourseOrgAuthorization, CourseOrgAuthorizationCollection>
    {
        public virtual void UpdateOrgAuthorizations(string recordID, IOrganization org, RecordType recordType = RecordType.Assign, RelationType relationType = RelationType.Owner)
        {
            IOrganization xuekezu;
            IOrganization department;
            xuekezu = org.GetParentOrganizationByType(DepartmentType.XueKeZu);
            xuekezu.IsNotNull(model => this.Update(new CourseOrgAuthorization()
            {
                ObjectID = model.ID,
                ObjectType = (OrgType)model.PPTSDepartmentType(),
                OwnerID = recordID,
                OwnerType = recordType,
                RelationType = relationType
            }));

            department = org.GetParentOrganizationByType(DepartmentType.Department);
            department.IsNotNull(model => this.Update(new CourseOrgAuthorization()
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
                this.Update(new CourseOrgAuthorization
                {
                    ObjectID = orgmodel.ID,
                    ObjectType = (OrgType)orgmodel.PPTSDepartmentType(),
                    OwnerID = recordID,
                    OwnerType = recordType,
                    RelationType = relationType
                });
            });
        }

        public virtual void UpdateOrgAuthorizationsInContext(string recordID, IOrganization org, RecordType recordType = RecordType.Assign, RelationType relationType = RelationType.Owner)
        {
            IOrganization xuekezu;
            IOrganization department;
            xuekezu = org.GetParentOrganizationByType(DepartmentType.XueKeZu);
            xuekezu.IsNotNull(model => this.UpdateInContext(new CourseOrgAuthorization()
            {
                ObjectID = model.ID,
                ObjectType = (OrgType)model.PPTSDepartmentType(),
                OwnerID = recordID,
                OwnerType = recordType,
                RelationType = relationType
            }));

            department = org.GetParentOrganizationByType(DepartmentType.Department);
            department.IsNotNull(model => this.UpdateInContext(new CourseOrgAuthorization()
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
                this.UpdateInContext(new CourseOrgAuthorization
                {
                    ObjectID = orgmodel.ID,
                    ObjectType = (OrgType)orgmodel.PPTSDepartmentType(),
                    OwnerID = recordID,
                    OwnerType = recordType,
                    RelationType = relationType
                });
            });
        }

        public virtual void UpdateOrgAuthorizations(CourseOrgAuthorization model, RecordType recordType = RecordType.Assign)
        {
            IOrganization org = OGUExtensions.GetOrganizationByID(model.ObjectID);
            org.NullCheck("org");
            UpdateOrgAuthorizations(model.OwnerID, org, recordType, model.RelationType);
        }

        public virtual void UpdateOrgAuthorizationsInContext(CourseOrgAuthorization model, RecordType recordType = RecordType.Assign)
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
