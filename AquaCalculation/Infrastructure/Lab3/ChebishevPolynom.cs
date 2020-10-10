using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Infrastructure.Lab3
{
    static class ChebishevPolynom
    {
        static public double ChebishevPolynomFind(double x, int nNeedPose)
        {
            switch (nNeedPose)
            {
                case 0:
                    return 1;
                case 1:
                    return x;
                default:
                    return ChebishevPolynomItteration(1, x, 1, nNeedPose, x);
            }
        }

        static private double ChebishevPolynomItteration(double Tn, double Tn1, int CurrentState, int nNeedPose, double x)
        {
            Tn1 = 2 * x * Tn - Tn1;

            if (++CurrentState <= nNeedPose) return ChebishevPolynomItteration(Tn1, Tn, CurrentState, nNeedPose, x);
            else return Tn1;
        }

        static public List<double> ChebishevPolynomZeroFind(int n, double a, double b)
        {
            List<double> x = new List<double> { };

            for(int i = 0; i < n; i++)
            {
                double tempX = ((a + b) / 2) + ((b - a) / 2) * Math.Cos(((n - i + 1 + 0.5) / n) * Math.PI);

                x.Add(tempX);
            }

            return x;
        }
    }
}
