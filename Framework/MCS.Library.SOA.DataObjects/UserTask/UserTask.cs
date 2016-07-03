#region ���߰汾
// -------------------------------------------------
// Assembly	��	HB.DataObjects
// FileName	��	UserTask.cs
// Remark	��	
// -------------------------------------------------
// VERSION  	AUTHOR		DATE			CONTENT
// 1.0		    ����	    20070723		����
// -------------------------------------------------
#endregion

using MCS.Library;
using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;

namespace MCS.Library.SOA.DataObjects
{
    /// <summary>
    /// ������ʵ����
    /// </summary>
    [Serializable]
    [XmlRootMapping("UserTask", false)]
    [ORTableMapping("WF.USER_TASK")]
    [DataContract]
    public class UserTask : IUserTask
    {
        private string taskID = string.Empty;
        private string appID = string.Empty;
        private string progID = string.Empty;

        private string taskTitle = string.Empty;
        private string resourceID = string.Empty;
        private string sourceID = string.Empty;
        private string sourceName = string.Empty;
        private string sendToUserID = string.Empty;

        private string body = string.Empty;

        private string processID = string.Empty;
        private string activityID = string.Empty;
        private TaskLevel level = TaskLevel.Normal;
        private string url = string.Empty;
        private FileEmergency emergency = FileEmergency.None;
        private string purpose = string.Empty;
        private TaskStatus status = TaskStatus.Ban;
        private DateTime taskStartTime = DateTime.MinValue;
        private DateTime expireTime = DateTime.MinValue;
        private DateTime readTime = DateTime.MinValue;

        private TaskCategory category = null;
        private int topFlag = 0;
        private DateTime completedTime = DateTime.MinValue;
        private DateTime deliverTime = DateTime.MinValue;
        private string draftDepartmentName = string.Empty;
        private string draftUserID = string.Empty;
        private string draftUserName = string.Empty;

        private Dictionary<string, object> context = null;

        #region
        /// <summary>
        /// ��ϢID
        /// </summary>
        [ORFieldMapping("TASK_GUID")]
        [DataMember]
        public string TaskID
        {
            get
            {
                return taskID;
            }
            set
            {
                taskID = value;
            }
        }

        /// <summary>
        /// Ӧ������
        /// </summary>
        [ORFieldMapping("APPLICATION_NAME")]
        [DataMember]
        public string ApplicationName
        {
            get
            {
                return this.appID;
            }
            set
            {
                this.appID = value;
            }
        }

        /// <summary>
        /// ģ������
        /// </summary>
        [ORFieldMapping("PROGRAM_NAME")]
        [DataMember]
        public string ProgramName
        {
            get
            {
                return this.progID;
            }
            set
            {
                this.progID = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        [ORFieldMapping("TASK_TITLE")]
        [DataMember]
        public string TaskTitle
        {
            get
            {
                return this.taskTitle;
            }
            set
            {
                this.taskTitle = value;
            }
        }

        /// <summary>
        /// ��ԴID
        /// </summary>
        [ORFieldMapping("RESOURCE_ID")]
        [DataMember]
        public string ResourceID
        {
            get
            {
                return this.resourceID;
            }
            set
            {
                this.resourceID = value;
            }
        }

        /// <summary>
        /// ������ID
        /// </summary>
        [ORFieldMapping("SOURCE_ID")]
        [DataMember]
        public string SourceID
        {
            get
            {
                return this.sourceID;
            }
            set
            {
                this.sourceID = value;
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        [ORFieldMapping("SOURCE_NAME")]
        [DataMember]
        public string SourceName
        {
            get
            {
                return this.sourceName;
            }
            set
            {
                this.sourceName = value;
            }
        }

        [ORFieldMapping("SEND_TO_USER")]
        [DataMember]
        public string SendToUserID
        {
            get
            {
                return this.sendToUserID;
            }
            set
            {
                this.sendToUserID = value;
            }
        }

        [ORFieldMapping("SEND_TO_USER_NAME")]
        [DataMember]
        public string SendToUserName
        {
            get;
            set;
        }

        [ORFieldMapping("DATA")]
        [DataMember]
        public string Body
        {
            get
            {
                return this.body;
            }
            set
            {
                this.body = value;
            }
        }

        /// <summary>
        /// ��Ϣ����
        /// </summary>
        [ORFieldMapping("TASK_LEVEL")]
        [DataMember]
        public TaskLevel Level
        {
            get
            {
                return this.level;
            }
            set
            {
                this.level = value;
            }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        [ORFieldMapping("PROCESS_ID")]
        [DataMember]
        public string ProcessID
        {
            get
            {
                return this.processID;
            }
            set
            {
                this.processID = value;
            }
        }

        /// <summary>
        /// �������ڵ�ID
        /// </summary>
        [ORFieldMapping("ACTIVITY_ID")]
        [DataMember]
        public string ActivityID
        {
            get
            {
                return this.activityID;
            }
            set
            {
                this.activityID = value;
            }
        }

        /// <summary>
        /// �������������
        /// </summary>
        [ORFieldMapping("URL")]
        [DataMember]
        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }

        /// <summary>
        /// �õ�����·����Url
        /// </summary>
        [NoMapping]
        [NoXmlObjectMappingAttribute]
        [DataMember]
        public string NormalizedUrl
        {
            get
            {
                return GetNormalizedUrl(this.ApplicationName, this.ProgramName, this.url);
            }
        }

        /// <summary>
        /// �������·����Url�����url���Ǿ���·������ô�ȸ��������ļ�resourceUriSettings��userTaskRoot�����á�
        /// �����·�����ǿգ���ʹ��WfGlobalParameters��FormBaseUrl
        /// </summary>
        public static string GetNormalizedUrl(string url)
        {
            return GetNormalizedUrl(string.Empty, string.Empty, url);
        }

        /// <summary>
        /// �������·����Url�����url���Ǿ���·������ô�ȸ��������ļ�resourceUriSettings��userTaskRoot�����á�
        /// �����·�����ǿգ���ʹ��WfGlobalParameters��FormBaseUrl 
        /// </summary>
        /// <param name="appCodeName"></param>
        /// <param name="progCodeName"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetNormalizedUrl(string appCodeName, string progCodeName, string url)
        {
            string result = url;

            Uri uri = new Uri(url, UriKind.RelativeOrAbsolute);

            if (uri.IsAbsoluteUri == false)
            {
                ResourceUriSettings settings = ResourceUriSettings.GetConfig();

                string rootPath = string.Empty;

                if (settings.Paths.ContainsKey("userTaskRoot"))
                {
                    rootPath = settings.Paths["userTaskRoot"].Uri.OriginalString;
                }
                else
                {
                    if (appCodeName.IsNotEmpty() && progCodeName.IsNotEmpty())
                        rootPath = WfGlobalParameters.GetValueRecursively(appCodeName, progCodeName, "FormBaseUrl", string.Empty);
                    else
                        rootPath = WfGlobalParameters.Default.Properties.GetValue("FormBaseUrl", string.Empty);
                }

                if (rootPath.IsNotEmpty())
                    result = UriHelper.CombinePath(rootPath, url);
            }

            return result;
        }

        /// <summary>
        /// �����̶�
        /// </summary>
        [ORFieldMapping("EMERGENCY")]
        [DataMember]
        public FileEmergency Emergency
        {
            get
            {
                return this.emergency;
            }
            set
            {
                this.emergency = value;
            }
        }

        /// <summary>
        /// �ڵ����ƣ���������
        /// </summary>
        [ORFieldMapping("PURPOSE")]
        [DataMember]
        public string Purpose
        {
            get
            {
                return this.purpose;
            }
            set
            {
                this.purpose = value;
            }
        }

        /// <summary>
        /// ��������״̬
        /// </summary>
        [ORFieldMapping("STATUS")]
        [DataMember]
        public TaskStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        /// <summary>
        /// ����ʼʱ��
        /// </summary>
        /// <remarks>һ��Ϊ�ļ������ʱ�䣬�Ե�ǰʱ��ΪĬ��ֵ�����ʵ������Ƿ��׵�</remarks>
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All, DefaultExpression = "GETUTCDATE()")]
        [ORFieldMapping("TASK_START_TIME", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime TaskStartTime
        {
            get
            {
                return this.taskStartTime;
            }
            set
            {
                this.taskStartTime = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All)]
        [ORFieldMapping("EXPIRE_TIME", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime ExpireTime
        {
            get
            {
                return this.expireTime;
            }
            set
            {
                this.expireTime = value;
            }
        }

        /// <summary>
        /// ��Ϣ����ʱ��
        /// </summary>
        [ORFieldMapping("DELIVER_TIME", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All, DefaultExpression = "GETUTCDATE()")]
        [DataMember]
        public DateTime DeliverTime
        {
            get
            {
                return this.deliverTime;
            }
            set
            {
                this.deliverTime = value;
            }
        }

        [ORFieldMapping("READ_TIME", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All)]
        [DataMember]
        public DateTime ReadTime
        {
            get
            {
                return this.readTime;
            }
            set
            {
                this.readTime = value;
            }
        }

        /// <summary>
        /// �û�����
        /// </summary>
        [SubClassORFieldMapping("CategoryID", "CATEGORY_GUID")]
        [SubClassType(typeof(TaskCategory))]
        public TaskCategory Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
            }
        }

        /// <summary>
        /// ��Ϣ�ö���־
        /// </summary>
        [ORFieldMapping("TOP_FLAG")]
        [DataMember]
        public int TopFlag
        {
            get
            {
                return this.topFlag;
            }
            set
            {
                this.topFlag = value;
            }
        }

        /// <summary>
        /// ��ݲ��ŵ�ʱ��
        /// </summary>
        [ORFieldMapping("DRAFT_DEPARTMENT_NAME")]
        [DataMember]
        public string DraftDepartmentName
        {
            get
            {
                return this.draftDepartmentName;
            }
            set
            {
                this.draftDepartmentName = value;
            }
        }

        /// <summary>
        /// �Ѱ�����İ���ʱ��
        /// </summary>
        [ORFieldMapping("COMPLETED_TIME", UtcTimeToLocal = true)]
        [SqlBehavior(ClauseBindingFlags.Select)]
        [DataMember]
        public DateTime CompletedTime
        {
            get
            {
                return this.completedTime;
            }
            set
            {
                this.completedTime = value;
            }
        }

        [ORFieldMapping("DRAFT_USER_ID")]
        [DataMember]
        public string DraftUserID
        {
            get
            {
                return this.draftUserID;
            }
            set
            {
                this.draftUserID = value;
            }
        }

        [ORFieldMapping("DRAFT_USER_NAME")]
        [DataMember]
        public string DraftUserName
        {
            get
            {
                return this.draftUserName;
            }
            set
            {
                this.draftUserName = value;
            }
        }

        [NoMapping]
        [ScriptIgnore]
        [NoXmlObjectMappingAttribute]
        public Dictionary<string, object> Context
        {
            get
            {
                if (this.context == null)
                    this.context = new Dictionary<string, object>();

                return this.context;
            }
        }

        public UserTask Clone()
        {
            UserTask newUserTask = new UserTask();
            TaskCategory taskCategory = null;

            if (this.category != null)
            {
                taskCategory = new TaskCategory();
                taskCategory.CategoryID = this.category.CategoryID;
                taskCategory.CategoryName = this.category.CategoryName;
                taskCategory.InnerSortID = this.category.InnerSortID;
                taskCategory.UserID = this.category.UserID;
            }

            newUserTask.ActivityID = this.activityID;
            newUserTask.ApplicationName = this.appID;
            newUserTask.Body = this.body;
            newUserTask.Category = taskCategory;
            newUserTask.Emergency = this.emergency;
            newUserTask.ExpireTime = this.expireTime;
            newUserTask.Level = this.level;
            newUserTask.ProcessID = this.processID;
            newUserTask.ProgramName = this.progID;
            newUserTask.Purpose = this.purpose;
            newUserTask.ReadTime = this.readTime;
            newUserTask.ResourceID = this.resourceID;
            newUserTask.SendToUserID = this.sendToUserID;
            newUserTask.SendToUserName = this.SendToUserName;
            newUserTask.SourceID = this.sourceID;
            newUserTask.SourceName = this.sourceName;
            newUserTask.Status = this.status;
            newUserTask.TaskID = this.taskID;
            newUserTask.TaskStartTime = this.taskStartTime;
            newUserTask.TaskTitle = this.taskTitle;
            newUserTask.TopFlag = this.topFlag;
            newUserTask.Url = this.url;
            newUserTask.DeliverTime = this.deliverTime;
            newUserTask.DraftDepartmentName = this.draftDepartmentName;
            newUserTask.CompletedTime = this.completedTime;
            newUserTask.DraftUserID = this.draftUserID;
            newUserTask.DraftUserName = this.draftUserName;

            return newUserTask;
        }

        #endregion
    }
}
