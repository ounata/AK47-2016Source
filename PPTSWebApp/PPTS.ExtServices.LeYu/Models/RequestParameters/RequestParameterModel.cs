using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Customers;
using PPTS.Data.Common;

namespace PPTS.ExtServices.LeYu.Models.RequestParameters
{
    /// <summary>
    /// 乐语Get参数实体
    /// </summary>
    public class RequestParameterModel
    {
        /// <summary>
        /// 家长姓名
        /// name
        /// </summary>
        public string ParentName
        { get; set; }

        /// <summary>
        /// 家长主叫号码，手机号码
        /// column1
        /// </summary>
        public string ParentPrimaryCallNumber
        { get; set; }

        /// <summary>
        /// 家长其他联系方式
        /// column6
        /// </summary>
        public string ParentOtherCallNumber
        { get; set; }

        /// <summary>
        /// 学生姓名
        /// column3
        /// </summary>
        public string StudentName
        { get; set; }

        /// <summary>
        /// 学生性别
        /// column4
        /// </summary>
        public GenderType StudentGender
        { get; set; }

        /// <summary>
        /// 入学大年级
        /// column5
        /// </summary>
        public string EnteredGrade
        { get; set; }

        /// <summary>
        /// 创建人名称
        /// CREATENAME
        /// 格式：技术测试(wangcd)
        /// </summary>
        public string CreateStaffName
        { get; set; }

        /// <summary>
        /// 创建人OA账号
        /// CREATENAME
        /// 格式：技术测试(wangcd)
        /// </summary>
        public string CreateStaffOA
        { get; set; }

        /// <summary>
        /// 客户资源归属地
        /// column7
        /// 
        /// 根据此参数获得分公司信息。
        /// </summary>
        public string CustomerFrom
        { get; set; }

        /// <summary>
        /// 接触方式
        /// defaultValue="4"在线咨询-乐语
        /// </summary>
        public NewContactType  ContactTypeStr
        {
            get; set;
        }

        /// <summary>
        /// 对话URL
        /// </summary>
        public string ChatPage
        { get; set; }

        /// <summary>
        /// 访问URL
        /// </summary>
        public string FirstPage
        { get; set; }

        /// <summary>
        /// 备注
        /// NOTE
        /// </summary>
        public string Remark
        { get; set; }

        /// <summary>
        /// 访客地域
        /// AREA
        /// </summary>
        public string RequestArea
        { get; set; }

        /// <summary>
        /// 访客来源
        /// REFERURL
        /// </summary>
        public string RequestFromURL
        { get; set; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string KeyWord
        { get; set; }

        /// <summary>
        /// 跟进记录的备注信息
        /// Remark + RequestArea + RequestFromURL + KeyWord
        /// </summary>
        public string SaleTrackRemark
        {
            get { return Remark + RequestArea + RequestFromURL + KeyWord; }
        }
    }
}