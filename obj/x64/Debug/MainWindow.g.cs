﻿#pragma checksum "..\..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7C6B933C4CC1A785ADD99524783CF33B"
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
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 71 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lbl_body;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img_status;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lb_orientations;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox upper_arm_score;
        
        #line default
        #line hidden
        
        
        #line 179 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lower_arm_score;
        
        #line default
        #line hidden
        
        
        #line 193 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox lower_arm_add1;
        
        #line default
        #line hidden
        
        
        #line 216 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_wrist_position;
        
        #line default
        #line hidden
        
        
        #line 398 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox neck_position_score;
        
        #line default
        #line hidden
        
        
        #line 430 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox trunk_poosition_score;
        
        #line default
        #line hidden
        
        
        #line 553 "..\..\..\MainWindow.xaml"
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
            System.Uri resourceLocater = new System.Uri("/BodyBasics-WPF;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
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
            
            #line 6 "..\..\..\MainWindow.xaml"
            ((ProjectK.ErgoMC.Assessment.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.MainWindow_Loaded);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\MainWindow.xaml"
            ((ProjectK.ErgoMC.Assessment.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.MainWindow_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 39 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 70 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked_1);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lbl_body = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.img_status = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.lb_orientations = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            
            #line 106 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.upper_arm_score = ((System.Windows.Controls.TextBox)(target));
            
            #line 120 "..\..\..\MainWindow.xaml"
            this.upper_arm_score.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 140 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalUpperArm_Checked);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 149 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalUpperArm_Checked);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 157 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalUpperArm_Checked);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 166 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.lower_arm_score = ((System.Windows.Controls.TextBox)(target));
            
            #line 178 "..\..\..\MainWindow.xaml"
            this.lower_arm_score.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 14:
            this.lower_arm_add1 = ((System.Windows.Controls.CheckBox)(target));
            
            #line 195 "..\..\..\MainWindow.xaml"
            this.lower_arm_add1.Checked += new System.Windows.RoutedEventHandler(this.CheckBoxLowerArmPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 205 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.txt_wrist_position = ((System.Windows.Controls.TextBox)(target));
            
            #line 225 "..\..\..\MainWindow.xaml"
            this.txt_wrist_position.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 242 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBoxWristPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 258 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 270 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 290 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionaWristTwist_Checked);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 296 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionaWristTwist_Checked);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 313 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 325 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 24:
            
            #line 334 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.ArmMuscleUse_Checked);
            
            #line default
            #line hidden
            return;
            case 25:
            
            #line 346 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 26:
            
            #line 358 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 27:
            
            #line 361 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_armWristLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 28:
            
            #line 362 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_armWristLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 29:
            
            #line 363 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_armWristLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 30:
            
            #line 364 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_armWristLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 31:
            
            #line 384 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 32:
            this.neck_position_score = ((System.Windows.Controls.TextBox)(target));
            
            #line 397 "..\..\..\MainWindow.xaml"
            this.neck_position_score.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 33:
            
            #line 404 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalNeckPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 34:
            
            #line 408 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalNeckPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 35:
            
            #line 417 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 36:
            this.trunk_poosition_score = ((System.Windows.Controls.TextBox)(target));
            
            #line 429 "..\..\..\MainWindow.xaml"
            this.trunk_poosition_score.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 37:
            
            #line 438 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalTrunkPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 38:
            
            #line 446 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalTrunkPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 39:
            
            #line 455 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 40:
            
            #line 467 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 41:
            
            #line 474 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalLegPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 42:
            
            #line 482 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_additionalLegPosition_Checked);
            
            #line default
            #line hidden
            return;
            case 43:
            
            #line 493 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 44:
            
            #line 505 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 45:
            
            #line 509 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.NeckMuscleUse_Checked);
            
            #line default
            #line hidden
            return;
            case 46:
            
            #line 521 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 47:
            
            #line 533 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 48:
            
            #line 537 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_neckForceLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 49:
            
            #line 538 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_neckForceLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 50:
            
            #line 539 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_neckForceLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 51:
            
            #line 540 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.rdb_neckForceLoad_Checked);
            
            #line default
            #line hidden
            return;
            case 52:
            
            #line 544 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_evaluate);
            
            #line default
            #line hidden
            return;
            case 53:
            this.statusBar = ((System.Windows.Controls.Primitives.StatusBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

