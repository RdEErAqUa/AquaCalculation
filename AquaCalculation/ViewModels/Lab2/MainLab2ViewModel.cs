using AquaCalculation.Data.Lab2;
using AquaCalculation.Infrastructure.Commands;
using AquaCalculation.Models;
using AquaCalculation.Models.Lab1;
using AquaCalculation.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AquaCalculation.ViewModels.Lab2
{
    internal class MainLab2ViewModel : ViewModel
    {
        public MainWindowModel MainModel { get; }

        /*-------------------------------------------------------------------------------------*/
        #region Model
        #region SelectedValue : ValueModel - Какое значение выбранно из ListBox. Lab1

        private ValueModel _SelectedValue;

        public ValueModel SelectedValue { get => _SelectedValue; set => Set(ref _SelectedValue, value); }

        #endregion

        #region DataXY : ObservableCollection<DataPoint> - Набор данних заданих в таблице

        private ObservableCollection<DataPoint> _DataXY;

        public ObservableCollection<DataPoint> DataXY { get => _DataXY; set => Set(ref _DataXY, value); }

        #endregion

        #region DataValue : IEnumerable<ValueModel> - набор значения и название

        private IEnumerable<ValueModel> _DataValue;

        public IEnumerable<ValueModel> DataValue { get => _DataValue; set => Set(ref _DataValue, value); }

        #endregion

        #region X : double - Набор данних заданих в таблице

        private double _X;

        public double X { get => _X; set => Set(ref _X, value); }

        #endregion

        #endregion
        /*-------------------------------------------------------------------------------------*/
        #region Command

        #region FirstPolynomRun : Запускает исчисление первого полинома Ньютона

        public ICommand FirstPolynomRun { get; }

        private bool CanFirstPolynomRunExecute(object p)
        {
            return true;
        }
        private void OnFirstPolynomRunExecuted(object p)
        {
            List<double> valueX = new List<double> { };
            List<double> valueY = new List<double> { };
            foreach (var el in DataXY)
            {
                valueX.Add(el.XValue);
                valueY.Add(el.YValue);
            }

            //var answer = LagrangePolynom.SchemeAitken(valueX, valueY, 10, _X);

            var answer = FirstFormula.FirstInterpolation(valueX, valueY, X, 2);

            var answer2 = FirstFormula.FindDelY(valueY, 2);

            List<ValueModel> temp1 = new List<ValueModel> { };
            temp1.Add(new ValueModel
            {
                ExternalValue = new List<MainValueClass> { new MainValueClass { MainValue = answer } },
                ValueName = "FirstPolynom"
            });
            foreach (var el in answer2)
            {
                List<MainValueClass> temp2 = new List<MainValueClass> { };

                foreach (var el2 in el)
                    temp2.Add(new MainValueClass { MainValue = el2 });

                temp1.Add(new ValueModel { ExternalValue = temp2, ValueName = $"delY [{temp1.Count}]" });
            }

            DataValue = temp1;
        }
        #endregion

        #endregion
        public MainLab2ViewModel(MainWindowModel MainModel)
        {
            this.MainModel = MainModel;

            DataXY = new ObservableCollection<DataPoint> { };

            FirstPolynomRun = new LambdaCommand(OnFirstPolynomRunExecuted, CanFirstPolynomRunExecute);
        }
    }
}
