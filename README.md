# Finite Element Berechnungen im Bauwesen

FE Berechnungen ist ein Programmpaket in C# für die Berechnung physikalischen Verhaltens im Bauingenieurwesen.
Insbesondere Studierende des Bauingenieurwesen sollen hiermit ein allgemeines, flexibles Werkzeug für ihr Studium erhalten.

Unterstützt werden Berechnungen von Tragwerken und Elastizitätsproblemen (DGL 2. Ordnung) sowohl für statische wie dynamische Problemstellungen.
Insbesondere Tragwerksberechnungen nach der Balkentheorie werden unterstützt für Fachwerke, Biegebalken mit und ohne Gelenk und Federlagerung. 
Dynamische Berechnungen werden unterstützt für zeitabhängige Knotenbelastung wie für Erdbebenbelastung.

Unterstützt werden Berechnungen von Wärmetransferproblemen (DGL 1. Ordnung) sowohl für stationäre wie instationäre Problemstellungen.

Eigenwertanalysen werden unterstützt für eine vordefinierbare Anzahl von Eigenwerten und -vektoren.

Grundsätzlich werden in allen Anwendungen Modelldaten eingelesen, die resultierenden Modelldefinitionen dargestellt, eine Modellberechhnung 
durchgeführt und die Ergebnisse dargestellt.

Modelldaten werden in der Regel aus einer input-Datei mit vordefinierten Beispielen eingelesen. Die daraus resultierenden 
Modelldaten können alfanumerisch angezeigt und editiert werden wie auch visuell dargestellt werden.
Die Ergebnissse der Berechnungen können ebenso in alfanumerischer Form angezeigt werden wie auch visuell dargestellt werden.
Schnittkraftverläufe für Normal- und Querkräfte wie auch Momentenverläufe werden in grafischer Form dargestellt.
Egebnisse dynamischer Berechnungen können als Modellzustände an bestimmten Zeitschritten wie als Knotenzeitverläufe über den
gesamten Berechnungszeitverlauf dargestellt werden.

Grundlage der Entwicklung ist eine Bibliothek für die Aufgaben der Finite Element Analyse (FE Bibilothek). Diese beinhaltet vor
allem eine Objekthierarchie mit Klassenimplementationen für applikationsübergreifende Modelldefinitionen sowie Lösungen für
allgemeine Aufgaben der Matrizenalgebra, Gleichungslöser zur Lösung linearer Gleichungssysteme, Eigenlöser zur Lösung von Eigenproblemen
und Zeitintegrationsverfahren für Systeme 1. und 2. Ordnung.
