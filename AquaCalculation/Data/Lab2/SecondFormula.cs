using AquaCalculation.Data.UsefulFormulas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Data.Lab2
{
    class SecondFormula
    {
        static public List<List<double>> FindDelY(List<double> y, int power)
        {
            List<List<double>> DelY = new List<List<double>> { };

            for (int i = 0; i < power; i++)
            {
                List<double> delY = new List<double> { };

                for (int z = 0; z < y.Count - i - 1; z++)
                {
                    if (i == 0)
                        delY.Add(y[z + 1] - y[z]);
                    else
                        delY.Add(DelY[i - 1][z + 1] - DelY[i - 1][z]);
                }

                DelY.Add(delY);

            }

            return DelY;

        }

        static public double SecondInterpolation(List<double> x, List<double> y, double X, int power)
        {
            double h = 0;
            if (x.Count > 1)
                h = x[1] - x[0];
            else
                return 0;

            int tempt = 0;

            foreach (var el in x)
            {
                if (el <= X) tempt++;
                else break;
            }

            if (!(x.Count < tempt) && tempt - 1 >= 0) tempt--;
            double t = (X - x[tempt]) / h;

            List<List<double>> delY = FindDelY(y, power);


            int n = delY.Count;

            double P = y[tempt];

            for (int i = 0; i < power; i++)
            {
                double ourTempT = t;
                for (int k = 0; k < i; k++)
                {
                    t *= (t + k);
                }
                P += ((t * delY[i][n - 1]) / Factorial.FindFactorial(i + 1));
            }

            return P;
        }
    }
}
