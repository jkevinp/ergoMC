﻿#pragma checksum "..\..\..\Reba.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4458A0C64B39AF02D125BE1E43F32CB7"
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
    /// Reba
    /// </summary>
    public partial class Reba : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 47 "..\..\..\Reba.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_status;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Reba.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lb_orientations;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\Reba.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox neck_position_score;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\Reba.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox trunk_poosition_score;
        
        #line default
        #line hidden
        
        
        #line 236 "..\..\..\Reba.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox upper_arm_score;
        
        #line default
        #line hidden
        
        
        #line 274 "..\..\..\Reba.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lower_arm_score;
        
        #line default
        #line hidden
        
        
        #line 292 "..\..\..\Reba.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_wrist_position;
        
        #line default
        #line hidden
        
        
        #line 415 "..\..\..\Reba.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.StatusBar statusBar;
        
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
            System.Uri resourceLocater = new System.Uri("/BodyBasics-WPF;component/reba.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Reba.xaml"
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
            
            #line 5 "..\..\..\Reba.xaml"
            ((ProjectK.ErgoMC.Assessment.Reba)(target)).Loaded += new System.Windows.RoutedEventHandler(this.MainWindow_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.img_status = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            
            #line 49 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked_1);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lb_orientations = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            
            #line 81 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.neck_position_score = ((System.Windows.Controls.TextBox)(target));
            
            #line 94 "..\..\..\Reba.xaml"
            this.neck_position_score.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 101 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalNeckPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 105 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalNeckPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 114 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.trunk_poosition_score = ((System.Windows.Controls.TextBox)(target));
            
            #line 126 "..\..\..\Reba.xaml"
            this.trunk_poosition_score.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 135 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalTrunkPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 143 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalTrunkPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 152 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 164 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 171 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalLegPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 177 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalLegPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 186 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 198 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 202 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_neckForceLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 203 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_neckForceLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 204 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_neckForceLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 205 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_neckForceLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 226 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 24:
            this.upper_arm_score = ((System.Windows.Controls.TextBox)(target));
            
            #line 236 "..\..\..\Reba.xaml"
            this.upper_arm_score.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 25:
            
            #line 247 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalUpperArm_Checked);
            
            #line default
            #line hidden
            return;
            case 26:
            
            #line 248 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalUpperArm_Checked);
            
            #line default
            #line hidden
            return;
            case 27:
            
            #line 256 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalUpperArm_Checked);
            
            #line default
            #line hidden
            return;
            case 28:
            
            #line 265 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 29:
            this.lower_arm_score = ((System.Windows.Controls.TextBox)(target));
            
            #line 274 "..\..\..\Reba.xaml"
            this.lower_arm_score.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 30:
            
            #line 282 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 31:
            this.txt_wrist_position = ((System.Windows.Controls.TextBox)(target));
            
            #line 301 "..\..\..\Reba.xaml"
            this.txt_wrist_position.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 32:
            
            #line 318 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBoxWristPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 33:
            
            #line 337 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionaWristTwist_Checked);
            
            #line default
            #line hidden
            return;
            case 34:
            
            #line 354 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 35:
            
            #line 363 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 36:
            
            #line 365 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionaWristTwist_Checked);
            
            #line default
            #line hidden
            return;
            case 37:
            
            #line 370 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionaWristTwist_Checked);
            
            #line default
            #line hidden
            return;
            case 38:
            
            #line 375 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionaWristTwist_Checked);
            
            #line default
            #line hidden
            return;
            case 39:
            
            #line 380 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionaWristTwist_Checked);
            
            #line default
            #line hidden
            return;
            case 40:
            
            #line 391 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 41:
            
            #line 402 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 42:
            
            #line 405 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_armWristLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 43:
            
            #line 406 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_armWristLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 44:
            
            #line 407 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_armWristLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 45:
            
            #line 408 "..\..\..\Reba.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_evaluate);
            
            #line default
            #line hidden
            return;
            case 46:
            this.statusBar = ((System.Windows.Controls.Primitives.StatusBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
