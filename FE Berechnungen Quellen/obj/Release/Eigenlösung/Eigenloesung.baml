   M S B A M L   `   `   `  ����   FE Berechnungen/    )FE_Berechnungen.Eigenlösung.Eigenloesung   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  Eigenlösung��$	��450��$	��800��.��� $��4,8,10.6,101q� � ColumnDefinitions �� $
��Auto �$	��100����  � RowDefinitions �� $
��Auto ��� $
��Auto �.+��� -    ��Name$ titel��$��0��$��2���� -   $ 
eigenwerte��$��
Eigenwerte��$
��Bold"�$��10,0,0,0q�$
��Left=�$	��Top=�$��74��$��30��$��1��$��0��� $��0,25,16.8,0q�$��Right=�$��92��$��0��$��1��$	��240��$	��Top=� �� $��90 �.+�&   System.Windows.Controls.DataGrid  -   $��EigenwerteGrid��$
��Left=�$	��238��$��96��$��10,0,-15.6,0q�$	��Top=�   AlternatingRowBackground
 ��� �   HeadersVisibility$
 None=�   AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Key+	 �� StringFormat$	 N2��   �� *Value+$	 N4���� -   $ eigenvektoren��$��Eigenvektoren��$
��Bold"�$��10,0,0,0q�$
��Left=�$	��Top=�$	��134��$��30��$��1��$��1��� $��10,0,500,-0.4q�$��1��$��1�� �� $	��150 �.+�  -   $��EigenvektorenGrid��$
��Left=�$	��239��$	��150��$��0,25,-200,0q�$	��Top=�
 ��� �$
 None=� .  $��2��    �� *Key+   �� *Value+