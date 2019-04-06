﻿#pragma checksum "..\..\..\Views\AppMainView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9B91ED816B59C97AD0726426DCD5F3E6280DED31"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using SourceChord.FluentWPF;
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
using TravelApp.Views;


namespace TravelApp.Views {
    
    
    /// <summary>
    /// AppMainView
    /// </summary>
    public partial class AppMainView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\Views\AppMainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel pnlLeftMenu;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Views\AppMainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CurrentUserLoginTextBlock;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Views\AppMainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageBrush LoginImage;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\Views\AppMainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLeftMenuHide;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\Views\AppMainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLeftMenuShow;
        
        #line default
        #line hidden
        
        
        #line 132 "..\..\..\Views\AppMainView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CurrentReviewedContentTopLabel;
        
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
            System.Uri resourceLocater = new System.Uri("/TravelApp;component/views/appmainview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\AppMainView.xaml"
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
            this.pnlLeftMenu = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.CurrentUserLoginTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.LoginImage = ((System.Windows.Media.ImageBrush)(target));
            return;
            case 4:
            this.btnLeftMenuHide = ((System.Windows.Controls.Button)(target));
            
            #line 124 "..\..\..\Views\AppMainView.xaml"
            this.btnLeftMenuHide.Click += new System.Windows.RoutedEventHandler(this.btnLeftMenuHide_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnLeftMenuShow = ((System.Windows.Controls.Button)(target));
            
            #line 125 "..\..\..\Views\AppMainView.xaml"
            this.btnLeftMenuShow.Click += new System.Windows.RoutedEventHandler(this.btnLeftMenuShow_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CurrentReviewedContentTopLabel = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

