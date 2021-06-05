using System;
using FE_Bibliothek.Modell;

namespace FE_Bibliothek.Gleichungslöser
{
    //--------------------------------------------------------------------
    //  CLASS : ProfillöserStatus             Lineares Gleichungssystem
    //--------------------------------------------------------------------
    //  FUNKTION :
    //
    //  Erzeugung und Lösung eines linearen Gleichungssystems
    //  mit symmetrischer Profilstruktur :
    //
    //      A * u = w + q
    //
    //  A   Systemmatrix mit vordefinierten Koeffizienten
    /// u   primal Lösungsvektor  (Vektor der Unkekannten)
    //  q   dual Lösungsvektor     (Vector der Reaktionen an Randbedingungen)
    //  w   Systemvektor with mit vordefinierten Koeffizienten (Lastvektor)
    //
    //  In jeder Zeile, ist entweder u[i] oder q[i] gegeben.
    //
    //-------------------------------------------------------------------
    //  METHODEN :
    //
    // public ProfileSolverStatus(double matrix [][], double vector [],
    // double primal[], double dual[],
    // boolean status[], int profile[])
    //
    // public ProfileSolverStatus(double matrix [][],
    // double primal[], double dual[],
    // boolean status[], int profile[])
    //
    //  public void SetRHS(double [] newVector)
    //  public void Decompose() throws Berechnungsausnahme
    //  public void Solve()
    //
    //-------------------------------------------------------------------

    public class ProfillöserStatus
    {
        private readonly bool[] status;              // true  : primal vorgegeben
                                                     // false : dual   vorgegeben
        private readonly int[] profile;              // Index der 1. spalte != 0
        private readonly double[][] matrix;          // Systemmatrix A
        private double[] vector;                     // Systemvektor w
        private readonly double[] primal;            // primal Lösungsvektor
        private readonly double[] dual;              // dual   Lösungsvektor
        private int row, column;
        private readonly int dimension;

        //..Erzeugung des Gleichungssystems..........................
        public ProfillöserStatus(double[][] mat, double[] vec,
               double[] prim, double[] dua,
               bool[] stat, int[] prof)
        {
            matrix = mat;
            vector = vec;
            primal = prim;
            dual = dua;
            status = stat;
            profile = prof;
            dimension = matrix.Length;
        }
        //..ohne vorgegebene Randbedingungen
        public ProfillöserStatus(double[][] mat, double[] vec,
              double[] prim,
              bool[] stat, int[] prof)
        {
            matrix = mat;
            vector = vec;
            primal = prim;
            status = stat;
            profile = prof;
            dimension = matrix.Length;
        }
        //..falls Matrix nur zerlegt werden soll
        public ProfillöserStatus(double[][] mat,
              bool[] stat, int[] prof)
        {
            matrix = mat;
            status = stat;
            profile = prof;
            dimension = matrix.Length;
        }

        public void SetRhs(double[] newVector) { this.vector = newVector; }

        // Dreieckszerlegung der Systemmatrix .........................
        public void Decompose()
        {
            //..A[i][m] = A[i][m] - Sum(A[i][k]*A[k][m]) / A[k][k]..........
            for (row = 0; row < dimension; row++)
            {
                if (status[row]) continue;
                double sum;
                for (column = profile[row]; column < row; column++)
                {
                    if (status[column]) continue;
                    var start = Math.Max(profile[row], profile[column]);
                    sum = matrix[row][column - profile[row]];
                    for (var m = start; m < column; m++)
                    {
                        if (status[m]) continue;
                        sum -= matrix[row][m - profile[row]] * matrix[column][m - profile[column]];
                    }
                    sum /= matrix[column][column - profile[column]];
                    matrix[row][column - profile[row]] = sum;
                }

                //..A[i][i] = sqrt{(A[i][i] - Sum(A[i][m]*A[m][i])}...................
                sum = matrix[row][row - profile[row]];
                for (var m = profile[row]; m < row; m++)
                {
                    if (status[m]) continue;
                    sum -= matrix[row][m - profile[row]] * matrix[row][m - profile[row]];
                }
                if (sum < double.Epsilon) throw new BerechnungAusnahme("Gleichungslöser: Element <= 0 in Dreieckszerlegung von Zeile " + row);
                matrix[row][row - profile[row]] = Math.Sqrt(sum);
            }
        }

        //__Lösung der Systemgleichungen_______________________________________
        //..ersetze die vorgegebenen Werte in den Zeilen ohne
        //  vorgegebene Primärvariable : u = c1 + y1 - A12 * x2
        public void Solve()
        {
            SolvePrimal();
            SolveDual();
        }
        public void SolvePrimal()
        {
            for (row = 0; row < dimension; row++)
            {
                if (status[row]) continue;
                primal[row] = vector[row];
                for (column = profile[row]; column < row; column++)
                {
                    if (!status[column]) continue;
                    primal[row] -= matrix[row][column - profile[row]] * primal[column];
                }
            }

            for (column = 0; column < dimension; column++)
            {
                if (!status[column]) continue;
                for (row = profile[column]; row < column; row++)
                {
                    if (status[row]) continue;
                    primal[row] -= matrix[column][row - profile[column]] * primal[column];
                }
            }

            //..berechne Primärvariable : zeilenweise Vorwärtszerlegung.................
            for (row = 0; row < dimension; row++)
            {
                if (status[row]) continue;
                for (column = profile[row]; column < row; column++)
                {
                    if (status[column]) continue;
                    primal[row] -= matrix[row][column - profile[row]] * primal[column];
                }
                primal[row] /= matrix[row][row - profile[row]];
            }

            //..berechne Primärvariable : zeilenweise Rückwärtszerlegung................
            for (column = dimension - 1; column >= 0; column--)
            {
                if (status[column]) continue;
                primal[column] /= matrix[column][column - profile[column]];
                for (row = profile[column]; row < column; row++)
                {
                    if (status[row]) continue;
                    primal[row] -= matrix[column][row - profile[column]] * primal[column];
                }
            }
        }
        public void SolveDual()
        {
            //..berechne die Dualvariablen : ersetze die Primärvariablen.........
            //  in den Zeilen mit den Vorgegebenen Primärvariablen :
            for (row = 0; row < dimension; row++)
            {
                if (!status[row]) continue;
                dual[row] = -vector[row];
                for (column = profile[row]; column <= row; column++)
                    dual[row] += matrix[row][column - profile[row]] * primal[column];
            }

            for (column = 0; column < dimension; column++)
            {
                for (row = profile[column]; row < column; row++)
                {
                    if (!status[row]) continue;
                    dual[row] += matrix[column][row - profile[column]] * primal[column];

                }
            }
        }
    }
}