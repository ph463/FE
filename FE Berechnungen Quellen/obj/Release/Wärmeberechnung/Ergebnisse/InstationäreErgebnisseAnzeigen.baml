   M S B A M L   `   `   `  ����   FE BerechnungenQ    KFE_Berechnungen.Wärmeberechnung.Ergebnisse.InstationäreErgebnisseAnzeigen   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  instationäre Ergebnisse��$	��680��$	��790��.��� -    �Name$ InstationaereErgebnisse��$��	5,10,15,2q� � ColumnDefinitions �� $
��Auto �$	��350���� $
��Auto �$	��420�� � RowDefinitions �� $��68 ��� $
��Auto �$��570.4��.+��� -    ��Name$ knotentemperatur��$��Knoten Zeitverlauf��$
��Left=�$��5,0,0,39q�$��0��$	��130���� $��Auswahl des Knotens��$
��Left=�$��5,30,0,0q�$	��Top=�$	��200��$��25���� -   $��Knotenauswahl��$
��Left=�$��
136,30,0,0q�$	��Top=�$	��100��$��0��$��22���� -   $��	btnKnoten��$��0��$��0��$��250,33,57.4,15q�.��
Anzeigen&   System.Windows.Controls.DataGrid  -   $��KnotentemperaturGrid��$
��Left=�$	��550��$	��358��$��4,6,0,0q�$��0��$��1��$	��Top=�   AlternatingRowBackground
 ��� �   AutoGenerateColumns .     HeadersVisibility$
 None=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� 
 �� Path$	
 Key�  	 �� $
 Value��� -   $ zeitschritt��$/��)Modellzustand an festgelegtem Zeitschritt��$
��Left=�$��
4.8,0,0,39q�$��1��$	��242���� $"��Auswahl der Integrationszeit��$
��Left=�$��
4.8,30,0,0q�$	��Top=�$	��163��$��28��$��1���� -   $��Zeitschrittauswahl��$
��Left=�$��169.8,30,0,0q�$	��Top=�$	��100��$��1��$��23�� �� ItemStringFormat$ N2���� -   $��btnZeit��$��1��$��0��$��287.6,33,79.4,15q�.��
Anzeigen  -	   $��ZeitschrittGrid��$
��Left=�$	��550��$	��394��$��7,6,0,0q�$��1��$��1��$	��Top=�
 ��� � .  $
 None=�   	 �� $	
 Key�  	 �� $
 Value�