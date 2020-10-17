using AquaCalculation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Servises.RootsSeparation.Base
{
    internal interface IRootSeparation <T>
    {
        ICollection<XYValueModel> RootSeparation(T valueModel);
    }
}
