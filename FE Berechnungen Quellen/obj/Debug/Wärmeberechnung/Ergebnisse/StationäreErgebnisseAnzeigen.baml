   M S B A M L   `   `   `  ����  FE BerechnungenO    IFE_Berechnungen.Wärmeberechnung.Ergebnisse.StationäreErgebnisseAnzeigen   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  stationäre Ergebnisse��5   	   $	��450��6'   $	��800��64   .��5      � $��0,5,0,0q�6    � ColumnDefinitions 5	   
   �� 5
      $
��100* �6   �� 5      $
��150* �6   �� 5      $
��150* �6   .+�5   
   � $��0,0,10,0q�6   $��0��6!   .+�5      �� -   5         ��Name$ Knotentemperatur��5      $��Knotentemperaturen��6.   $
��Left=�6K   $��10,0,0,0q�6f   $	��Top=�6x    J� RenderTransformOrigin$ -4.609,-0.052&�6�   $	��150��6�   &   System.Windows.Controls.DataGrid  5      -   5        $��
KnotenGrid��5      $
��Left=�6)   $	��325��6D   $	��170��6Q   $��	10,25,0,0q�6]   $	��Top=�5         AlternatingRowBackground
 ��� �   AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Knoten��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� 	 �� Path$		 Key�  5      $ 
Temperatur��6)    6=   �� $	 Value.NodalDof[0]�
 �� StringFormat$
 N2��5      5      � 5   
   $��0,0,10,0q�6   $��1��6!   .+�5      �� -   5        $ TemperaturVektoren��5      $(��"Temperaturvektoren in Elementmitte��60   $
��Left=�6]   $��5,0,0,0q�6x   $	��Top=�6�   $	��274��6�     5      -   5        $��TemperaturVektorGrid��5      $
��Left=�63   $	��325��6N   $	��250��6[   $��0,25,0,0q�6g   $	��Top=�5      
 ��� � .   5         5!      $ Element��6)    6;   �� $		 Key�  5"      $ X��6)    64   �� $	 Value.ElementState[0]�$
 N2��  5#      $ Y��6)    64   �� $	 Value.ElementState[1]�$
 N2��5%      5&      � 5'   
   $��0,0,10,0q�6   $��2��6!   .+�5(      �� -   5        $ Wärmefluss��5(      $$��vordefinierte Randtemperaturen��6(   $
��Left=�6Q   $��5,0,0,0q�6l   $	��Top=�6}   $	��180��6�     5)      -   5        $��WärmeflussGrid��5)      $
��Left=�6-   $	��325��6H   $	��200��6U   $��0,25,0,0q�6a   $	��Top=�5*      
 ��� � .   5,        5-      $ Knoten��6)    6:   �� $	 Value.NodeId�  5.      $ Wärmefluss��6)    6=   �� $	 Value.Node.Reactions[0]�$
 N2��  5/      $ 
Temperatur��6)    6=   �� $	 Value.Prescribed[0]�$
 N2��51      52      53      54      