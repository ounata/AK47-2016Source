using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Security;

namespace PPTS.Data.Common.Adapters
{
    public class ConfigAdapter : UpdatableAndLoadableAdapterBase<ConfigEntity, ConfigEntityCollection>
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSMetaDataConnectionName;
        }

        public static readonly ConfigAdapter Instance = new ConfigAdapter();

        /// <summary>
        /// 加载配置信息。
        /// </summary>
        /// <param name="orgID">机构ID</param>
        /// <returns></returns>
        private ConfigEntity Load(string configKey)
        {
            return this.Load(builder => builder.AppendItem("ConfigKey", configKey)).SingleOrDefault();
        }

        /// <summary>
        /// 获取除GobalArgs外的所有配置
        /// </summary>
        /// <returns></returns>
        private ConfigEntityCollection LoadArgs()
        {
            return this.Load(builder => builder.AppendItem("ConfigKey", "GlobalArgs", "<>", true));
        }

        private ConfigValue DefaultConfigValue
        {
            get
            {
                return new ConfigValue()
                {
                    AccountFirstChargeMinMoneyValue = 500,
                    AccountChargeEarlyMinDaysValue = 15,
                    EndingClassMinAccountValueValue = 200,
                    AccountRefundTypeJudgeDaysValue = 7
                };
            }
        }

        private string GetConfigKey(string orgID)
        {
            return string.IsNullOrEmpty(orgID) ? "Args" : "Args_" + orgID;
        }

        public void SetConfigValue(string orgID, ConfigValue value)
        {
            string configKey = GetConfigKey(orgID);
            ConfigEntity entity = new ConfigEntity(configKey, value.ToString());
            ConfigAdapter.Instance.Update(entity);
        }

        public ConfigValue GetConfigValue(string orgID)
        {
            List<ConfigValue> list = new List<ConfigValue>();
            this.FillConfigValue(orgID, list);
            list.Add(this.DefaultConfigValue);

            ConfigValue c = list[0];
            for (int i = 1; i < list.Count; i++)
                c.Merge(list[i]);
            return c;
        }

        private void FillConfigValue(string orgID, List<ConfigValue> list)
        {
            string configKey = this.GetConfigKey(orgID);
            ConfigEntity entity = this.Load(configKey);
            if (entity != null)
                list.Add(new ConfigValue(entity.ConfigValue));
            IOrganization org = OguMechanismFactory.GetMechanism().GetObjects<IOrganization>(SearchOUIDType.Guid, orgID).SingleOrDefault();
            if (org != null && org.Parent != null)
                this.FillConfigValue(org.Parent.ID, list);
        }

        public void SetGlobalArgs(GlobalArgs args)
        {
            string v = ConfigValue.Serialize<GlobalArgs>(args);
            ConfigEntity entity = new ConfigEntity(this.GlobalArgsKey, v);
            ConfigAdapter.Instance.Update(entity);
        }

        public GlobalArgs GetGlobalArgs()
        {
            ConfigEntity entity = this.Load(this.GlobalArgsKey);
            if (entity != null)
                return ConfigValue.Desialize<GlobalArgs>(entity.ConfigValue);
            return this.DefaultGlobalArgs;
        }

        private GlobalArgs DefaultGlobalArgs
        {
            get
            {
                return new GlobalArgs(2);
            }
        }

        private string GlobalArgsKey
        {
            get
            {
                return "GlobalArgs";
            }
        }
    }
}
