﻿using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using MCS.Web.Library.Script.Mechanism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MCS.Library.SOA.DataObjects
{
    /// <summary>
    /// 固定时间执行的任务
    /// </summary>
    [Serializable]
    [ORTableMapping("WF.FIXED_TIME_TASK")]
    [TenantRelativeObject]
    public class FixedTimeTask
    {
        private IUser _Source = null;
        private Dictionary<string, object> context = null;

        public FixedTimeTask()
        {
        }

        public FixedTimeTask(FixedTimeTask other)
        {
            this.Category = other.Category;
            this.context = other.context;
            this.CreateTime = other.CreateTime;
            this.Data = other.Data;
            this.ResourceID = other.ResourceID;
            this.SortID = other.SortID;
            this.Source = other.Source;
            this.StartTime = other.StartTime;
            this.TaskID = other.TaskID;
            this.TaskTitle = other.TaskTitle;
            this.TaskType = other.TaskType;
            this.Url = other.Url;
        }

        /// <summary>
        /// 任务ID
        /// </summary>
        [ORFieldMapping("TASK_GUID", PrimaryKey = true)]
        public virtual string TaskID
        {
            get;
            set;
        }

        [ORFieldMapping("SORT_ID", IsIdentity = true)]
        public virtual int SortID
        {
            get;
            set;
        }

        /// <summary>
        /// 类别
        /// </summary>
        [ORFieldMapping("CATEGORY")]
        public virtual string Category
        {
            get;
            set;
        }

        /// <summary>
        /// 任务类型
        /// </summary>
        [ORFieldMapping("TASK_TYPE")]
        public virtual string TaskType
        {
            get;
            set;
        }

        /// <summary>
        /// 标题
        /// </summary>
        [ORFieldMapping("TASK_TITLE")]
        public virtual string TaskTitle
        {
            get;
            set;
        }

        /// <summary>
        /// 关联ID，例如JOB_ID
        /// </summary>
        [ORFieldMapping("RESOURCE_ID")]
        public virtual string ResourceID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建者
        /// </summary>
        [SubClassORFieldMapping("ID", "SOURCE_ID")]
        [SubClassORFieldMapping("DisplayName", "SOURCE_NAME")]
        [SubClassType(typeof(OguUser))]
        public virtual IUser Source
        {
            get
            {
                return this._Source;
            }
            set
            {
                this._Source = (IUser)OguBase.CreateWrapperObject(value);
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CREATE_TIME", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All, DefaultExpression = "GETUTCDATE()")]
        public virtual DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        [ORFieldMapping("START_TIME", UtcTimeToLocal = true)]
        public virtual DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 相关地址
        /// </summary>
        [ORFieldMapping("URL")]
        public virtual string Url
        {
            get;
            set;
        }

        /// <summary>
        /// 相关数据
        /// </summary>
        [ORFieldMapping("DATA")]
        public virtual string Data
        {
            get;
            set;
        }

        /// <summary>
        /// 上下文信息，这个属性是不需要入库和JSON序列化的
        /// </summary>
        [NoMapping]
        [ScriptIgnore]
        public Dictionary<string, object> Context
        {
            get
            {
                if (this.context == null)
                    this.context = new Dictionary<string, object>();

                return this.context;
            }
        }

        /// <summary>
        /// 向xml类型的Data属性填充数据。同时填充环境信息
        /// </summary>
        /// <param name="extraData"></param>
        public virtual void FillData(Dictionary<string, string> extraData)
        {
            XmlDocument xml = XmlHelper.CreateDomDocument("<Data/>");

            if (extraData != null)
            {
                foreach (KeyValuePair<string, string> kp in extraData)
                    XmlHelper.AppendNode(xml.DocumentElement, kp.Key, kp.Value);
            }

            XmlHelper.AppendNode(xml.DocumentElement, "Environment", EnvironmentHelper.GetEnvironmentInfo());

            this.Data = xml.OuterXml;
        }

        /// <summary>
        /// 加载完成后调用此方法
        /// </summary>
        public virtual void AfterLoad()
        {
        }

        public SysTask ToSysTask()
        {
            SysTask task = new SysTask();

            task.TaskID = this.TaskID;
            task.TaskTitle = this.TaskTitle;
            task.TaskType = this.TaskType;
            task.Source = this.Source;
            task.ResourceID = this.ResourceID;
            task.SortID = this.SortID;
            task.Category = this.Category;
            task.Url = this.Url;
            task.Data = this.Data;

            return task;
        }
    }

    /// <summary>
	/// 系统任务集合
	/// </summary>
	[Serializable]
    public class FixedTimeTaskCollection : EditableDataObjectCollectionBase<FixedTimeTask>
    {
    }
}