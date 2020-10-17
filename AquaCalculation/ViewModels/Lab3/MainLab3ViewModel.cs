using AquaCalculation.Data.Lab2;
using AquaCalculation.Infrastructure.Commands;
using AquaCalculation.Infrastructure.Lab3;
using AquaCalculation.Models;
using AquaCalculation.Models.Lab1;
using AquaCalculation.Models.lab3;
using AquaCalculation.Servises.RootsSeparation;
using AquaCalculation.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;

namespace AquaCalculation.ViewModels.Lab3
{
    class MainLab3ViewModel : ViewModel
    {
        public MainWindowModel MainModel { get; }

        #region Properties

        #region ABvalue : ICollection<ABModel> - сохраненое свойство a и b

        private ICollection<ABModel> _ABvalue;

        public ICollection<ABModel> ABvalue { get => _ABvalue; set => Set(ref _ABvalue, value); }
        #endregion

        #region NValue : Int - N - значение n-ого полинома

        private int _NValue;

        public int NValue { get => _NValue; set => Set(ref _NValue, value); }
        #endregion

        #region XYValue : ICollection<XYValueModel> - значение X и Y соответствено

        private ICollection<XYValueModel> _XYValue;

        public ICollection<XYValueModel> XYValue { get => _XYValue; set => Set(ref _XYValue, value); }
        #endregion

        #region XYZero : ICollection<XYValueModel> - значение X и Y соответствено

        private ICollection<XYValueModel> _XYZero;

        public ICollection<XYValueModel> XYZero { get => _XYZero; set => Set(ref _XYZero, value); }
        #endregion

        #region XYMaxMin : ICollection<XYValueModel> - точки экстремума
        private ICollection<XYValueModel> _XYMaxMin;

        public ICollection<XYValueModel> XYMaxMin { get => _XYMaxMin; set => Set(ref _XYMaxMin, value); }
        #endregion

        #endregion

        #region Command

        public ICommand ChebyshevPolynomFind { get; }

        private bool CanChebyshevPolynomExecute(object p)
        {
            return true;
        }
        private void OnChebyshevPolynomExecuted(object p)
        {
            double AValue = -1, BValue = 1;
            foreach(var el in ABvalue)
            {
                AValue = el.A;
                BValue = el.B;
            }

            List<double> XYZeroInner = ChebishevPolynom.ChebishevPolynomZeroFind(NValue, AValue, BValue);

            List<XYValueModel> XYZeroHere = new List<XYValueModel> { };
            foreach(var el in XYZeroInner)
            {
                XYZeroHere.Add(new XYValueModel { X = el, Y = 0});
            }

            XYZero = XYZeroHere;

            List<XYValueModel> XYValueLocal = new List<XYValueModel> { };

            for(double i = AValue; i < BValue; i += 0.0001)
            {
                var temp1 = new XYValueModel { X = i, Y = ChebishevPolynom.ChebishevPolynomFind(i, NValue) };

                XYValueLocal.Add(temp1);
            }

            XYValue = XYValueLocal;

            AnalyticalSeparation analyticalSeparation = new AnalyticalSeparation();

            XYMaxMin = analyticalSeparation.RootSeparation(XYValue);
        }

        #endregion
        public MainLab3ViewModel(MainWindowModel MainModel)
        {
            this.MainModel = MainModel;

            NValue = 1;

            ABvalue = new List<ABModel>(){ new ABModel { A = -1, B = 1} };

            ChebyshevPolynomFind = new LambdaCommand(OnChebyshevPolynomExecuted, CanChebyshevPolynomExecute);
        }
    }
}
