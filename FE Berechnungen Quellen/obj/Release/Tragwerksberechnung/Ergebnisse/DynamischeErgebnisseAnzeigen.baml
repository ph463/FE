   M S B A M L   `   `   `  ����   FE BerechnungenQ    KFE_Berechnungen.Tragwerksberechnung.Ergebnisse.DynamischeErgebnisseAnzeigen   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$.  (Ergebnisse der zeitabhänigen Berechnung��$	��500��$	��800��.��� -    �Name$ DynamischeErgebnisse��$��0,5,2.4,0.4q� � ColumnDefinitions �� $
��Auto �$	��400���� $
��Auto �$��404.8�� � RowDefinitions �� $��68 ��� $
��Auto �$	��300��.+��� -    ��Name$ knotenzeitverlauf��$��Knoten Zeitverlauf��$
��Left=�$��
5,0,0,38.8q�$��0��$	��130���� $��Auswahl des Knotens��$
��Left=�$��5,30,0,0q�$	��Top=�$	��200��$��25���� -   $��Knotenauswahl��$
��Left=�$��
136,30,0,0q�$	��Top=�$	��100��$��0��$��22���� -   $��	btnKnoten��$��0��$��0��$��250,33,62,14.8q�.��
Anzeigen&   System.Windows.Controls.DataGrid  -   $��KnotenverformungenGrid��$
��Left=�$	��390��$��5,0.2,0,-0.2q�   AlternatingRowBackground
 ��� �   AutoGenerateColumns .  $��0��$��1��$	��396��$	��Top=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Zeit��1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *Zeit+
 �� StringFormat$
 N2��  $ DeltaX��	 �� *VerformungX+$
 N4��  $ DeltaY��	 �� *VerformungY+$
 N4��  $	 Phi��	 �� *
Verdrehung+$
 N4��  $
 AccX��	 �� *BeschleunigungX+$
 N4��  $
 AccY��	 �� *BeschleunigungY+$
 N4��  $ AccPhi��	 �� *BeschleunigungPhi+$
 N4���� -   $ modellZustand��$5��/Modellzustand an einem festgelegtem Zeitschritt��$
��Left=�$��
5,0,0,38.8q�$��1��$��0��$	��285���� $��Auswahl des Zeitschritts��$
��Left=�$��5,30,0,0q�$	��Top=�$��1��$��0��$	��200��$��25���� -   $��Zeitschrittauswahl��$
��Left=�$��
154,30,0,0q�$	��Top=�$	��100��$��1��$��22�� �� ItemStringFormat$ N2���� -   $��btnZeitschritt��$��1��$��0��$��268,33,43.8,14.8q�.��
Anzeigen  -	   $��ZeitschrittGrid��$
��Left=�$	��390��$��5,0.2,0,-0.2q�
 ��� � .  $��1��$��1��$	��396��$	��Top=�   $ Knoten��	 �� *Knoten+  $ DeltaX��	 �� *VerformungX+$
 N4��  $ DeltaY��	 �� *VerformungY+$
 N4��  $	 Phi��	 �� *
Verdrehung+$
 N4��  $
 AccX��	 �� *BeschleunigungX+$
 N4��  $
 AccY��	 �� *BeschleunigungY+$
 N4��  $ AccPhi��	 �� *BeschleunigungPhi+$
 N4��