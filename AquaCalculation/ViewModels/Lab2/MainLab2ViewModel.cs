using AquaCalculation.Data.Lab2;
using AquaCalculation.Infrastructure.Commands;
using AquaCalculation.Models;
using AquaCalculation.Models.Lab1;
using AquaCalculation.ViewModels.Base;
using LiveCharts.Wpf;
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

        #region DataXYAnswer : ICollection<DataPoint> - Набор данних заданих в таблице

        private ICollection<DataPoint> _DataXYAnswer;

        public ICollection<DataPoint> DataXYAnswer { get => _DataXYAnswer; set => Set(ref _DataXYAnswer, value); }

        #endregion

        #region DataXYAnswer2 : ICollection<DataPoint> - Набор данних заданих в таблице

        private ICollection<DataPoint> _DataXYAnswer2;

        public ICollection<DataPoint> DataXYAnswer2 { get => _DataXYAnswer2; set => Set(ref _DataXYAnswer2, value); }

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

            var answer2 = FirstFormula.FindDelY(valueY, 3);

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

            var tempData = new List<DataPoint> {};

            var tempData1 = new List<DataPoint> { new DataPoint { XValue = X, YValue = answer } };

            //МЫ ПРОТИВ РЕФАКТОРИНГА! ВО ИМЯ ЛУЛЗА
            for (double i = DataXY[0].XValue; i < DataXY[DataXY.Count - 1].XValue; i += 0.0001)
            {
                answer = FirstFormula.FirstInterpolation(valueX, valueY, i, 2);
                tempData.Add(new DataPoint { XValue = i, YValue = answer });
            }
            DataXYAnswer = tempData;

            DataXYAnswer2 = tempData1;

            //Ищем ошибку

            int tempt = 0;

            foreach (var el in valueX)
            {
                if (el < X) tempt++;
                else break;
            }

            if (tempt > 0) tempt--;

            double h = 0;
            if (valueX.Count > 1)
                h = valueX[1] - valueX[0];

            double t = (X - valueX[tempt]) / h;

            double ourYMax = 0;
            foreach (var el2 in answer2[2])
                if (ourYMax < el2) ourYMax = el2;

            double Rn = ourYMax / (3);

            for(int i = 0; i <= 2; i++)
            {
                Rn *= t - i;
            }

            temp1.Add(new ValueModel
            {
                ExternalValue = new List<MainValueClass> { new MainValueClass { MainValue = Rn } },
                ValueName = "R2(x)"
            });

            DataValue = temp1;
        }
        #endregion

        #region SecondPolynomRun : Запускает исчисление первого полинома Ньютона

        public ICommand SecondPolynomRun { get; }

        private bool CanSecondPolynomRunExecute(object p)
        {
            return true;
        }
        private void OnSecondPolynomRunExecuted(object p)
        {
            List<double> valueX = new List<double> { };
            List<double> valueY = new List<double> { };
            foreach (var el in DataXY)
            {
                valueX.Add(el.XValue);
                valueY.Add(el.YValue);
            }
            var answer = SecondFormula.SecondInterpolation(valueX, valueY, X, 2);
            //var answer = LagrangePolynom.SchemeAitken(valueX, valueY, 10, _X);

            var answer2 = SecondFormula.FindDelY(valueY, 3);

            List<ValueModel> temp1 = new List<ValueModel> { };
            temp1.Add(new ValueModel
            {
                ExternalValue = new List<MainValueClass> { new MainValueClass { MainValue = answer } },
                ValueName = "SecondPolynom"
            });
            foreach (var el in answer2)
            {
                List<MainValueClass> temp2 = new List<MainValueClass> { };

                foreach (var el2 in el)
                    temp2.Add(new MainValueClass { MainValue = el2 });

                temp1.Add(new ValueModel { ExternalValue = temp2, ValueName = $"delY [{temp1.Count}]" });
            }



            var tempData = new List<DataPoint> { };

            var tempData1 = new List<DataPoint> { new DataPoint { XValue = X, YValue = answer } };

            //МЫ ПРОТИВ РЕФАКТОРИНГА! ВО ИМЯ ЛУЛЗА
            for (double i = DataXY[0].XValue; i < DataXY[DataXY.Count - 1].XValue; i += 0.0001)
            {
                answer = SecondFormula.SecondInterpolation(valueX, valueY, i, 2);
                tempData.Add(new DataPoint { XValue = i, YValue = answer });
            }
            DataXYAnswer = tempData;

            DataXYAnswer2 = tempData1;
            //Ищем ошибку

            int tempt = 0;

            foreach (var el in valueX)
            {
                if (el < X) tempt++;
                else break;
            }

            if (!(valueX.Count < tempt) && tempt - 1 >= 0) tempt--;

            double h = 0;
            if (valueX.Count > 1)
                h = valueX[1] - valueX[0];

            double t = (X - valueX[tempt]) / h;

            double ourYMax = 0;
            foreach (var el2 in answer2[2])
                if (ourYMax < el2) ourYMax = el2;

            double Rn = ourYMax / (3);

            for (int i = 0; i <= 2; i++)
            {
                Rn *= t + i;
            }

            temp1.Add(new ValueModel
            {
                ExternalValue = new List<MainValueClass> { new MainValueClass { MainValue = Rn } },
                ValueName = "R2(x)"
            });


            DataValue = temp1;
        }
        #endregion

        #endregion
        public MainLab2ViewModel(MainWindowModel MainModel)
        {
            this.MainModel = MainModel;

            DataXY = new ObservableCollection<DataPoint> { };

            FirstPolynomRun = new LambdaCommand(OnFirstPolynomRunExecuted, CanFirstPolynomRunExecute);
            SecondPolynomRun = new LambdaCommand(OnSecondPolynomRunExecuted, CanSecondPolynomRunExecute);
        }
    }
}
