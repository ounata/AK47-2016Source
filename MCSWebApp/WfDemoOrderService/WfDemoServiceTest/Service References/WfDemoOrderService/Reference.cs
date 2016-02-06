﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WfDemoServiceTest.WfDemoOrderService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DemoOrderData", Namespace="http://schemas.datacontract.org/2004/07/WfDemoService")]
    [System.SerializableAttribute()]
    public partial class DemoOrderData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AmountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime Create_TimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatorIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatorNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CurrencyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OrderIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool PayableField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SubjectField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Amount {
            get {
                return this.AmountField;
            }
            set {
                if ((this.AmountField.Equals(value) != true)) {
                    this.AmountField = value;
                    this.RaisePropertyChanged("Amount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Content {
            get {
                return this.ContentField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentField, value) != true)) {
                    this.ContentField = value;
                    this.RaisePropertyChanged("Content");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Create_Time {
            get {
                return this.Create_TimeField;
            }
            set {
                if ((this.Create_TimeField.Equals(value) != true)) {
                    this.Create_TimeField = value;
                    this.RaisePropertyChanged("Create_Time");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CreatorID {
            get {
                return this.CreatorIDField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatorIDField, value) != true)) {
                    this.CreatorIDField = value;
                    this.RaisePropertyChanged("CreatorID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CreatorName {
            get {
                return this.CreatorNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatorNameField, value) != true)) {
                    this.CreatorNameField = value;
                    this.RaisePropertyChanged("CreatorName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Currency {
            get {
                return this.CurrencyField;
            }
            set {
                if ((object.ReferenceEquals(this.CurrencyField, value) != true)) {
                    this.CurrencyField = value;
                    this.RaisePropertyChanged("Currency");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ID {
            get {
                return this.IDField;
            }
            set {
                if ((object.ReferenceEquals(this.IDField, value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OrderID {
            get {
                return this.OrderIDField;
            }
            set {
                if ((object.ReferenceEquals(this.OrderIDField, value) != true)) {
                    this.OrderIDField = value;
                    this.RaisePropertyChanged("OrderID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Payable {
            get {
                return this.PayableField;
            }
            set {
                if ((this.PayableField.Equals(value) != true)) {
                    this.PayableField = value;
                    this.RaisePropertyChanged("Payable");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Subject {
            get {
                return this.SubjectField;
            }
            set {
                if ((object.ReferenceEquals(this.SubjectField, value) != true)) {
                    this.SubjectField = value;
                    this.RaisePropertyChanged("Subject");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WfDemoOrderService.IDemoOrderService")]
    public interface IDemoOrderService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDemoOrderService/Update", ReplyAction="http://tempuri.org/IDemoOrderService/UpdateResponse")]
        void Update(WfDemoServiceTest.WfDemoOrderService.DemoOrderData data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDemoOrderService/LoadSingleOrder", ReplyAction="http://tempuri.org/IDemoOrderService/LoadSingleOrderResponse")]
        WfDemoServiceTest.WfDemoOrderService.DemoOrderData LoadSingleOrder(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDemoOrderService/LoadByOrderID", ReplyAction="http://tempuri.org/IDemoOrderService/LoadByOrderIDResponse")]
        WfDemoServiceTest.WfDemoOrderService.DemoOrderData LoadByOrderID(string demoOrderID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDemoOrderService/IsPayableOrder", ReplyAction="http://tempuri.org/IDemoOrderService/IsPayableOrderResponse")]
        bool IsPayableOrder(string demoOrderID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDemoOrderService/GetNewOrderID", ReplyAction="http://tempuri.org/IDemoOrderService/GetNewOrderIDResponse")]
        string GetNewOrderID();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDemoOrderServiceChannel : WfDemoServiceTest.WfDemoOrderService.IDemoOrderService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DemoOrderServiceClient : System.ServiceModel.ClientBase<WfDemoServiceTest.WfDemoOrderService.IDemoOrderService>, WfDemoServiceTest.WfDemoOrderService.IDemoOrderService {
        
        public DemoOrderServiceClient() {
        }
        
        public DemoOrderServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DemoOrderServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DemoOrderServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DemoOrderServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Update(WfDemoServiceTest.WfDemoOrderService.DemoOrderData data) {
            base.Channel.Update(data);
        }
        
        public WfDemoServiceTest.WfDemoOrderService.DemoOrderData LoadSingleOrder(string id) {
            return base.Channel.LoadSingleOrder(id);
        }
        
        public WfDemoServiceTest.WfDemoOrderService.DemoOrderData LoadByOrderID(string demoOrderID) {
            return base.Channel.LoadByOrderID(demoOrderID);
        }
        
        public bool IsPayableOrder(string demoOrderID) {
            return base.Channel.IsPayableOrder(demoOrderID);
        }
        
        public string GetNewOrderID() {
            return base.Channel.GetNewOrderID();
        }
    }
}
