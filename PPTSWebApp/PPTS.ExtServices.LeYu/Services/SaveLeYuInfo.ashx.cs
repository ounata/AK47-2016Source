using MCS.Library.Core;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Entities;
using PPTS.ExtServices.LeYu.Executors;
using PPTS.ExtServices.LeYu.Models.PotentialCustomers;
using PPTS.ExtServices.LeYu.Models.RequestParameters;
using PPTS.ExtServices.LeYu.Models.CallingOrgCenterConfig;
using PPTS.ExtServices.LeYu.Models.CustomerSource;
using PPTS.ExtServices.LeYu.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Text;

namespace PPTS.ExtServices.LeYu
{
    /// <summary>
    /// SaveLeYuInfo 的摘要说明
    /// </summary>
    public class SaveLeYuInfo : IHttpHandler
    {
        /// <summary>
        /// 参数Name=teshleyukehu&Column1=13288440033&Column2=F1&Column3=&Column4=男&Column5=11&Column6=&
        /// Column7=福州-福清校区&FirstPage=http://nanjing.xueda.com/School/154.Shtml&
        /// ReferUrl=so.360.cn&CHATPAGE=&CREATENAME=技术测试(wangcd)&CUSTNAME=技术测试(wangcd)&NOTE=&
        /// AREA=中国江苏省南京市&KEYWORD=怎样辅导一年级孩子
        /// </summary>
        /// <param name="context"></param>

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = 0;
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("Cache-Control", "no-store, must-revalidate");

            InitPotentialCustomer(context);

            //context.Response.Write("uploadcrmok:<推送成功>");
            context.Response.Write(strOut);
            context.Response.End();
        }

        //变量
        private RequestParameterModel requestParameter = new RequestParameterModel();
        PPTSJob agentJob;//员工岗位信息
        string strSubSourceId;
        string strSubSourceValue;
        string StrMainSourceId;
        string strMainSourceValue;
        IUser user;//员工信息
        IOrganization orgBranch;
        IOrganization orgSchool;
        string strSchoolYear;

        //输出
        string strOut;

        /// <summary>
        /// 学年制字典
        /// </summary>
        private readonly Dictionary<string, string> gradeTypeMapping = new Dictionary<string, string>()
        {
            { "小学一年级" , "6" } ,
            { "小学二年级" , "6" } ,
            { "小学三年级" , "6" } ,
            { "小学四年级" , "6" } ,
            { "小学五年级" , "6" } ,
            { "小学六年级" , "6" } ,
            { "初中一年级" , "3" } ,
            { "初中二年级" , "3" } ,
            { "初中三年级" , "3" } ,
            { "高中一年级" , "3" } ,
            { "高中二年级" , "3" } ,
            { "高中三年级" , "3" }
        };
        /// <summary>
        /// 潜在客户表,家长信息表常量字典
        /// </summary>
        private readonly Dictionary<string, IEnumerable<BaseConstantEntity>> dictionaryCategories = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(PotentialCustomer), typeof(Parent));

        #region 解析POST参数

        /// <summary>
        /// 解析POST参数
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="context"></param>
        private void GetParameters(NameValueCollection queryString, HttpContext context)
        {
            //家长姓名
            Regex nameRegex = new Regex("(^(([\u4e00-\u9fa5]*)|([a-zA-Z]*))+$)");
            if (queryString["Name"].IsNullOrEmpty())
            {
                requestParameter.ParentName = "未知";
            }
            if (!nameRegex.Match(queryString["Name"]).Success)
            {
                context.Response.Write("uploadcrmerror:<家长名称只能是中文或字母>");
                context.Response.End();
            }
            requestParameter.ParentName = queryString["Name"];

            //家长主叫号码
            if (queryString["Column1"].IsNullOrEmpty())
            {
                context.Response.Write("uploadcrmerror:<主叫号码为空>");
                context.Response.End();
            }
            if (!(ValidatePhone(queryString["Column1"], PhoneTypeDefine.MobilePhone) || ValidatePhone(queryString["Column1"], PhoneTypeDefine.HomePhone)))
            {
                context.Response.Write("uploadcrmerror:<主叫号码格式不正确>");
                context.Response.End();
            }
            requestParameter.ParentPrimaryCallNumber = queryString["Column1"];

            //家长其他联系方式
            if (!queryString["Column6"].IsNullOrEmpty() && !(ValidatePhone(queryString["Column6"], PhoneTypeDefine.MobilePhone) || ValidatePhone(queryString["column6"], PhoneTypeDefine.HomePhone)))
            {
                context.Response.Write("uploadcrmerror:<其他联系方式号码格式不正确>");
                context.Response.End();
            }
            requestParameter.ParentOtherCallNumber = queryString["Column6"];

            //学生姓名
            if(queryString["Column3"].IsNullOrEmpty())
            {
                requestParameter.StudentName = "未知";
            }
            if (!nameRegex.Match(queryString["Column3"]).Success)
            {
                context.Response.Write("uploadcrmerror:<学生名称只能是中文或字母>");
                context.Response.End();
            }
            requestParameter.StudentName = queryString["Column3"];

            //学生性别
            //requestParameter.StudentGender = queryString["Column4"];
            if (queryString["Column4"] == "男")
            {
                requestParameter.StudentGender = GenderType.Male;
            }
            else if (queryString["Column4"] == "女")
            {
                requestParameter.StudentGender = GenderType.Female;
            }
            else
            {
                requestParameter.StudentGender = GenderType.Unknown;
            }

            //入学大年级
            requestParameter.EnteredGrade = queryString["Column5"];
            GetSchoolYear(requestParameter.EnteredGrade);//学年制
            if (requestParameter.EnteredGrade.IsNullOrEmpty() || strSchoolYear.IsNullOrEmpty())
            {
                context.Response.Write("uploadcrmerror:<入学大年级无法识别>");
                context.Response.End();
            }


            //创建人（OA账号）
            if (!queryString["CREATENAME"].IsNullOrEmpty())
            {
                string[] aryStaff = queryString["CREATENAME"].Split('(');
                if (aryStaff.Length >= 2)
                {
                    requestParameter.CreateStaffName = aryStaff[0];
                    requestParameter.CreateStaffOA = aryStaff[1].Substring(0, aryStaff[1].Length - 1);
                }
                else
                {
                    context.Response.Write("uploadcrmerror:<创建人信息格式错误>");
                    context.Response.End();
                }
            }

            //员工信息
            user = OGUExtensions.GetUserByOAName(requestParameter.CreateStaffOA);
            PPTSJobCollection jobCollection = user.Jobs();
            if (!queryString["column7"].IsNullOrEmpty())
            {
                IOrganization orgInfo = OGUExtensions.GetOrganizationByShortName(queryString["Column7"]);
                if (orgInfo == null)
                {
                    context.Response.Write(string.Format("uploadcrmerror:<{0}>", "校区信息错误"));
                    context.Response.End();
                }
                //得到员工所在分公司信息
                IList<IOrganization> orgListAllParent = orgInfo.GetAllDataScopeParents();
                IOrganization orgBranchTmp = orgListAllParent.Where(o => o.PPTSDepartmentType() == DepartmentType.Branch).SingleOrDefault();

                //分公司对应的岗位
                IList<PPTSJob> jobcollectionTmp = jobCollection.Where(job => job.GetParentOrganizationByType(DepartmentType.Branch).ID == orgBranchTmp.ID).ToList();
                if (jobcollectionTmp == null || jobcollectionTmp.Count == 0)
                {
                    context.Response.Write(string.Format("uploadcrmerror:<{0}>", "您没有与名片中分公司对应的岗位，不能推送"));
                    context.Response.End();
                }

                //组织类型是分公司或者校区
                if (orgInfo.PPTSDepartmentType() == DepartmentType.Branch || orgInfo.PPTSDepartmentType() == DepartmentType.Campus)
                {
                    //获得当前推送的岗位信息。
                    agentJob = GetSingleUserJobInfo(jobCollection, orgBranchTmp.ID);
                    
                    if (!agentJob.Organization().ID.IsNullOrEmpty() && agentJob.GetParentOrganizationByType(DepartmentType.Branch).ID != orgBranchTmp.ID)
                    {
                        context.Response.Write(string.Format("uploadcrmerror:<{0}>", "您所在的分公司与名片中的分公司不同，不能推送，请确认该信息"));
                        context.Response.End();
                    }
                    if (orgInfo.PPTSDepartmentType() == DepartmentType.Branch)
                    {
                        //校区信息为空时，建档人属于总部，推送到总公司库，否则推到分公司库
                        //strBranchId = agentJob.Organization().ID.IsNullOrEmpty() ? string.Empty : orgInfo.Parent.ID;
                        orgBranch = agentJob.Organization() == null ? null : orgInfo.Parent;
                    }
                    else if (orgInfo.PPTSDepartmentType() == DepartmentType.Campus)
                    {
                        //是校区
                        //strSchoolId = orgInfo.ID;
                        //strBranchId = orgInfo.Parent.ID;
                        orgSchool = orgInfo;
                        orgBranch = orgInfo.Parent;
                    }
                }
                else
                {
                    //校区信息错误
                    context.Response.Write(string.Format("uploadcrmerror:<{0}>", "校区信息错误"));
                    context.Response.End();
                }
            }
            else
            {
                //归属地字段为空
                context.Response.Write(string.Format("uploadcrmerror:<{0}>", "校区信息为空"));
                context.Response.End();
            }

            //接触方式
            requestParameter.ContactTypeStr = NewContactType.OnlineLeYu;

            //信息来源
            requestParameter.ChatPage = queryString["CHATPAGE"];
            string requestURL = context.Request.Url.Authority;
            requestParameter.FirstPage = queryString["FirstPage"];
            CustomerSourceModel custSource = GetCustomerSourceModel(requestParameter.ChatPage, requestURL, requestParameter.FirstPage);
            if (null != custSource)
            {
                if (null == custSource.MainSource)
                {
                    context.Response.Write(string.Format("uploadcrmerror:<{0}>", "一级信息来源不存在"));
                    context.Response.End();
                }
                if (null == custSource.SubSource)
                {
                    context.Response.Write(string.Format("uploadcrmerror:<{0}>", "二级信息来源不存在"));
                    context.Response.End();
                }
                strSubSourceId = custSource.SubSource.Key;
                strSubSourceValue = custSource.SubSource.Value;
                StrMainSourceId = custSource.MainSource.Key;
                strMainSourceValue = custSource.MainSource.Value;
            }
            else
            {
                context.Response.Write(string.Format("uploadcrmerror:<{0}>", "信息来源不存在"));
                context.Response.End();
            }

            //备注
            requestParameter.Remark = queryString["NOTE"].IsNullOrEmpty() ? string.Empty : "备注：" + queryString["NOTE"] + "\r\n";
            //访客区域
            requestParameter.RequestArea = queryString["AREA"].IsNullOrEmpty() ? string.Empty : "访客区域：" + queryString["AREA"] + "\r\n";
            //访客来源
            requestParameter.RequestFromURL = queryString["ReferUrl"].IsNullOrEmpty() ? string.Empty : "访客来源：" + queryString["ReferUrl"] + "\r\n";
            //搜索词
            requestParameter.KeyWord = queryString["KEYWORD"].IsNullOrEmpty() ? string.Empty : "搜索词：" + queryString["KEYWORD"] + "\r\n";

        }

        #endregion

        #region 验证电话号码重复，登录人是否是OA

        /// <summary>
        /// 验证电话号码重复，登录人是否是OA
        /// </summary>
        /// <param name="context"></param>
        private void ValidateBaseInfo(HttpContext context)
        {
            #region 电话号码排重
            PhoneCollection phones = null;
            if (!requestParameter.ParentOtherCallNumber.IsNullOrEmpty())
            {
                Phone phoneOther = new Phone();
                phoneOther = requestParameter.ParentOtherCallNumber.ToPhone(string.Empty, false);

                //查询Phone，是否已存在
                phones = PPTS.Data.Customers.Adapters.PhoneAdapter.Instance.Load((builder) => { builder.LogicOperator = MCS.Library.Data.Builder.LogicOperatorDefine.Or; builder.AppendItem("PhoneNumber", requestParameter.ParentPrimaryCallNumber).AppendItem("PhoneNumber", phoneOther.PhoneNumber); }, DateTime.MinValue);
            }
            else
            {
                phones = PPTS.Data.Customers.Adapters.PhoneAdapter.Instance.Load((builder) => { builder.AppendItem("PhoneNumber", requestParameter.ParentPrimaryCallNumber); }, DateTime.MinValue);
            }
            if (phones != null && phones.Count > 0)
            {
                context.Response.Write("uploadcrmerror:<电话已存在>");
                context.Response.End();
            }
            
            #endregion

            #region 验证登录人OA

            if (user == null)
            {
                context.Response.Write("uploadcrmerror:<登陆用户名不在ERP系统中>");
                context.Response.End();
            }
            if (user.Jobs() == null || user.Jobs().Count <= 0)
            {
                context.Response.Write("uploadcrmerror:<登陆用户名所属岗位信息不在ERP系统中>");
                context.Response.End();
            }
            if (agentJob == null)
            {
                context.Response.Write("uploadcrmerror:<登陆人没有符合要求的岗位信息>");
                context.Response.End();
            }

            #endregion
        }

        #endregion

        #region 获取坐席岗位信息

        /// <summary>
        /// 获取坐席岗位信息
        /// </summary>
        /// <param name="currentJobCollection">当前推送人的岗位信息</param>
        /// <param name="strOrgId"></param>
        /// <returns></returns>
        private PPTSJob GetSingleUserJobInfo(PPTSJobCollection currentJobCollection, string strOrgId)
        {
            PPTSJob job = new PPTSJob();

            if (currentJobCollection == null || currentJobCollection.Count == 0)
            {
                return null;
            }

            List<CallingOrgCenterCofigModel> listCallingOrgCenterCofigModel = new List<CallingOrgCenterCofigModel>();
            listCallingOrgCenterCofigModel = APPFunc.GetJobCallingList();

            //筛选岗位,筛选当前推送人的岗位与配置文件中相同岗位名称的岗位信息
            IList<PPTSJob> listJobInOrg = new List<PPTSJob>();
            listJobInOrg = currentJobCollection.Where(c => listCallingOrgCenterCofigModel.Where(d => d.JobName == c.JobName).ToList().Count > 0).ToList();
            //listCallingOrgCenterCofigModel.ForEach(l => l.JobCollection = (PPTSJobCollection)currentJobCollection.Where(c => c.Name == l.JobName));
            //属于分公司的岗位
            listJobInOrg = listJobInOrg.Where(j => j.GetParentOrganizationByType(DepartmentType.Branch).ID == strOrgId).ToList();
            
            if (listJobInOrg.Count == 0)
            {
                return null;
            }
            else
            {
                //默认选用一个
                job = listJobInOrg[0];
                //按照jobName合并CallingOrgCenterCofigModel和PPTSJob
                List<JobModel> jobModels = (from a in listCallingOrgCenterCofigModel join b in listJobInOrg on a.JobName equals b.JobName select new JobModel() { CofigModel = a, Job = b }).ToList<JobModel>();
                //选择CallingOrgCenterCofigModel.SortId最小的
                int minSortId = jobModels.Min(j => Convert.ToInt32(j.CofigModel.SortId));
                //专员级别的岗位信息
                List<JobModel> _jobModel = jobModels.Where(m => (m.CofigModel.SortId == minSortId.ToString())).ToList<JobModel>();
                if (_jobModel.Count >= 2)
                {
                    //若一个员工有多个专员级别的岗位，则优先推送到主岗
                    JobModel jm = _jobModel.Where(j => j.Job.IsPrimary).FirstOrDefault();
                    if (null != jm)
                    {
                        job = jm.Job;
                    }
                    else
                    {
                        //若一个员工有多个专员级别的岗位，但没有主岗，则任意推送
                        job = _jobModel[0].Job;
                    }
                }
                else if (_jobModel.Count == 0)
                {
                    //若没有专员级别的岗位，则推送至其他岗位是主岗的岗位
                    JobModel jm = jobModels.Where(j => j.Job.IsPrimary).FirstOrDefault();
                    if (null != jm)
                    {
                        job = jm.Job;
                    }
                }
                else if (_jobModel.Count == 1)
                {
                    //如果只有一个专员岗位，则直接推送
                    job = _jobModel[0].Job;
                }
            }
            return job;
        }

        #endregion

        #region 扩展方法，根据三种URL确定信息来源，验证电话号码的合法性，根据乐语的来源值得到PPTS的来源，根据学员的年级获得学年制

        /// <summary>
        /// 根据三种URL确定信息来源
        /// </summary>
        /// <param name="strChatPage"></param>
        /// <param name="strUrl"></param>
        /// <param name="strFirstPage"></param>
        /// <returns></returns>
        private CustomerSourceModel GetCustomerSourceModel(string strChatPage, string strUrl, string strFirstPage)
        {
            //信息来源二级分类
            CustomerSourceModel custSource = null;

            string strLeYuSourse = string.Empty;
            if (!string.IsNullOrEmpty(strChatPage))
            {
                string[] temp = strChatPage.Split('&');
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].Contains("k=") || temp[i].Contains("f="))
                        strLeYuSourse += temp[i] + "&";
                }
                strLeYuSourse = strLeYuSourse.Length > 1 ? strLeYuSourse.Substring(0, strLeYuSourse.Length - 1) : string.Empty;
                custSource = !strLeYuSourse.IsNullOrEmpty() ? GetSource(strLeYuSourse) : null;
            }
            if (null == custSource)
            {
                strLeYuSourse = string.Empty;
                if (strFirstPage.IndexOf('?') > 0)
                {
                    string leyuSourceTmp = strFirstPage.Substring(strFirstPage.IndexOf('?') + 1);
                    if (!leyuSourceTmp.IsNullOrEmpty())
                    {
                        string[] aryFirstPage = leyuSourceTmp.Split('&');
                        for (int i = 0; i < aryFirstPage.Length; i++)
                        {
                            if (aryFirstPage[i].Contains("k=") || aryFirstPage[i].Contains("f="))
                            {
                                strLeYuSourse += aryFirstPage[i] + "&";
                            }
                        }
                        strLeYuSourse = strLeYuSourse.Length > 1 ? strLeYuSourse.Substring(0, strLeYuSourse.Length - 1) : string.Empty;
                        custSource = !strLeYuSourse.IsNullOrEmpty() ? GetSource(strLeYuSourse) : null;
                    }
                }
            }
            if (null == custSource)
            {
                if (!strUrl.IsNullOrEmpty() && strUrl.Contains(".xueda.com"))
                {
                    strLeYuSourse = "官网-SEO免费推广";
                }
                else
                {
                    strLeYuSourse = "官网-学大辅导SEM";
                }
                custSource = GetSource(strLeYuSourse);
            }

            return custSource;
        }

        /// <summary>
        /// 验证电话号码的合法性
        /// </summary>
        /// <param name="phoneNumber">电话号码</param>
        /// <param name="phoneType">电话类型</param>
        /// <returns></returns>
        private bool ValidatePhone(string phoneNumber, PhoneTypeDefine phoneType)
        {
            Regex phoneRegex = new Regex("(^(\\d{3,4})-(\\d{7,8})-(\\d{1,5}))|(^(\\d{3,4})-(\\d{7,8})$)");
            Regex mobileRegex = new Regex("^1[3458]\\d{9}$");

            if (phoneType == PhoneTypeDefine.HomePhone || phoneType == PhoneTypeDefine.WorkPhone)
            {
                //住宅电话或办公电话
                return phoneRegex.Match(phoneNumber).Success;
            }
            if (phoneType == PhoneTypeDefine.MobilePhone)
            {
                //手机号码
                return mobileRegex.Match(phoneNumber).Success;
            }
            return false;
        }

        /// <summary>
        /// 根据乐语的来源值得到PPTS的来源
        /// [Category]C_Code_Abbr_BO_Customer_Source
        /// </summary>
        /// <param name="strLeYuSource"></param>
        /// <returns></returns>
        private CustomerSourceModel GetSource(string strLeYuSource)
        {
            strLeYuSource = strLeYuSource.ToLower();
            BaseConstantEntity subSource = null;
            BaseConstantEntity mainSource = null;
            string strERPSourceValue = strLeYuSource;
            if (strLeYuSource.Contains("k=") || strLeYuSource.Contains("f="))
            {
                strERPSourceValue = APPFunc.GetERPSourceValue(strLeYuSource);
            }

            KeyValuePair<string, IEnumerable<BaseConstantEntity>> constants = dictionaryCategories.Where(d => d.Key == "C_Code_Abbr_BO_Customer_Source").SingleOrDefault();

            subSource = constants.Value.Where(c => c.Value == strERPSourceValue).SingleOrDefault();

            if (null == subSource)
            {
                return null;
            }
            mainSource = constants.Value.Where(c => c.Key == subSource.ParentKey).SingleOrDefault();

            return new CustomerSourceModel() { MainSource = mainSource, SubSource = subSource };
        }

        /// <summary>
        /// 根据学员的年级获得学年制
        /// </summary>
        /// <param name="strEntranceGrade"></param>
        /// <returns></returns>
        private string GetSchoolYear(string strEntranceGrade)
        {
            strSchoolYear = string.Empty;

            //得到年级常量
            KeyValuePair<string, IEnumerable<BaseConstantEntity>> constants = dictionaryCategories.Where(d => d.Key == "C_CODE_ABBR_CUSTOMER_GRADE").SingleOrDefault();

            BaseConstantEntity constGrade = constants.Value.Where(c => c.Key == strEntranceGrade).SingleOrDefault();

            if (constGrade != null)
            {
                strSchoolYear = gradeTypeMapping[constGrade.Value];
            }

            return strSchoolYear;
        }

        

        #endregion

        /// <summary>
        /// 初始化并保存推送信息
        /// PotentialCustomer潜客信息
        /// Parent家长信息
        /// CustomerParentRelation学员与家长关系
        /// CustomerStaffRelation学员与员工关系
        /// Phone联系方式信息
        /// CustomerFollow跟进信息
        /// </summary>
        /// <param name="context"></param>
        private void InitPotentialCustomer(HttpContext context)
        {
            NameValueCollection queryString = context.Request.Form;

            string strCreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            if (queryString["Column2"].IsNullOrEmpty() || queryString["Column2"] == "F2")
            {
                context.Response.Write("uploadcrmerror:<无效客户不允许推送>");
                context.Response.End();
            }
            else if (queryString["Column2"] == "F1")
            {
                GetParameters(queryString, context);
            }

            ValidateBaseInfo(context);
            
            #region 录入信息

            CreatablePortentialCustomerModel pCustmodel = new CreatablePortentialCustomerModel();

            //保存潜客信息
            pCustmodel.PotentialCustomer = new PotentialCustomer()
            {
                CustomerID = UuidHelper.NewUuidString(),
                CustomerName = requestParameter.StudentName,
                OrgID = orgBranch.ID,
                OrgName = orgBranch.Name,
                OrgType = (OrgTypeDefine)orgBranch.PPTSDepartmentType().GetHashCode(),
                Gender = requestParameter.StudentGender,
                EntranceGrade = requestParameter.EnteredGrade,
                SchoolYear = strSchoolYear,
                ContactType = requestParameter.ContactTypeStr.GetHashCode().ToString(),
                SourceMainType = StrMainSourceId,
                SourceSubType = strSubSourceId                
            };

            //学校信息，因无学校名称等信息，无法创建，学校与学员关系亦不能创建

            //家长信息
            pCustmodel.Parent = new Parent()
            {
                ParentID = UuidHelper.NewUuidString(),
                ParentName = requestParameter.ParentName
            };

            //学员与家长关系
            pCustmodel.CustomerParentRelation = new CustomerParentRelation()
            {
                CustomerID = pCustmodel.PotentialCustomer.CustomerID,
                ParentID = pCustmodel.Parent.ParentID
            };
            //电话信息
            PhoneCollection phoneCollection = new PhoneCollection();

            phoneCollection.Add(requestParameter.ParentPrimaryCallNumber.ToPhone(pCustmodel.Parent.ParentID, true));
            if (!requestParameter.ParentOtherCallNumber.IsNullOrEmpty())
            {
                phoneCollection.Add(requestParameter.ParentOtherCallNumber.ToPhone(pCustmodel.Parent.ParentID, false));
            }
            pCustmodel.PhoneCollection = phoneCollection;

            //学员与员工关系
            pCustmodel.CustomerStaffRelation = new CustomerStaffRelation()
            {
                CustomerID = pCustmodel.PotentialCustomer.CustomerID,
                StaffID = user.ID,
                StaffName = user.Name,
                StaffJobID = agentJob.ID,
                StaffJobName = agentJob.Name,
                StaffJobOrgID = agentJob.Organization().ID,
                StaffJobOrgName = agentJob.Organization().Name,
                RelationType = CustomerRelationType.Callcenter
            };

            //跟进记录
            pCustmodel.CustomerFollow = new CustomerFollow()
            {
                OrgID = orgBranch.ID,
                OrgName = orgBranch.Name,
                OrgType = (OrgTypeDefine)orgBranch.PPTSDepartmentType().GetHashCode(),
                CustomerID = pCustmodel.PotentialCustomer.CustomerID,
                FollowID = UuidHelper.NewUuidString(),
                FollowerID = user.ID,
                FollowerName = user.Name,
                FollowerJobID = agentJob.ID,
                FollowerJobName = agentJob.Name,
                FollowType = SaleContactType.OnLine.GetHashCode().ToString(),
                FollowStage = SalesStageType.NotInvited,
                FollowObject = SaleContactTarget.Others.GetHashCode().ToString(),
                FollowMemo = requestParameter.SaleTrackRemark,
                FollowTime = SNTPClient.AdjustedTime
            };

            pCustmodel.UpdatePotentialCustomer();
            //strOut = pCustmodel.PotentialCustomer.CustomerID + "," + pCustmodel.Parent.ParentID + "," +pCustmodel.CustomerFollow.FollowID;
            StringBuilder str = new StringBuilder();
            str.AppendFormat("delete from [CM].[PotentialCustomers] where [CustomerID] = '{0}'\r\n", pCustmodel.PotentialCustomer.CustomerID);
            str.AppendFormat("delete from [CM].[Parents] where [ParentID] = '{0}'\r\n", pCustmodel.Parent.ParentID);
            str.AppendFormat("delete from [CM].[CustomerParentRelations] where [CustomerID] = '{0}'\r\n", pCustmodel.PotentialCustomer.CustomerID);
            str.AppendFormat("delete from [CM].[Phones] where [OwnerID] = '{0}'\r\n", pCustmodel.Parent.ParentID);
            str.AppendFormat("delete from [CM].[CustomerStaffRelations] where [CustomerID] = '{0}'\r\n", pCustmodel.PotentialCustomer.CustomerID);
            str.AppendFormat("delete from [CM].[CustomerFollows] where [FollowID] = '{0}'\r\n", pCustmodel.CustomerFollow.FollowID);
            strOut = str.ToString();
            #endregion

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}