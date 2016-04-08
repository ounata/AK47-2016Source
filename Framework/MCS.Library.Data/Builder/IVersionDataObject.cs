using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.Data.Builder
{
	/// <summary>
	/// 带版本信息的数据实体
	/// </summary>
	public interface IVersionDataObject : IVersionDataObjectWithoutID
    {
        /// <summary>
        /// 除版本信息之外的ID（主键）
        /// </summary>
		string ID { get; }
	}
}
