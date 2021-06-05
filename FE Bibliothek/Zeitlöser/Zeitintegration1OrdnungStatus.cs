﻿using FE_Bibliothek.Gleichungslöser;
using FE_Bibliothek.Modell;
using FE_Bibliothek.Werkzeuge.FEALibrary.Utils;

namespace FE_Bibliothek.Zeitlöser
{
    public class Zeitintegration1OrdnungStatus
    {
        private readonly int dimension;
        private readonly double dt, alfa;
        private readonly double[] c;
        public double[] That { get; private set; }
        private readonly double[][] k;
        public double[][] ForcingFunction { get; }
        private readonly int[] profile;
        private readonly bool[] status;

        public double[][] Temperature { get; set; }
        public double[][] Tdot { get; set; }

        public Zeitintegration1OrdnungStatus(Gleichungen systemEquations, double[][] excitation,
                                                double dt, double alfa, double[][] initial)
        {
            c = systemEquations.DiagonalMatrix;
            k = systemEquations.Matrix;
            ForcingFunction = excitation;
            profile = systemEquations.Profile;
            status = systemEquations.Status;
            this.dt = dt;
            this.alfa = alfa;
            Temperature = initial;
            dimension = k.Length;
        }

        public Zeitintegration1OrdnungStatus(double[] c, double[][] k, double[][] excitation,
                                                int[] profile, bool[] status,
                                                double dt, double alfa, double[][] initial)
        {
            this.c = c;
            this.k = k;
            ForcingFunction = excitation;
            this.profile = profile;
            this.status = status;
            this.dt = dt;
            this.alfa = alfa;
            Temperature = initial;
            dimension = this.k.Length;
        }

        public void Perform()
        {
            var alfaDt = alfa * dt;
            var dtAlfa = dt * (1 - alfa);
            var primal = new double[dimension];

            var timeSteps = Temperature.Length;
            That = new double[dimension];
            Tdot = new double[timeSteps][];
            for (var i = 0; i < timeSteps; i++) { Tdot[i] = new double[dimension]; }

            // calculate initial temperature gradients
            var rhs = MatrizenAlgebra.Mult(k, Temperature[0], profile);
            for (var i = 0; i < dimension; i++)
                Tdot[0][i] = (ForcingFunction[0][i] - rhs[i]) / c[i];

            // evaluate constant coefficient matrix
            var cm = new double[dimension][];
            for (var row = 0; row < dimension; row++)
            {
                cm[row] = new double[row + 1 - profile[row]];
                for (var col = 0; col <= (row - profile[row]); col++)
                    cm[row][col] = alfaDt * k[row][col];
                cm[row][row - profile[row]] += c[row];
            }

            var profileSolverStatus =
                            new ProfillöserStatus(cm, rhs, primal, status, profile);
            profileSolverStatus.Decompose();

            for (var counter = 1; counter < timeSteps; counter++)
            {
                // calculate temperature gradients at restrained nodes 
                for (var i = 0; i < dimension; i++)
                    if (status[i])
                        Tdot[counter][i] = (Temperature[counter][i] - Temperature[counter - 1][i]) / dt;

                // calculate T(hat) for next step
                for (var i = 0; i < dimension; i++)
                    if (status[i]) That[i] = Temperature[counter][i];
                    else That[i] = Temperature[counter - 1][i] + dtAlfa * Tdot[counter - 1][i];

                // modification of RHS
                rhs = MatrizenAlgebra.Mult(k, That, status, profile);
                var rhSfr = MatrizenAlgebra.MultUl(cm, Tdot[counter], status, profile);
                for (var i = 0; i < dimension; i++)
                    rhs[i] = ForcingFunction[counter][i] - rhSfr[i] - rhs[i];

                // backsubstitution
                profileSolverStatus.SetRhs(rhs);
                profileSolverStatus.SolvePrimal();

                // temperatures and gradients at next time step for unrestrained nodes
                for (var i = 0; i < dimension; i++)
                {
                    if (status[i]) continue;
                    Tdot[counter][i] = primal[i];
                    Temperature[counter][i] = That[i] + alfaDt * primal[i];
                }
            }
        }
    }
}
