﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cfb.CandidatePortal.ServiceProcess.CPReminderDistributionService.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("15")]
        public int TimerIntervalMinutes {
            get {
                return ((int)(this["TimerIntervalMinutes"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4")]
        public byte DoingBusinessReminderDays {
            get {
                return ((byte)(this["DoingBusinessReminderDays"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<html>\r\n<body>\r\n<h1><img src=\"{0}/images/caccess-logo.png\" width=\"113\" height=\"29" +
            "\" alt=\"C-Access\"/></h1>\r\n")]
        public string BodyTemplateHeader {
            get {
                return ((string)(this["BodyTemplateHeader"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<p><em>NOTE: Please do not reply to this automated message, as e-mail sent to thi" +
            "s address will not be answered.</em></p>\r\n</body>\r\n</html>")]
        public string BodyTemplateFooter {
            get {
                return ((string)(this["BodyTemplateFooter"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Doing Business Reminder")]
        public string DoingBusinessSubject {
            get {
                return ((string)(this["DoingBusinessSubject"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<p>Please be reminded that a response is due in {0} days to a notification by the CFB regarding contributions during the {1} election that exceed the doing business limit. No extensions can be granted. The notification was sent directly to your campaign via e-mail on {2:M}, {2:yyyy}. If you did not receive the notification, or if you have any questions, please contact <a href=""mailto:CFBDoingBusiness@nyccfb.info"">CFBDoingBusiness@nyccfb.info</a> immediately.</p>
")]
        public string DoingBusinessBodyTemplate {
            get {
                return ((string)(this["DoingBusinessBodyTemplate"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("CFB Notice")]
        public string SenderDisplayName {
            get {
                return ((string)(this["SenderDisplayName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SenderEmail {
            get {
                return ((string)(this["SenderEmail"]));
            }
        }
    }
}