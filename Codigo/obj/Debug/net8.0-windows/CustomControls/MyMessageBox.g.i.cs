﻿#pragma checksum "..\..\..\..\CustomControls\MyMessageBox.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B3331FE095D68537A33D1CB6B1D4E85F43B66976"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using BS360.CustomControls;
using FontAwesome.Sharp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace BS360.CustomControls {
    
    
    /// <summary>
    /// MyMessageBox
    /// </summary>
    public partial class MyMessageBox : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 51 "..\..\..\..\CustomControls\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel pnlControl;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\CustomControls\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\CustomControls\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TBLOCK_Title;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\CustomControls\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TBLOCK_Message;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\CustomControls\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel SP_ContainsButton;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\CustomControls\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_OK;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\..\CustomControls\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextButton;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\..\CustomControls\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_YES;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\..\CustomControls\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_NO;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\..\..\CustomControls\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTN_CANCEL;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BS360;V1.0.0;component/customcontrols/mymessagebox.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\CustomControls\MyMessageBox.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.pnlControl = ((System.Windows.Controls.StackPanel)(target));
            
            #line 58 "..\..\..\..\CustomControls\MyMessageBox.xaml"
            this.pnlControl.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.pnlControl_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 59 "..\..\..\..\CustomControls\MyMessageBox.xaml"
            this.pnlControl.MouseEnter += new System.Windows.Input.MouseEventHandler(this.pnlControl_MouseEnter);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\..\..\CustomControls\MyMessageBox.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.Button_Click_CANCEL);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TBLOCK_Title = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.TBLOCK_Message = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.SP_ContainsButton = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            this.BTN_OK = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\..\..\CustomControls\MyMessageBox.xaml"
            this.BTN_OK.Click += new System.Windows.RoutedEventHandler(this.Button_Click_OK);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TextButton = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.BTN_YES = ((System.Windows.Controls.Button)(target));
            
            #line 117 "..\..\..\..\CustomControls\MyMessageBox.xaml"
            this.BTN_YES.Click += new System.Windows.RoutedEventHandler(this.Button_Click_YES);
            
            #line default
            #line hidden
            return;
            case 9:
            this.BTN_NO = ((System.Windows.Controls.Button)(target));
            
            #line 139 "..\..\..\..\CustomControls\MyMessageBox.xaml"
            this.BTN_NO.Click += new System.Windows.RoutedEventHandler(this.Button_Click_NO);
            
            #line default
            #line hidden
            return;
            case 10:
            this.BTN_CANCEL = ((System.Windows.Controls.Button)(target));
            
            #line 163 "..\..\..\..\CustomControls\MyMessageBox.xaml"
            this.BTN_CANCEL.Click += new System.Windows.RoutedEventHandler(this.Button_Click_CANCEL);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
