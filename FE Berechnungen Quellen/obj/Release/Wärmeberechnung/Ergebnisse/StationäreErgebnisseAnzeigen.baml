   M S B A M L   `   `   `  ����   FE BerechnungenO    IFE_Berechnungen.Wärmeberechnung.Ergebnisse.StationäreErgebnisseAnzeigen   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  stationäre Ergebnisse��$	��450��$	��800��.��� $��0,5,0,0q� � ColumnDefinitions �� $
��100* ��� $
��150* ��� $
��150* �.+�� $��0,0,10,0q�$��0��.+��� -    ��Name$ knotentemperatur��$��Knotentemperaturen��$
��Left=�$��10,0,0,0q�$	��Top=� J� RenderTransformOrigin$ -4.609,-0.052&�$	��150��&   System.Windows.Controls.DataGrid  -   $��
KnotenGrid��$
��Left=�$	��325��$	��170��$��	10,25,0,0q�$	��Top=�   AlternatingRowBackground
 ��� �   AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Knoten��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� 	 �� Path$		 Key�  $ 
Temperatur�� �� $	 Value.NodalDOF[0]�
 �� StringFormat$
 N2��� $��0,0,10,0q�$��1��.+��� -   $ temperaturVektoren��$(��"Temperaturvektoren in Elementmitte��$
��Left=�$��5,0,0,0q�$	��Top=�$	��274��  -   $��TemperaturVektorGrid��$
��Left=�$	��325��$	��250��$��0,25,0,0q�$	��Top=�
 ��� � .     $ Element�� �� $		 Key�  $ X�� �� $	 Value.ElementState[0]�$
 N2��  $ Y�� �� $	 Value.ElementState[1]�$
 N2��� $��0,0,10,0q�$��2��.+��� -   $ wärmefluss��$$��vordefinierte Randtemperaturen��$
��Left=�$��5,0,0,0q�$	��Top=�$	��180��  -   $��WärmeflussGrid��$
��Left=�$	��325��$	��200��$��0,25,0,0q�$	��Top=�
 ��� � .     $ Knoten�� �� $	 Value.NodeId�  $ Wärmefluss�� �� $	 Value.Reactions[0]�$
 N2��  $ 
Temperatur�� �� $	 Value.Prescribed[0]�$
 N2��