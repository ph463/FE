   M S B A M L   `   `   `  ����  FE Berechnungen"    FE_Berechnungen.StartFenster   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$$  FE Berechnungen Stand Mai 2021��5   	   $	��450��60   $	��800��6=   .��5      � .+�5	   
   o� $
��Left=�6   $��30��6*   $��2,1,0,0q�66   $	��Top=�6G   $	��451��6_   $��LeftToRight=�6k   .@�5
      m� $��Tragwerksberechnung��6   $��30��64   $	��150��6@   
�������.@�5      m� -   5        $��Tragwerksdaten einlesen��5      $
��Left=�6<   $��20��6W   $	��250��6c   $��0,0,0,0q�6o   6�   m� 5      -   5        $��Tragwerksdaten editieren��5      $
��Left=�6=   $��20��6X   $	��250��6d   $��0,0,0,0q�6p   6�   �� 5      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 5      -   5        $��Tragwerksdaten anzeigen��5      $
��Left=�6<   $��20��6W   $	��250��6c   $��0,0,0,0q�6o   6�   m� 5      -   5        $"��Tragwerksdaten visualisieren��5      $
��Left=�6A   $��20��6\   $	��250��6h   $��0,0,0,0q�6t   6�   �� 5      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 5      -   5        $��statische Berechnung��5      $
��Left=�69   $��20��6T   $	��250��6`   $��0,0,0,0q�6l   6}   �� 5      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 5      -   5        $#��statische Ergebnisse anzeigen��5      $
��Left=�6B   $��20��6]   $	��250��6i   $��0,0,0,0q�6u   6�   m� 5      -   5        $(��"statische Ergebnisse visualisieren��5      $
��Left=�6G   $��20��6b   $	��300��6n   $��0,0,0,0q�6z   6�   �� 5      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 5      -   5        $��dynamische Daten anzeigen��5      $
��Left=�6>   $��20��6Y   $	��250��6e   $��0,0,0,0q�6q   6�   �� 5      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 5      -	   5        $��Eigenlösung berechnen��5      $
��Left=�6:   $��20��6U   $	��250��6a   $��0,0,0,0q�6m   6~   m� 5      -
   5        $��Eigenlösung anzeigen��5      $
��Left=�69   $��20��6T   $	��250��6`   $��0,0,0,0q�6l   6}   m� 5      -   5        $ ��Eigenlösung visualisieren��5      $
��Left=�6>   $��20��6Y   $	��250��6e   $��0,0,0,0q�6q   6�   �� 5      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 5      -   5        $��dynamische Berechnung��5      $
��Left=�6:   $��20��6U   $	��250��6a   $��0,0,0,0q�6m   6~   �� 5      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 5      -   5        $$��dynamische Ergebnisse anzeigen��5      $
��Left=�6C   $��20��6^   $	��271��6j   $��0,0,0,0q�6v   6�   m� 5      -   5        $2��,zeitabhängige Modellzustände visualisieren��5      $
��Left=�6O   $��20��6j   $	��350��6v   $��0,0,0,0q�6�   6�   m� 5       -   5        $'��!Knotenzeitverläufe visualisieren��5       $
��Left=�6E   $��20��6`   $	��300��6l   $��0,0,0,0q�6x   6�   5!      m� 5"      $��Wärmeberechnung��6   $��30��60   $	��150��6<   
�������.@�5#      m� -   5        $��Wärmedaten einlesen��5#      $
��Left=�68   $��20��6S   $	��250��6_   $��0,0,0,0q�6k   6|   m� 5$      -   5        $��Wärmedaten editieren��5$      $
��Left=�69   $��20��6T   $	��250��6`   $��0,0,0,0q�6l   6}   �� 5%      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 5&      -   5        $��Wärmedaten anzeigen��5&      $
��Left=�68   $��20��6S   $	��250��6_   $��0,0,0,0q�6k   6|   m� 5'      -   5        $��Wärmedaten visualisieren��5'      $
��Left=�6=   $��20��6X   $	��250��6d   $��0,0,0,0q�6p   6�   �� 5(      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 5)      -   5        $��stationäre Berechnung��5)      $
��Left=�6:   $��20��6U   $	��250��6a   $��0,0,0,0q�6m   6~   �� 5*      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 5+      -   5        $%��stationäre Ergebnisse anzeigen��5+      $
��Left=�6C   $��20��6^   $	��250��6j   $��0,0,0,0q�6v   6�   m� 5,      -   5        $*��$stationäre Ergebnisse visualisieren��5,      $
��Left=�6H   $��20��6c   $	��300��6o   $��0,0,0,0q�6{   6�   �� 5-      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 5.      -   5        $"��instationäre Daten anzeigen��5.      $
��Left=�6@   $��20��6[   $	��250��6g   $��0,0,0,0q�6s   6�   �� 5/      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 50      -   5        $��Eigenlösung berechnen��50      $
��Left=�6:   $��20��6U   $	��250��6a   $��0,0,0,0q�6m   6~   m� 51      -   5        $��Eigenlösung anzeigen��51      $
��Left=�69   $��20��6T   $	��250��6`   $��0,0,0,0q�6l   6}   m� 52      -   5        $ ��Eigenlösung visualisieren��52      $
��Left=�6>   $��20��6Y   $	��250��6e   $��0,0,0,0q�6q   6�   �� 53      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 54      -   5        $��instationäre Berechnung��54      $
��Left=�6<   $��20��6W   $	��250��6c   $��0,0,0,0q�6o   6�   �� 55      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 56      -   5        $'��!instationäre Ergebnisse anzeigen��56      $
��Left=�6E   $��20��6`   $	��262��6l   $��0,0,0,0q�6x   6�   m� 57      -   5        $2��,zeitabhängige Modellzustände visualisieren��57      $
��Left=�6O   $��20��6j   $	��350��6v   $��0,0,0,0q�6�   6�   m� 58      -   5        $'��!Knotenzeitverläufe visualisieren��58      $
��Left=�6E   $��20��6`   $	��300��6l   $��0,0,0,0q�6x   6�   59      m� 5:      $��Elastizitätsberechnung��6   $��30��67   $	��150��6C   
�������.@�5;      m� -   5        $!��Elastizitätsdaten einlesen��5;      $
��Left=�6?   $��20��6Z   $	��250��6f   $��0,0,0,0q�6r   6�   m� 5<      -    5        $"��Elastizitätsdaten editieren��5<      $
��Left=�6@   $��20��6[   $	��250��6g   $��0,0,0,0q�6s   6�   m� 5=      -!   5        $!��Elastizitätsdaten anzeigen��5=      $
��Left=�6?   $��20��6Z   $	��250��6f   $��0,0,0,0q�6r   6�   m� 5>      -"   5        $'��!Elastizitätsmodell visualisieren��5>      $
��Left=�6E   $��20��6`   $	��250��6l   $��0,0,0,0q�6x   6�   �� 5?      $
��Left=�6   $��4��67   $	��250��6B   $��
0,0,-0.2,0q�6N   m� 5@      -#   5        $��Elastizitätsberechnung��5@      $
��Left=�6;   $��20��6V   $	��250��6b   $��0,0,0,0q�6n   6   m� 5A      -$   5        $#��statische Ergebnisse anzeigen��5A      $
��Left=�6B   $��20��5B      $	��250��6'   $��0,0,0,0q�63   6D   m� 5C      -%   5        $(��"statische Ergebnisse visualisieren��5C      $
��Left=�6G   $��20��6b   $	��261��6n   $��0,0,-11.4,0q�6z   6�   5D      5E      5F      5G      