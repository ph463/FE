﻿#pragma checksum "..\..\..\..\Tragwerksberechnung\TragwerksErgebnisse\Eigenfrequenzen.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "116271B9957152FC5BA16552C67E1610F1C679E42CC2CA0A5B0962FF6D741D3D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using FE_Berechnungen.Tragwerksberechnung.TragwerksErgebnisse;
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


namespace FE_Berechnungen.Tragwerksberechnung.TragwerksErgebnisse {
    
    
    /// <summary>
    /// Eigenfrequenzen
    /// </summary>
    public partial class Eigenfrequenzen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\Tragwerksberechnung\TragwerksErgebnisse\Eigenfrequenzen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label eigenfrequenzen;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Tragwerksberechnung\TragwerksErgebnisse\Eigenfrequenzen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid EigenfrequenzenGrid;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Tragwerksberechnung\TragwerksErgebnisse\Eigenfrequenzen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label eigenvektoren;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Tragwerksberechnung\TragwerksErgebnisse\Eigenfrequenzen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid EigenvektorenGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/FE Berechnungen;component/tragwerksberechnung/tragwerksergebnisse/eigenfrequenze" +
                    "n.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Tragwerksberechnung\TragwerksErgebnisse\Eigenfrequenzen.xaml"
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
            this.eigenfrequenzen = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.EigenfrequenzenGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 27 "..\..\..\..\Tragwerksberechnung\TragwerksErgebnisse\Eigenfrequenzen.xaml"
            this.EigenfrequenzenGrid.Loaded += new System.Windows.RoutedEventHandler(this.EigenfrequenzenGrid_Loaded);
            
            #line default
            #line hidden
            return;
            case 3:
            this.eigenvektoren = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.EigenvektorenGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 44 "..\..\..\..\Tragwerksberechnung\TragwerksErgebnisse\Eigenfrequenzen.xaml"
            this.EigenvektorenGrid.Loaded += new System.Windows.RoutedEventHandler(this.EigenvektorenGrid_Loaded);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
