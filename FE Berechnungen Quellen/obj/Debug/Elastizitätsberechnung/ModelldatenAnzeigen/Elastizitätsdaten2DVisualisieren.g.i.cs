﻿#pragma checksum "..\..\..\..\Elastizitätsberechnung\ModelldatenAnzeigen\Elastizitätsdaten2DVisualisieren.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9408FE928D487D56105C7264BF4B02BD76F46118B54F26E2394600D361621AF3"
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


namespace FE_Berechnungen.Elastizitätsberechnung.ModelldatenAnzeigen {
    
    
    /// <summary>
    /// Elastizitätsdaten2DVisualisieren
    /// </summary>
    public partial class Elastizitätsdaten2DVisualisieren : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\Elastizitätsberechnung\ModelldatenAnzeigen\Elastizitätsdaten2DVisualisieren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Elastizitätsmodell;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\Elastizitätsberechnung\ModelldatenAnzeigen\Elastizitätsdaten2DVisualisieren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button KnotenLasten;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Elastizitätsberechnung\ModelldatenAnzeigen\Elastizitätsdaten2DVisualisieren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ElementLasten;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Elastizitätsberechnung\ModelldatenAnzeigen\Elastizitätsdaten2DVisualisieren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Randbedingungen;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\Elastizitätsberechnung\ModelldatenAnzeigen\Elastizitätsdaten2DVisualisieren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas VisualModel;
        
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
            System.Uri resourceLocater = new System.Uri("/FE Berechnungen;component/elastizit%c3%a4tsberechnung/modelldatenanzeigen/elasti" +
                    "zit%c3%a4tsdaten2dvisualisieren.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Elastizitätsberechnung\ModelldatenAnzeigen\Elastizitätsdaten2DVisualisieren.xaml"
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
            this.Elastizitätsmodell = ((System.Windows.Controls.Grid)(target));
            
            #line 10 "..\..\..\..\Elastizitätsberechnung\ModelldatenAnzeigen\Elastizitätsdaten2DVisualisieren.xaml"
            this.Elastizitätsmodell.Loaded += new System.Windows.RoutedEventHandler(this.ModelGrid_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.KnotenLasten = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\..\Elastizitätsberechnung\ModelldatenAnzeigen\Elastizitätsdaten2DVisualisieren.xaml"
            this.KnotenLasten.Click += new System.Windows.RoutedEventHandler(this.Toggle_KnotenLasten);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ElementLasten = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\..\Elastizitätsberechnung\ModelldatenAnzeigen\Elastizitätsdaten2DVisualisieren.xaml"
            this.ElementLasten.Click += new System.Windows.RoutedEventHandler(this.Toggle_ElementLasten);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Randbedingungen = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\Elastizitätsberechnung\ModelldatenAnzeigen\Elastizitätsdaten2DVisualisieren.xaml"
            this.Randbedingungen.Click += new System.Windows.RoutedEventHandler(this.Toggle_Randbedingungen);
            
            #line default
            #line hidden
            return;
            case 5:
            this.VisualModel = ((System.Windows.Controls.Canvas)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

