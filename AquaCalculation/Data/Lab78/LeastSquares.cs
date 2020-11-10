using AquaCalculation.Data.SLAE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AquaCalculation.Data.Lab78
{
    static class LeastSquares
    {
        //ax + b
        static public List<double> LinearAproximation(List<double> valueX, List<double> valueY) 
        {
            double SX = 0, SXX = 0, SY = 0, SXY = 0;

            for(int i = 0; i < valueX.Count; i++)
            {
                SX += valueX[i];
                SXX += Math.Pow(valueX[i], 2);
                SY += valueY[i];
                SXY += (valueY[i] * valueX[i]);
            }
            double delta = (valueX.Count + 1) * SXX - Math.Pow(SX, 2);
            double deltaA = (valueX.Count + 1) * SXY - SX * SY;
            double deltaB = SXX * SY - SX * SXY;

            double a = deltaA / delta;
            double b = deltaB / delta;

            List<double> AB = new List<double> { a, b};

            return AB;
        }

        static public List<double> QuadraticAproximation(List<double> valueX, List<double> valueY)
        {
            double y1 = 0, y2 = 0, y3 = 0;

            double x1 = 0, x2 = 0, x3 = 0, x4 = 0;

            for(int i = 0; i < valueX.Count; i++)
            {
                y1 += valueY[i];
                y2 += (valueX[i] * valueY[i]);
                y3 += (Math.Pow(valueX[i], 2) * valueY[i]);
                x1 += valueX[i];
                x2 += Math.Pow(valueX[i], 2);
                x3 += Math.Pow(valueX[i], 3);
                x4 += Math.Pow(valueX[i], 4);
            }
            List<double> FreeValue = new List<double> { y1, y2, y3};
            List<double> ElementValue = new List<double> { (valueX.Count + 1), x1, x2,  
                                                            x1, x2, x3,
                                                            x2, x3, x4};
            ElementValue = GaussienElimination.GaussienInvoke(ElementValue, FreeValue);
            return ElementValue;
        }

    }
}
