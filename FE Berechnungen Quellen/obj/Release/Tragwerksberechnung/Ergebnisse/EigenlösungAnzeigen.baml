   M S B A M L   `   `   `  ����   FE BerechnungenI    CFE_Berechnungen.Tragwerksberechnung.Ergebnisse.EigenlösungAnzeigen   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  EigenlösungAnzeigen��$	��450��$	��800��.��� $��0,25,0,0q� � ColumnDefinitions �� $
��Auto �$	��120����  � RowDefinitions �� $
��Auto �.+��� -    ��Name$ eigenfrequenzen��$��Eigenfrequenzen��$
��Bold"�$��5,0,0,0q�$
��Left=�$	��Top=�$	��115��$��30��$��0��� $��0,30,0,0q�$��Right=�$	��100��$��0��$	��240��$	��Top=� �� $��90 �.+�&   System.Windows.Controls.DataGrid  -   $��EigenfrequenzenGrid��$	��240��$��-10,0,-10,0q�$��0��$	��Top=�   AlternatingRowBackground
 ��� �   HeadersVisibility$
 None=�   AutoGenerateColumns .  $��Right=�$	��110��   Columns 0  *System.Windows.Controls.DataGridTextColumn  1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Key+   �� *Value+	 �� StringFormat$	 N4���� -   $ eigenvektoren��$��Eigenvektoren��$
��Bold"�$��10,0,0,0q�$
��Left=�$	��Top=�$	��130��$��30��$��1��� $��10,0,0,0q�$��1�� �� $	��150 �.+�  -   $��EigenvektorenGrid��$	��240��$��5,30,-150,0q�$	��Top=�
 ��� �$
 None=� .  $
��Left=�$	��300��    �� *Key+   �� *Value+