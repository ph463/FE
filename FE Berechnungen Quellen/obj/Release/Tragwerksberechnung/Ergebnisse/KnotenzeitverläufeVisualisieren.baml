   M S B A M L   `   `   `  ����   FE BerechnungenU    OFE_Berechnungen.Tragwerksberechnung.Ergebnisse.KnotenzeitverläufeVisualisieren   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$?  9Knotenzeitverläufe der Verformungen und Beschleunigungen��$	��600��$
��1000��.���� $
��Auto=�$��0,0,0,0q�.���� $
��Left=�$	��Top=�.+�� 
�������$
��1000��$��60�� � ColumnDefinitions �� $	��240 ��� $	��110 ��� $	��110 ��� $	��130 ��� $	��130 ��� $	��110 � � RowDefinitions �� $��60 �.+��� $��Auswahl eines Knotens��$
��Left=�$��5,24,0,0q�$	��Top=�$	��132��$��28���� -   $��Knotenauswahl��$��138,24,43.2,0q�$	��Top=�$��23�� �� ItemStringFormat$ N2���� -   $��	btnDeltaX��$��0��$��2.8,28,0,12q� �� 	IsDefault .  $	��100��$
��Left=�$��80��$��1��.��Verformung X�� -   $��	btnDeltaY��$��0��$��4.2,28,0,12q� .  $	��100��$
��Left=�$��80��$��2��.��Verformung Y�� -   $��btnAccX��$��0��$��4.8,28,0,12q� .  $	��100��$
��Left=�$	��105��$��3��.��Beschleunigung X�� -   $��btnAccY��$��0��$��	5,28,0,12q� .  $	��100��$
��Left=�$	��105��$��4��.��Beschleunigung Y�� -   $��	btnRegion��$��0��$��0,28,-135,10q� .  $��5��$��Right=�$	��153��.��Darstellungsbereich ändern�� -    ��Name$ visualErgebnisse��$	��489��$
��1000����. $
��Auto=�