using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Data.MathLabs
{
    static class LagrangePolynom
    {

        static List<double> _x { get; set; }
        static List<double> _y { get; set; }
        static List<double> _needX { get; set; }

        static public double InterpolationPolynomN(double y1, double y2, double x1, double x2, double x) =>
            (1 / (x2 - x1)) * (Determinant.findDeterminant2(y1, x1 - x, y2, x2 - x));

        static public double InterpolationPolynom(List<double> x, List<double> y, double X, double Size)
        {
            double answer = 0;

            for (int i = 0; i < Size; i++)
            {
                double temp1 = 1;
                for (int z = 0; z < Size; z++)
                {
                    if (z == i)
                        continue;
                    else
                        temp1 *= (X - x[z]);
                }
                double temp2 = 1;
                for (int z = 0; z < Size; z++)
                {
                    if (z == i)
                        continue;
                    else
                        temp2 *= (x[i] - x[z]);
                }

                double temp3 = temp1 / temp2;
                temp3 *= y[i];

                answer += temp3;
            }

            return answer;
        }

        static public List<List<double>> SchemeAitken(List<double> valueX, List<double> valueY, int k, double x)
        {
            List<List<double>> L = new List<List<double>> { };

            for (int i = 0; i < k; i++)
            {
                List<double> tempList = new List<double> { };
                L.Add(tempList);

                for (int z = 0; z < valueX.Count - 1 - i; z++)
                {
                    if (i == 0)
                        L[i].Add(Math.Round(InterpolationPolynomN(valueY[z], valueY[z + 1], valueX[z], valueX[z + 1], x), 5));
                    else
                        L[i].Add(Math.Round(InterpolationPolynomN(L[i - 1][z], L[i - 1][z + 1], valueX[z], valueX[z + 1], x), 5));
                }
            }
            return L;
        }
    }
}
