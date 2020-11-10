using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Data.SLAE
{
    static class GaussienElimination
    {
        static public List<double> GaussienInvoke(List<double> ElementValue, List<double> FreeValue, int n = 0, int m = 0)
        {
            if(n <= 0 || m <= 0)
            {
                m = n = (int)Math.Sqrt((double)ElementValue.Count);
            }
            else if(n * m != ElementValue.Count)
            {
                return null;
            }
            double FirstElement = ElementValue[0];
            for (int i = 0; i < m; i++)
                ElementValue[i] /= FirstElement;
            FreeValue[0] /= FirstElement;
            for (int step = 0; step < 2; step++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    for (int i = j + 1; i < n; i++)
                    {
                        double ValueExpression = ElementValue[i * n + j] / ElementValue[j * n + j];
                        for (int k = j; k < m; k++)
                        {
                            ElementValue[i * n + k] -= (ValueExpression * ElementValue[j * n + k]);
                        }
                        FreeValue[i] -= (ValueExpression * FreeValue[j]);

                        if (step == 0)
                        {
                            double Element = ElementValue[i * n + i];
                            for (int k = j; k < m; k++)
                            {
                                ElementValue[i * n + k] /= Element;
                            }
                            FreeValue[i] /= Element;
                        }
                    }
                }
                FreeValue = BasicTransformation.Reverse(FreeValue);
                ElementValue = BasicTransformation.Reverse(ElementValue);
            }

            return FreeValue;
        }
    }
}
