   M S B A M L   `   `   `  ����  FE BerechnungenU    OFE_Berechnungen.Tragwerksberechnung.Ergebnisse.KnotenzeitverläufeVisualisieren   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$?  9Knotenzeitverläufe der Verformungen und Beschleunigungen��5   	   $	��600��6J   $
��1000��6W   .��5      �� $
��Auto=�6   $��0,0,0,0q�68   .��5	   
   �� $
��Left=�6   $	��Top=�60   .+�5
      � 
�������$
��1000��6*   $��60��67    � ColumnDefinitions 5      �� 5      $	��240 �6'   �� 5      $	��110 �6'   �� 5      $	��110 �6'   �� 5      $	��130 �6'   �� 5      $	��130 �6'   �� 5      $	��110 �6'    � RowDefinitions 5      �� 5      $��60 �6$   .+�5      �� $��Auswahl eines Knotens��6   $
��Left=�68   $��5,24,0,0q�6S   $	��Top=�6e   $	��132��6}   $��28��6�   �� 5      -   5        $��Knotenauswahl��5   +   $��0��6   $��138,24,43.2,0q�6@   $	��Top=�6W   $��23��6o    �� ItemStringFormat$ N2��5   H   �� 5      -   5        $��	btnDeltaX��5      $��0��6*   $��2.8,28,0,12q�6P    �� 	IsDefault .  $	��100��6w   $
��Left=�5      $��80��64   $��1��6?   .��6O   Verformung X6]   �� 5      -   5        $��	btnDeltaY��5      $��0��6*   $��4.2,28,0,12q�6P    .  $	��100��6w   $
��Left=�5      $��80��64   $��2��6?   .��6O   Verformung Y6]   �� 5      -   5        $��btnAccX��5      $��0��6(   $��4.8,28,0,12q�6L    .  $	��100��6s   $
��Left=�5      $	��105��64   $��3��6@   .��6P   Beschleunigung X6b   �� 5      -   5        $��btnAccY��5      $��0��6(   $��	5,28,0,12q�6L    .  $	��100��6q   $
��Left=�5       $	��105��64   $��4��6@   .��6P   Beschleunigung Y6b   �� 5!      -   5        $��	btnRegion��5!      $��0��6*   $��0,28,-135,10q�6c    .  $��5��6�   $��Right=�6�   $	��153��6�   .��6�   Darstellungsbereich ändern6�   5"      �� 5#      -   5         ��Name$ visualErgebnisse��5#      $	��489��6/   $
��1000��6<   ��. $
��Auto=�6n   5$      5%      5&      