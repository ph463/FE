   M S B A M L   `   `   `  ����  FE Berechnungen/    )FE_Berechnungen.Eigenlösung.Eigenlösung   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Eigenlösung��5   	   $	��450��6   $	��800��6*   .��5      � $��4,8,10.6,101q�6    � ColumnDefinitions 5	   
   �� 5
      $
��Auto �6   $	��100��6,   �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ titel��5      $��0��6   $��2��6,   6@   �� 5   
   -   5        $ 
eigenwerte��5      $��
Eigenwerte��6$   $
��Bold"�69   $��10,0,0,0q�6K   $
��Left=�5      $	��Top=�6,   $��74��6D   $��30��6O   $��1��6[   $��0��6h   � 5   
   $��0,25,16.8,0q�6   $��Right=�6$   $��92��6@   $��0��6K   $��1��6[   $	��240��6h   $	��Top=�6u    5      �� 5      $��90 �6#   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��EigenwerteGrid��5      $
��Left=�6'   $	��238��6B   $��96��6O   $��10,0,-15.6,0q�6Z   $	��Top=�5         AlternatingRowBackground
 ��� �   HeadersVisibility$
 None=�6I      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *Key+	 �� StringFormat$	 N2��  5       6)   �� *Value+$	 N4��5!      5"      �� 5$   
   -   5        $ eigenvektoren��5$      $��Eigenvektoren��6'   $
��Bold"�6?   $��10,0,0,0q�6Q   $
��Left=�5%      $	��Top=�6,   $	��134��6D   $��30��6P   $��1��6\   $��1��6i   � 5&   
   $��10,0,500,-0.4q�6   $��1��6&   $��1��63    5'      �� 5(      $	��150 �6#   .+�5*        -   5        $��EigenvektorenGrid��5+      $
��Left=�6*   $	��239��6E   $	��150��6R   $��0,25,-200,0q�6^   $	��Top=�5,      
 ��� �$
 None=�6I    .  $��2��5-   O    5.        5/       6)   �� *Key+  50       6)   �� *Value+52      53      54      55      