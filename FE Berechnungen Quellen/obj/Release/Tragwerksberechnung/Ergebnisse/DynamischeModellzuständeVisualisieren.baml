   M S B A M L   `   `   `  ����   FE Berechnungen[    UFE_Berechnungen.Tragwerksberechnung.Ergebnisse.DynamischeModellzuständeVisualisieren   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$+  %Dynamik Modellzustände visualisieren��$	��720��$
��1000��.���� $
��Auto=�$��0,0,0,0q�.���� $
��Left=�$	��Top=�.+�� 
�������$
��1000��$��60�� � ColumnDefinitions �� $	��252 ��� $	��110 ��� $	��130 ��� $	��110 ��� $	��110 ��� $	��130 ��� $	��712 � � RowDefinitions �� $��60 �.+��� -    ��Name$ zeitschritt��$/��)Modellzustand an festgelegtem Zeitschritt��$
��Left=�$��5,0,0,24q�$	��242���� $"��Auswahl der Integrationszeit��$
��Left=�$��5,24,0,0q�$	��Top=�$	��163��$��28���� -   $��Zeitschrittauswahl��$
��Left=�$��
170,24,0,0q�$	��Top=�$��74��$��23�� �� ItemStringFormat$ N2���� -   $��btnGeometrie��$��0��$��	5,28,0,12q� �� 	IsDefault .  $	��100��$
��Left=�$	��105��$��1��.��	Geometrie�� -   $��btnVerformung��$��0��$��	5,28,0,12q� .  $	��100��$
��Left=�$	��105��$��2��.��
Verformung�� -   $��btnNormalkraft��$��0��$��	5,28,0,12q� .  $	��100��$
��Left=�$	��105��$��3��$��2��.��Normalkraft�� -   $��btnQuerkraft��$��0��$��	5,28,0,12q� .  $	��100��$
��Left=�$	��105��$��4��.��	Querkraft�� -   $��btnBiegemoment��$��0��$��	5,28,0,12q� .  $	��100��$
��Left=�$	��105��$��5��.��Biegemoment�� -    ��Name$ visualErgebnisse��$	��660��$
��1000����. $
��Auto=�