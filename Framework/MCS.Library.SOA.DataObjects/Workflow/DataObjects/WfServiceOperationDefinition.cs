using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Web.Library.Script;
using MCS.Library.WcfExtensions.Inspectors;

namespace MCS.Library.SOA.DataObjects.Workflow
{
    [Serializable]
    [XElementSerializable]
    public class WfServiceOperationDefinition : ISimpleXmlSerializer
    {
        private WfServiceAddressDefinition _AddressDef = null;
        private WfServiceOperationParameterCollection _Params;
        private TimeSpan _Timeout = TimeSpan.FromSeconds(30);

        public static WfServiceOperationDefinition CreateFromAction<TChannel>(WfServiceAddressDefinition addressDef, Action<TChannel> action, string xmlStoreParaName = "")
        {
            WfServiceOperationDefinition opDefine = new WfServiceOperationDefinition();

            opDefine.RtnXmlStoreParamName = xmlStoreParaName;
            opDefine.SetAddress(addressDef);
            FillFromChannel(opDefine, action);

            return opDefine;
        }

        public static WfServiceOperationDefinition CreateFromAction<TChannel>(Action<TChannel> action, string xmlStoreParaName = "")
        {
            WfServiceOperationDefinition opDefine = new WfServiceOperationDefinition();

            opDefine.RtnXmlStoreParamName = xmlStoreParaName;
            FillFromChannel(opDefine, action);

            return opDefine;
        }

        private static void FillFromChannel<TChannel>(WfServiceOperationDefinition opDefine, Action<TChannel> action)
        {
            if (action != null)
            {
                WfClientOperationInfo opInfo = WfClientInspector.InspectParameters<TChannel>(action);

                opDefine.OperationName = opInfo.Name;
                
                foreach(WfClientParameter parameter in opInfo.Parameters)
                    opDefine.AddParameter(parameter.Name, parameter.Value);
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public WfServiceOperationDefinition()
        {
        }

        public WfServiceOperationDefinition(string operationName, string xmlStoreParaName)
        {
            this.OperationName = operationName;
            this.RtnXmlStoreParamName = xmlStoreParaName;
        }

        public WfServiceOperationDefinition(string addressKey,
            string operationName,
            WfServiceOperationParameterCollection parameters,
            string xmlStoreParaName)
        {
            if (WfGlobalParameters.Default.ServiceAddressDefs[addressKey] == null)
                throw new ArgumentException(string.Format("无法找到Key为{0}的服务地址定义", addressKey));

            this._AddressDef = WfGlobalParameters.Default.ServiceAddressDefs[addressKey];
            this.OperationName = operationName;
            this.Params = parameters;
            this.RtnXmlStoreParamName = xmlStoreParaName;
        }

        public WfServiceOperationDefinition(WfServiceAddressDefinition address,
            string operationName)
        {
            this._AddressDef = address;
            this.OperationName = operationName;
        }

        public WfServiceOperationDefinition(WfServiceAddressDefinition address,
            string operationName, WfServiceOperationParameterCollection parameters,
            string xmlStoreParaName)
        {
            this._AddressDef = address;
            this.OperationName = operationName;
            this.Params = parameters;
            this.RtnXmlStoreParamName = xmlStoreParaName;
        }

        /// <summary>
        /// 从配置信息项初始化
        /// </summary>
        /// <param name="element"></param>
        public WfServiceOperationDefinition(WfServiceOperationDefinitionConfigurationElement element)
        {
            this.FromConfigurationElement(element);
        }

        /// <summary>
        /// 从配置信息项初始化
        /// </summary>
        /// <param name="element"></param>
        public void FromConfigurationElement(WfServiceOperationDefinitionConfigurationElement element)
        {
            element.NullCheck("element");

            this._AddressDef = new WfServiceAddressDefinition(element.AddressKey);

            this.OperationName = element.OperationName;
            this.Params = new WfServiceOperationParameterCollection(element.Parameters);
            this.RtnXmlStoreParamName = element.ReturnParamName;
            this.Timeout = element.Timeout;
            this.InvokeWhenPersist = element.InvokeWhenPersist;
        }

        public WfServiceAddressDefinition AddressDef
        {
            get
            {
                WfServiceAddressDefinition result = this._AddressDef;

                if (this._AddressDef != null && this._AddressDef.Key.IsNotEmpty())
                {
                    if (WfGlobalParameters.Default.ServiceAddressDefs.ContainsKey(this._AddressDef.Key))
                    {
                        result = WfGlobalParameters.Default.ServiceAddressDefs[this._AddressDef.Key];
                    }
                    else
                    {
                        WfServiceAddressDefinitionConfigurationElement addressElement = WfServiceDefinitionSettings.GetSection().Addresses[this._AddressDef.Key];

                        if (addressElement != null)
                            result = new WfServiceAddressDefinition(addressElement);
                    }
                }

                return result;
            }
            internal set
            {
                this._AddressDef = value;
            }
        }

        /// <summary>
        /// 是否在流程持久化时调用。
        /// </summary>
        public bool InvokeWhenPersist
        {
            get;
            set;
        }

        public string OperationName
        {
            get;
            set;
        }

        /// <summary>
        /// 调用服务超时间 
        /// </summary>
        //2012-11-28
        public TimeSpan Timeout
        {
            get
            {
                return this._Timeout;
            }
            set
            {
                this._Timeout = value;
            }
        }

        public WfServiceOperationParameterCollection Params
        {
            get
            {
                if (this._Params == null)
                    this._Params = new WfServiceOperationParameterCollection();

                return _Params;
            }
            set
            {
                this._Params = value;
            }
        }

        /// <summary>
        /// 服务返回的xml存放在流程中的参数名
        /// </summary>
        public string RtnXmlStoreParamName
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public WfServiceOperationDefinition Clone()
        {
            return (WfServiceOperationDefinition)SerializationHelper.CloneObject(this);
        }

        #region 快捷方法
        public WfServiceOperationDefinition SetAddress(string addressKey)
        {
            this.AddressDef = new WfServiceAddressDefinition(addressKey);

            return this;
        }

        public WfServiceOperationDefinition SetAddress(WfServiceAddressDefinitionConfigurationElement element)
        {
            this.AddressDef = new WfServiceAddressDefinition(element);

            return this;
        }

        public WfServiceOperationDefinition SetAddress(WfServiceRequestMethod method, string address, WfServiceContentType contentType)
        {
            this.AddressDef = new WfServiceAddressDefinition(method, address, contentType);

            return this;
        }

        public WfServiceOperationDefinition SetAddress(WfServiceRequestMethod method, string address, WfServiceContentType contentType, WfNetworkCredential credential)
        {
            this.AddressDef = new WfServiceAddressDefinition(method, credential, address, contentType);

            return this;
        }

        public WfServiceOperationDefinition SetAddress(WfServiceAddressDefinition addressDef)
        {
            this.AddressDef = addressDef;

            return this;
        }

        public WfServiceOperationDefinition AddParameter(WfServiceOperationParameterConfigurationElement element)
        {
            this.Params.Add(new WfServiceOperationParameter(element));

            return this;
        }

        public WfServiceOperationDefinition AddParameter(string name, object value)
        {
            this.Params.Add(new WfServiceOperationParameter(name, value));

            return this;
        }

        public WfServiceOperationDefinition AddParameter(string name, WfSvcOperationParameterType type, object value)
        {
            this.Params.Add(new WfServiceOperationParameter(name, type, value));

            return this;
        }
        #endregion 快捷方法

        #region ISimpleXmlSerializer Members

        void ISimpleXmlSerializer.ToXElement(XElement element, string refNodeName)
        {
            if (refNodeName.IsNotEmpty())
                element = element.AddChildElement(refNodeName);

            element.SetAttributeValue("key", this.Key);
            element.SetAttributeValue("rtnXmlStoreParamName", this.RtnXmlStoreParamName);

            if (this._Params != null)
                ((ISimpleXmlSerializer)this._Params).ToXElement(element, "Params");

            if (this.AddressDef != null)
                ((ISimpleXmlSerializer)this.AddressDef).ToXElement(element, "AddressDef");
        }

        #endregion
    }

    [Serializable]
    [XElementSerializable]
    public class WfServiceOperationDefinitionCollection : EditableDataObjectCollectionBase<WfServiceOperationDefinition>, ISimpleXmlSerializer
    {
        #region ISimpleXmlSerializer Members

        void ISimpleXmlSerializer.ToXElement(XElement element, string refNodeName)
        {
            if (refNodeName.IsNotEmpty())
                element = element.AddChildElement(refNodeName);

            foreach (WfServiceOperationDefinition operation in this)
                ((ISimpleXmlSerializer)operation).ToXElement(element, "Operation");
        }

        #endregion

        /// <summary>
        /// 得到在持久化时需要调用的服务
        /// </summary>
        /// <returns></returns>
        public WfServiceOperationDefinitionCollection GetServiceOperationsWhenPersist()
        {
            WfServiceOperationDefinitionCollection result = new WfServiceOperationDefinitionCollection();

            foreach (WfServiceOperationDefinition serviceOpDef in this)
            {
                if (serviceOpDef.InvokeWhenPersist)
                    result.Add(serviceOpDef);
            }

            return result;
        }

        /// <summary>
        /// 得到在持久化之前需要调用的服务
        /// </summary>
        /// <returns></returns>
        public WfServiceOperationDefinitionCollection GetServiceOperationsBeforePersist()
        {
            WfServiceOperationDefinitionCollection result = new WfServiceOperationDefinitionCollection();

            foreach (WfServiceOperationDefinition serviceOpDef in this)
            {
                if (serviceOpDef.InvokeWhenPersist == false)
                    result.Add(serviceOpDef);
            }

            return result;
        }

        public void SyncPropertiesToFields(PropertyValue property)
        {
            if (property != null)
            {
                this.Clear();

                if (property.StringValue.IsNotEmpty())
                {
                    IEnumerable<WfServiceOperationDefinition> deserializedData = (IEnumerable<WfServiceOperationDefinition>)JSONSerializerExecute.DeserializeObject(property.StringValue, this.GetType());

                    this.CopyFrom(deserializedData);
                }
            }
        }
    }
}
