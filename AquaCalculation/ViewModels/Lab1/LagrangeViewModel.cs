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
using System.Linq;
using System.Windows.Input;

namespace AquaCalculation.ViewModels.Lab1
{
    internal class LagrangeViewModel : ViewModel
    {
        public MainWindowModel MainModel { get; }


        /*-------------------------------------------------------------------------------------*/
        #region Model
        #region SelectedValue : ValueModel - Какое значение выбранно из ListBox. Lab1

        private ValueModel _SelectedValue;

        public ValueModel SelectedValue { get => _SelectedValue; set => Set(ref _SelectedValue, value); }

        #endregion

        #region DataXY : ICollection<DataPoint> - Набор данних заданих в таблице

        private ICollection<DataPoint> _DataXY;

        public ICollection<DataPoint> DataXY { get => _DataXY; set => Set(ref _DataXY, value); }

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

        #region LagrangeRun : Запускает исчисления Лагранжа

        public ICommand LagrangeRun { get; }

        private bool CanLagrangeRunExecute(object p)
        {
            return true;
        }
        private void OnLagrangeRunExecuted(object p)
        {
            List<double> valueX = new List<double> { };
            List<double> valueY = new List<double> { };
            foreach (var el in DataXY)
            {
                valueX.Add(el.XValue);
                valueY.Add(el.YValue);
            }

            //var answer = LagrangePolynom.SchemeAitken(valueX, valueY, 10, _X);

            var answer2 = LagrangePolynom.InterpolationPolynom(valueX, valueY, X, valueX.Count);

            List<ValueModel> myModel = new List<ValueModel> { };

            myModel.Add(new ValueModel
            {
                ExternalValue = new List<MainValueClass> { new MainValueClass { MainValue = answer2 } },
                ValueName = "IntPolynom"
            });

            var tempData = new List<DataPoint> { new DataPoint { XValue = X, YValue = answer2 } };

            //МЫ ПРОТИВ РЕФАКТОРИНГА! ВО ИМЯ ЛУЛЗА
            for(double i = DataXY.ElementAt(0).XValue; i < DataXY.Last().XValue; i += 0.001 )
            {
                answer2 = LagrangePolynom.InterpolationPolynom(valueX, valueY, i, valueX.Count);
                tempData.Add(new DataPoint { XValue = i, YValue = answer2 });
            }

            DataXYAnswer = tempData;

            Argument x = new Argument($"x = {valueX?[0]}");

            Expression eh = new Expression(FormulaValue, x);

            double maxFunc = -1000;

            foreach (var el in valueX)
            {
                if (Math.Abs(eh.calculate()) > maxFunc) maxFunc = Math.Abs(eh.calculate());
                else x.setArgumentValue(el);
            }

            double Rn = maxFunc / Factorial.FindFactorial(3);

            for (int i = 0; i <= 2; i++)
            {
                Rn *= Math.Abs(X - valueX[i]);
            }

            myModel.Add(new ValueModel
            {
                ExternalValue = new List<MainValueClass> { new MainValueClass { MainValue = Rn } },
                ValueName = "R2(x)"
            });

            DataValue = myModel;
        }
        #endregion

        #endregion

        public LagrangeViewModel(MainWindowModel MainModel)
        {
            this.MainModel = MainModel;


            DataXY = new ObservableCollection<DataPoint> { };

            LagrangeRun = new LambdaCommand(OnLagrangeRunExecuted, CanLagrangeRunExecute);
        }
    }
}
