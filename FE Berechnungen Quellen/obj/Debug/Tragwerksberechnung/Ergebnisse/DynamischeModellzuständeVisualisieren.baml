   M S B A M L   `   `   `  ����  FE Berechnungen[    UFE_Berechnungen.Tragwerksberechnung.Ergebnisse.DynamischeModellzuständeVisualisieren   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$+  %Dynamik Modellzustände visualisieren��5   	   $	��720��66   $
��1000��6C   .��5      �� $
��Auto=�6   $��0,0,0,0q�68   .��5	   
   �� $
��Left=�6   $	��Top=�60   .+�5
      � 
�������$
��1000��6*   $��60��67    � ColumnDefinitions 5      �� 5      $	��252 �6'   �� 5      $	��110 �6'   �� 5      $	��130 �6'   �� 5      $	��110 �6'   �� 5      $	��110 �6'   �� 5      $	��130 �6'   �� 5      $	��712 �6'    � RowDefinitions 5      �� 5      $��60 �6$   .+�5      �� -   5         ��Name$ zeitschritt��5      $/��)Modellzustand an festgelegtem Zeitschritt��6-   $
��Left=�6a   $��5,0,0,24q�6|   $	��242��6�   �� 5      $"��Auswahl der Integrationszeit��6   $
��Left=�6?   $��5,24,0,0q�6Z   $	��Top=�6l   $	��163��6�   $��28��6�   �� 5      -   5        $��Zeitschrittauswahl��5      $
��Left=�65   $��
170,24,0,0q�6P   $	��Top=�6d   $��74��6|   $��23��6�    �� ItemStringFormat$ N2��5   I   �� 5      -   5        $��btnGeometrie��5      $��0��6-   $��	5,28,0,12q�6V    �� 	IsDefault .  $	��100��6{   $
��Left=�5      $	��105��64   $��1��6@   .��6P   	Geometrie6[   �� 5      -   5        $��btnVerformung��5      $��0��6.   $��	5,28,0,12q�6X    .  $	��100��6}   $
��Left=�5      $	��105��64   $��2��6@   .��6P   
Verformung6\   �� 5      -   5        $��btnNormalkraft��5      $��0��6/   $��	5,28,0,12q�6Z    .  $	��100��6   $
��Left=�5       $	��105��64   $��3��6@   $��2��6P   .��6d   Normalkraft6q   �� 5!      -   5        $��btnQuerkraft��5!      $��0��6-   $��	5,28,0,12q�6V    .  $	��100��6{   $
��Left=�5"      $	��105��64   $��4��6@   .��6P   	Querkraft6[   �� 5#      -   5        $��btnBiegemoment��5#      $��0��6/   $��	5,28,0,12q�6Z    .  $	��100��6   $
��Left=�5$      $	��105��64   $��5��6@   .��6P   Biegemoment6]   5%      �� 5&      -   5         ��Name$ visualErgebnisse��5&      $	��660��6/   $
��1000��6<   ��. $
��Auto=�6n   5'      5(      5)      