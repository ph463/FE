   M S B A M L   `   `   `  ����   FE BerechnungenU    OFE_Berechnungen.Wärmeberechnung.ModelldatenAnzeigen.InstationäreDatenAnzeigen   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$"  instationäre Daten Anzeigen��$	��600��$	��800��.��� -    �Name$ instationaereDaten��$	��Top=�$
��Left=�$��
0,0,-6.4,0q�$	��800��$	��435��
������� � ColumnDefinitions �� �� �� ��  � RowDefinitions �� $
��124* ��� $
��162* ��� $
��149* �.+�� -   $ 	parameter��$	��Top=�$��0��$��0��$
��Left=�$��5,5,0,0q�$	��287��$	��100��
�������$��2�� �� $	��140 ��� $��50 � �� �� �� �� �� .+��� $��12(�$
��Bold"�$��2��$��0��$��	0,0,-91,0q�*(Eigenlösungen und Integrationsparameter�� $��1��$��0,0,3,0q�Anzahl Eigenlösungen =�� $��2��$��0,0,3,0q�Zeitintervall Delta T =�� $��3��$��0,0,3,0q�Maximalzeit Tmax =�� $��4��$��0,0,3,0q�Integrationsparameter =�� -   $��eigen��$��1��$��1��$��1.4,0,0.2,0q����� *Eigenstate.NumberOfStates+�� -   $��	zeitintDt��$��1��$��2��$��1.4,0,0.2,0q����� *Zeitintegration.Dt+�� -   $��zeitintTmax��$��1��$��3��$��1.4,0,0.2,0q����� *Zeitintegration.Tmax+�� -   $��zeitintParameter1��$��1��$��4��$��1.4,0,0.2,0q����� *Zeitintegration.Parameter1+�� -    ��Name$ anfangstemperaturen��$��Anfangstemperaturen��$
��Bold"�$
��Left=�$��5,5,0,0q�$	��Top=�$	��135��$��30��$��1��$��0��� $��5,32,16,73.4q�$��1��$��0�� �� $	��49* ��� $
��113* �.+�&   System.Windows.Controls.DataGrid  -   $��AnfangstemperaturenGrid��$
��Left=�$	��120��$	��160��$	��Top=�   AlternatingRowBackground
 ��� �   AutoGenerateColumns .  $��2��$   HorizontalScrollBarVisibility$ Disabled=�$��	0,0,0,-63q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 Knoten��1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *
KnotenId+  $	 Anfangstemperatur��
 �� *
Werte[0]+�� -	   $
��alle��$��0��$��2�����	#FFF0EEEE$��5,7.6,151,110.2q����� *knoten+�� -
   $��
temperatur��$��0��$��2�����	#FFF0EEEE$��50,7.6,35,110.2q� �� TextWrapping$
 Wrap=����� *daten+�� -   $ zeitabhängig��$o��izeitabhängige Einflußwertdaten aus: (a)stückweise, linearer Verlauf, (b)aus Datei  oder  (c)harmonisch��$
��Bold"�$��0,6,0,0q�$
��Left=�$	��Top=�$	��596��$��30��$��1��$��1��$��3���� -   $ randtemperaturen��$��Randbedingungen��$
��Bold"�$��0,29,0,0q�$
��Left=�$	��Top=�$	��135��$��30��$��1��$��1��� $��0,22,32,0.4q�$��1��$��1�� �� $��50 ��� $	��150 �.+�  -   $��RandtemperaturenGrid��$
��Left=�$	��160��$��
5,33,0,8.8q�
 ��� � .  $��2��   $	 Knoten��
 �� *
KnotenId+  
 �� *Time+  
 �� *
Temperatur+�� -   $��
randKnoten��$��1��$��2�����	#FFF0EEEE$��5,7.6,150,108.2q����� *knoten+�� -   $��randTemperatur��$��1��$��2�����	#FFF0EEEE$��50,7.6,35,108.2q�$
 Wrap=����� *daten+� $��0,31.6,35,1.2q�$��2��$��1�� �� $��40 ��� $��40 ��� $��40 ��� $��40 �.+�  -   $��RandHarmonischGrid��$��95��$��
5,15,0.2,0q�$	��Top=�
 ��� � .  $��4��   $	 Knoten��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 
 �� *NodeId+  $	 	Amplitude��$ 40 
 �� *	Amplitude+  $	 Phase��$ 40 
 �� *
PhaseAngle+  $	 Frequenz��$ 40 
 �� *	Frequency+�� -   $ knotentemperaturen��$��Knotentemperaturen��$
��Bold"�$��0,30,0,0q�$
��Left=�$	��Top=�$	��135��$��30��$��1��$��2��� $��0,32,38,2.4q�$��1��$��2�� �� $��50 ��� $	��150 �.+�  -   $��KnotentemperaturGrid��$
��Left=�$��98��$	��155��$��5,23,0,0q�$	��Top=�
 ��� � .  $��2��   $	 Knoten��
 �� *
KnotenId+  
 �� *Time+  
 �� *
Temperatur+�� -   $��Knoten��$��2��$��2�����	#FFF0EEEE$��5,7.6,150,107.2q����� *knoten+�� -   $��knotenTemperatur��$��2��$��2�����	#FFF0EEEE$��50,7.6,37,107.2q�$
 Wrap=����� *daten+� $��	0,35,30,0q�$��2��$��2�� �� $��40 ��� $��40 ��� $��40 ��� $��40 �.+�  -   $��KnotenHarmonischGrid��$��5,11,0.2,5.8q�
 ��� � .  $��4��   $	 Knoten��$ 40 
 �� *NodeId+  $	 	Amplitude��$ 40 
 �� *	Amplitude+  $	 Phase��$ 40 
 �� *
PhaseAngle+  $	 Frequenz��$ 40 
 �� *	Frequency+�� -   $ elementtemperaturen��$��Elementtemperaturen��$
��Bold"�$��2,31,0,0q�$
��Left=�$	��Top=�$	��135��$��30��$��1��$��3��� $��0,27,30,2.4q�$��1��$��3�� �� $��50 ��� $	��150 �.+�  -   $��ElementtemperaturenGrid��$
��Left=�$��97��$	��155��$��5,28,0,0q�$	��Top=�
 ��� � .  $��2��   $	 Element��
 �� *	ElementId+  $	 P0��
 �� *P[0]+  $	 P1��
 �� *P[1]+  $	 P2��
 �� *P[2]+  $	 P3��
 �� *P[3]+