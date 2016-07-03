using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCS.Library.Data.Mapping;
using MCS.Library.Data.Builder;

namespace MCS.Library.SOA.DataObjects.Workflow
{
    /// <summary>
    /// 用户关于委托待办的实现
    /// </summary>
    public sealed class WfDelegationAdapter : UpdatableAndLoadableAdapterBase<WfDelegation, WfDelegationCollection>, IWfDelegationReader
    {
        /// <summary>
        /// 得到实例
        /// </summary>
        public static readonly WfDelegationAdapter Instance = new WfDelegationAdapter();

        private WfDelegationAdapter()
        {

        }

        public WfDelegationCollection GetUserActiveDelegations(IUser user, IWfProcess process)
        {
            user.NullCheck("user");

            WfDelegationCollection delegations = GetUserActiveDelegations(user.ID);

            if (process != null)
            {
                WfDelegationCollection filtered = new WfDelegationCollection();

                filtered.CopyFrom(delegations, d => d.Matched(process.Descriptor.ApplicationName, process.Descriptor.ProgramName));

                delegations = filtered;
            }

            return delegations;
        }

        /// <summary>
        /// 得到委托人的所有委托信息
        /// </summary>
        /// <param name="sourceUserID"></param>
        /// <returns></returns>
        public WfDelegationCollection Load(string sourceUserID)
        {
            sourceUserID.CheckStringIsNullOrEmpty("sourceUserID");

            return Load(builder => builder.AppendItem("SOURCE_USER_ID", sourceUserID));
        }

        /// <summary>
        /// 得到委托人的所有在有效期内的委托信息
        /// </summary>
        /// <param name="sourceUserID"></param>
        /// <returns></returns>
        public WfDelegationCollection GetUserActiveDelegations(string sourceUserID)
        {
            sourceUserID.CheckStringIsNullOrEmpty("sourceUserID");

            WfDelegationCollection delegationsInCache = WfDelegationCache.Instance.GetOrAddNewValue(CalculateCacheKey(sourceUserID), (cache, key) =>
            {
                WfDelegationCollection delegations = Load(builder => builder.AppendItem("SOURCE_USER_ID", sourceUserID));

                MixedDependency dependency = new MixedDependency(new UdpNotifierCacheDependency(), new MemoryMappedFileNotifierCacheDependency());

                cache.Add(key, delegations, dependency);

                return delegations;
            });

            WfDelegationCollection result = new WfDelegationCollection();

            DateTime now = SNTPClient.AdjustedUtcTime;

            foreach (WfDelegation delegation in delegationsInCache)
            {
                if (now >= delegation.StartTime && now < delegation.EndTime)
                    result.Add(delegation);
            }

            return result;
        }

        /// <summary>
        /// 重载对象更新后的代码，发送UDP通知，更新各服务器中的缓存
        /// </summary>
        /// <param name="data"></param>
        /// <param name="context"></param>
        protected override void AfterInnerUpdate(WfDelegation data, Dictionary<string, object> context)
        {
            base.AfterInnerUpdate(data, context);
            UpdateCache(data);
        }

        private static void UpdateCache(WfDelegation data)
        {
            CacheNotifyData notifyData = new CacheNotifyData(typeof(WfDelegationCache), data.SourceUserID, CacheNotifyType.Invalid);

            UdpCacheNotifier.Instance.SendNotifyAsync(notifyData);
            MmfCacheNotifier.Instance.SendNotify(notifyData);
        }

        protected override void AfterInnerDelete(WfDelegation data, Dictionary<string, object> context)
        {
            base.AfterInnerDelete(data, context);
            UpdateCache(data);
        }

        protected override string GetConnectionName()
        {
            return WorkflowSettings.GetConfig().ConnectionName;
        }

        protected override string GetUpdateSql(WfDelegation data, ORMappingItemCollection mappings, Dictionary<string, object> context, string[] ignoreProperties)
        {
            UpdateSqlClauseBuilder uBuilder = ORMapping.GetUpdateSqlClauseBuilder(data, mappings, "ApplicationName", "ProgramName");

            uBuilder.AppendItem("APPLICATION_NAME", data.ApplicationName.IsNotEmpty() ? data.ApplicationName : string.Empty);
            uBuilder.AppendItem("PROGRAM_NAME", data.ProgramName.IsNotEmpty() ? data.ProgramName : string.Empty);

            WhereSqlClauseBuilder wBuilder = ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(data, mappings, "ApplicationName", "ProgramName");

            wBuilder.AppendItem("APPLICATION_NAME", data.ApplicationName.IsNotEmpty() ? data.ApplicationName : string.Empty);
            wBuilder.AppendItem("PROGRAM_NAME", data.ProgramName.IsNotEmpty() ? data.ProgramName : string.Empty);

            return string.Format("UPDATE {0} SET {1} WHERE {2}",
                mappings.TableName,
                uBuilder.ToSqlString(TSqlBuilder.Instance),
                wBuilder.ToSqlString(TSqlBuilder.Instance));
        }

        protected override string GetInsertSql(WfDelegation data, ORMappingItemCollection mappings, Dictionary<string, object> context, string[] ignoreProperties)
        {
            InsertSqlClauseBuilder builder = ORMapping.GetInsertSqlClauseBuilder(data, mappings, "ApplicationName", "ProgramName");

            builder.AppendItem("APPLICATION_NAME", data.ApplicationName.IsNotEmpty() ? data.ApplicationName : string.Empty);
            builder.AppendItem("PROGRAM_NAME", data.ProgramName.IsNotEmpty() ? data.ProgramName : string.Empty);

            return string.Format("INSERT INTO {0} {1}",
                mappings.TableName,
                builder.ToSqlString(TSqlBuilder.Instance));
        }

        private static string CalculateCacheKey(string sourceUserID)
        {
            string result = sourceUserID;

            if (TenantContext.Current.Enabled)
                result = string.Format("{0}-{1}", TenantContext.Current.TenantCode, sourceUserID);

            return result;
        }
    }
}
