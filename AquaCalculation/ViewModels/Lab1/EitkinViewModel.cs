using AquaCalculation.Data.MathLabs;
using AquaCalculation.Data.UsefulFormulas;
using AquaCalculation.Infrastructure.Commands;
using AquaCalculation.Models;
using AquaCalculation.Models.Lab1;
using AquaCalculation.ViewModels.Base;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AquaCalculation.ViewModels.Lab1
{
    internal class EitkinViewModel : ViewModel
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

        #region DataXYAnswer : ICollection<DataPoint> - Набор данних заданих в таблице

        private ICollection<DataPoint> _DataXYAnswer;

        public ICollection<DataPoint> DataXYAnswer { get => _DataXYAnswer; set => Set(ref _DataXYAnswer, value); }

        #endregion

        #region FormulaValue : String - производная функции для подсеча ошибки
        private string _FormulaValue;

        public string FormulaValue { get => _FormulaValue; set => Set(ref _FormulaValue, value); }

        #endregion

        #endregion

        /*-------------------------------------------------------------------------------------*/
        #region Command

        #region Eitkin : Запускает исчисления Ейткіна

        public ICommand EitkinRun { get; }

        private bool CanEitkinRunExecute(object p)
        {
            return true;
        }
        private void OnEitkinRunExecuted(object p)
        {
            List<double> valueX = new List<double> { };
            List<double> valueY = new List<double> { };
            foreach (var el in DataXY)
            {
                valueX.Add(el.XValue);
                valueY.Add(el.YValue);
            }

            var answer = LagrangePolynom.SchemeAitken(valueX, valueY, valueX.Count - 1, X);

            List<ValueModel> temp1 = new List<ValueModel> { };
            foreach (var el in answer)
            {
                List<MainValueClass> temp2 = new List<MainValueClass> { };

                foreach (var el2 in el)
                    temp2.Add(new MainValueClass { MainValue = el2 });

                temp1.Add(new ValueModel { ExternalValue = temp2, ValueName = $"L + {temp1.Count}" });
            }

            DataValue = temp1;
        }
        #endregion

        #endregion

        public EitkinViewModel(MainWindowModel MainModel)
        {
            this.MainModel = MainModel;

            DataXY = new ObservableCollection<DataPoint> { };

            EitkinRun = new LambdaCommand(OnEitkinRunExecuted, CanEitkinRunExecute);
        }
    }
}
