   M S B A M L   `   `   `  ����  FE BerechnungenI    CFE_Berechnungen.Tragwerksberechnung.Ergebnisse.EigenlösungAnzeigen   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  EigenlösungAnzeigen��5   	   $	��450��6%   $	��800��62   .��5      � $��0,25,0,0q�6    � ColumnDefinitions 5	   
   �� 5
      $
��Auto �6   $	��120��6,   �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ Eigenfrequenzen��5      $��Eigenfrequenzen��6)   $
��Bold"�6C   $��5,0,0,0q�6U   $
��Left=�5      $	��Top=�6,   $	��115��6D   $��30��6P   $��0��6\   � 5   
   $��0,30,0,0q�6   $��Right=�6!   $	��100��6=   $��0��6I   $	��240��6Y   $	��Top=�6f    5      �� 5      $��90 �6#   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��EigenfrequenzenGrid��5      $	��240��6,   $��-10,0,-10,0q�69   $��0��6N   $	��Top=�5         AlternatingRowBackground
 ��� �   HeadersVisibility$
 None=�6I      AutoGenerateColumns .  $��Right=�5   Q   $	��110��6m      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *Key+  5       6)   �� *Value+	 �� StringFormat$	 N4��5      5       �� 5"   
   -   5        $ Eigenvektoren��5"      $��Eigenvektoren��6'   $
��Bold"�6?   $��10,0,0,0q�6Q   $
��Left=�5#      $	��Top=�6,   $	��130��6D   $��30��6P   $��1��6\   � 5$   
   $��10,0,0,0q�6   $��1��6!    5%      �� 5&      $	��150 �6#   .+�5(        -   5        $��EigenvektorenGrid��5)      $	��240��6*   $��5,30,-150,0q�67   $	��Top=�5*      
 ��� �$
 None=�6I    .  $
��Left=�5+   O   $	��300��6j    5,        5-       6)   �� *Key+  5.       6)   �� *Value+50      51      52      53      