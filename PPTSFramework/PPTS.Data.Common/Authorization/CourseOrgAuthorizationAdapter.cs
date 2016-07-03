using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using PPTS.Data.Common.Entities;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Security;
using PPTS.Contracts.Orders.Models;
using PPTS.Data.Common.Service;

namespace PPTS.Data.Common.Authorization
{
    public class CourseOrgAuthorizationAdapter : UpdatableAndLoadableAdapterBase<CourseOrgAuthorization, CourseOrgAuthorizationCollection>
    {
        private string connectionName = string.Empty;
        private CourseOrgAuthorizationAdapter(string connName)
        {
            this.connectionName = connName;
        }

        public static CourseOrgAuthorizationAdapter GetInstance(string connName)
        {
            return new CourseOrgAuthorizationAdapter(connName);
        }

        public void DeleteOrgAuthorizations(string recordID, RecordType recordType, RelationType relationType)
        {
            this.Delete(builder => builder.AppendItem("OwnerID", recordID).AppendItem("OwnerType", (int)recordType).AppendItem("RelationType", (int)relationType));
        }

        public void DeleteOrgAuthorizationsInContext(string recordID, RecordType recordType, RelationType relationType)
        {
            this.DeleteInContext(builder => builder.AppendItem("OwnerID", recordID).AppendItem("OwnerType", (int)recordType).AppendItem("RelationType", (int)relationType));
        }

        public virtual void UpdateOrgAuthorizationsBase(string recordID, IOrganization org, RecordType recordType = RecordType.Assign, RelationType relationType = RelationType.Owner)
        {
            IOrganization xuekezu;
            IOrganization department;

            //DeleteOrgAuthorizations(recordID, recordType, relationType);

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

        public virtual void UpdateOrgAuthorizationsInContextBase(string recordID, IOrganization org, RecordType recordType = RecordType.Assign, RelationType relationType = RelationType.Owner)
        {
            IOrganization xuekezu;
            IOrganization department;

            //DeleteOrgAuthorizationsInContext(recordID, recordType, relationType);

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

        public virtual void UpdateOrgAuthorizations(string recordID, IOrganization org, RecordType recordType = RecordType.Assign, RelationType relationType = RelationType.Owner)
        {
            UpdateOrgAuthorizationsBase(recordID, org, recordType, relationType);
            CreateCopyTask(recordID, recordType, relationType);
        }

        public virtual void UpdateOrgAuthorizationsInContext(string recordID, IOrganization org, RecordType recordType = RecordType.Assign, RelationType relationType = RelationType.Owner)
        {
            UpdateOrgAuthorizationsInContextBase(recordID, org, recordType, relationType);
            CreateCopyTaskInContext(recordID, recordType, relationType);
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

        public virtual void CreateCopyTask(string recordID, RecordType recordType,RelationType relationType )
        {
            OrderScopeAuthorzationTask.Instance.CourseOrgAuthorizationToSearchByTask(new OrderScopeAuthorizationModel() { OwnerID = recordID, OwnerType = recordType, RelationType = relationType });
        }

        public virtual void CreateCopyTaskInContext(string recordID, RecordType recordType, RelationType relationType)
        {
            this.GetSqlContext().AfterActions.Add(() =>
            {
                OrderScopeAuthorzationTask.Instance.CourseOrgAuthorizationToSearchByTask(new OrderScopeAuthorizationModel() { OwnerID = recordID, OwnerType = recordType, RelationType = relationType });
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
