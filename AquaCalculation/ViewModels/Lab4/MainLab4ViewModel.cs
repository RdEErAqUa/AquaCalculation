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
using AquaCalculation.Data.Lab4;

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
            CubicSpline SplineFind = new CubicSpline();

            SplineFind.BuildSpline(valueX, valueY, valueY.Count - 1);

            for(double i = valueX.Min(); i < valueX.Max(); i += 0.001)
            {
                XYValueSpline.Add(new XYValueModel { X = i, Y = SplineFind.Interpolate(i)});
            }

            
        }

        #endregion
        public MainLab4ViewModel(MainWindowModel MainModel)
        {
            this.MainModel = MainModel;

            XYValue = new ObservableCollection<XYValueModel> { };

            XYValueSpline = new ObservableCollection<XYValueModel> { };

            DoSplineInterpolation = new LambdaCommand(OnDoSplineInterpolationExecuted, CanDoSplineInterpolationExecute);
        }
    }
}
