   M S B A M L   `   `   `  ����  FE BerechnungenN    HFE_Berechnungen.Tragwerksberechnung.Ergebnisse.EigenlösungVisualisieren   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$   Eigenlösung Visualisieren��5   	   $	��660��6+   $
��1000��68   .��5      � .+�5	   
   �� $
��Auto=�6   $��0,0,0,0q�6<   .��5
      �� $
��Left=�6   $	��Top=�64   .+�5      � -   5        $��SteuerLeiste��5      
�������$
��1000��6B   $��60��6O    � ColumnDefinitions 5      �� 5      $	��252 �6+   �� 5      $	��130 �6+   �� 5      $	��130 �6+    � RowDefinitions 5      �� 5      $��60 �6(   .+�5      �� -   5         ��Name$ Eigenlösung��5      $%��Eigenfrequenzen und Eigenformen��61   $
��Left=�6[   $��5,0,0,24q�6v   $	��242��6�   �� 5      $��0��6   $��Auswahl der Eigenlösung��6,   $
��Left=�6N   $��5,24,0,0q�6i   $	��Top=�6{   $	��163��6�   $��28��6�   �� 5      -   5        $��Eigenlösungauswahl��5   /   $��0��6   $
��Left=�6I   $��
170,28,0,0q�6d   $	��Top=�6x   $��74��6�   $��20��6�    �� ItemStringFormat$ N0��5   O   �� 5      -   5        $��BtnGeometrie��5      $��0��61   $��	5,28,0,12q�6Z   $
��Left=�5      $	��105��64   $��1��6@   .��6P   	Geometrie6[   �� 5      -   5        $��BtnEigenform��5      $��0��61   $��
10,28,0,12q�6Z   $
��Left=�5      $	��105��64   $��2��6@   .��6P   	Eigenform6[   �� 5      -   5        $��LblSkalierung��5      $��0��62   $��2��6?   $��30��6O   $��115,28,-115,0q�6\   $
��Left=�5      $	��Top=�67   .��6O   Skalierung Eigenform6e   �� 5      -   5        $��TxtSkalierung��5      $��0��63   $��2��6@   $��20��6P   $��30��6]   $��240,30,-158.4,0q�6h   $
��Left=�5      $	��Top=�67   6O   5       �� 5!      -   5         ��Name$ VisualErgebnisse��5!      $	��660��63   $
��1000��6@   ��. 6M   5"      5#      5$      5%      