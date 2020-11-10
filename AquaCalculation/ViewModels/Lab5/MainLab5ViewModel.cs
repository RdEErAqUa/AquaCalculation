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
using AquaCalculation.Models.Lab5;
using AquaCalculation.Data.Lab5;

namespace AquaCalculation.ViewModels.Lab5
{
    class MainLab5ViewModel : ViewModel
    {
        public MainWindowModel MainModel { get; }

        #region XYValue
        private ICollection<XYValueModel> _XYValue;

        public ICollection<XYValueModel> XYValue { get => _XYValue; set => Set(ref _XYValue, value); }

        #endregion

        #region B3Spline
        private ICollection<XYValueModel> _B3Spline;

        public ICollection<XYValueModel> B3Spline { get => _B3Spline; set => Set(ref _B3Spline, value); }

        #endregion

        #region B5Spline
        private ICollection<XYValueModel> _B5Spline;

        public ICollection<XYValueModel> B5Spline { get => _B5Spline; set => Set(ref _B5Spline, value); }

        #endregion

        #region X

        private double _X;

        public double X { get => _X; set => Set(ref _X, value); }

        #endregion

        public ICommand B3SplineInvoke { get; }
        private bool CanB3SplineInvokeExecute(object p)
        {
            return true;
        }
        private void OnB3SplineInvokeExecuted(object p)
        {
            List<XYValueModel> answerValue = new List<XYValueModel> { };

            List<double> XValue = new List<double> { };
            foreach (var el in XYValue)
                XValue.Add(el.X);


            List<double> YValue = new List<double> { };
            foreach (var el in XYValue)
                YValue.Add(el.Y);

            for (double i = 0; i < 1; i += 0.01)
            {
                List<double> zrs = BSpline.BNSpline(XValue, YValue, (int)X, i);
                answerValue.Add(new XYValueModel { X = zrs[0], Y = zrs[1]});
            }

            B3Spline = answerValue;
        }

        //
        public ICommand B5SplineInvoke { get; }
        private bool CanB5SplineInvokeExecute(object p)
        {
            return true;
        }
        private void OnB5SplineInvokeExecuted(object p)
        {
        }

        public MainLab5ViewModel(MainWindowModel MainModel)
        {
            this.MainModel = MainModel;

            XYValue = new List<XYValueModel> { };

            B3SplineInvoke = new LambdaCommand(OnB3SplineInvokeExecuted, CanB3SplineInvokeExecute);

            B5SplineInvoke = new LambdaCommand(OnB5SplineInvokeExecuted, CanB5SplineInvokeExecute);
        }
    }
}
