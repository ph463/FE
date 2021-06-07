# Finite Element Berechnungen im Bauwesen

FE Berechnungen ist ein Programmpaket in C# für die Berechnung physikalischen Verhaltens im Bauingenieurwesen.
Insbesondere Studierende des Bauingenieurwesen sollen hiermit ein allgemeines, flexibles Werkzeug für ihr Studium erhalten.

Unterstützt werden Berechnungen von Differentialgleichungssystemen 1. Ordnung (z.B. Wärme) sowohl für stationäre
Wärmeberechnungen wie auch für instationäre Wärmeberechnungen.

Berechnungen von Differentialgleichungssystemen 2. Ordnung wie Tragwerksberechnungen oder Elastizitätsberechnungen 
werden sowohl für statische wie dynamische Problemstellungen unterstützt. Insbesondere Tragwerksberechnungen
nach der Balkentheorie werden unterstützt für Fachwerke, Biegebalken mit und ohne Gelenk und Federlagerung. Dynamische 
Berechnungen werden unterstützt für zeitabhängige Knotenbelastung wie für Erdbebenbelastung.

Eigenwertanalysen werden unterstützt für eine vordefinierbare Anzahl von Eigenwerten und -vektoren.

Modelldaten werden in der Regel aus einer input-Datei mit vordefinierten Beispielen eingelesen. Die daraus resultierenden 
Modelldaten können alfanumerisch angezeigt und editiert werden wie auch visuell dargestellt werden.
Die Ergebnissse der Berechnungen können ebenso in alfanumerischer Form angezeigt werden wie auch visuell dargestellt werden.
Insbesondere Schnittkraftverläufe für Normal- und Querkräfte wie auch Momentenverläufe werden in grafischer Form dargestellt.
Egebnisse dynamischer Berechnungen werden als Modellzustände an bestimmten Zeitschritten wie als Knotenzeitverläufe über den
gesamten Berechnungszeitverlauf dargestellt werden.

Grundlage der Entwicklung ist eine Bibliothek für die Aufgaben der Finite Element Analyse (FE Bibilothek). Diese beinhaltet vor
allem allgemeine Aufgaben der Matrizenalgebra, Gleichungslöser zur Lösung linearer Gleichungssysteme und Zeitintegrations-
verfahren für Systeme 1. und 2. Ordnung.
