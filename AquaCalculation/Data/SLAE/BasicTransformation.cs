using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Data.SLAE
{
    static class BasicTransformation
    {
        static public List<double> Transponding(List<double> valueX)
        {
            int n = (int)Math.Sqrt((double)valueX.Count);

            List<double> returnValue = new List<double>(valueX);

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    returnValue[j * n + i] = valueX[i * n + j];
                }
            }
            return returnValue;
        }

        static public List<double> Reverse(List<double> valueX)
        {
            List<double> returnValue = new List<double> { };

            for (int i = valueX.Count - 1; i >= 0; i--)
            {
                returnValue.Add(valueX[i]);
            }
            return returnValue;
        }
    }
}
