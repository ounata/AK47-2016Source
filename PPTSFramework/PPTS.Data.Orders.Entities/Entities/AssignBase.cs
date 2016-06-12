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
        /// 资产ID
        string AssetID
        {
            get;
            set;
        }
        
        /// 资产编码
        string AssetCode
        {
            get;
            set;
        }

        /// 学员ID
        string CustomerID
        {
            get;
            set;
        }

        /// 学员编码
        string CustomerCode
        {
            get;
            set;
        }

        /// 学员姓名
        string CustomerName
        {
            get;
            set;
        }

        /// 产品ID
        string ProductID
        {
            get;
            set;
        }

        /// 产品编码
        string ProductCode
        {
            get;
            set;
        }

		/// 产品名称
        string ProductName
        {
            get;
            set;
        }

        /// 教室ID
        string RoomID
        {
            get;
            set;
        }

        /// 教室编码
        string RoomCode
        {
            get;
            set;
        }

        /// 教室名称
        string RoomName
        {
            get;
            set;
        }
        
        /// 教师ID
        string TeacherID
        {
            get;
            set;
        }

        /// 教师姓名
        string TeacherName
        {
            get;
            set;
        }

        /// 教师岗位ID
        string TeacherJobID { get; set; }

        ///教师学科组名称
        string TeacherJobOrgName
        {
            get; set;
        }

        ///教师学科组ID
        string TeacherJobOrgID
        {
            get; set;
        }

        ///教师，全职还是兼职
        int? IsFullTimeTeacher { get; set; }

        ///学员账户ID
        string AccountID { get; set; }

        /// 年级代码
        string Grade
        {
            get;
            set;
        }

        /// 年级名称
        string GradeName
        {
            get;
            set;
        }

        /// 科目代码
        string Subject
        {
            get;
            set;
        }

        /// 科目名称
        string SubjectName
        {
            get;
            set;
        }

        /// 创建时间
        DateTime CreateTime
        {
            get;
            set;
        }

        /// 最后修改时间
        DateTime ModifyTime
        {
            get;
            set;
        }

        ///课时类型代码
        string CategoryType { get; set; }

        ///课时类型名称
        string CategoryTypeName { get; set; }


        string TenantCode
        {
            get;
            set;
        }
 
        #endregion
    }
}
