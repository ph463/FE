   M S B A M L   `   `   `  ����  FE Berechnungen[    UFE_Berechnungen.Wärmeberechnung.Ergebnisse.InstationäreModellzuständeVisualisieren   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$,  &instationäre Ergebnisse visualisieren��5   	   $	��800��67   $
��1000��6D   .��5      �� $
��Auto=�6   .��5	   
   �� $
��Left=�6   $	��Top=�60   $	��751��6H   .+�5
      � 
�������$
��1000��6*   $��60��67    � RowDefinitions 5      �� 5      $��60 �6$   .+�5      �� -   5         ��Name$ Zeitschritt��5      $/��)Modellzustand an festgelegtem Zeitschritt��6-   $	��300��6a   $��0��6m   $��10,0,0,0q�6}   $	��Top=�5      $
��Left=�60   �� 5      $��Auswahl des Zeitschritts��6   $��0��6;   $
��Left=�6K   $��	10,25,0,0q�6f   $	��Top=�6y   $	��137��6�   $��25��6�   �� 5      -   5        $��Zeitschrittauswahl��5      $��0��65   $��151,28,0,10q�6E   $��22��6Z   $	��100��6f   $
��Left=�6r    �� ItemStringFormat$ N2��5   M   �� 5      -   5        $��Knotentemperaturen��5      $��300,30,0,10q�66   $��20��6K   $	��120��6W   $
��Left=�5      64   �� 5      -   5        $��Knotengradienten��5      $��450,30,0,10q�64   $��20��6I   $	��108��6U   $
��Left=�5      64   �� 5      -   5        $��Elementtemperaturen��5      $��594,30,0,10q�67   $��20��6L   $	��123��6X   $
��Left=�5      64   5      �� 5      -   5         ��Name$ VisualErgebnisse��5      $	��690��6/   $
��1000��6<   ��. $
��Auto=�6n   5      5      5      