   M S B A M L   `   `   `  ����   FE BerechnungenR    LFE_Berechnungen.Wärmeberechnung.Ergebnisse.KnotenzeitverläufeVisualisieren   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$,  &instationäre Ergebnisse visualisieren��$	��600��$
��1000��.���� $
��Auto=�.���� $
��Left=�$	��Top=�.+�� 
�������$
��1000��$��60�� � ColumnDefinitions �� $	��250 ��� $	��110 ��� $	��110 ��� $	��130 � � RowDefinitions �� $��60 �.+��� -    ��Name$ knotentemperatur��$(��"Zeitverlauf an festgelegtem Knoten��$	��300��$��0��$��10,0,0,0q�$	��Top=�$
��Left=��� $��Auswahl des Knotens��$
��Left=�$��	10,25,0,0q�$	��Top=�$	��130��$��25���� -   $��Knotenauswahl��$
��Left=�$��
140,28,0,0q�$	��Top=�$	��106��$��0��$��22���� -   $��btnTemperatur��$��
Temperatur��$��10.2,30,0,10q�$��20��$��89��$��1��$
��Left=��� -   $��btnGradient��$��Wärmefluss��$��
10,30,0,10q�$��20��$��90��$��2��$
��Left=��� -   $��	btnRegion��$��0��$��0,28,-135,10q� �� 	IsDefault .  $��3��$��Right=�$	��153��.��Darstellungsbereich ändern�� -    ��Name$ visualErgebnisse��$	��490��$
��1000����. $
��Auto=�