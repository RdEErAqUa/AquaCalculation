using AquaCalculation.Data.MathLabs;
using AquaCalculation.Infrastructure.Commands;
using AquaCalculation.Models;
using AquaCalculation.Models.Lab1;
using AquaCalculation.ViewModels.Base;
using AquaCalculation.ViewModels.Lab1;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AquaCalculation.ViewModels
{
    internal class MainWindowModel : ViewModel
    {
        /* ----------------------------------ViewModel--------------------------------------- */

        public LagrangeViewModel LagrangeModel { get; }

        public EitkinViewModel EitkenModel { get; }

        /* ------------------------------------------------------------------------- */
        public MainWindowModel()
        {
            LagrangeModel = new LagrangeViewModel(this);

            EitkenModel = new EitkinViewModel(this);
        }
    }
}
