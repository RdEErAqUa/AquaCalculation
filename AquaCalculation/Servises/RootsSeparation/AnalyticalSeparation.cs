using AquaCalculation.Models;
using AquaCalculation.Servises.RootsSeparation.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Servises.RootsSeparation
{
    class AnalyticalSeparation : IRootSeparation<ICollection<XYValueModel>>
    {
        public ICollection<XYValueModel> RootSeparation(ICollection<XYValueModel> valueModel)
        {
            List<double> xReturnValue = new List<double> { };
            List<double> yReturnValue = new List<double> { };

            double tempValue = (double)valueModel?.First().Y;
            double tempValue2 = (double)valueModel?.First().Y;

            bool needFirst = true, needSecond = true;

            for (int i = 1; i < valueModel.Count; i++)
            {
                if((valueModel.ElementAt(i).Y < tempValue || tempValue == 1) && needFirst)
                {
                    xReturnValue.Add(tempValue != -1 ? (valueModel.ElementAt(i).X + valueModel.ElementAt(i - 1).X) / 2 : valueModel.ElementAt(i - 1).X);
                    yReturnValue.Add(1);
                    needFirst = false;
                    needSecond = true;
                    continue;
                }
                if ((valueModel.ElementAt(i).Y > tempValue2 || tempValue2 == -1) && needSecond)
                {
                    xReturnValue.Add(tempValue2 != -1 ? (valueModel.ElementAt(i).X + valueModel.ElementAt(i - 1).X) / 2 : valueModel.ElementAt(i - 1).X);
                    yReturnValue.Add(-1);
                    needSecond = false;
                    needFirst = true;
                    continue;
                }
                tempValue = valueModel.ElementAt(i).Y;
                tempValue2 = valueModel.ElementAt(i).Y;
            }

            ICollection<XYValueModel> pZ = new List<XYValueModel> { };

            for (int i = 0; i < xReturnValue.Count; i++)
                pZ.Add(new XYValueModel { X = xReturnValue[i], Y = yReturnValue[i] });

            return pZ;
        }
    }
}
