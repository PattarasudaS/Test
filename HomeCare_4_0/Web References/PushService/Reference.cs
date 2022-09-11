﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace HomeCare_4_0.PushService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="PushServiceSoap", Namespace="http://tempuri.org/")]
    public partial class PushService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback testsendOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendNotificationOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public PushService() {
            this.Url = global::HomeCare_4_0.Properties.Settings.Default.HomeCare_4_0_PushService_PushService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event testsendCompletedEventHandler testsendCompleted;
        
        /// <remarks/>
        public event SendNotificationCompletedEventHandler SendNotificationCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/testsend", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void testsend() {
            this.Invoke("testsend", new object[0]);
        }
        
        /// <remarks/>
        public void testsendAsync() {
            this.testsendAsync(null);
        }
        
        /// <remarks/>
        public void testsendAsync(object userState) {
            if ((this.testsendOperationCompleted == null)) {
                this.testsendOperationCompleted = new System.Threading.SendOrPostCallback(this.OntestsendOperationCompleted);
            }
            this.InvokeAsync("testsend", new object[0], this.testsendOperationCompleted, userState);
        }
        
        private void OntestsendOperationCompleted(object arg) {
            if ((this.testsendCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.testsendCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendNotification", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("result")]
        public string SendNotification(PushData data) {
            object[] results = this.Invoke("SendNotification", new object[] {
                        data});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SendNotificationAsync(PushData data) {
            this.SendNotificationAsync(data, null);
        }
        
        /// <remarks/>
        public void SendNotificationAsync(PushData data, object userState) {
            if ((this.SendNotificationOperationCompleted == null)) {
                this.SendNotificationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendNotificationOperationCompleted);
            }
            this.InvokeAsync("SendNotification", new object[] {
                        data}, this.SendNotificationOperationCompleted, userState);
        }
        
        private void OnSendNotificationOperationCompleted(object arg) {
            if ((this.SendNotificationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendNotificationCompleted(this, new SendNotificationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class PushData {
        
        private string processField;
        
        private string intnoField;
        
        private string gsberField;
        
        private string deptField;
        
        private string cnttypeField;
        
        private string doctypeField;
        
        private string intitemField;
        
        private string emailField;
        
        /// <remarks/>
        public string process {
            get {
                return this.processField;
            }
            set {
                this.processField = value;
            }
        }
        
        /// <remarks/>
        public string intno {
            get {
                return this.intnoField;
            }
            set {
                this.intnoField = value;
            }
        }
        
        /// <remarks/>
        public string gsber {
            get {
                return this.gsberField;
            }
            set {
                this.gsberField = value;
            }
        }
        
        /// <remarks/>
        public string dept {
            get {
                return this.deptField;
            }
            set {
                this.deptField = value;
            }
        }
        
        /// <remarks/>
        public string cnttype {
            get {
                return this.cnttypeField;
            }
            set {
                this.cnttypeField = value;
            }
        }
        
        /// <remarks/>
        public string doctype {
            get {
                return this.doctypeField;
            }
            set {
                this.doctypeField = value;
            }
        }
        
        /// <remarks/>
        public string intitem {
            get {
                return this.intitemField;
            }
            set {
                this.intitemField = value;
            }
        }
        
        /// <remarks/>
        public string email {
            get {
                return this.emailField;
            }
            set {
                this.emailField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void testsendCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void SendNotificationCompletedEventHandler(object sender, SendNotificationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendNotificationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendNotificationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591