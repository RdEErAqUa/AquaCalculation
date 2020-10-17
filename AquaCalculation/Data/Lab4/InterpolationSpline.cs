using AquaCalculation.Model.Lab4;
using AquaCalculation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Data.Lab4
{
    class InterpolationSpline
    {
        public List<lab4Data> findAllABCDValue(List<XYValueModel> XYValue)
        {
            List<lab4Data> returnAnswer = new List<lab4Data> { };

            List<double> h = new List<double> { };

            for (int i = 1; i < XYValue.Count; i++)
            {
                h.Add(XYValue[i].X - XYValue[i - 1].X);
            }

            for (int i = 0; i < XYValue.Count - 1; i++)
            {
                returnAnswer.Add(new lab4Data { A = XYValue[i].Y });
            }

            return null;


        }
    }
}
