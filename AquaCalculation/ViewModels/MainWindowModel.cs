using AquaCalculation.Data.MathLabs;
using AquaCalculation.Infrastructure.Commands;
using AquaCalculation.Models;
using AquaCalculation.Models.Lab1;
using AquaCalculation.ViewModels.Base;
using AquaCalculation.ViewModels.Lab1;
using AquaCalculation.ViewModels.Lab2;
using AquaCalculation.ViewModels.Lab3;
using AquaCalculation.ViewModels.Lab4;
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

        public MainLab2ViewModel MainLab2Model { get; }

        public MainLab3ViewModel MainLab3Model { get; }

        public MainLab4ViewModel MainLab4Model { get; }

        /* ------------------------------------------------------------------------- */
        public MainWindowModel()
        {
            LagrangeModel = new LagrangeViewModel(this);

            EitkenModel = new EitkinViewModel(this);

            MainLab2Model = new MainLab2ViewModel(this);

            MainLab3Model = new MainLab3ViewModel(this);

            MainLab4Model = new MainLab4ViewModel(this);
        }
    }
}
