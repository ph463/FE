   M S B A M L   `   `   `  ����  FE BerechnungenQ    KFE_Berechnungen.Tragwerksberechnung.Ergebnisse.DynamischeErgebnisseAnzeigen   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$.  (Ergebnisse der zeitabhänigen Berechnung��5   	   $	��500��69   $	��800��6F   .��5      � $��0,5,2.4,0.4q�6    � ColumnDefinitions 5	   
   �� 5
      $
��Auto �6   $	��400��6,   �� 5      $
��Auto �6   $��404.8��6,    � RowDefinitions 5   
   �� 5      $��68 �6   �� 5      $
��Auto �6   $	��300��6*   .+�5   
   �� -   5         ��Name$ Knotenzeitverlauf��5      $��Knoten Zeitverlauf��6+   $
��Left=�6H   $��
5,0,0,38.8q�6c   $��0��6w   $	��130��6�   �� 5   
   $��0��6   $��0��6   $��Auswahl des Knotens��6-   $
��Left=�6K   $��5,30,0,0q�6f   $	��Top=�6x   $	��200��6�   $��25��6�   �� 5   
   -   5        $��Knotenauswahl��5      $
��Left=�6(   $��
136,30,0,0q�6C   $	��Top=�6W   $	��100��6o   $��0��5      $��0��6    $��22��60   5      �� 5   
   -   5        $��	BtnKnoten��5      $��0��6"   $��0��62   $��250,33,62,14.8q�6g   .��6   
Anzeigen6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��KnotenverformungenGrid��5      $
��Left=�61   $	��390��6L   $��5,0.2,0,-0.2q�6X      AlternatingRowBackground
 ��� �   AutoGenerateColumns .  $��0��5   -   $��1��6=   $	��396��6J   $	��Top=�6W      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Zeit��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Zeit+	 �� StringFormat$	 N2��  5      $ DeltaX��6%    65   �� *VerformungX+$	 N4��  5      $ DeltaY��6%    65   �� *VerformungY+$	 N4��  5      $	 Phi��6%    62   �� *
Verdrehung+$	 N4��  5       $
 AccX��6%    63   �� *BeschleunigungX+$	 N4��  5!      $
 AccY��6%    63   �� *BeschleunigungY+$	 N4��  5"      $ AccPhi��6%    65   �� *BeschleunigungPhi+$	 N4��5$      �� 5&   
   -   5        $ ModellZustand��5&      $5��/Modellzustand an einem festgelegtem Zeitschritt��6'   $
��Left=�6a   $��
5,0,0,38.8q�6|   $��1��6�   $��0��6�   $	��285��6�   �� 5'   
   $��Auswahl des Zeitschritts��6   $
��Left=�63   $��5,30,0,0q�6N   $	��Top=�6`   $��1��6x   $��0��6�   $	��200��6�   $��25��6�   �� 5(   
   -   5        $��Zeitschrittauswahl��5(      $
��Left=�6-   $��
154,30,0,0q�6H   $	��Top=�6\   $	��100��6t   $��0��5)      $��1��6    $��22��60   
 �� ItemStringFormat$
 N2��5*   E   �� 5+   
   -   5        $��BtnZeitschritt��5+      $��1��6'   $��0��67   $��268,33,43.8,14.8q�6e   .��6   
Anzeigen6�     5-   
   -   5        $��ZeitschrittGrid��5-      $
��Left=�6*   $	��390��6E   $��5,0.2,0,-0.2q�6Q   
 ��� � .  $��1��5.   -   $��1��6=   $	��396��6J   $	��Top=�6W    5/        50      $ Knoten��6%    65   �� *Knoten+  51      $ DeltaX��6%    65   �� *VerformungX+$	 N4��  52      $ DeltaY��6%    65   �� *VerformungY+$	 N4��  53      $	 Phi��6%    62   �� *
Verdrehung+$	 N4��  54      $
 AccX��6%    63   �� *BeschleunigungX+$	 N4��  55      $
 AccY��6%    63   �� *BeschleunigungY+$	 N4��  56      $ AccPhi��6%    65   �� *BeschleunigungPhi+$	 N4��58      59      5:      