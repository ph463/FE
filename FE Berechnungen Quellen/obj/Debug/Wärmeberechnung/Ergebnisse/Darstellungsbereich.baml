   M S B A M L   `   `   `  ����J  FFE Berechnungen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:FE_Berechnungen.Wärmeberechnung.Ergebnisse+FE_Berechnungen.Wärmeberechnung.Ergebnisse  5   	   E    ?FE_Berechnungen.Wärmeberechnung.Ergebnisse.Darstellungsbereich   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Klocal9clr-namespace:FE_Berechnungen.Wärmeberechnung.Ergebnisse     5   	     � Title$  Darstellungsbereich��5   	   $	��215��6%   $	��350��62    � WindowStartupLocation$ Manual=�6>    � Left$	 700��6]   
 � Top$	 200��6h    J� IsManipulationEnabled . .��5	      � $��0,0,0,0q�6    � ColumnDefinitions 5
   
   �� 5      $	��150 �6   �� 5      $	��200 �6    � RowDefinitions 5   
   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   �� 5      $��35 �6   .+�5   
   �� -   5        $��Header��5      $��0��6   $��2��6+   $��0,0,0,0q�6?   .��6P   :8Darstellungsbereich für Zeitachse und Ordinate ändern:6�   �� 5   
   -   5        $��
LblMaxZeit��5      $��1��6"   $��0��6/   $��0,5,0,0q�6?   .��6P   maximale Zeit:6`   �� 5   
   -   5        $��
TxtMaxZeit��5      $��1��6$   $��1��61   $��10,10,100,5q�6A   $��20��6V   $��Center=�6b   �� 5   
   -   5        $��LblMaxV��5      $��2��6   $��0��6,   $��0,5,0,0q�6<   .��6M   maximale Temperatur:6c   �� 5   
   -   5        $��TxtMaxTemperatur��5      $��2��6*   $��1��67   $��10,10,100,5q�6G   $��20��6\   $��Center=�6h   �� 5   
   -   5        $��LblMaxB��5      $��3��6   $��0��6,   $��0,5,0,0q�6<   .��6M   maximaler Wärmefluss:6d   �� 5   
   -   5        $��TxtMaxWärmefluss��5      $��3��6*   $��1��67   $��10,10,100,5q�6G   $��20��6\   $��Center=�6h   �� 5   
   -   5        $��BtnDialogOk��5   "    �� 	IsDefault . $��4��65   $��1��6B   $��35��6R   $��0,10,19.4,0q�6^   $��Right=�6�   .��6�   _Ok6�   �� 5   
   -	   5        $��BtnDialogCancel��5      $��4��6(   $��1��65   $��80��6E   $��0,10,59.4,0q�6P   $��Right=�5   /   .��6K   
_Abbrechen6W   5      5       