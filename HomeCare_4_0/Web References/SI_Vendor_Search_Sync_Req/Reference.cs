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

namespace HomeCare_4_0.SI_Vendor_Search_Sync_Req {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="SI_Vendor_Search_Sync_ReqBinding", Namespace="http://www.sansiri.com/ECC/VENDOR/")]
    public partial class SI_Vendor_Search_Sync_ReqService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SI_Vendor_Search_Sync_ReqOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SI_Vendor_Search_Sync_ReqService() {
            this.Url = global::HomeCare_4_0.Properties.Settings.Default.HomeCare_4_0_SI_Vendor_Search_Sync_Req_SI_Vendor_Search_Sync_ReqService;
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
        public event SI_Vendor_Search_Sync_ReqCompletedEventHandler SI_Vendor_Search_Sync_ReqCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", RequestElementName="Z_PI_VENDOR_SEARCH", RequestNamespace="urn:sap-com:document:sap:rfc:functions", ResponseElementName="Z_PI_VENDOR_SEARCH.Response", ResponseNamespace="urn:sap-com:document:sap:rfc:functions", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("EX_RETURN", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public BAPIRETURN1 SI_Vendor_Search_Sync_Req([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string IM_BUKRS, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)] RANGE_LIFNR[] IM_LIFNR, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string IM_NAME, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string IM_SPERQ, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string IM_STCD, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string IM_ZWELS, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)] ref ZSPI_CONTRACT_PERSON[] EX_CONTRACT_PERSON, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)] ref ZSPI_VENDOR_SEARCH[] EX_VENDOR_SEARCH, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)] ref ZSPI_WITHT[] EX_WITHT) {
            object[] results = this.Invoke("SI_Vendor_Search_Sync_Req", new object[] {
                        IM_BUKRS,
                        IM_LIFNR,
                        IM_NAME,
                        IM_SPERQ,
                        IM_STCD,
                        IM_ZWELS,
                        EX_CONTRACT_PERSON,
                        EX_VENDOR_SEARCH,
                        EX_WITHT});
            EX_CONTRACT_PERSON = ((ZSPI_CONTRACT_PERSON[])(results[1]));
            EX_VENDOR_SEARCH = ((ZSPI_VENDOR_SEARCH[])(results[2]));
            EX_WITHT = ((ZSPI_WITHT[])(results[3]));
            return ((BAPIRETURN1)(results[0]));
        }
        
        /// <remarks/>
        public void SI_Vendor_Search_Sync_ReqAsync(string IM_BUKRS, RANGE_LIFNR[] IM_LIFNR, string IM_NAME, string IM_SPERQ, string IM_STCD, string IM_ZWELS, ZSPI_CONTRACT_PERSON[] EX_CONTRACT_PERSON, ZSPI_VENDOR_SEARCH[] EX_VENDOR_SEARCH, ZSPI_WITHT[] EX_WITHT) {
            this.SI_Vendor_Search_Sync_ReqAsync(IM_BUKRS, IM_LIFNR, IM_NAME, IM_SPERQ, IM_STCD, IM_ZWELS, EX_CONTRACT_PERSON, EX_VENDOR_SEARCH, EX_WITHT, null);
        }
        
        /// <remarks/>
        public void SI_Vendor_Search_Sync_ReqAsync(string IM_BUKRS, RANGE_LIFNR[] IM_LIFNR, string IM_NAME, string IM_SPERQ, string IM_STCD, string IM_ZWELS, ZSPI_CONTRACT_PERSON[] EX_CONTRACT_PERSON, ZSPI_VENDOR_SEARCH[] EX_VENDOR_SEARCH, ZSPI_WITHT[] EX_WITHT, object userState) {
            if ((this.SI_Vendor_Search_Sync_ReqOperationCompleted == null)) {
                this.SI_Vendor_Search_Sync_ReqOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSI_Vendor_Search_Sync_ReqOperationCompleted);
            }
            this.InvokeAsync("SI_Vendor_Search_Sync_Req", new object[] {
                        IM_BUKRS,
                        IM_LIFNR,
                        IM_NAME,
                        IM_SPERQ,
                        IM_STCD,
                        IM_ZWELS,
                        EX_CONTRACT_PERSON,
                        EX_VENDOR_SEARCH,
                        EX_WITHT}, this.SI_Vendor_Search_Sync_ReqOperationCompleted, userState);
        }
        
        private void OnSI_Vendor_Search_Sync_ReqOperationCompleted(object arg) {
            if ((this.SI_Vendor_Search_Sync_ReqCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SI_Vendor_Search_Sync_ReqCompleted(this, new SI_Vendor_Search_Sync_ReqCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class RANGE_LIFNR {
        
        private string sIGNField;
        
        private string oPTIONField;
        
        private string lOWField;
        
        private string hIGHField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SIGN {
            get {
                return this.sIGNField;
            }
            set {
                this.sIGNField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OPTION {
            get {
                return this.oPTIONField;
            }
            set {
                this.oPTIONField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LOW {
            get {
                return this.lOWField;
            }
            set {
                this.lOWField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string HIGH {
            get {
                return this.hIGHField;
            }
            set {
                this.hIGHField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class BAPIRETURN1 {
        
        private string tYPEField;
        
        private string idField;
        
        private string nUMBERField;
        
        private string mESSAGEField;
        
        private string lOG_NOField;
        
        private string lOG_MSG_NOField;
        
        private string mESSAGE_V1Field;
        
        private string mESSAGE_V2Field;
        
        private string mESSAGE_V3Field;
        
        private string mESSAGE_V4Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TYPE {
            get {
                return this.tYPEField;
            }
            set {
                this.tYPEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NUMBER {
            get {
                return this.nUMBERField;
            }
            set {
                this.nUMBERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE {
            get {
                return this.mESSAGEField;
            }
            set {
                this.mESSAGEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LOG_NO {
            get {
                return this.lOG_NOField;
            }
            set {
                this.lOG_NOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LOG_MSG_NO {
            get {
                return this.lOG_MSG_NOField;
            }
            set {
                this.lOG_MSG_NOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE_V1 {
            get {
                return this.mESSAGE_V1Field;
            }
            set {
                this.mESSAGE_V1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE_V2 {
            get {
                return this.mESSAGE_V2Field;
            }
            set {
                this.mESSAGE_V2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE_V3 {
            get {
                return this.mESSAGE_V3Field;
            }
            set {
                this.mESSAGE_V3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE_V4 {
            get {
                return this.mESSAGE_V4Field;
            }
            set {
                this.mESSAGE_V4Field = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZSPI_WITHT {
        
        private string lIFNRField;
        
        private string bUKRSField;
        
        private string wITHTField;
        
        private string wT_SUBJECTField;
        
        private string qSRECField;
        
        private string wT_WITHCDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LIFNR {
            get {
                return this.lIFNRField;
            }
            set {
                this.lIFNRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BUKRS {
            get {
                return this.bUKRSField;
            }
            set {
                this.bUKRSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string WITHT {
            get {
                return this.wITHTField;
            }
            set {
                this.wITHTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string WT_SUBJECT {
            get {
                return this.wT_SUBJECTField;
            }
            set {
                this.wT_SUBJECTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string QSREC {
            get {
                return this.qSRECField;
            }
            set {
                this.qSRECField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string WT_WITHCD {
            get {
                return this.wT_WITHCDField;
            }
            set {
                this.wT_WITHCDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZSPI_VENDOR_SEARCH {
        
        private string lIFNRField;
        
        private string nAME1Field;
        
        private string nAME2Field;
        
        private string nAME3Field;
        
        private string nAME4Field;
        
        private string sORT1Field;
        
        private string sTREETField;
        
        private string sUB_DISTRICTField;
        
        private string dISTRICTField;
        
        private string cITYField;
        
        private string pOST_CODEField;
        
        private string cOUNTRYField;
        
        private string tEL_NUMBERField;
        
        private string tEL_EXTENSField;
        
        private string fAX_NUMBERField;
        
        private string fAX_EXTENSField;
        
        private string mOBILEField;
        
        private string eMAILField;
        
        private string kTOKKField;
        
        private string sTCD1Field;
        
        private string sTCD2Field;
        
        private string sTCD3Field;
        
        private string bANKSField;
        
        private string bANKLField;
        
        private string bANKNField;
        
        private string kOINHField;
        
        private string xZEMPField;
        
        private string bUKRSField;
        
        private string aKONTField;
        
        private string zTERMField;
        
        private string zSABEField;
        
        private string tLFNSField;
        
        private string eKORGField;
        
        private string wAERSField;
        
        private string bCODEField;
        
        private string fITH_DESCField;
        
        private string sPERQField;
        
        private string kURZTEXTField;
        
        private string zWELSField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LIFNR {
            get {
                return this.lIFNRField;
            }
            set {
                this.lIFNRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NAME1 {
            get {
                return this.nAME1Field;
            }
            set {
                this.nAME1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NAME2 {
            get {
                return this.nAME2Field;
            }
            set {
                this.nAME2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NAME3 {
            get {
                return this.nAME3Field;
            }
            set {
                this.nAME3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NAME4 {
            get {
                return this.nAME4Field;
            }
            set {
                this.nAME4Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SORT1 {
            get {
                return this.sORT1Field;
            }
            set {
                this.sORT1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string STREET {
            get {
                return this.sTREETField;
            }
            set {
                this.sTREETField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SUB_DISTRICT {
            get {
                return this.sUB_DISTRICTField;
            }
            set {
                this.sUB_DISTRICTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DISTRICT {
            get {
                return this.dISTRICTField;
            }
            set {
                this.dISTRICTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CITY {
            get {
                return this.cITYField;
            }
            set {
                this.cITYField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string POST_CODE {
            get {
                return this.pOST_CODEField;
            }
            set {
                this.pOST_CODEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string COUNTRY {
            get {
                return this.cOUNTRYField;
            }
            set {
                this.cOUNTRYField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TEL_NUMBER {
            get {
                return this.tEL_NUMBERField;
            }
            set {
                this.tEL_NUMBERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TEL_EXTENS {
            get {
                return this.tEL_EXTENSField;
            }
            set {
                this.tEL_EXTENSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FAX_NUMBER {
            get {
                return this.fAX_NUMBERField;
            }
            set {
                this.fAX_NUMBERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FAX_EXTENS {
            get {
                return this.fAX_EXTENSField;
            }
            set {
                this.fAX_EXTENSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOBILE {
            get {
                return this.mOBILEField;
            }
            set {
                this.mOBILEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EMAIL {
            get {
                return this.eMAILField;
            }
            set {
                this.eMAILField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string KTOKK {
            get {
                return this.kTOKKField;
            }
            set {
                this.kTOKKField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string STCD1 {
            get {
                return this.sTCD1Field;
            }
            set {
                this.sTCD1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string STCD2 {
            get {
                return this.sTCD2Field;
            }
            set {
                this.sTCD2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string STCD3 {
            get {
                return this.sTCD3Field;
            }
            set {
                this.sTCD3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BANKS {
            get {
                return this.bANKSField;
            }
            set {
                this.bANKSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BANKL {
            get {
                return this.bANKLField;
            }
            set {
                this.bANKLField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BANKN {
            get {
                return this.bANKNField;
            }
            set {
                this.bANKNField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string KOINH {
            get {
                return this.kOINHField;
            }
            set {
                this.kOINHField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string XZEMP {
            get {
                return this.xZEMPField;
            }
            set {
                this.xZEMPField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BUKRS {
            get {
                return this.bUKRSField;
            }
            set {
                this.bUKRSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AKONT {
            get {
                return this.aKONTField;
            }
            set {
                this.aKONTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZTERM {
            get {
                return this.zTERMField;
            }
            set {
                this.zTERMField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZSABE {
            get {
                return this.zSABEField;
            }
            set {
                this.zSABEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TLFNS {
            get {
                return this.tLFNSField;
            }
            set {
                this.tLFNSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EKORG {
            get {
                return this.eKORGField;
            }
            set {
                this.eKORGField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string WAERS {
            get {
                return this.wAERSField;
            }
            set {
                this.wAERSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BCODE {
            get {
                return this.bCODEField;
            }
            set {
                this.bCODEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FITH_DESC {
            get {
                return this.fITH_DESCField;
            }
            set {
                this.fITH_DESCField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SPERQ {
            get {
                return this.sPERQField;
            }
            set {
                this.sPERQField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string KURZTEXT {
            get {
                return this.kURZTEXTField;
            }
            set {
                this.kURZTEXTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ZWELS {
            get {
                return this.zWELSField;
            }
            set {
                this.zWELSField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZSPI_CONTRACT_PERSON {
        
        private string lIFNRField;
        
        private string cONTRACT_FUNCField;
        
        private string cONTRACT_TAXField;
        
        private string cONTRACT_TITLEField;
        
        private string cONTRACT_FNAMEField;
        
        private string cONTRACT_LNAMEField;
        
        private string cONTRACT_TELField;
        
        private string cONTRACT_TEL_EXTENSField;
        
        private string cONTRACT_EMAILField;
        
        private string cONTRACT_DEPField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LIFNR {
            get {
                return this.lIFNRField;
            }
            set {
                this.lIFNRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CONTRACT_FUNC {
            get {
                return this.cONTRACT_FUNCField;
            }
            set {
                this.cONTRACT_FUNCField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CONTRACT_TAX {
            get {
                return this.cONTRACT_TAXField;
            }
            set {
                this.cONTRACT_TAXField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CONTRACT_TITLE {
            get {
                return this.cONTRACT_TITLEField;
            }
            set {
                this.cONTRACT_TITLEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CONTRACT_FNAME {
            get {
                return this.cONTRACT_FNAMEField;
            }
            set {
                this.cONTRACT_FNAMEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CONTRACT_LNAME {
            get {
                return this.cONTRACT_LNAMEField;
            }
            set {
                this.cONTRACT_LNAMEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CONTRACT_TEL {
            get {
                return this.cONTRACT_TELField;
            }
            set {
                this.cONTRACT_TELField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CONTRACT_TEL_EXTENS {
            get {
                return this.cONTRACT_TEL_EXTENSField;
            }
            set {
                this.cONTRACT_TEL_EXTENSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CONTRACT_EMAIL {
            get {
                return this.cONTRACT_EMAILField;
            }
            set {
                this.cONTRACT_EMAILField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CONTRACT_DEP {
            get {
                return this.cONTRACT_DEPField;
            }
            set {
                this.cONTRACT_DEPField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void SI_Vendor_Search_Sync_ReqCompletedEventHandler(object sender, SI_Vendor_Search_Sync_ReqCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SI_Vendor_Search_Sync_ReqCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SI_Vendor_Search_Sync_ReqCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public BAPIRETURN1 Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((BAPIRETURN1)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public ZSPI_CONTRACT_PERSON[] EX_CONTRACT_PERSON {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ZSPI_CONTRACT_PERSON[])(this.results[1]));
            }
        }
        
        /// <remarks/>
        public ZSPI_VENDOR_SEARCH[] EX_VENDOR_SEARCH {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ZSPI_VENDOR_SEARCH[])(this.results[2]));
            }
        }
        
        /// <remarks/>
        public ZSPI_WITHT[] EX_WITHT {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ZSPI_WITHT[])(this.results[3]));
            }
        }
    }
}

#pragma warning restore 1591