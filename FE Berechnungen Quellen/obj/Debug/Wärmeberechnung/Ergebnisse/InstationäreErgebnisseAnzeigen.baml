   M S B A M L   `   `   `  ����  FE BerechnungenQ    KFE_Berechnungen.Wärmeberechnung.Ergebnisse.InstationäreErgebnisseAnzeigen   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  instationäre Ergebnisse��5   	   $	��680��6)   $	��790��66   .��5      � $��	5,10,15,2q�6    � ColumnDefinitions 5	   
   �� 5
      $
��Auto �6   $	��350��6,   �� 5      $
��Auto �6   $	��420��6,    � RowDefinitions 5   
   �� 5      $��68 �6   �� 5      $
��Auto �6   $��570.4��6*   .+�5   
   �� -   5         ��Name$ Knotentemperatur��5      $��Knoten Zeitverlauf��6*   $
��Left=�6G   $��5,0,0,39q�6b   $��0��6t   $	��130��6�   �� 5   
   $��0��6   $��0��6   $��Auswahl des Knotens��6-   $
��Left=�6K   $��5,30,0,0q�6f   $	��Top=�6x   $	��200��6�   $��25��6�   �� 5   
   -   5        $��Knotenauswahl��5      $
��Left=�6(   $��
136,30,0,0q�6C   $	��Top=�6W   $	��100��6o   $��0��5      $��0��6    $��22��60   5      �� 5   
   -   5        $��	BtnKnoten��5      $��0��6"   $��0��62   $��250,33,57.4,15q�6e   .��6}   
Anzeigen6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��KnotentemperaturGrid��5      $
��Left=�6/   $	��550��6J   $	��358��6W   $��4,6,0,0q�6c   $��0��6t   $��1��6�   $	��Top=�5         AlternatingRowBackground
 ��� �   AutoGenerateColumns .     HeadersVisibility$
 None=�6g      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� 	 �� Path$		 Key�  5       6%   �� $	 Value�5      �� 5!   
   -   5        $ Zeitschritt��5!      $/��)Modellzustand an festgelegtem Zeitschritt��6%   $
��Left=�6Y   $��
4.8,0,0,39q�6t   $��0��5"      $��1��6   $	��242��6-   �� 5#   
   $"��Auswahl der Integrationszeit��6   $
��Left=�67   $��
4.8,30,0,0q�6R   $	��Top=�6f   $	��163��6~   $��28��6�   $��0��5$      $��1��6   �� 5%   
   -   5        $��Zeitschrittauswahl��5%      $
��Left=�6-   $��169.8,30,0,0q�6H   $	��Top=�6^   $	��100��6v   $��0��5&      $��1��6    $��23��60   
 �� ItemStringFormat$
 N2��5'   E   �� 5(   
   -   5        $��BtnZeit��5(      $��1��6    $��0��60   $��287.6,33,79.4,15q�6_   .��6y   
Anzeigen6�     5*   
   -   5        $��ZeitschrittGrid��5*      $
��Left=�6*   $	��550��6E   $	��394��6R   $��7,6,0,0q�6^   $��1��6o   $��1��6   $	��Top=�5+      
 ��� � .  $
 None=�6g    5,        5-       6%   �� $		 Key�  5.       6%   �� $	 Value�50      51      52      