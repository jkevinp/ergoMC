﻿#pragma checksum "..\..\..\RulaReport.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3CC80CB1EC9A741C6E449C3AB6CF652C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ProjectK.ErgoMC.Assessment {
    
    
    /// <summary>
    /// EmployeeView
    /// </summary>
    public partial class EmployeeView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\RulaReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ProjectK.ErgoMC.Assessment.EmployeeView RulaReport;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\RulaReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_evaluate;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\RulaReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_reset;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\RulaReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_cancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BodyBasics-WPF;component/rulareport.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\RulaReport.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.RulaReport = ((ProjectK.ErgoMC.Assessment.EmployeeView)(target));
            return;
            case 2:
            this.btn_evaluate = ((System.Windows.Controls.Button)(target));
            
            #line 115 "..\..\..\RulaReport.xaml"
            this.btn_evaluate.Click += new System.Windows.RoutedEventHandler(this.btn_evaluate_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_reset = ((System.Windows.Controls.Button)(target));
            
            #line 116 "..\..\..\RulaReport.xaml"
            this.btn_reset.Click += new System.Windows.RoutedEventHandler(this.btn_reset_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_cancel = ((System.Windows.Controls.Button)(target));
            
            #line 117 "..\..\..\RulaReport.xaml"
            this.btn_cancel.Click += new System.Windows.RoutedEventHandler(this.btn_cancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

