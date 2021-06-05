using FEALibrary.Gleichungslöser;
using FEALibrary.Modell.abstrakte_Klassen;
using FEALibrary.Zeitlöser;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace FEALibrary.Modell
{
    public class Berechnung
    {
        // ....Object parameters
        private FEModell modell;
        private Knoten node;
        private AbstraktElement element;
        private Gleichungen systemEquations;
        private ProfillöserStatus profileSolver;
        private int dimension;
        private bool decomposed, setDimension, profile, diagonalMatrix;

        public Berechnung(FEModell m)
        {
            modell = m;
            if (modell == null)
            {
                throw new BerechnungAusnahme("Modelleingabedaten noch nicht eingelesen");
            }
            // set System Indices
            var k = 0;
            foreach (var item in modell.Knoten)
            {
                node = item.Value;
                k = node.SetSystemIndices(k);
            }
            SetReferences(m);
        }
        // object references are initially established only on the basis of unique identifiers, i.e. before object instantiation ****
        // when analyis is started, object references must be established using the unique identifiers ******************************
        private void SetReferences(FEModell m)
        {
            modell = m;

            // Referenzen für Querschnittsverweise von 2D Elementen setzen
            foreach (var abstractElement in
                        from KeyValuePair<string, AbstraktElement> item in modell.Elemente
                        where item.Value != null
                        where item.Value is Abstrakt2D
                        let element = item.Value
                        select element)
            {
                var element2D = (Abstrakt2D)abstractElement;
                element2D.SetCrossSectionReferences(modell);
            }
            // setzen aller notwendigen Elementreferenzen und der Systemindizes aller Elemente 
            foreach (var abstractElement in modell.Elemente.Select(item => item.Value))
            {
                abstractElement.SetReferences(modell);
                abstractElement.SetSystemIndicesOfElement();
            }

            foreach (var support in modell.Randbedingungen.Select(item => item.Value))
            {
                support.SetReferences(modell);
            }
            foreach (var load in modell.ElementLasten.Select(item => item.Value))
            {
                load.SetReferences(modell);
            }
            foreach (var timeDepLoad in modell.ZeitabhängigeKnotenLasten.Select(item => item.Value))
            {
                timeDepLoad.SetReferences(modell);
            }
            foreach (var timeDepLoad in modell.ZeitabhängigeElementLasten.Select(item => item.Value))
            {
                timeDepLoad.SetReferences(modell);
            }
            foreach (var timeDepBc in modell.ZeitabhängigeRandbedingung.Select(item => item.Value))
            {
                timeDepBc.SetReferences(modell);
            }
        }
        // determine dimension of system matrix *************************************************************************************
        private void DetermineDimension()
        {
            dimension = 0;
            foreach (var item in modell.Knoten)
            {
                dimension += item.Value.NumberOfNodalDof;
            }
            systemEquations = new Gleichungen(dimension);
            setDimension = true;
        }
        // compute and solve system matrix in profile format with status vector *****************************************************
        private void SetProfile()
        {
            foreach (var item in modell.Elemente)
            {
                element = item.Value;
                systemEquations.SetProfile(element.SystemIndicesOfElement);
            }
            systemEquations.AllocateMatrix();
            profile = true;
        }
        public void ComputeSystemMatrix()
        {
            if (!setDimension) DetermineDimension();
            if (!profile) SetProfile();
            // traverse the elements to assemble the stiffness coefficients
            foreach (var item in modell.Elemente)
            {
                element = item.Value;
                var elementMatrix = element.ComputeMatrix();
                systemEquations.AddMatrix(element.SystemIndicesOfElement, elementMatrix);
            }
            SetStatusVector();
        }
        private void SetStatusVector()
        {
            // für alle festen Randbedingungen
            foreach (var item in modell.Randbedingungen) StatusNodes(item.Value);
        }
        private void StatusNodes(AbstraktRandbedingung randbedingung)
        {
            var nodeId = randbedingung.NodeId;
            //_ = new double[randbedingung.Node.NumberOfNodalDof];
            //_ = new bool[randbedingung.Node.NumberOfNodalDof];
            if (modell.Knoten.TryGetValue(nodeId, out node))
            {
                systemEquations.SetProfile(node.SystemIndices);
                var prescribed = randbedingung.Prescribed;
                var restrained = randbedingung.Restrained;
                for (var i = 0; i < restrained.Length; i++)
                {
                    if (restrained[i])
                        systemEquations.SetStatus(true, node.SystemIndices[i], prescribed[i]);
                }
            }
            else
            {
                throw new BerechnungAusnahme("Endknoten " + nodeId + " ist nicht im Modell enthalten.");
            }
        }
        private void ReComputeSystemMatrix()
        {
            // traverse the elements to assemble the stiffness coefficients
            systemEquations.InitializeMatrix();
            foreach (var item in modell.Elemente)
            {
                element = item.Value;
                var indices = element.SystemIndicesOfElement;
                var elementMatrix = element.ComputeMatrix();
                systemEquations.AddMatrix(indices, elementMatrix);
            }
        }
        public void ComputeSystemVector()
        {
            int[] indices;
            double[] loadVector;

            // Knotenlasten
            foreach (var item in modell.Lasten)
            {
                var nodeLoad = item.Value;
                var nodeId = item.Value.NodeId;
                if (modell.Knoten.TryGetValue(nodeId, out var loadNode))
                {
                    var systemIndices = loadNode.SystemIndices;
                    indices = systemIndices;
                    loadVector = nodeLoad.ComputeLoadVector();
                    systemEquations.AddVector(indices, loadVector);
                }
                else
                {
                    throw new BerechnungAusnahme("Lastknoten " + nodeId + " ist nicht im Modell enthalten.");
                }
            }
            // Linienenlasten
            foreach (var item in modell.LinienLasten)
            {
                var lineLoad = item.Value;
                var startNodeId = item.Value.StartNodeId;
                if (modell.Knoten.TryGetValue(startNodeId, out node))
                {
                    lineLoad.StartNode = node;
                }
                else
                {
                    throw new BerechnungAusnahme("Linienlastknoten " + startNodeId + " ist nicht im Modell enthalten.");
                }
                var endNodeId = item.Value.EndNodeId;
                if (modell.Knoten.TryGetValue(endNodeId, out node))
                {
                    lineLoad.EndNode = node;
                }
                else
                {
                    throw new BerechnungAusnahme("Linienlastknoten " + endNodeId + " ist nicht im Modell enthalten.");
                }
                var start = lineLoad.StartNode.SystemIndices.Length;
                var end = lineLoad.EndNode.SystemIndices.Length;
                indices = new int[start + end];
                for (var i = 0; i < start; i++)
                    indices[i] = lineLoad.StartNode.SystemIndices[i];
                for (var i = 0; i < end; i++)
                    indices[start + i] = lineLoad.EndNode.SystemIndices[i];
                loadVector = lineLoad.ComputeLoadVector();
                systemEquations.AddVector(indices, loadVector);
            }
            //Elementlasten
            foreach (var item in modell.ElementLasten)
            {
                var elementLoad = item.Value;
                var elementId = item.Value.ElementId;
                if (modell.Elemente.TryGetValue(elementId, out element))
                {
                    indices = element.SystemIndicesOfElement;
                    loadVector = elementLoad.ComputeLoadVector();
                    systemEquations.AddVector(indices, loadVector);
                }
                else
                {
                    throw new BerechnungAusnahme("Element " + elementId + " für Elementlast ist nicht im Modell enthalten.");
                }
            }
            foreach (var item in modell.PunktLasten)
            {
                var pointLoad = item.Value;
                var elementId = item.Value.ElementId;
                if (modell.Elemente.TryGetValue(elementId, out element))
                {
                    pointLoad.Element = element;
                    indices = element.SystemIndicesOfElement;
                    loadVector = pointLoad.ComputeLoadVector();
                    systemEquations.AddVector(indices, loadVector);
                }
                else
                {
                    throw new BerechnungAusnahme("Element " + elementId + " für Linienlasten ist nicht im Modell enthalten.");
                }
            }
        }
        public void SolveEquations()
        {
            if (!decomposed)
            {
                profileSolver = new ProfillöserStatus(
                    systemEquations.Matrix, systemEquations.Vector,
                    systemEquations.Primal, systemEquations.Dual,
                    systemEquations.Status, systemEquations.Profile);
                profileSolver.Decompose();
                decomposed = true;
            }
            profileSolver.Solve();
            // ... save system unknowns (primal values)
            foreach (var item in modell.Knoten)
            {
                node = item.Value;
                var index = node.SystemIndices;
                node.NodalDof = new double[node.NumberOfNodalDof];
                for (var i = 0; i < node.NodalDof.Length; i++)
                    node.NodalDof[i] = systemEquations.Primal[index[i]];
            }
            // ... save dual values
            var reactions = systemEquations.Dual;
            foreach (var support in modell.Randbedingungen.Select(item => item.Value))
            {
                node = support.Node;
                var index = node.SystemIndices;
                var supportReaction = new double[node.NumberOfNodalDof];
                for (var i = 0; i < supportReaction.Length; i++)
                    supportReaction[i] = reactions[index[i]];
                node.Reactions = supportReaction;
            }
            modell.Solved = true;
        }

        // Eigensolutions ***********************************************************************************************************
        public void Eigenstates()
        {
            var numberOfStates = modell.Eigenstate.NumberOfStates;
            var aMatrix = systemEquations.Matrix;
            if (!diagonalMatrix) ComputeDiagonalMatrix();
            var bDiag = systemEquations.DiagonalMatrix;

            // general B-Matrix is expanded to the same structure as A
            var bMatrix = new double[dimension][];
            int row;
            for (row = 0; row < aMatrix.Length; row++)
            {
                bMatrix[row] = new double[aMatrix[row].Length];
                int col;
                for (col = 0; col < bMatrix[row].Length - 1; col++)
                    bMatrix[row][col] = 0;
                bMatrix[row][col] = bDiag[row];
            }

            if (!decomposed)
            {
                profileSolver = new ProfillöserStatus(
                    systemEquations.Matrix,
                    systemEquations.Status, systemEquations.Profile);
                profileSolver.Decompose();
                decomposed = true;
            }

            var eigenSolver = new Eigenlöser(systemEquations.Matrix, bMatrix,
                systemEquations.Profile, systemEquations.Status,
                numberOfStates);
            eigenSolver.SolveEigenstates();

            // save eigenvalues and eigenvectors
            var eigenvalues = new double[numberOfStates];
            var eigenvectors = new double[numberOfStates][];
            for (var i = 0; i < numberOfStates; i++)
            {
                eigenvalues[i] = eigenSolver.GetEigenValue(i);
                eigenvectors[i] = eigenSolver.GetEigenVector(i);
            }
            modell.Eigenstate.Eigenvalues = eigenvalues;
            modell.Eigenstate.Eigenvectors = eigenvectors;
            modell.Eigenstate.Status = systemEquations.Status;
            modell.Eigen = true;
        }
        private void ComputeDiagonalMatrix()
        {
            // diagonal specific heat resp. mass matrix
            if (!setDimension) DetermineDimension();

            // traverse the elements to assemble coefficients of the diagonal matrix
            foreach (var item in modell.Elemente)
            {
                var abstractElement = item.Value;
                var index = abstractElement.SystemIndicesOfElement;
                var elementMatrix = abstractElement.ComputeDiagonalMatrix();
                systemEquations.AddDiagonalMatrix(index, elementMatrix);
            }
            diagonalMatrix = true;
        }

        // 1st order time integration ***********************************************************************************************
        public void TimeIntegration1StOrder()
        {
            // ... Compute specific heat matrix ..............................
            if (!diagonalMatrix) ComputeDiagonalMatrix();
            _ = systemEquations.DiagonalMatrix;


            var dt = modell.Zeitintegration.Dt;
            if (dt == 0)
            {
                throw new BerechnungAusnahme("Abbruch: Zeitschrittintervall nicht definiert.");
            }
            var tmax = modell.Zeitintegration.Tmax;
            var alfa = modell.Zeitintegration.Parameter1;
            var nSteps = (int)(tmax / dt) + 1;
            var forceFunction = new double[nSteps][];
            for (var k = 0; k < nSteps; k++)
                forceFunction[k] = new double[dimension];
            var temperature = new double[nSteps][];
            for (var i = 0; i < nSteps; i++) temperature[i] = new double[dimension];

            Set1StOrderInitialConditions(temperature);
            SetInstationaryStatusVector();

            // ... compute time dependent forcing function and boundary conditions
            Compute1StOrderForcingFunction(dt, forceFunction);
            Compute1StOrderBoundaryConditions(dt, temperature);

            // ... system matrix needs to be recomputed if it is decomposed
            if (decomposed) { ReComputeSystemMatrix(); decomposed = false; }

            var timeIntegration = new Zeitintegration1OrdnungStatus(
                systemEquations, forceFunction, dt, alfa, temperature);
            timeIntegration.Perform();

            // save nodal time histories
            foreach (var item in modell.Knoten)
            {
                node = item.Value;
                var index = item.Value.SystemIndices[0];
                node.NodalVariables = new double[1][];
                node.NodalVariables[0] = new double[nSteps];
                node.NodalDerivatives = new double[1][];
                node.NodalDerivatives[0] = new double[nSteps];

                // temperature[nSteps][index], NodalVariables[index][nSteps]
                for (var k = 0; k < nSteps; k++)
                {
                    node.NodalVariables[0][k] = temperature[k][index];
                    node.NodalDerivatives[0][k] = timeIntegration.Tdot[k][index];
                }
            }
            modell.Timeint = true;
        }
        private void Set1StOrderInitialConditions(IList<double[]> temperature)
        {
            // set initial conditions to stationary solution
            if (modell.Zeitintegration.FromStationary)
            {
                temperature[0] = systemEquations.Primal;
            }
            foreach (Knotenwerte anf in modell.Zeitintegration.Anfangsbedingungen)
            {
                if (anf.NodeId == "alle")
                {
                    var temperatur = anf.Values[0];
                    for (var i = 0; i < dimension; i++) temperature[0][i] = temperatur;
                }
                else
                {
                    var nodeId = anf.NodeId;
                    if (modell.Knoten.TryGetValue(nodeId, out var knoten))
                    {
                        temperature[0][knoten.SystemIndices[0]] = anf.Values[0];
                    }
                }
            }
        }
        private void SetInstationaryStatusVector()
        {
            // für alle zeitabhaengigen Randbedingungen
            if (modell == null) return;
            foreach (var randbedingung in
                modell.ZeitabhängigeRandbedingung.Select(item => item.Value))
            {
                StatusNodes(randbedingung);
            }
        }
        // time dependent nodal and element loads
        private void Compute1StOrderForcingFunction(double dt, double[][] temperature)
        {
            var force = new double[temperature.Length];
            var nSteps = force.Length;

            // finde zeitabhängige Knotenlasten
            foreach (var item in modell.ZeitabhängigeKnotenLasten)
            {
                if (modell.Knoten.TryGetValue(item.Value.NodeId, out node))
                {
                    var lastIndex = node.SystemIndices;

                    switch (item.Value.VariationType)
                    {
                        case 0:
                            {
                                // Datei einlesen
                                const string inputDirectory = "\\Visual Studio 2019\\Projekte\\FE Berechnungen\\input\\Wärmeberechnung\\instationär\\Anregungsdateien";
                                const int col = 1;
                                FromFile(inputDirectory, col, force);
                                break;
                            }
                        case 1:
                            {
                                // stückweise linear
                                var interval = item.Value.Interval;
                                PiecewiseLinear(dt, interval, force);
                                break;
                            }
                        case 2:
                            {
                                // periodisch
                                var amplitude = item.Value.Amplitude;
                                var frequency = item.Value.Frequency;
                                var phaseAngle = item.Value.PhaseAngle;
                                Periodic(dt, amplitude, frequency, phaseAngle, force);
                                break;
                            }
                    }
                    for (var k = 0; k < nSteps; k++)
                        temperature[k][lastIndex[0]] = force[k];
                }
                else
                {
                    throw new BerechnungAusnahme("Knoten " + item.Value.NodeId + " für zeitabhängige Knotenlast ist nicht im Modell enthalten.");
                }
            }

            // finde zeitabhängige Elementlasten
            foreach (var timeDepElementLoad in modell.ZeitabhängigeElementLasten.Select(item => item.Value))
            {
                if (modell.Elemente.TryGetValue(timeDepElementLoad.ElementId, out var abstractElement))
                {
                    var index = abstractElement.SystemIndicesOfElement;
                    var lastVektor = timeDepElementLoad.ComputeLoadVector();
                    systemEquations.AddVector(index, lastVektor);
                }
                for (var k = 0; k < nSteps; k++)
                    temperature[k] = systemEquations.Vector;
            }
        }
        // time dependent predefined fixed boundary conditions
        private void Compute1StOrderBoundaryConditions(double dt, double[][] temperature)
        {
            var nSteps = temperature.Length;
            var preTemperature = new double[nSteps];

            foreach (var item in modell.ZeitabhängigeRandbedingung)
            {
                if (modell.Knoten.TryGetValue(item.Value.NodeId, out node))
                {
                    var lastIndex = node.SystemIndices;

                    switch (item.Value.VariationType)
                    {
                        case 0:
                            {
                                // Datei einlesen
                                const string inputDirectory = "\\Visual Studio 2019\\Projekte\\FE Berechnungen\\input\\Wärmeberechnung\\instationär\\Anregungsdateien";
                                const int col = 1;
                                FromFile(inputDirectory, col, preTemperature);
                                break;
                            }
                        case 1:
                            {
                                // stückweise linear
                                var interval = item.Value.Interval;
                                PiecewiseLinear(dt, interval, preTemperature);
                                break;
                            }
                        case 2:
                            {
                                // periodisch
                                var amplitude = item.Value.Amplitude;
                                var frequency = item.Value.Frequency;
                                var phaseAngle = item.Value.PhaseAngle;
                                Periodic(dt, amplitude, frequency, phaseAngle, preTemperature);
                                break;
                            }
                        case 3:
                            {
                                // konstant
                                for (var k = 0; k < nSteps; k++)
                                {
                                    preTemperature[k] = item.Value.KonstanteTemperatur;
                                }

                                break;
                            }
                    }
                    StatusNodes(item.Value);
                    for (var k = 0; k < nSteps; k++)
                        temperature[k][lastIndex[0]] = preTemperature[k];
                }
                else
                {
                    throw new BerechnungAusnahme("Knoten " + item.Value.NodeId + " für zeitabhängige Randbedingung ist nicht im Modell enthalten.");
                }
            }
        }

        // 2nd order time integration ***********************************************************************************************
        public void TimeIntegration2NdOrder()
        {
            var dt = modell.Zeitintegration.Dt;
            if (dt == 0)
            {
                throw new BerechnungAusnahme("Zeitschrittintervall nicht definiert");
            }
            var tmax = modell.Zeitintegration.Tmax;
            var nSteps = (int)(tmax / dt) + 1;
            var method = modell.Zeitintegration.Method;
            var parameter1 = modell.Zeitintegration.Parameter1;
            var parameter2 = modell.Zeitintegration.Parameter2;
            var anregung = new double[nSteps + 1][];
            for (var i = 0; i < (nSteps + 1); i++) anregung[i] = new double[dimension];
            // ... Compute diagonal mass matrix ..............................
            if (!diagonalMatrix) ComputeDiagonalMatrix();

            // ... Compute diagonal damping matrix ..............................
            // ... if "damping" contains modal damping ratios, the damping matrix
            // ... may be evaluated by a sum over "n" modes considered C&P p.198, 13-37
            // ... M*(SUM(((2*(xi)n*(omega)n )/(M)n))*phi(n)*(phi)nT)*M
            // ... where M is the mass matrix, (xi)n the modal damping ratio,
            // ... (omega(n) eigenfrequency, (M)n modal masses and phi the eigenvectors
            var dampingMatrix = ComputeDampingMatrix();

            // ... compute time dependent forcing function and boundary conditions
            Compute2NdOrderForcingFunction(dt, anregung);

            var displacement = new double[nSteps][];
            for (var k = 0; k < nSteps; k++) displacement[k] = new double[dimension];
            var velocity = new double[2][];
            for (var k = 0; k < 2; k++) velocity[k] = new double[dimension];

            Set2NdOrderInitialConditions(displacement, velocity);
            SetDynamicStatusVector();

            if (decomposed)
            {
                ReComputeSystemMatrix();
                decomposed = false;
            }

            var timeIntegration = new Zeitintegration2OrdnungStatus(systemEquations, dampingMatrix,
                dt, method, parameter1, parameter2,
                displacement, velocity, anregung);
            timeIntegration.Perform();

            // save nodal time histories
            foreach (var item2 in modell.Knoten)
            {
                node = item2.Value;
                var index = item2.Value.SystemIndices;
                var ndof = node.NumberOfNodalDof;

                node.NodalVariables = new double[ndof][];
                for (var i = 0; i < ndof; i++) node.NodalVariables[i] = new double[nSteps];
                node.NodalDerivatives = new double[ndof][];
                for (var i = 0; i < ndof; i++) node.NodalDerivatives[i] = new double[nSteps];

                // displacement[nSteps][index], velocity[2][index], NodalVariables[index][nSteps]
                for (var i = 0; i < node.NumberOfNodalDof; i++)
                {
                    if (systemEquations.Status[index[i]]) continue;
                    for (var k = 0; k < nSteps; k++)
                    {
                        node.NodalVariables[i][k] = timeIntegration.displacement[k][index[i]];
                        node.NodalDerivatives[i][k] = timeIntegration.acceleration[k][index[i]];
                    }
                }
            }
            modell.Timeint = true;
            _ = MessageBox.Show("Zeitverlaufberechnung 2. Ordnung erfolgreich durchgeführt", "TimeIntegration2ndOrder");
        }
        private double[] ComputeDampingMatrix()
        {
            var damping = new double[dimension];
            foreach (Knotenwerte dämpfung in modell.Zeitintegration.DämpfungsRaten)
            {
                var dampedNodeId = dämpfung.NodeId;
                var dampingRatios = dämpfung.Values;
                if (dampedNodeId.Equals("all", StringComparison.InvariantCultureIgnoreCase))
                {
                    for (var i = 0; i < damping.Length; i++) damping[i] = dampingRatios[0];
                }
                else
                {
                    if (modell.Knoten.TryGetValue(dampedNodeId, out node))
                    {
                        for (var i = 0; i < node.SystemIndices.Length; i++)
                            damping[node.SystemIndices[i]] = dampingRatios[i];
                    }
                    else
                    {
                        throw new BerechnungAusnahme("Dämpfungsknoten " + dampedNodeId + " ist nicht im Modell enthalten.");
                    }
                }
            }
            return damping;
        }
        private void Set2NdOrderInitialConditions(IReadOnlyList<double[]> displ, IReadOnlyList<double[]> veloc)
        {
            // find pedefined initial conditions
            foreach (Knotenwerte anf in modell.Zeitintegration.Anfangsbedingungen)
            {
                if (!modell.Knoten.TryGetValue(anf.NodeId, out var knoten)) continue;
                for (var i = 0; i < anf.Values.Length / 2; i += 2)
                {
                    foreach (var nodeIndexIndex in knoten.SystemIndices)
                    {
                        displ[i][nodeIndexIndex] = anf.Values[i];
                        veloc[i + 1][nodeIndexIndex] = anf.Values[i + 1];
                    }
                }
            }
        }
        private void SetDynamicStatusVector()
        {
            // für alle zeitabhaengigen Randbedingungen
            foreach (var randbedingung in
                modell.ZeitabhängigeRandbedingung.Select(item => item.Value))
            {
                StatusNodes(randbedingung);
            }
        }
        // time dependent nodal influences
        private void Compute2NdOrderForcingFunction(double dt, IReadOnlyList<double[]> excitation)
        {
            var force = new double[excitation.Count];

            // find time dependent nodal influences
            foreach (var item in modell.ZeitabhängigeKnotenLasten)
            {
                if (!modell.Knoten.TryGetValue(item.Value.NodeId, out node)) continue;
                var index = node.SystemIndices;
                var nodalDof = item.Value.NodalDof;
                var ground = item.Value.Bodenanregung;
                var type = item.Value.VariationType;

                switch (type)
                {
                    case 0:
                        {
                            const string inputDirectory = "\\Visual Studio 2019\\Projekte\\FE Berechnungen\\input\\Tragwerksberechnung\\Dynamik\\Anregungsdateien";
                            const int col = -1; // ALLE Values in Datei
                                                // Ordinatenwerte im Zeitintervall dt aus Datei lesen
                            FromFile(inputDirectory, col, force);
                            break;
                        }
                    case 1:
                        {
                            var interval = item.Value.Interval;
                            // lineare Interpolation der abschnittweise linearen Eingabedaten im Zeitintervall dt
                            PiecewiseLinear(dt, interval, force);
                            break;
                        }
                    case 2:
                        {
                            var amplitude = item.Value.Amplitude;
                            var frequency = item.Value.Frequency;
                            var phaseAngle = item.Value.PhaseAngle;
                            // periodische Anregung mit Ausgabe "force" im Zeitintervall dt
                            Periodic(dt, amplitude, frequency, phaseAngle, force);
                            break;
                        }
                }

                if (ground)
                {
                    var mass = systemEquations.DiagonalMatrix;
                    foreach (var item2 in modell.Knoten)
                    {
                        index = item2.Value.SystemIndices;
                        if (systemEquations.Status[index[nodalDof]]) continue;
                        for (var k = 0; k < excitation.Count; k++)
                            excitation[k][index[nodalDof]] = -mass[index[nodalDof]] * force[k];
                    }
                }

                else
                {
                    for (var k = 0; k < excitation.Count; k++)
                        for (var j = 0; j < excitation[0].Length; j++)
                            excitation[k][index[nodalDof]] = force[k];
                }
            }
        }

        // time varying input data
        private void FromFile(string inputDirectory, int col, IList<double> force)
        {
            string[] lines, substrings;
            var delimiters = new[] { '\t' };

            var file = new OpenFileDialog
            {
                Filter = "All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            file.InitialDirectory += inputDirectory;

            if (file.ShowDialog() != true)
                return;
            var path = file.FileName;

            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (IOException ex)
            {
                _ = MessageBox.Show(ex + " Anregungsfunktion konnte nicht aus Datei gelesen werden!!!", "Analysis FromFile");
                return;
            }
            // Anregungsfunktion[timeSteps]
            var counter = 0;
            if (col < 0)
            {
                // lies alle Values einer Datei
                foreach (var line in lines)
                {
                    substrings = line.Split(delimiters);
                    foreach (var word in substrings)
                    {
                        force[counter] = double.Parse(word);
                        counter++;
                    }
                }
            }
            else
            {
                // lies alle Values einer bestimmten Spalte [][0-n]
                for (var k = 0; k < lines.Length; k++)
                {
                    substrings = lines[k].Split(delimiters);
                    force[k] = double.Parse(substrings[col]);
                }
            }
        }
        private static void PiecewiseLinear(double dt, IReadOnlyList<double> interval, IList<double> force)
        {
            int counter = 0, nSteps = force.Count;
            double endLoad = 0;
            var startTime = interval[0];
            var startLoad = interval[1];
            force[counter] = startLoad;
            for (var j = 2; j < interval.Count; j += 2)
            {
                var endTime = interval[j];
                endLoad = interval[j + 1];
                var steps = (int)(Math.Round((endTime - startTime) / dt));
                var increment = (endLoad - startLoad) / steps;
                for (var k = 1; k <= steps; k++)
                {
                    counter++;
                    if (counter == nSteps) return;
                    force[counter] = force[counter - 1] + increment;
                }
                startTime = endTime;
                startLoad = endLoad;
            }
            for (var k = counter + 1; k < nSteps; k++) force[k] = endLoad;
        }
        private static void Periodic(double dt, double amplitude, double frequency, double phaseAngle, double[] force)
        {
            var nSteps = force.GetLength(0);
            double time = 0;
            for (var k = 0; k < nSteps; k++)
            {
                force[k] = amplitude * Math.Sin(frequency * time + phaseAngle);
                time += dt;
            }
        }
    }
}
