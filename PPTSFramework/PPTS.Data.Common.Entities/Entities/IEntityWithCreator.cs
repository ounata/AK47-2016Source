using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Entities
{
    /// <summary>
    /// 带创建者实体需要实现的接口
    /// </summary>
    public interface IEntityWithCreator
    {
        /// <summary>
        /// 创建者的ID
        /// </summary>
        string CreatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建者的名称
        /// </summary>
        string CreatorName
        {
            get;
            set;
        }
    }
}
