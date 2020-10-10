using AquaCalculation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.ViewModels.Lab4
{
    class MainLab4ViewModel : ViewModel
    {
        public MainWindowModel MainModel { get; }
        public MainLab4ViewModel(MainWindowModel MainModel)
        {
            this.MainModel = MainModel;
        }
    }
}
