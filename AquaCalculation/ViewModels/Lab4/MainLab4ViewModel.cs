using AquaCalculation.Model.Lab4;
using AquaCalculation.Infrastructure.Commands;
using AquaCalculation.Models;
using AquaCalculation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace AquaCalculation.ViewModels.Lab4
{
    class MainLab4ViewModel : ViewModel
    {
        public MainWindowModel MainModel { get; }

        #region Properties

        #region XYValue : ICollaction<XYValueModel> - значение X и Y соответствено

        private ICollection<XYValueModel> _XYValue;

        public ICollection<XYValueModel> XYValue { get => _XYValue; set => Set(ref _XYValue, value); }
        #endregion

        #region ABCDValue : ICollaction<lab4Data> - значение a, b, c, d соответствено

        private ICollection<lab4Data> _ABCDValue;

        public ICollection<lab4Data> ABCDValue { get => _ABCDValue; set => Set(ref _ABCDValue, value); }
        #endregion

        #region XYValueSpline : ICollaction<XYValueModel> - значение X и Y соответствено интерполированого сплайна

        private ICollection<XYValueModel> _XYValueSpline;

        public ICollection<XYValueModel> XYValueSpline { get => _XYValueSpline; set => Set(ref _XYValueSpline, value); }
        #endregion

        #endregion

        #region Command

        public ICommand DoSplineInterpolation { get; }

        private bool CanDoSplineInterpolationExecute(object p)
        {
            return true;
        }
        private void OnDoSplineInterpolationExecuted(object p)
        {
            List<double> valueX = new List<double> { };
            List<double> valueY = new List<double> { };
            foreach (var el in XYValue)
            {
                valueX.Add(el.X);
                valueY.Add(el.Y);
            }
            //
            List<double> valueH = new List<double> { };
            for(int i = 0; i < valueX.Count - 1; i++)
            {
                valueH.Add(valueX[i + 1] - valueX[i]);
            }

            List<double> valueDelta = new List<double> { };

            for(int i = 1; i < valueX.Count; i++)
            {
                valueDelta.Add((valueY[i] - valueY[i-1]) / valueH[i - 1]);
            }

            List<double> valueA = new List<double> { };
            List<double> valueB = new List<double> { };

            valueA.Add(2 * (valueH[0] + valueH[1]));
            valueB.Add(6 * (valueDelta[1] - valueDelta[0]));

            for(int i = 1; i < valueX.Count - 1; i++)
            {
                valueA.Add(2 * (valueH[i - 1] + valueH[i]) - (Math.Pow(valueH[i - 1], 2.0) / valueA[i - 1]));
                valueB.Add(6 * (valueDelta[i] - valueDelta[i - 1]) - ((valueH[i - 1] * valueB[i-1]) / valueA[i - 1]));
            }

            List<double> valueU = new List<double> { };

            valueU.Add(0);

            for(int i = 0; i < valueA.Count - 1; i++)
            {
                valueU.Add((valueB[valueB.Count - i - 1] - valueH[valueB.Count - i - 1] * valueU[i]) / valueA[valueB.Count - i - 1]);
            }

            valueU.Add(0);

            int x = 0;
        }

        #endregion
        public MainLab4ViewModel(MainWindowModel MainModel)
        {
            this.MainModel = MainModel;

            XYValue = new ObservableCollection<XYValueModel> { };

            DoSplineInterpolation = new LambdaCommand(OnDoSplineInterpolationExecuted, CanDoSplineInterpolationExecute);
        }
    }
}
