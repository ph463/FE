   M S B A M L   `   `   `  ����  FE BerechnungenR    LFE_Berechnungen.Wärmeberechnung.Ergebnisse.KnotenzeitverläufeVisualisieren   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$,  &instationäre Ergebnisse visualisieren��5   	   $	��600��67   $
��1000��6D   .��5      �� $
��Auto=�6   .��5	   
   �� $
��Left=�6   $	��Top=�60   .+�5
      � 
�������$
��1000��6*   $��60��67    � ColumnDefinitions 5      �� 5      $	��250 �6'   �� 5      $	��110 �6'   �� 5      $	��110 �6'   �� 5      $	��130 �6'    � RowDefinitions 5      �� 5      $��60 �6$   .+�5      �� -   5         ��Name$ Knotentemperatur��5      $(��"Zeitverlauf an festgelegtem Knoten��62   $	��300��6_   $��0��6k   $��10,0,0,0q�6{   $	��Top=�5      $
��Left=�60   �� 5      $��0��6   $��Auswahl des Knotens��6(   $
��Left=�6F   $��	10,25,0,0q�6a   $	��Top=�6t   $	��130��6�   $��25��6�   �� 5      -   5        $��Knotenauswahl��5      $
��Left=�60   $��
140,28,0,0q�6K   $	��Top=�6_   $	��106��6w   $��0��6�   $��22��6�   5      �� 5      -   5        $��BtnTemperatur��5      $��
Temperatur��6.   $��10.2,30,0,10q�6C   $��20��6Y   $��89��6e   $��1��6p   $
��Left=�5      64   �� 5      -   5        $��BtnGradient��5      $��Wärmefluss��6,   $��
10,30,0,10q�6A   $��20��6U   $��90��6a   $��2��6l   $
��Left=�5      64   �� 5      -   5        $��	BtnRegion��5      $��0��6*   $��0,28,-135,10q�6c    �� 	IsDefault .  $��3��6�   $��Right=�6�   $	��153��6�   .��6�   Darstellungsbereich ändern6�   5      �� 5      -   5         ��Name$ VisualErgebnisse��5      $	��490��6/   $
��1000��6<   ��. $
��Auto=�6n   5       5!      5"      