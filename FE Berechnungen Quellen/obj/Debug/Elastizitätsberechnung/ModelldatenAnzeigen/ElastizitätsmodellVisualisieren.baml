   M S B A M L   `   `   `  ����  FE Berechnungenb    \FE_Berechnungen.Elastizitätsberechnung.ModelldatenAnzeigen.ElastizitätsmodellVisualisieren   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$&   ElastizitätsmodellVisualisieren��5   	   $	��700��61   $
��1200��6>   .��5      �� $
��Auto=�6   $��0,0,0,0q�68   .��5	   
   �� $
��Left=�6   $	��Top=�60   .+�5
      � 
�������$
��1200��6*   $��60��67    � ColumnDefinitions 5      �� 5      $	��200 �6'   �� 5      $	��200 �6'   �� 5      $	��200 �6'   �� 5      $	��200 �6'    � RowDefinitions 5      �� 5      $��60 �6$   .+�5      �� -   5        $��	BtnKnoten��5      $��0��6*   $��0��67   $��
50,20,0,17q�6G    �� 	IsDefault .  $	��200��6m   $
��Left=�6|   $	��120��5      .��6A   
Knoten IDs6M   �� 5      -   5        $��BtnElemente��5      $��0��6,   $��1��69   $��
50,20,0,17q�6I    .  $	��200��6o   $
��Left=�6~   $	��120��5      .��6B   Element IDs6O   �� 5      -   5        $��	BtnLasten��5      $��0��6*   $��2��67   $��
50,20,0,17q�6G    .  $	��200��6m   $
��Left=�6|   $	��120��5      .��6>   Lasten6F   �� 5      -   5        $��BtnLager��5      $��0��6)   $��3��66   $��
50,20,0,17q�6F    .  $	��200��6l   $
��Left=�6{   $	��120��5      .��6E   Festhaltungen6T   5      �� 5      -   5         ��Name$ VisualErgebnisse��5      $	��593��6/   $
��1195��6<   ��. $
��Auto=�6n   5      5      5       