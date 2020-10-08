using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Data.UsefulFormulas
{
    static class Factorial
    {
        static public double FindFactorial(int value)
        {
            if (value == 0)
                return 1;
            else
                return value * FindFactorial(value - 1);
        }
    }
}
