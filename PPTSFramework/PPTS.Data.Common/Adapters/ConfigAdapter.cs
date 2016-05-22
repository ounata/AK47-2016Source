using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;

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
        public ConfigArgs Load(string orgID)
        {
            return new ConfigArgs() {
                ClosingAccountDay = 2,
                AccountFirstChargeMinMoney = 500,
                AccountChargeEarlyMinDays = 15,
                AccountChargeEarlyMinDaysX = 30,
                EndingClassMinAccountValue = 200,
                AccountRefundTypeJudgeDays = 7};
        }
    }
}
