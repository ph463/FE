﻿#pragma checksum "..\..\..\..\Elastizitätsberechnung\ElastizitätErgebnisse\Statik2DErgebnisse.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DF160912ACA937809F238BF0330C70DF941B07C6875203D4A84B55573CF05975"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
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


namespace FE_Berechnungen.Elastizitätsberechnung.ElastizitätErgebnisse {
    
    
    /// <summary>
    /// Statik2DErgebnisse
    /// </summary>
    public partial class Statik2DErgebnisse : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\Elastizitätsberechnung\ElastizitätErgebnisse\Statik2DErgebnisse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label knotenverformungen2D;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Elastizitätsberechnung\ElastizitätErgebnisse\Statik2DErgebnisse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Knotenverformungen2DGrid;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\Elastizitätsberechnung\ElastizitätErgebnisse\Statik2DErgebnisse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label elementspannungen2D;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Elastizitätsberechnung\ElastizitätErgebnisse\Statik2DErgebnisse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Elementspannungen2DGrid;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Elastizitätsberechnung\ElastizitätErgebnisse\Statik2DErgebnisse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label reaktionen2D;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\Elastizitätsberechnung\ElastizitätErgebnisse\Statik2DErgebnisse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Reaktionen2DGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/FE Berechnungen;component/elastizit%c3%a4tsberechnung/elastizit%c3%a4tergebnisse" +
                    "/statik2dergebnisse.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Elastizitätsberechnung\ElastizitätErgebnisse\Statik2DErgebnisse.xaml"
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
            this.knotenverformungen2D = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.Knotenverformungen2DGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 20 "..\..\..\..\Elastizitätsberechnung\ElastizitätErgebnisse\Statik2DErgebnisse.xaml"
            this.Knotenverformungen2DGrid.Loaded += new System.Windows.RoutedEventHandler(this.Knoten2D_Loaded);
            
            #line default
            #line hidden
            return;
            case 3:
            this.elementspannungen2D = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.Elementspannungen2DGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 34 "..\..\..\..\Elastizitätsberechnung\ElastizitätErgebnisse\Statik2DErgebnisse.xaml"
            this.Elementspannungen2DGrid.Loaded += new System.Windows.RoutedEventHandler(this.Elemente2DGrid_Loaded);
            
            #line default
            #line hidden
            return;
            case 5:
            this.reaktionen2D = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.Reaktionen2DGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 49 "..\..\..\..\Elastizitätsberechnung\ElastizitätErgebnisse\Statik2DErgebnisse.xaml"
            this.Reaktionen2DGrid.Loaded += new System.Windows.RoutedEventHandler(this.Reaktionen2D_Loaded);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

