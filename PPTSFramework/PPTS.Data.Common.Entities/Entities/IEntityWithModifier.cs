using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Entities
{
    /// <summary>
    /// 带修改者实体需要实现的接口
    /// </summary>
    public interface IEntityWithModifier
    {
        /// <summary>
        /// 修改者的ID
        /// </summary>
        string ModifierID
        {
            get;
            set;
        }

        /// <summary>
        /// 修改者的名称
        /// </summary>
        string ModifierName
        {
            get;
            set;
        }
    }
}
