   M S B A M L   `   `   `  ����J  FFE Berechnungen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:FE_Berechnungen.DateieingabeFE_Berechnungen.Dateieingabe  5   	   7    1FE_Berechnungen.Dateieingabe.ModelldatenEditieren   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:FE_Berechnungen.Dateieingabe     5   	     � Title$  Modelldaten editieren��5   	   $	��400��6'   $	��600��64   .��5	      �� $
��Auto=�6   $��0,0,0,0q�68   .��5
   
   �� $
��Left=�6   $	��Top=�60   .+�5      � 
�������$	��600��6*   $��60��66    � ColumnDefinitions 5      �� 5      $	��200 �6'   �� 5      $	��200 �6'    � RowDefinitions 5      �� 5      $��60 �6$   .+�5      �� -   5         ��Name$ btnOpenFile��5      $��0��6.   $��0��6<   $��
15,20,0,15q�6e    �� 	IsDefault . $	��200��5      $
��Left=�6(   $	��170��6C   $��Modelldatendatei einlesen��6O   �� 5      -   5        $ btnSaveFile��5      $��0��6.   $��1��6<   $��
15,20,0,15q�6f    . $	��200��5      $
��Left=�6(   $	��170��6C   $��Modelldatendatei sichern��6O   5      �� 5      -   5        $��	txtEditor��5       �� TextWrapping$
 Wrap=�6'    �� AcceptsReturn . $
��Auto=�5       �� 
AcceptsTab . $	��290��5      $	��600��6*   5      5      5      