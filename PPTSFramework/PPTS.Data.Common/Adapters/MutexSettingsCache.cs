using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using PPTS.Data.Common.Adapters;

namespace PPTS.Data.Common
{
    public class MutexSettingsCache
    {
        static object _syncRoot = new object();
        static Dictionary<MutexAction, List<MutexAction>> _dict = new Dictionary<MutexAction, List<MutexAction>>();
        /// <summary>
        /// 获取互斥信息。
        /// </summary>
        /// <returns></returns>
        public static List<MutexAction> GetMutexActions(MutexAction action)
        {
            if(!_dict.ContainsKey(action))
            {
                lock(_syncRoot)
                {
                    if (!_dict.ContainsKey(action))
                    {
                        MutexSettingCollection settings = MutexSettingAdapter.Instance.LoadAll();
                        foreach(MutexSetting setting in settings)
                        {
                            if (!_dict.ContainsKey(setting.BizAction))
                                _dict.Add(setting.BizAction, new List<MutexAction>());
                            _dict[setting.BizAction].Add(setting.MutexAction);
                        }
                    }
                }
            }
            if (_dict.ContainsKey(action))
                return _dict[action];
            return new List<MutexAction>();
        }
    }
}
