﻿#pragma checksum "..\..\..\..\Elastizitätsberechnung\Ergebnisse\Statikergebnisse.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6BBB56F23F556E4BB8468CD313BD824D89E5B1CCAF9880BED1623D93A8983FD4"
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


namespace FE_Berechnungen.Elastizitätsberechnung.Ergebnisse {
    
    
    /// <summary>
    /// Statikergebnisse
    /// </summary>
    public partial class Statikergebnisse : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\Elastizitätsberechnung\Ergebnisse\Statikergebnisse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label knotenverformungen;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Elastizitätsberechnung\Ergebnisse\Statikergebnisse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid KnotenverformungenGrid;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Elastizitätsberechnung\Ergebnisse\Statikergebnisse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label elementspannungen;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Elastizitätsberechnung\Ergebnisse\Statikergebnisse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ElementspannungenGrid;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Elastizitätsberechnung\Ergebnisse\Statikergebnisse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label reaktionen;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\Elastizitätsberechnung\Ergebnisse\Statikergebnisse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ReaktionenGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/FE Berechnungen;component/elastizit%c3%a4tsberechnung/ergebnisse/statikergebniss" +
                    "e.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Elastizitätsberechnung\Ergebnisse\Statikergebnisse.xaml"
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
            this.knotenverformungen = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.KnotenverformungenGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 20 "..\..\..\..\Elastizitätsberechnung\Ergebnisse\Statikergebnisse.xaml"
            this.KnotenverformungenGrid.Loaded += new System.Windows.RoutedEventHandler(this.Knoten_Loaded);
            
            #line default
            #line hidden
            return;
            case 3:
            this.elementspannungen = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.ElementspannungenGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 35 "..\..\..\..\Elastizitätsberechnung\Ergebnisse\Statikergebnisse.xaml"
            this.ElementspannungenGrid.Loaded += new System.Windows.RoutedEventHandler(this.ElementeGrid_Loaded);
            
            #line default
            #line hidden
            return;
            case 5:
            this.reaktionen = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.ReaktionenGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 53 "..\..\..\..\Elastizitätsberechnung\Ergebnisse\Statikergebnisse.xaml"
            this.ReaktionenGrid.Loaded += new System.Windows.RoutedEventHandler(this.Reaktionen_Loaded);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

