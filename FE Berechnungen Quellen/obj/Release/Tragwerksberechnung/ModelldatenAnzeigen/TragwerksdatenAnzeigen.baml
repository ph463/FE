   M S B A M L   `   `   `  ����   FE BerechnungenT    NFE_Berechnungen.Tragwerksberechnung.ModelldatenAnzeigen.TragwerksdatenAnzeigen   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  TragwerksdatenAnzeigen��$��519.2��$��830.4��.��� $��0,0,0,0q� � ColumnDefinitions �� $
��140* ��� $
��220* ��� $
��220* ��� $
��220* � � RowDefinitions �� $	��50* ��� $	��40* ��� $	��57* ��� $	��62* �.+�� $��0,0,0,0q�$��0��$��4��.+��� -    ��Name$ knotendefinitionen��$��Knotendefinitionen��$
��Left=�$��0,0,0,0q�$	��Top=�$	��113��&   System.Windows.Controls.DataGrid  -   $��
KnotenGrid��$��0,20,0,0q�$��0��   AlternatingRowBackground
 ��� �   AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Knoten��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $ X�� �� *Coordinates[0]+  $ Y�� �� *Coordinates[1]+� $��0,0,0,0q�$��1��$��4��.+��� -   $ elementdefinitionen��$��Elementdefinitionen��$
��Left=�$	��Top=�$	��198��  -   $��ElementGrid��$
��Left=�$	��220��$��
0,20,0,0.6q�
 ��� � .     $ Element�� �� *	ElementId+	 �� StringFormat$	 N2��  $ k1�� �� *
NodeIds[0]+$	 N2��  $ k2�� �� *
NodeIds[1]+$	 N2��  $ Material�� �� *ElementMaterialId+  $ Querschnitt�� �� *ElementCrossSectionId+  $ Gelenk�� �� *Type+� $��0,0,0,0q�$��2��$��0��.+��� -   $ materialdefinitionen��$��Materialdefinitionen��$
��Left=�$��0,0,0,0q�$	��Top=�$	��158��$��55��  -   $��MaterialGrid��$��0,20,0.4,-5.2q�
 ��� � .     $ Material�� �� *
MaterialId+  $ E-Modul�� �� *MaterialWerte[0]+$	 G3��  $ Poisson�� �� *MaterialWerte[1]+$	 G3��  $ Masse�� �� *MaterialWerte[2]+$	 G3��� $��0,0,0,0q�$��2��$��1��.+��� -   $ federdefinitionen��$��Federdefinitionen��$
��Left=�$��0,0,0,0q�$	��Top=�$	��158��$��24��  -   $��	FederGrid��$��0,20,0,0q�
 ��� � .     $ Feder�� �� *
MaterialId+  $ kx�� �� *MaterialWerte[0]+$	 G3��  $ ky�� �� *MaterialWerte[1]+$	 G3��  $
 kphi�� �� *MaterialWerte[2]+$	 G3��� $��0,0,0,0q�$��2��$��2��.+��� -	   $ querschnittdefinitionen��$��Querschnittdefinitionen��$
��Left=�$��0,0,0,0q�$	��Top=�$	��158��$��24��  -
   $��QuerschnittGrid��$��0,20,0,0q�
 ��� � .     $ Querschnitt�� �� *QuerschnittId+  $ Fläche�� �� *QuerschnittsWerte[0]+$	 N4��  $	 Ixx�� �� *QuerschnittsWerte[1]+$	 E2��� $��0,0,0,0q�$��2��$��3��.+��� -   $ lagerdefinitionen��$��Lagerdefinitionen��$	��Top=�$��27��$��0,0,0,0q�  -   $��	LagerGrid��$��0,20,0,0q�
 ��� � .     $ Knoten�� �� *NodeId+  $ X fest�� �� *Prescribed[0]+$	 N2��  $ Y fest�� �� *Prescribed[1]+$	 N2��  $ R fest�� �� *Prescribed[2]+$	 N2��� $��0,0,0,0q�$��3��$��0��$��2��.+��� -   $ knotenlastdefinitionen��$��Knotenlastdefinitionen��$
��Left=�$��0,0,0,0q�$	��Top=�$	��160��  -   $��KnotenlastGrid��$��0,20,0,0q�
 ��� � .     $ Knoten�� �� *NodeId+  $ px�� �� *Intensity[0]+$	 N2��  $ py�� �� *Intensity[1]+$	 N2��  $ Moment�� �� *Intensity[2]+$	 N2��� $��0,0,0,0q�$��3��$��3��.+��� -   $ punktlastdefinitionen��$��Punktlastdefinitionen��$
��Left=�$��0,0,0,0q�$	��Top=�$	��160��  -   $��PunktlastGrid��$��0,20,0,0q�
 ��� � .     $ Element�� �� *	ElementId+  $ Offset�� �� *Offset+$	 N2��  $ fx�� �� *Intensity[0]+$	 N2��  $ fy�� �� *Intensity[1]+$	 N2��� $��0,0,0,0q�$��2��$��3��.+��� -   $ linienlastdefinitionen��$��Linienlastdefinitionen��$
��Left=�$	��Top=�$	��160��$��25��  -   $��LinienlastGrid��$��0,20,0,0q�
 ��� � .     $ Element�� �� *	ElementId+  $	 p1x�� �� *Intensity[0]+$	 N2��  $	 p1y�� �� *Intensity[1]+$	 N2��  $	 p2x�� �� *Intensity[2]+$	 N2��  $	 p2y�� �� *Intensity[3]+$	 N2��