﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.235.
// 
#pragma warning disable 1591

namespace Cfb.CandidatePortal.Web.Security.SPPeopleService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="PeopleSoap", Namespace="http://schemas.microsoft.com/sharepoint/soap/")]
    public partial class People : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ResolvePrincipalsOperationCompleted;
        
        private System.Threading.SendOrPostCallback SearchPrincipalsOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public People() {
            this.Url = global::Cfb.CandidatePortal.Web.Security.Properties.Settings.Default.Cfb_CandidatePortal_Web_Security_SPPeopleService_People;
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
        public event ResolvePrincipalsCompletedEventHandler ResolvePrincipalsCompleted;
        
        /// <remarks/>
        public event SearchPrincipalsCompletedEventHandler SearchPrincipalsCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/ResolvePrincipals", RequestNamespace="http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace="http://schemas.microsoft.com/sharepoint/soap/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public PrincipalInfo[] ResolvePrincipals(string[] principalKeys, SPPrincipalType principalType, bool addToUserInfoList) {
            object[] results = this.Invoke("ResolvePrincipals", new object[] {
                        principalKeys,
                        principalType,
                        addToUserInfoList});
            return ((PrincipalInfo[])(results[0]));
        }
        
        /// <remarks/>
        public void ResolvePrincipalsAsync(string[] principalKeys, SPPrincipalType principalType, bool addToUserInfoList) {
            this.ResolvePrincipalsAsync(principalKeys, principalType, addToUserInfoList, null);
        }
        
        /// <remarks/>
        public void ResolvePrincipalsAsync(string[] principalKeys, SPPrincipalType principalType, bool addToUserInfoList, object userState) {
            if ((this.ResolvePrincipalsOperationCompleted == null)) {
                this.ResolvePrincipalsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnResolvePrincipalsOperationCompleted);
            }
            this.InvokeAsync("ResolvePrincipals", new object[] {
                        principalKeys,
                        principalType,
                        addToUserInfoList}, this.ResolvePrincipalsOperationCompleted, userState);
        }
        
        private void OnResolvePrincipalsOperationCompleted(object arg) {
            if ((this.ResolvePrincipalsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ResolvePrincipalsCompleted(this, new ResolvePrincipalsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/SearchPrincipals", RequestNamespace="http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace="http://schemas.microsoft.com/sharepoint/soap/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public PrincipalInfo[] SearchPrincipals(string searchText, int maxResults, SPPrincipalType principalType) {
            object[] results = this.Invoke("SearchPrincipals", new object[] {
                        searchText,
                        maxResults,
                        principalType});
            return ((PrincipalInfo[])(results[0]));
        }
        
        /// <remarks/>
        public void SearchPrincipalsAsync(string searchText, int maxResults, SPPrincipalType principalType) {
            this.SearchPrincipalsAsync(searchText, maxResults, principalType, null);
        }
        
        /// <remarks/>
        public void SearchPrincipalsAsync(string searchText, int maxResults, SPPrincipalType principalType, object userState) {
            if ((this.SearchPrincipalsOperationCompleted == null)) {
                this.SearchPrincipalsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSearchPrincipalsOperationCompleted);
            }
            this.InvokeAsync("SearchPrincipals", new object[] {
                        searchText,
                        maxResults,
                        principalType}, this.SearchPrincipalsOperationCompleted, userState);
        }
        
        private void OnSearchPrincipalsOperationCompleted(object arg) {
            if ((this.SearchPrincipalsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SearchPrincipalsCompleted(this, new SearchPrincipalsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.FlagsAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sharepoint/soap/")]
    public enum SPPrincipalType {
        
        /// <remarks/>
        None = 1,
        
        /// <remarks/>
        User = 2,
        
        /// <remarks/>
        DistributionList = 4,
        
        /// <remarks/>
        SecurityGroup = 8,
        
        /// <remarks/>
        SharePointGroup = 16,
        
        /// <remarks/>
        All = 32,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sharepoint/soap/")]
    public partial class PrincipalInfo {
        
        private string accountNameField;
        
        private int userInfoIDField;
        
        private string displayNameField;
        
        private string emailField;
        
        private string departmentField;
        
        private string titleField;
        
        private bool isResolvedField;
        
        private PrincipalInfo[] moreMatchesField;
        
        private SPPrincipalType principalTypeField;
        
        /// <remarks/>
        public string AccountName {
            get {
                return this.accountNameField;
            }
            set {
                this.accountNameField = value;
            }
        }
        
        /// <remarks/>
        public int UserInfoID {
            get {
                return this.userInfoIDField;
            }
            set {
                this.userInfoIDField = value;
            }
        }
        
        /// <remarks/>
        public string DisplayName {
            get {
                return this.displayNameField;
            }
            set {
                this.displayNameField = value;
            }
        }
        
        /// <remarks/>
        public string Email {
            get {
                return this.emailField;
            }
            set {
                this.emailField = value;
            }
        }
        
        /// <remarks/>
        public string Department {
            get {
                return this.departmentField;
            }
            set {
                this.departmentField = value;
            }
        }
        
        /// <remarks/>
        public string Title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        public bool IsResolved {
            get {
                return this.isResolvedField;
            }
            set {
                this.isResolvedField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public PrincipalInfo[] MoreMatches {
            get {
                return this.moreMatchesField;
            }
            set {
                this.moreMatchesField = value;
            }
        }
        
        /// <remarks/>
        public SPPrincipalType PrincipalType {
            get {
                return this.principalTypeField;
            }
            set {
                this.principalTypeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void ResolvePrincipalsCompletedEventHandler(object sender, ResolvePrincipalsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ResolvePrincipalsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ResolvePrincipalsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public PrincipalInfo[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((PrincipalInfo[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void SearchPrincipalsCompletedEventHandler(object sender, SearchPrincipalsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SearchPrincipalsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SearchPrincipalsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public PrincipalInfo[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((PrincipalInfo[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591