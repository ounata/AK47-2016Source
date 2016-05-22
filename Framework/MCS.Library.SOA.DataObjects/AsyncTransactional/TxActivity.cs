using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
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
    /// 交易活动
    /// </summary>
    [Serializable]
    [XElementSerializable]
    public class TxActivity
    {
        private WfServiceOperationDefinitionCollection _ActionService;
        private WfServiceOperationDefinitionCollection _CompensationServices;

        private TxActivityContext _Context = null;

        public string ActivityID
        {
            get;
            set;
        }

        public string ActivityName
        {
            get;
            set;
        }

        public TxActivityStatus Status
        {
            get;
            set;
        }

        public string ConnectionName
        {
            get;
            set;
        }

        public string StatusText
        {
            get;
            set;
        }

        [NoMapping]
        public WfServiceOperationDefinitionCollection ActionServices
        {
            get
            {
                if (this._ActionService == null)
                    this._ActionService = new WfServiceOperationDefinitionCollection();

                return this._ActionService;
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

        public WfServiceOperationDefinition AddActionService(string url, string operationName)
        {
            url.CheckStringIsNullOrEmpty("url");
            operationName.CheckStringIsNullOrEmpty("operationName");

            return this.ActionServices.Append(new WfServiceOperationDefinition(operationName, string.Empty).
                SetAddress(WfServiceRequestMethod.Post, url, WfServiceContentType.Json));
        }

        public WfServiceOperationDefinition AddActionService<TChannel>(string url, Action<TChannel> action)
        {
            url.CheckStringIsNullOrEmpty("url");

            return this.ActionServices.Append(WfServiceOperationDefinition.CreateFromAction(action).
                SetAddress(WfServiceRequestMethod.Post, url, WfServiceContentType.Json));
        }

        public WfServiceOperationDefinition AddActionService(WfServiceAddressDefinition address, string operationName)
        {
            address.NullCheck("address");
            operationName.CheckStringIsNullOrEmpty("operationName");

            return this.ActionServices.Append(new WfServiceOperationDefinition(address, operationName));
        }

        public WfServiceOperationDefinition AddCompensationService(string url, string operationName)
        {
            url.CheckStringIsNullOrEmpty("url");
            operationName.CheckStringIsNullOrEmpty("operationName");

            return this.CompensationServices.Append(new WfServiceOperationDefinition(operationName, string.Empty).
                SetAddress(WfServiceRequestMethod.Post, url, WfServiceContentType.Json));
        }

        public WfServiceOperationDefinition AddCompensationService(WfServiceAddressDefinition address, string operationName)
        {
            address.NullCheck("address");
            operationName.CheckStringIsNullOrEmpty("operationName");

            return this.CompensationServices.Append(new WfServiceOperationDefinition(address, operationName));
        }

        public WfServiceOperationDefinition AddCompensationService<TChannel>(string url, Action<TChannel> action)
        {
            url.CheckStringIsNullOrEmpty("url");

            return this.CompensationServices.Append(WfServiceOperationDefinition.CreateFromAction(action).
                SetAddress(WfServiceRequestMethod.Post, url, WfServiceContentType.Json));
        }

        [NoMapping]
        public TxActivityContext Context
        {
            get
            {
                if (this._Context == null)
                    this._Context = new TxActivityContext();

                return this._Context;
            }
        }
    }

    [Serializable]
    [XElementSerializable]
    public class TxActivityCollection : SerializableEditableKeyedDataObjectCollectionBase<string, TxActivity>
    {
        public TxActivityCollection()
        {
        }

        public TxActivityCollection(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        public TxActivity AddActivity(string name, string connectionName = "")
        {
            TxActivity activity = new TxActivity()
            {
                ActivityID = UuidHelper.NewUuidString(),
                ActivityName = name,
                ConnectionName = connectionName
            };

            this.Add(activity);

            return activity;
        }

        protected override string GetKeyForItem(TxActivity item)
        {
            return item.ActivityID;
        }

        protected override void OnInsert(int index, object value)
        {
            TxActivity activity = (TxActivity)value;

            if (activity.ActivityID.IsNullOrEmpty())
                activity.ActivityID = UuidHelper.NewUuidString();

            base.OnInsert(index, activity);
        }

        protected override void OnSet(int index, object oldValue, object newValue)
        {
            TxActivity activity = (TxActivity)newValue;

            if (activity.ActivityID.IsNullOrEmpty())
                activity.ActivityID = UuidHelper.NewUuidString();

            base.OnSet(index, oldValue, activity);
        }
    }
}
