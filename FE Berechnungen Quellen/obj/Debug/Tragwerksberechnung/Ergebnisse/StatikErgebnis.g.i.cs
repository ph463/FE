﻿#pragma checksum "..\..\..\..\Tragwerksberechnung\Ergebnisse\StatikErgebnis.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D27CECF004DEAC0605824ABDFC7BBA940D0D607D11B706599C08CC4DBDA7703F"
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


namespace FE_Berechnungen.Tragwerksberechnung.Ergebnisse {
    
    
    /// <summary>
    /// Ergebnisausgabe
    /// </summary>
    public partial class Ergebnisausgabe : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\Tragwerksberechnung\Ergebnisse\StatikErgebnis.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label knotenverformungen;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Tragwerksberechnung\Ergebnisse\StatikErgebnis.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid KnotenverformungenGrid;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Tragwerksberechnung\Ergebnisse\StatikErgebnis.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label elementEndkraefte;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Tragwerksberechnung\Ergebnisse\StatikErgebnis.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ElementendkraefteGrid;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Tragwerksberechnung\Ergebnisse\StatikErgebnis.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lagerreaktionen;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\Tragwerksberechnung\Ergebnisse\StatikErgebnis.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid LagerreaktionenGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/FE Berechnungen;component/tragwerksberechnung/ergebnisse/statikergebnis.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Tragwerksberechnung\Ergebnisse\StatikErgebnis.xaml"
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
            
            #line 20 "..\..\..\..\Tragwerksberechnung\Ergebnisse\StatikErgebnis.xaml"
            this.KnotenverformungenGrid.Loaded += new System.Windows.RoutedEventHandler(this.Knotenverformungen_Loaded);
            
            #line default
            #line hidden
            return;
            case 3:
            this.elementEndkraefte = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.ElementendkraefteGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 35 "..\..\..\..\Tragwerksberechnung\Ergebnisse\StatikErgebnis.xaml"
            this.ElementendkraefteGrid.Loaded += new System.Windows.RoutedEventHandler(this.Elementendkraefte_Loaded);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lagerreaktionen = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.LagerreaktionenGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 53 "..\..\..\..\Tragwerksberechnung\Ergebnisse\StatikErgebnis.xaml"
            this.LagerreaktionenGrid.Loaded += new System.Windows.RoutedEventHandler(this.Lagerreaktionen_Loaded);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

