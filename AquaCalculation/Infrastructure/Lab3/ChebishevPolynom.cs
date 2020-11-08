using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Infrastructure.Lab3
{
    static class ChebishevPolynom
    {
        static public double ChebishevPolynomFind(double x, int nNeedPose, double AValue, double BValue)
        {
            switch (nNeedPose)
            {
                case 0:
                    return 1;
                case 1:
                    return x;
                default:
                    return ChebishevPolynomItteration(1, (2 * x - AValue - BValue) / (BValue - AValue), 1, nNeedPose, (2 * x - AValue - BValue) / (BValue - AValue));
            }
        }

        static private double ChebishevPolynomItteration(double Tn, double Tn1, int CurrentState, int nNeedPose, double t)
        {
            Tn1 = 2 * t * Tn - Tn1;

            if (++CurrentState <= nNeedPose) return ChebishevPolynomItteration(Tn1, Tn, CurrentState, nNeedPose, t);
            else return Tn1;
        }

        static public List<double> ChebishevPolynomZeroFind(int n, double a, double b)
        {
            List<double> x = new List<double> { };

            for(int i = 0; i < n; i++)
            {
                double tempX = 0.5 * (a + b) +  0.5 * (b - a) * Math.Cos((((2 * (double)i - 1) / (2 * n)) * Math.PI));
                if (i == 0)
                    tempX = -tempX;
                x.Add(tempX);
            }

            return x;
        }
    }
}
