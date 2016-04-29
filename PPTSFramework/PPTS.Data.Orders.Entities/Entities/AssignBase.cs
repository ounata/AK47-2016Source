using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PPTS.Data.Common.Entities;

namespace PPTS.Data.Orders.Entities
{
    public interface IAssignShareAttr
    {
        #region
        /// <summary>
        /// 学员ID
        /// </summary>
        string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 资产ID
        /// </summary>
        string AssetID
        {
            get;
            set;
        }

        /// <summary>
        /// 资产编码
        /// </summary>
        string AssetCode
        {
            get;
            set;
        }

        /// <summary>
        /// 产品ID
        /// </summary>
        string ProductID
        {
            get;
            set;
        }

        /// <summary>
        /// 产品编码
        /// </summary>
        string ProductCode
        {
            get;
            set;
        }

        /// <summary>
		/// 产品名称
		/// </summary>
        string ProductName
        {
            get;
            set;
        }

        /// <summary>
		/// 年级代码
		/// </summary>
        string Grade
        {
            get;
            set;
        }

        /// <summary>
        /// 年级名称
        /// </summary>
        string GradeName
        {
            get;
            set;
        }

        /// <summary>
        /// 科目代码
        /// </summary>
        string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// 科目名称
        /// </summary>
        string SubjectName
        {
            get;
            set;
        }

        /// <summary>
        /// 教室ID
        /// </summary>
        string RoomID
        {
            get;
            set;
        }

        /// <summary>
        /// 教室编码
        /// </summary>
        string RoomCode
        {
            get;
            set;
        }

        /// <summary>
        /// 教室名称
        /// </summary>
        string RoomName
        {
            get;
            set;
        }

        /// <summary>
        /// 教师ID
        /// </summary>
        string TeacherID
        {
            get;
            set;
        }

        /// <summary>
        /// 教师姓名
        /// </summary>
        string TeacherName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime ModifyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        string TenantCode
        {
            get;
            set;
        }
        #endregion
    }

}
