using MCS.Library.Core;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Entities;
using PPTS.ExtServices.CTI.Models.RequestParameters;
using PPTS.ExtServices.CTI.Common;
using PPTS.Data.Common;
using PPTS.ExtServices.CTI.Models.CallingOrgCenterConfig;
using PPTS.ExtServices.CTI.Models.CustomerSource;
using PPTS.ExtServices.CTI.Models.PotentialCustomers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Net.SNTP;
using System.Text;

namespace PPTS.ExtServices.CTI.Controllers
{
    public class PPTSToERPInterfaceController : Controller
    {
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(string xml, string auth)
        {
            try
            {
                CTIParameterModel parameterModel = GetCTIParamModelFormXml(xml);

                parameterModel.NullCheck("参数错误，数据转化发生错误");

                string strError = ValidateBaseInfo(parameterModel, auth);
                if (!strError.IsNullOrEmpty())
                {
                    //返回的错误信息是xml格式的
                    return Content(
                        CreateErrorXml(strError, "1")
                        );
                }

                //推送岗位
                agentJob = GetSingleUserJobInfo(jobCollection);
                (agentJob == null).TrueThrow("没有可推送岗位");

                //根据参数获得推送的分公司(学员所属分公司)
                orgBranch = OGUExtensions.GetOrganizationByID(parameterModel.BranchOrgID);
                orgBranch.NullCheck("没有查询到分公司信息");

                //获得学年制
                strSchoolYear = GetSchoolYear(parameterModel.StudentGrade);
                strSchoolYear.IsNullOrEmpty().TrueThrow("学年制无法获取");

                InitPotentialCustomer(parameterModel);

                return Content(strOut);
                //return Content(CreateErrorXml("分配成功", "0"));
            }
            catch(Exception e)
            {
                return Content(CreateErrorXml("呼叫中心数据转换失败:" + e.Message, "1"));
            }
            
        }

        #region 数据字典

        /// <summary>
        /// 潜在客户表,家长信息表常量字典
        /// </summary>
        private readonly Dictionary<string, IEnumerable<BaseConstantEntity>> dictionaryCategories = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(PotentialCustomer), typeof(Parent));

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
        /// 过滤名单
        /// </summary>
        private readonly List<String> filterList = new List<String>()
        {
            "2264090011" ,  //58同城
            "52600" ,       //浪淘金
            "96299"         //华创
        };

        #endregion

        #region 变量


        JobModel agentJob;//员工岗位信息
        IUser user;//登录用户
        PPTSJobCollection jobCollection;//登录用户的岗位集合
        //string strSourceSystem;
        IOrganization orgBranch;//推送分公司
        CustomerSourceModel customerSourceModel;//信息来源
        string strSchoolYear;//学年制
        string strOut;

        #endregion 变量

        #region 验证参数,并初始化部分变量

        /// <summary>
        /// 验证参数
        /// </summary>
        /// <param name="context"></param>
        private string ValidateBaseInfo(CTIParameterModel parmModel,string strAuth)
        {
            //验证数据是否是伪造的
            string strMD5 = APPFunc.MD5Encryption(string.Format("{0}{1}", parmModel.LogDate, parmModel.AgentID));
            if (strAuth != strMD5)
            {
                return "加密密钥核对失败";
            }

            if (parmModel.StudentName.IsNullOrEmpty())
            {
                return "学生信息缺失";
            }

            if (parmModel.ParentName.IsNullOrEmpty())
            {
                return "家长信息都缺失";
            }

            if (parmModel.CTIPrimaryCallNumber.IsNullOrEmpty() && parmModel.ParentMobile.IsNullOrEmpty() && parmModel.ParentTel.IsNullOrEmpty())
            {
                return "主叫号码、座机、手机均为空";
            }

            // 电话号码排重
            PhoneCollection phones;
            if (!parmModel.ParentTel.IsNullOrEmpty())
            {
                Phone phoneOther = new Phone();
                phoneOther = parmModel.ParentTel.ToPhone(string.Empty, false);
                //查询Phone，是否已存在
                phones = PPTS.Data.Customers.Adapters.PhoneAdapter.Instance.Load((builder) => { builder.LogicOperator = MCS.Library.Data.Builder.LogicOperatorDefine.Or; builder.AppendItem("PhoneNumber", parmModel.ParentMobile).AppendItem("PhoneNumber", phoneOther.PhoneNumber); }, DateTime.MinValue);
            }
            else
            {
                phones = PPTS.Data.Customers.Adapters.PhoneAdapter.Instance.Load((builder) => { builder.AppendItem("PhoneNumber", parmModel.ParentMobile); }, DateTime.MinValue);
            }
            if (phones != null && phones.Count > 0)
            {
                return "电话号码已存在";
            }

            //验证人员信息
            user = OGUExtensions.GetUserByOAName(parmModel.AgentID);
            user.NullCheck("座席登录用户名不在ERP系统中");
            jobCollection = user.Jobs();
            jobCollection.NullCheck("座席登录用户名所属岗位信息不在ERP系统中");
            
            if (jobCollection != null)
            {
                if (jobCollection.Count <= 0)
                {
                    return "座席登录用户名所属岗位信息不在ERP系统中";
                }
            }
            List<CallingOrgCenterCofigModel> listCallingOrgCenterCofigModel = APPFunc.GetJobCallingList();

            IList<PPTSJob> listJob = jobCollection.Where(j => listCallingOrgCenterCofigModel.Where(l => l.JobName == j.JobName).ToList().Count > 0).ToList<PPTSJob>();
            if (listJob.Count == 0)
            {
                return "坐席所属岗位不在总呼或分呼";
            }
            //验证信息来源
            customerSourceModel = GetSource(parmModel.InfoFrom);
            
            return string.Empty;
        }

        #endregion 验证参数,并初始化部分变量

        #region 返回的错误信息格式

        /// <summary>
        /// 返回的错误信息格式
        /// </summary>
        /// <param name="strErrMsg"></param>
        /// <param name="strErrKey"></param>
        /// <returns></returns>
        private string CreateErrorXml(string strErrMsg, string strErrKey)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlNode xmlNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlNode);

            XmlElement xmlElementRoot = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(xmlElementRoot);

            XmlElement xmlElementError = xmlDoc.CreateElement("error");
            xmlElementError.InnerText = strErrKey;
            xmlElementRoot.AppendChild(xmlElementError);

            XmlElement xmlElementMsg = xmlDoc.CreateElement("msg");
            xmlElementMsg.InnerText = strErrMsg;
            xmlElementRoot.AppendChild(xmlElementMsg);

            return xmlDoc.OuterXml;

        }

        #endregion 返回的错误信息格式

        #region  读取xml，实例化CTIParameterModel

        /// <summary>
        /// 读取xml，实例化CTIParameterModel
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        private CTIParameterModel GetCTIParamModelFormXml(string strXml)
        {
            CTIParameterModel paramCTIModel = null;

            XmlDocument xmlDocument = XmlHelper.CreateDomDocument(strXml);

            XmlNodeList nodeList = xmlDocument.SelectNodes("root");

            nodeList.IsNotNull(n =>
            {
                if (n.Count == 1)
                {
                    XmlNode node = n[0];
                    paramCTIModel = new CTIParameterModel();
                    paramCTIModel.CTICustomerID = node.GetSingleNodeText("customerid").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("customerid");
                    paramCTIModel.CTIPrimaryCallNumber = node.GetSingleNodeText("ani").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("ani");
                    paramCTIModel.AgentID = node.GetSingleNodeText("agentid").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("agentid");
                    paramCTIModel.ParentName = node.GetSingleNodeText("parentname").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("parentname");
                    paramCTIModel.ParentGender = node.GetSingleNodeText("parentsex").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("parentsex");
                    paramCTIModel.StudentName = node.GetSingleNodeText("stuname").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("stuname");
                    paramCTIModel.StudentGender = node.GetSingleNodeText("stusex").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("stusex");
                    paramCTIModel.ParentTel = node.GetSingleNodeText("parenttel").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("parenttel");
                    paramCTIModel.ParentMobile = node.GetSingleNodeText("mobile").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("mobile");
                    paramCTIModel.StudentGrade = node.GetSingleNodeText("stugrade").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("stugrade");
                    paramCTIModel.StudentSchool = node.GetSingleNodeText("stuschool").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("stuschool");
                    paramCTIModel.StudentCity = node.GetSingleNodeText("stucity").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("stucity");
                    paramCTIModel.CityID_PPTS = node.GetSingleNodeText("CityID_PPTS").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("CityID_PPTS");
                    paramCTIModel.CampusName = node.GetSingleNodeText("WPName_PPTS").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("WPName_PPTS");
                    paramCTIModel.CampusID = node.GetSingleNodeText("WPID_PPTS").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("WPID_PPTS");
                    paramCTIModel.BranchOrgID = node.GetSingleNodeText("orgid").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("orgid");
                    paramCTIModel.InfoFrom = node.GetSingleNodeText("InfoFrom").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("InfoFrom");
                    paramCTIModel.StudentIntroduce = node.GetSingleNodeText("StuIntro").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("StuIntro");
                    paramCTIModel.Remark = node.GetSingleNodeText("Memo").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("Memo");
                    paramCTIModel.TalkConts = node.GetSingleNodeText("TalkConts").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("TalkConts");
                    paramCTIModel.LogDate = node.GetSingleNodeText("LogDate").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("LogDate");
                    paramCTIModel.PurchaseIntention = node.GetSingleNodeText("PurchaseIntention").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("PurchaseIntention");
                    paramCTIModel.TalkReason = node.GetSingleNodeText("TalkReason").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("TalkReason");
                    paramCTIModel.ParentInfo = node.GetSingleNodeText("ParentInfo").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("ParentInfo");
                    paramCTIModel.StudentScore = node.GetSingleNodeText("StuScore").IsNullOrEmpty() ? string.Empty : node.GetSingleNodeText("StuScore");
                }
            }
            );
            //跟进记录备注
            paramCTIModel.ParentInfo = (!paramCTIModel.ParentInfo.IsNullOrEmpty()) ? "家长情况：" + paramCTIModel.ParentInfo.Trim().Replace("\r\n", "") + "\r\n" : string.Empty;
            paramCTIModel.StudentScore = (!paramCTIModel.StudentScore.IsNullOrEmpty()) ? "学生成绩：" + paramCTIModel.StudentScore.Trim().Replace("\r\n", "") + "\r\n" : string.Empty;
            paramCTIModel.StudentIntroduce = (!paramCTIModel.StudentIntroduce.IsNullOrEmpty()) ? "学生信息：" + paramCTIModel.StudentIntroduce.Trim().Replace("\r\n", "") + "\r\n" : string.Empty;
            paramCTIModel.Remark = (!paramCTIModel.Remark.IsNullOrEmpty()) ? "备注信息：" + paramCTIModel.Remark.Trim().Replace("\r\n", "") + "\r\n" : string.Empty;
            paramCTIModel.TalkReason = (!paramCTIModel.TalkReason.IsNullOrEmpty()) ? "约访理由：" + paramCTIModel.TalkReason.Trim().Replace("\r\n", "") + "\r\n" : string.Empty;
            paramCTIModel.TalkConts = (!paramCTIModel.TalkConts.IsNullOrEmpty()) ? "回访记录：" + paramCTIModel.TalkConts.Trim().Replace("\r\n", "") + "\r\n" : string.Empty;
            return paramCTIModel;
        }

        #endregion  读取xml，实例化CTIParameterModel

        #region 辅助方法

        /// <summary>
        /// 根据CTI的来源值得到PPTS的来源
        /// [Category]C_Code_Abbr_BO_Customer_Source
        /// </summary>
        /// <param name="strCTISource"></param>
        /// <returns></returns>
        private CustomerSourceModel GetSource(string strCTISource)
        {
            strCTISource = strCTISource.ToLower();
            BaseConstantEntity subSource = null;
            BaseConstantEntity mainSource = null;
            string strERPSourceValue = strCTISource;
            if (!strCTISource.IsNullOrEmpty())
            {
                strERPSourceValue = APPFunc.GetERPSourceValue(strCTISource);
            }

            KeyValuePair<string, IEnumerable<BaseConstantEntity>> constants = dictionaryCategories.Where(d => d.Key == "C_Code_Abbr_BO_Customer_Source").SingleOrDefault();

            //默认选取第一个结果
            subSource = constants.Value.Where(c => c.Value == strERPSourceValue).FirstOrDefault();

            if (null == subSource)
            {
                subSource = new BaseConstantEntity() { Key = "0", ParentKey = "0", Value = "未知" };
            }
            mainSource = constants.Value.Where(c => c.Key == subSource.ParentKey).SingleOrDefault();
            mainSource.IsNull(delegate { mainSource = new BaseConstantEntity() { Key = "0", ParentKey = "0", Value = "未知" }; });

            return new CustomerSourceModel() { MainSource = mainSource, SubSource = subSource };
        }

        /// <summary>
        /// 获取坐席岗位信息
        /// </summary>
        /// <param name="currentJobCollection">当前推送人的岗位信息</param>
        /// <param name="strOrgId"></param>
        /// <returns></returns>
        private JobModel GetSingleUserJobInfo(PPTSJobCollection currentJobCollection)
        {
            JobModel job = new JobModel();

            if (currentJobCollection == null || currentJobCollection.Count == 0)
            {
                return null;
            }

            List<CallingOrgCenterCofigModel> listCallingOrgCenterCofigModel = new List<CallingOrgCenterCofigModel>();
            listCallingOrgCenterCofigModel = APPFunc.GetJobCallingList();

            //筛选岗位,筛选当前推送人的岗位与配置文件中相同岗位名称的岗位信息
            IList<PPTSJob> listJob = new List<PPTSJob>();
            listJob = currentJobCollection.Where(c => listCallingOrgCenterCofigModel.Where(d => d.JobName == c.JobName).ToList().Count > 0).ToList();
            //按照jobName合并CallingOrgCenterCofigModel和PPTSJob
            List<JobModel> jobModels = (from a in listCallingOrgCenterCofigModel join b in listJob on a.JobName equals b.JobName select new JobModel() { CofigModel = a, Job = b }).ToList<JobModel>();
            if (jobModels.Count == 0)
            {
                return null;
            }
            else
            {
                if (jobModels.Count == 1)
                {
                    return jobModels[0];
                }
                else
                { 
                    //优先推送至专员岗位，包括总专员
                    IList<JobModel> lstJobAgent = new List<JobModel>();
                    int index = 0;
                    for (int i = 0;i<jobModels.Count;i++)
                    {
                        if (jobModels[i].CofigModel.SortId == "1" || jobModels[i].CofigModel.SortId == "0")
                        {
                            index = i;
                            lstJobAgent.Add(jobModels[i]);
                            if(jobModels[i].Job.IsPrimary)
                            {
                                break;
                            }
                        }
                    }
                    if (lstJobAgent == null || lstJobAgent.Count == 0)
                    {
                        JobModel jobModel = jobModels.Where(j => j.Job.IsPrimary).FirstOrDefault();
                        jobModel.IsNotNull(j => job = j);
                    }
                    else if (lstJobAgent.Count >= 1)
                    {
                        job = lstJobAgent[index];
                    }
                }
            }
            return job;
        }

        /// <summary>
        /// 根据学员的年级获得学年制
        /// </summary>
        /// <param name="strEntranceGrade"></param>
        /// <returns></returns>
        private string GetSchoolYear(string strEntranceGrade)
        {
            if (gradeTypeMapping.ContainsKey(strEntranceGrade))
            {
                strSchoolYear = gradeTypeMapping[strEntranceGrade];
            }


            return strSchoolYear;
        }

        /// <summary>
        /// 根据来源信息得到接触方式
        /// </summary>
        /// <param name="strInfoFrom"></param>
        /// <returns></returns>
        private string GetContactType(string strInfoFrom)
        {
            string strContactType = string.Empty;

            if (strInfoFrom.IndexOf("呼出") > 0)
            {
                strContactType = NewContactType.CallOut.GetHashCode().ToString();
            }
            else if (strInfoFrom.IndexOf("乐语") > 0)
            {
                strContactType = NewContactType.OnlineLeYu.GetHashCode().ToString();
            }
            else
            {
                strContactType = NewContactType.CallIn.GetHashCode().ToString();
            }
            if (strInfoFrom == "乐语语音" || strInfoFrom == "乐语-语音")
            {
                strContactType = NewContactType.CallIn.GetHashCode().ToString();
            }

            return strContactType;
        }

        /// <summary>
        /// 通过汉字男或女得到性别枚举值
        /// </summary>
        /// <param name="strGenderDescription"></param>
        /// <returns></returns>
        private GenderType GetGender(string strGenderDescription)
        {
            GenderType gender = new GenderType();
            if (strGenderDescription == "男")
            {
                gender = GenderType.Male;
            }
            else if (strGenderDescription == "女")
            {
                gender = GenderType.Female;
            }
            else
            {
                gender = GenderType.Unknown;
            }
            return gender;
        }

        #endregion

        #region 初始化潜客信息集，并调用保存

        /// <summary>
        /// 初始化潜客信息集，并调用保存
        /// </summary>
        /// <param name="param"></param>
        private void InitPotentialCustomer(CTIParameterModel param)
        {
            PotentialCustomerModel model = new PotentialCustomerModel();
            
            //录入潜客信息
            model.PotentialCustomer = new PotentialCustomer() {
                CustomerID = UuidHelper.NewUuidString(),
                CustomerName = param.StudentName,
                OrgID = orgBranch.ID,
                OrgName = orgBranch.Name,
                OrgType = (OrgTypeDefine)orgBranch.PPTSDepartmentType().GetHashCode(),
                Gender = GetGender(param.StudentGender),
                EntranceGrade = param.StudentGrade,
                SchoolYear = strSchoolYear,
                ContactType = GetContactType(param.InfoFrom),
                SourceMainType = customerSourceModel.MainSource.Key,
                SourceSubType = customerSourceModel.SubSource.Key
            };

            //学员与在读学校关系
            School schoolModel = new School();
            SchoolCollection schoolCollection = SchoolAdapter.Instance.Load(builder => { builder.LogicOperator = LogicOperatorDefine.And; builder.AppendItem("SchoolName", param.StudentSchool, "like").AppendItem("OrgID",orgBranch.ID); });
            schoolModel = schoolCollection.FirstOrDefault();
            schoolModel.IsNotNull(school => {
                model.CustomerSchoolRelation = new CustomerSchoolRelation()
                {
                    CustomerID = model.PotentialCustomer.CustomerID,
                    SchoolID = school.SchoolID
                };
            });

            //家长信息
            model.Parent = new Parent()
            {
                ParentID = UuidHelper.NewUuidString(),
                ParentName = param.ParentName,
                Gender = GetGender(param.ParentGender)
            };

            //学员与家长的关系
            model.CustomerParentRelation = new CustomerParentRelation()
            {
                CustomerID = model.PotentialCustomer.CustomerID,
                ParentID = model.Parent.ParentID
            };

            //家长联系方式
            PhoneCollection phoneCollection = new PhoneCollection();
            if (!param.ParentMobile.IsNullOrEmpty())
            {
                phoneCollection.Add(param.ParentMobile.ToPhone(model.Parent.ParentID, true));
            }
            if (!param.ParentTel.IsNullOrEmpty())
            {
                phoneCollection.Add(param.ParentTel.ToPhone(model.Parent.ParentID, false));
            }
            model.PhoneCollection = phoneCollection;

            //学员与员工关系
            model.CustomerStaffRelation = new CustomerStaffRelation()
            {
                CustomerID = model.PotentialCustomer.CustomerID,
                StaffID = user.ID,
                StaffName = user.Name,
                StaffJobID = agentJob.Job.ID,
                StaffJobName = agentJob.Job.Name,
                StaffJobOrgID = orgBranch.ID,
                StaffJobOrgName = orgBranch.Name,
                RelationType = CustomerRelationType.Callcenter
            };
            //跟进记录
            model.CustomerFollow = new CustomerFollow()
            {
                OrgID = orgBranch.ID,
                OrgName = orgBranch.Name,
                OrgType = (OrgTypeDefine)orgBranch.PPTSDepartmentType().GetHashCode(),
                CustomerID = model.PotentialCustomer.CustomerID,
                FollowID = UuidHelper.NewUuidString(),
                FollowerID = user.ID,
                FollowerName = user.Name,
                FollowerJobID = agentJob.Job.ID,
                FollowerJobName = agentJob.Job.Name,
                FollowType = param.InfoFrom == "呼出" ? SaleContactType.Call.GetHashCode().ToString() : SaleContactType.InCommingCall.GetHashCode().ToString(),
                FollowStage = SalesStageType.NotInvited,
                FollowObject = SaleContactTarget.Others.GetHashCode().ToString(),
                PurchaseIntention = (PurchaseIntentionDefine)param.PurchaseIntention.GetHashCode(),
                FollowTime = SNTPClient.AdjustedTime,
                FollowMemo = (param.ParentInfo + param.StudentScore + param.StudentIntroduce + param.Remark + param.TalkReason + param.TalkConts).TrimEnd('\r', '\n'),
                //SourceSystem = param.InfoFrom == "呼出" ? 3 : 4;//没有系统来源属性成员字段，3，4不知道从哪里来的
            };

            model.UpdatePotentialCustomer();
            StringBuilder str = new StringBuilder();
            str.AppendFormat("delete from [CM].[PotentialCustomers] where [CustomerID] = '{0}'\r\n", model.PotentialCustomer.CustomerID);
            str.AppendFormat("delete from [CM].[Parents] where [ParentID] = '{0}'\r\n", model.Parent.ParentID);
            str.AppendFormat("delete from [CM].[CustomerParentRelations] where [CustomerID] = '{0}'\r\n", model.PotentialCustomer.CustomerID);
            str.AppendFormat("delete from [CM].[CustomerSchoolRelations] where [CustomerID] = '{0}'\r\n", model.PotentialCustomer.CustomerID);
            str.AppendFormat("delete from [CM].[CustomerStaffRelations] where [CustomerID] = '{0}'\r\n", model.PotentialCustomer.CustomerID);
            str.AppendFormat("delete from [CM].[Phones] where [OwnerID] = '{0}'\r\n", model.Parent.ParentID);
            str.AppendFormat("delete from [CM].[CustomerFollows] where [FollowID] = '{0}'\r\n", model.CustomerFollow.FollowID);
            strOut = str.ToString();
        }

        #endregion 初始化潜客信息集，并调用保存
    }
}