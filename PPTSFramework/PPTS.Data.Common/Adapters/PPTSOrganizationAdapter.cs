using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Adapters
{
    public class PPTSOrganizationAdapter : UpdatableAndLoadableAdapterBase<PPTSOrganization, PPTSOrganizationCollection>
    {
        private static readonly OguDataCollection<IUser> EmptyUsers = new OguDataCollection<IUser>();

        public static readonly PPTSOrganizationAdapter Instance = new PPTSOrganizationAdapter();

        private PPTSOrganizationAdapter()
        {

        }

        protected override int InnerDelete(PPTSOrganization data, Dictionary<string, object> context)
        {
            return 0;
            //return base.InnerDelete(data, context);
        }

        protected override string InnerDeleteInContext(PPTSOrganization data, SqlContextItem sqlContext, Dictionary<string, object> context)
        {
            return null;
            //return base.InnerDeleteInContext(data, sqlContext, context);
        }

        public PPTSOrganization Load(string id)
        {
            return this.Load(builder => builder.AppendItem("ID", id)).SingleOrDefault();
        }

        public PPTSOrganization LoadByName(string name)
        {
            return this.Load(builder => builder.AppendItem("Name", name)).SingleOrDefault();
        }

        public PPTSOrganization LoadByShortName(string shortName)
        {
            return this.Load(builder => builder.AppendItem("ShortName", shortName)).SingleOrDefault();
        }

        /// <summary>
        /// 根据组织的ID和岗位名称去查询下面某个岗位下包含的人员（经过缓存）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public IEnumerable<IUser> GetUsersInJobsByOrganizationID(string id, string jobName)
        {
            if (id.IsNullOrEmpty() || jobName.IsNullOrEmpty())
                return EmptyUsers;

            string cacheKey = CalculateOrgAndJobCacheKey(id, jobName);

            return PPTSOrgAndJobCacheQueue.Instance.GetOrAddNewValue(cacheKey, (cache, key) =>
            {
                MixedDependency dependency = new MixedDependency(new UdpNotifierCacheDependency(), new MemoryMappedFileNotifierCacheDependency());

                return cache.Add(key, LoadUsersInJobsByOrganizationID(id, jobName), dependency);
            });
        }

        /// <summary>
        ///  根据组织的ID和岗位名称去查询下面某个岗位下包含的人员
        /// </summary>
        /// <param name="id">组织ID</param>
        /// <param name="jobName">岗位名称</param>
        /// <returns></returns>
        public OguDataCollection<IUser> LoadUsersInJobsByOrganizationID(string id, string jobName)
        {
            IOrganization org = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, id).SingleOrDefault();

            (org != null).FalseThrow(string.Format("不能找到ID为{0}的组织", id));

            string template = ResourceHelper.LoadStringFromResource(Assembly.GetExecutingAssembly(), "PPTS.Data.Common.Adapters.Templates.QueryUsersInJobByOrganization.sql");

            string sql = string.Format(template, TSqlBuilder.Instance.CheckUnicodeQuotationMark(org.FullPath + "\\%"),
                TSqlBuilder.Instance.CheckUnicodeQuotationMark(jobName));

            DataTable table = DbHelper.RunSqlReturnDS(sql, this.GetConnectionName()).Tables[0];

            OguDataCollection<IUser> users = new OguDataCollection<IUser>();

            foreach (DataRow row in table.Rows)
            {
                OguUser user = new OguUser();

                user.ID = row["ID"].ToString();
                user.Name = row["Name"].ToString();
                user.DisplayName = row["DisplayName"].ToString();
                user.LogOnName = row["CodeName"].ToString();

                users.Add(user);
            }

            return users;
        }

        private static string CalculateOrgAndJobCacheKey(string orgID, string jobName)
        {
            return orgID + "~" + jobName;
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSPermissionCenterConnectionName;
        }
    }
}
