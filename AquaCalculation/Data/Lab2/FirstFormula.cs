using AquaCalculation.Data.UsefulFormulas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Data.Lab2
{
    static class FirstFormula
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

        static public double FirstInterpolation(List<double> x, List<double> y, double X, int power)
        {
            double h = 0;
            if (x.Count > 1)
                h = x[1] - x[0];
            else
                return 0;

            double t = (X - x[0]) / h;

            List<List<double>> delY = FindDelY(y, power);

            double P = y[0];

            int posX = 0;

            for (int i = 0; i < x.Count; i++)
            {
                if (x[i] < X)
                    continue;
                else
                    posX = i - 1;

                break;
            }


            for (int i = 0; i < power; i++)
            {
                double ourTempT = t;
                for (int k = 0; k < i; k++)
                {
                    t *= (t - k);
                }
                P += ((t * delY[i][1]) / Factorial.FindFactorial(i + 1));
            }

            return P;
        }
    }
}
