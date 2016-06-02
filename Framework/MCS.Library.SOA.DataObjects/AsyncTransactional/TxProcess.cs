using MCS.Library.Configuration;
using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.OGUPermission;
using MCS.Library.SOA.DataObjects.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.AsyncTransactional
{
    /// <summary>
    /// 异步交易相关的流程
    /// </summary>
    [Serializable]
    [ORTableMapping("TX.TRANSACTION_PROCESS")]
    [TenantRelativeObject]
    [XElementSerializable]
    public class TxProcess
    {
        private WfServiceOperationDefinitionCollection _CompensationServices;

        private TxActivityCollection _Activities = null;

        private TxProcessContext _Context = null;

        private int _CurrentActivityIndex = -1;

        public TxProcess()
        {
            this.ProcessID = UuidHelper.NewUuidString();
        }

        [ORFieldMapping("PROCESS_ID", PrimaryKey = true)]
        public string ProcessID
        {
            get;
            set;
        }

        [ORFieldMapping("RESOURCE_ID")]
        public string ResourceID
        {
            get;
            set;
        }

        [ORFieldMapping("CURRENT_ACTIVITY_INDEX")]
        public int CurrentActivityIndex
        {
            get
            {
                return this._CurrentActivityIndex;
            }
            set
            {
                this._CurrentActivityIndex = value;
            }
        }

        [ORFieldMapping("PROCESS_NAME")]
        public string ProcessName
        {
            get;
            set;
        }

        [ORFieldMapping("CATEGORY")]
        public string Category
        {
            get;
            set;
        }

        /// <summary>
        /// 创建此事务的链接名称
        /// </summary>
        [ORFieldMapping("CONNECTION_NAME")]
        public string ConnectionName
        {
            get;
            set;
        }

        [ORFieldMapping("STATUS")]
        [SqlBehavior(EnumUsageTypes.UseEnumString)]
        public TxProcessStatus Status
        {
            get;
            set;
        }

        [ORFieldMapping("STATUS_TEXT")]
        public string StatusText
        {
            get;
            set;
        }

        [ORFieldMapping("START_TIME", UtcTimeToLocal = true)]
        public DateTime StartTime
        {
            get;
            set;
        }

        [ORFieldMapping("END_TIME", UtcTimeToLocal = true)]
        public DateTime EndTime
        {
            get;
            set;
        }

        [ORFieldMapping("CREATE_TIME", UtcTimeToLocal = true)]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()")]
        public DateTime CreateTime
        {
            get;
            set;
        }

        private IUser _Creator = null;

        [SubClassORFieldMapping("ID", "CREATOR_ID")]
        [SubClassORFieldMapping("DisplayName", "CREATOR_NAME")]
        [SubClassType(typeof(OguUser))]
        public virtual IUser Creator
        {
            get
            {
                return this._Creator;
            }
            set
            {
                this._Creator = (IUser)OguBase.CreateWrapperObject(value);
            }
        }

        [NoMapping]
        public WfServiceOperationDefinitionCollection CompensationServices
        {
            get
            {
                if (this._CompensationServices == null)
                    this._CompensationServices = new WfServiceOperationDefinitionCollection();

                return this._CompensationServices;
            }
        }

        [NoMapping]
        public TxActivityCollection Activities
        {
            get
            {
                if (this._Activities == null)
                    this._Activities = new TxActivityCollection();

                return this._Activities;
            }
        }

        [NoMapping]
        public TxProcessContext Context
        {
            get
            {
                if (this._Context == null)
                    this._Context = new TxProcessContext();

                return this._Context;
            }
        }

        [NoMapping]
        public TxActivity CurrentActivity
        {
            get
            {
                TxActivity result = null;

                if (this.CurrentActivityIndex >= 0 && this.CurrentActivityIndex < this.Activities.Count)
                    result = this.Activities[this.CurrentActivityIndex];

                return result;
            }
        }

        [NoMapping]
        public TxActivity PreviousActivity
        {
            get
            {
                TxActivity result = null;

                if (this.CurrentActivityIndex > 0 && this.CurrentActivityIndex <= this.Activities.Count)
                    result = this.Activities[this.CurrentActivityIndex - 1];

                return result;
            }
        }

        [NoMapping]
        public TxActivity NextActivity
        {
            get
            {
                TxActivity result = null;

                if (this.CurrentActivityIndex >= -1 && this.CurrentActivityIndex < this.Activities.Count - 1)
                    result = this.Activities[this.CurrentActivityIndex + 1];

                return result;
            }
        }

        /// <summary>
        /// 流转到下一环节
        /// </summary>
        /// <returns></returns>
        public TxActivity MoveToNextActivity()
        {
            TxActivity originalActivity = this.CurrentActivity;
            TxActivity nextActivity = null;

            if (this.CurrentActivityIndex < this.Activities.Count)
            {
                this.CurrentActivityIndex++;
                nextActivity = this.CurrentActivity;
            }

            originalActivity.IsNotNull(a => a.Status = TxActivityStatus.Completed);
            nextActivity.IsNotNull(a =>
            {
                a.Status = TxActivityStatus.Running;
                this.Status = TxProcessStatus.Running;
            }).IsNull(() => this.Status = TxProcessStatus.Completed);

            return nextActivity;
        }

        public TxActivity RollbackToPreviousActivity()
        {
            TxActivity originalActivity = this.CurrentActivity;
            TxActivity previousActivity = null;

            if (this.CurrentActivityIndex >= 0)
            {
                this.CurrentActivityIndex--;
                previousActivity = this.CurrentActivity;
            }

            originalActivity.IsNotNull(a => a.Status = TxActivityStatus.RolledBack);
            previousActivity.IsNotNull(a =>
            {
                a.Status = TxActivityStatus.RollingBack;
                this.Status = TxProcessStatus.RollingBack;
            }).IsNull(() => this.Status = TxProcessStatus.RolledBack);

            return previousActivity;
        }

        public WfServiceOperationDefinition AddCompensationService(WfServiceAddressDefinition address, string operationName)
        {
            address.NullCheck("address");
            operationName.CheckStringIsNullOrEmpty("operationName");

            return this.CompensationServices.Append(new WfServiceOperationDefinition(address, operationName));
        }

        public WfServiceOperationDefinition AddCompensationService(string url, string operationName)
        {
            url.CheckStringIsNullOrEmpty("url");
            operationName.CheckStringIsNullOrEmpty("operationName");

            return this.CompensationServices.Append(new WfServiceOperationDefinition(operationName, string.Empty).
                SetAddress(WfServiceRequestMethod.Post, url, WfServiceContentType.Json));
        }

        public WfServiceOperationDefinition AddCompensationService<TChannel>(string url, Action<TChannel> action)
        {
            url.CheckStringIsNullOrEmpty("url");

            return this.CompensationServices.Append(WfServiceOperationDefinition.CreateFromAction(action).
                SetAddress(WfServiceRequestMethod.Post, url, WfServiceContentType.Json));
        }
        #region Task Generator
        /// <summary>
        /// 生成启动流程的Task
        /// </summary>
        /// <returns></returns>
        public InvokeServiceTask ToStartWorkflowTask()
        {
            InvokeServiceTask task = new InvokeServiceTask();

            task.TaskID = UuidHelper.NewUuidString();
            task.ResourceID = this.ProcessID;
            task.TaskTitle = string.Format("启动\"{0}\"", this.ProcessName);

            task.SvcOperationDefs.Add(new WfServiceOperationDefinition("StartProcess", string.Empty).
                SetAddress(WfServiceRequestMethod.Post, UriSettings.GetConfig().CheckAndGet("wfPlatformService", "txProcessService").ToString(), WfServiceContentType.Json).
                AddParameter("srcConnectionName", this.ConnectionName).AddParameter("processID", this.ProcessID));

            task.FillData();

            return task;
        }

        /// <summary>
        /// 生成同步并且执行活动的Task
        /// </summary>
        /// <returns></returns>
        public InvokeServiceTask ToSyncAndExecuteActivityTask()
        {
            this.PreviousActivity.NullCheck("流程当前活动为空。流程尚未启动或者已经完成");

            InvokeServiceTask task = new InvokeServiceTask();

            task.TaskID = UuidHelper.NewUuidString();
            task.ResourceID = this.ProcessID;
            task.TaskTitle = string.Format("流转\"{0}\"中的活动\"{1}\"", this.ProcessName, this.PreviousActivity.ActivityName);

            task.SvcOperationDefs.Add(new WfServiceOperationDefinition("SyncAndExecuteActivity", string.Empty).
                SetAddress(WfServiceRequestMethod.Post, UriSettings.GetConfig().CheckAndGet("wfPlatformService", "txProcessService").ToString(), WfServiceContentType.Json).
                AddParameter("srcConnectionName", this.ConnectionName).AddParameter("processID", this.ProcessID));

            task.FillData();

            return task;
        }

        /// <summary>
        /// 生成同步并且执行活动的Task
        /// </summary>
        /// <returns></returns>
        public InvokeServiceTask ToSyncAndRollbackActivityTask()
        {
            InvokeServiceTask task = new InvokeServiceTask();

            task.TaskID = UuidHelper.NewUuidString();
            task.ResourceID = this.ProcessID;

            if (this.CurrentActivity != null)
                task.TaskTitle = string.Format("回滚\"{0}\"中的活动\"{1}\"", this.ProcessName, this.CurrentActivity.ActivityName);
            else
                task.TaskTitle = string.Format("回滚\"{0}\"完成", this.ProcessName);

            task.SvcOperationDefs.Add(new WfServiceOperationDefinition("SyncAndRollbackActivity", string.Empty).
                SetAddress(WfServiceRequestMethod.Post, UriSettings.GetConfig().CheckAndGet("wfPlatformService", "txProcessService").ToString(), WfServiceContentType.Json).
                AddParameter("srcConnectionName", this.ConnectionName).AddParameter("processID", this.ProcessID));

            task.FillData();

            return task;
        }

        /// <summary>
        /// 生成执行当前活动的Task
        /// </summary>
        /// <returns></returns>
        public InvokeServiceTask ToExecuteCurrentActivityTask()
        {
            this.CurrentActivity.NullCheck("流程当前活动为空。流程尚未启动或者已经完成");

            TxActivityActionServiceTask task = null;

            if (this.CurrentActivity.ActionServices.Count > 0)
            {
                task = new TxActivityActionServiceTask();

                task.TaskID = UuidHelper.NewUuidString();
                task.ResourceID = this.ProcessID;
                task.TaskTitle = string.Format("执行\"{0}\"中的活动\"{1}\"", this.ProcessName, this.CurrentActivity.ActivityName);

                task.SvcOperationDefs.CopyFrom(this.CurrentActivity.ActionServices);

                task.FillData();
            }

            return task;
        }

        /// <summary>
        /// 生成执行当前活动回滚操作的Task
        /// </summary>
        /// <returns></returns>
        public InvokeServiceTask ToExecuteRollbackCurrentActivityTask()
        {
            this.CurrentActivity.NullCheck("流程当前活动为空。流程尚未启动或者已经完成");

            InvokeServiceTask task = null;

            if (this.CurrentActivity.CompensationServices.Count > 0)
            {
                task = new InvokeServiceTask();

                task.TaskID = UuidHelper.NewUuidString();
                task.ResourceID = this.ProcessID;
                task.TaskTitle = string.Format("执行回滚\"{0}\"中的活动\"{1}\"", this.ProcessName, this.CurrentActivity.ActivityName);

                task.SvcOperationDefs.CopyFrom(this.CurrentActivity.CompensationServices);

                task.FillData();
            }

            return task;
        }

        public InvokeServiceTask ToExecuteRollbackProcessTask()
        {
            InvokeServiceTask task = null;

            if (this.CompensationServices.Count > 0)
            {
                task = new InvokeServiceTask();

                task.TaskID = UuidHelper.NewUuidString();
                task.ResourceID = this.ProcessID;
                task.TaskTitle = string.Format("执行流程回滚\"{0}\"", this.ProcessName);

                task.SvcOperationDefs.CopyFrom(this.CompensationServices);

                task.FillData();
            }

            return task;
        }
        #endregion Task Generator
    }

    [Serializable]
    [XElementSerializable]
    public class TxProcessCollection : SerializableEditableKeyedDataObjectCollectionBase<string, TxProcess>
    {
        public TxProcessCollection()
        {
        }

        public TxProcessCollection(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        public void AddOrReplace(SysTaskProcess process)
        {
            process.NullCheck("process");

            if (this.ContainsKey(process.ID) == false)
                this.Add(process);
        }

        protected override string GetKeyForItem(TxProcess item)
        {
            return item.ProcessID;
        }
    }
}
