using PPTS.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.ExtServices.CTI.Models.RequestParameters
{
    public class CTIParameterModel
    {
        /// <summary>
        /// CTI客户编号[customerid]
        /// </summary>
        public string CTICustomerID
        { get; set; }

        /// <summary>
        /// CTI主叫号码[ani]
        /// </summary>
        public string CTIPrimaryCallNumber
        { get; set; }

        /// <summary>
        /// 坐席登录用户名[agentid]
        /// </summary>
        public string AgentID
        { get; set; }

        /// <summary>
        /// 家长姓名[parentname]
        /// </summary>
        public string ParentName
        { get; set; }

        /// <summary>
        /// 家长性别[parentsex]
        /// </summary>
        public string ParentGender
        { get; set; }

        /// <summary>
        /// 学生姓名[stuname]
        /// </summary>
        public string StudentName
        { get; set; }

        /// <summary>
        /// 学生性别[stusex]
        /// </summary>
        public string StudentGender
        { get; set; }

        /// <summary>
        /// 座机[parenttel]
        /// </summary>
        public string ParentTel
        { get; set; }

        /// <summary>
        /// 手机[mobile]
        /// </summary>
        public string ParentMobile
        { get; set; }

        /// <summary>
        /// 学生年级[stugrade]
        /// </summary>
        public string StudentGrade
        { get; set; }

        /// <summary>
        /// 学生就读学校[stuschool]
        /// </summary>
        public string StudentSchool
        { get; set; }

        /// <summary>
        /// 学生所在城市[stucity]
        /// </summary>
        public string StudentCity
        { get; set; }

        /// <summary>
        /// 学生所在城市PPTS的编码[CityID_PPTS]
        /// </summary>
        public string CityID_PPTS
        { get; set; }

        /// <summary>
        /// 校区名称[WPName_PPTS]
        /// </summary>
        public string CampusName
        { get; set; }

        /// <summary>
        /// 校区ID[WPID_PPTS]
        /// </summary>
        public string CampusID
        { get; set; }

        /// <summary>
        /// 所属分工司ID[orgid]
        /// </summary>
        public string BranchOrgID
        { get; set; }

        /// <summary>
        /// 信息来源[InfoFrom]
        /// </summary>
        public string InfoFrom
        { get; set; }

        /// <summary>
        /// 学生基本信息[StuIntro]
        /// </summary>
        public string StudentIntroduce
        { get; set; }

        /// <summary>
        /// 备注[Memo]
        /// </summary>
        public string Remark
        { get; set; }

        /// <summary>
        /// 回访记录( 沟通经过 )[TalkConts]
        /// </summary>
        public string TalkConts
        { get; set; }

        /// <summary>
        /// 登记时间[LogDate]
        /// </summary>
        public string LogDate
        { get; set; }

        /// <summary>
        /// 产品意向[PurchaseIntention]
        /// </summary>
        public string PurchaseIntention
        { get; set; }

        /// <summary>
        /// 约访理由[TalkReason]
        /// </summary>
        public string TalkReason
        { get; set; }

        /// <summary>
        /// 家长情况[ParentInfo]
        /// </summary>
        public string ParentInfo
        { get; set; }

        /// <summary>
        /// 学生成绩[StuScore]
        /// </summary>
        public string StudentScore
        { get; set; }

    }
}