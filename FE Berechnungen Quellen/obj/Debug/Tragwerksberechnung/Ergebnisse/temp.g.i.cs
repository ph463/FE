﻿#pragma checksum "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DBBC49214E6DAD4D3A1E3ED9BB659263C286B258D432BEBD9C01AD4EB8FA2113"
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
    /// DynamikErgebnisse
    /// </summary>
    public partial class DynamikErgebnisse : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid DynamischeErgebnisse;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label knotentemperatur;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Knotenauswahl;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnKnoten;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid KnotenverformungenGrid;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label modellZustand;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Zeitschrittauswahl;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnZeitschritt;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ZeitschrittGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/FE Berechnungen;component/tragwerksberechnung/ergebnisse/temp.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
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
            this.DynamischeErgebnisse = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.knotentemperatur = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.Knotenauswahl = ((System.Windows.Controls.ComboBox)(target));
            
            #line 21 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
            this.Knotenauswahl.DropDownClosed += new System.EventHandler(this.DropDownKnotenauswahlClosed);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnKnoten = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
            this.btnKnoten.Click += new System.Windows.RoutedEventHandler(this.KnotenverformungenGrid_Anzeigen);
            
            #line default
            #line hidden
            return;
            case 5:
            this.KnotenverformungenGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.modellZustand = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.Zeitschrittauswahl = ((System.Windows.Controls.ComboBox)(target));
            
            #line 40 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
            this.Zeitschrittauswahl.DropDownClosed += new System.EventHandler(this.DropDownZeitschrittauswahlClosed);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnZeitschritt = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\..\Tragwerksberechnung\Ergebnisse\temp.xaml"
            this.btnZeitschritt.Click += new System.Windows.RoutedEventHandler(this.ZeitschrittGrid_Anzeigen);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ZeitschrittGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

