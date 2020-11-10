using AquaCalculation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Data.Lab5
{
    static class BSpline
    {
        static public List<double> BNSpline(List<double> pointsX, List<double> pointsY, int degree, double x)
        {
            int NValue = pointsX.Count;
            List<double> Weights = new List<double> { };
            List<double> Knots = new List<double> { };

            for (int i = 0; i < NValue; i++)
                Weights.Add(1);
            for (int i = 0; i < NValue + degree + 1; i++)
                Knots.Add(i);


            if (degree < 1 && degree > (NValue - 1))
                return null;

            int lowValue = degree;
            int HighValue = NValue;

            x = x * (HighValue - lowValue) + lowValue;

            int s = 0;

            List<double> xData = new List<double>(pointsX);
            List<double> yData = new List<double> (pointsY);


            for (s = lowValue; s < HighValue; s++)
            {
                if (x >= Knots[s] && x <= Knots[s + 1])
                {
                    break;
                }
            }

            for (int l = 1; l <= degree + 1; l++)
            {
                for (int i = s; i > s - degree - 1 + l; i--)
                {
                    double aplha = (x - Knots[i]) / (Knots[i + degree + 1 - l] - Knots[i]);

                    double answer = (1 - aplha) * xData[i - 1] + aplha * xData[i];

                    double answer2 = (1 - aplha) * yData[i - 1] + aplha * yData[i];

                    xData[i] = answer;
                    yData[i] = answer2;
                }
            }
            List<double> answer321 = new List<double> { };

            answer321.Add(xData[s]);
            answer321.Add(yData[s]);
            return answer321;


        }
    }
}
