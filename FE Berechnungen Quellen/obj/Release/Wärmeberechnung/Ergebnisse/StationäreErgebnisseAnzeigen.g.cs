#pragma checksum "..\..\..\..\Wärmeberechnung\Ergebnisse\StationäreErgebnisseAnzeigen.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3BCD9DADB9E18F2CC278465EA47B3503F5ED65AE1A6E1C5C0A7B5D7682CAC1AE"
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


namespace FE_Berechnungen.Wärmeberechnung.Ergebnisse {
    
    
    /// <summary>
    /// StationäreErgebnisseAnzeigen
    /// </summary>
    public partial class StationäreErgebnisseAnzeigen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\Wärmeberechnung\Ergebnisse\StationäreErgebnisseAnzeigen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label knotentemperatur;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Wärmeberechnung\Ergebnisse\StationäreErgebnisseAnzeigen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid KnotenGrid;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Wärmeberechnung\Ergebnisse\StationäreErgebnisseAnzeigen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label temperaturVektoren;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Wärmeberechnung\Ergebnisse\StationäreErgebnisseAnzeigen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid TemperaturVektorGrid;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Wärmeberechnung\Ergebnisse\StationäreErgebnisseAnzeigen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label wärmefluss;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Wärmeberechnung\Ergebnisse\StationäreErgebnisseAnzeigen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid WärmeflussGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/FE Berechnungen;component/w%c3%a4rmeberechnung/ergebnisse/station%c3%a4reergebni" +
                    "sseanzeigen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Wärmeberechnung\Ergebnisse\StationäreErgebnisseAnzeigen.xaml"
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
            this.knotentemperatur = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.KnotenGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 19 "..\..\..\..\Wärmeberechnung\Ergebnisse\StationäreErgebnisseAnzeigen.xaml"
            this.KnotenGrid.Loaded += new System.Windows.RoutedEventHandler(this.Knoten_Loaded);
            
            #line default
            #line hidden
            return;
            case 3:
            this.temperaturVektoren = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.TemperaturVektorGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 31 "..\..\..\..\Wärmeberechnung\Ergebnisse\StationäreErgebnisseAnzeigen.xaml"
            this.TemperaturVektorGrid.Loaded += new System.Windows.RoutedEventHandler(this.TemperaturVektoren_Loaded);
            
            #line default
            #line hidden
            return;
            case 5:
            this.wärmefluss = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.WärmeflussGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 43 "..\..\..\..\Wärmeberechnung\Ergebnisse\StationäreErgebnisseAnzeigen.xaml"
            this.WärmeflussGrid.Loaded += new System.Windows.RoutedEventHandler(this.Wärmefluss_Loaded);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

