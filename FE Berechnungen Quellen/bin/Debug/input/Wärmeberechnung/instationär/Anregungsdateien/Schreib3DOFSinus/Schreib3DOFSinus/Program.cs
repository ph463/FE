using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Schreib3DOFSinus
{
    class Program
    {
        static int nSteps;
        static int dimension = 1;
        static double tmax = 0, dt = 0;
        static double[][] forceFunction;
        static StreamWriter writer;

        static void Main(string[] args)
        {
            string line;
            string[] substrings;
            char[] delimiters = new char[] { '\t', ' ' };

            WriteLine("Eingabe der Maximaldauer und des Zeitintervall:");
            // z.B. 400s und 0,99s Zeitintervall
            line = ReadLine();
            substrings = line.Split(delimiters);
            tmax = Double.Parse(substrings[0]);
            dt = Double.Parse(substrings[1]);

            nSteps = (int)(tmax / dt);
            forceFunction = new double[nSteps + 1][];
            for (int i = 0; i < nSteps+1; i++)
                forceFunction[i] = new double[dimension];

            String forceFile = "DreiDOFSinus.txt";
            forceFunction = forcingFunction();
            try
            {
                writer = File.CreateText(forceFile);


                for (int k = 0; k < nSteps + 1; k++)
                {
                    line = forceFunction[k][0].ToString();
                    for (int i = 1; i < dimension; i++)
                    {
                        line += "\t" + forceFunction[k][i];
                    }
                    writer.WriteLine(line);	    
                }
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        public static double[][] forcingFunction()
        {
            for (int k = 0; k < nSteps + 1; k++)
            {
                forceFunction[k][0] = Math.Sin(0.03 * k * dt);
                for (int i = 1; i < dimension; i++)
                    forceFunction[k][i] = 0;
            }
            return forceFunction;
        }
    }
}
