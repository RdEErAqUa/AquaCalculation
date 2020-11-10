using AquaCalculation.Data.Lab78;
using AquaCalculation.Infrastructure.Commands;
using AquaCalculation.Models;
using AquaCalculation.Models.Lab1;
using AquaCalculation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AquaCalculation.ViewModels.Lab78
{
    class MainLab78ViewModel : ViewModel
    {
        MainWindowModel MainWindow { get; set; }
        #region SelectedValue : ValueModel - Какое значение выбранно из ListBox. Lab1

        private ValueModel _SelectedValue;

        public ValueModel SelectedValue { get => _SelectedValue; set => Set(ref _SelectedValue, value); }

        #endregion

        #region MistakeValue : IEnumerable<ValueModel> - набор значения и название

        private IEnumerable<ValueModel> _MistakeValue;

        public IEnumerable<ValueModel> MistakeValue { get => _MistakeValue; set => Set(ref _MistakeValue, value); }

        #endregion

        #region DataValue : IEnumerable<DataPoint> - набор значения и название

        private ICollection<XYValueModel> _DataValue;

        public ICollection<XYValueModel> DataValue { get => _DataValue; set => Set(ref _DataValue, value); }

        #endregion

        #region FirstFunctionValue
        private ICollection<XYValueModel> _FirstFunctionValue;

        public ICollection<XYValueModel> FirstFunctionValue { get => _FirstFunctionValue; set => Set(ref _FirstFunctionValue, value); }

        #endregion

        #region SecondFunctionValue
        private ICollection<XYValueModel> _SecondFunctionValue;

        public ICollection<XYValueModel> SecondFunctionValue { get => _SecondFunctionValue; set => Set(ref _SecondFunctionValue, value); }

        #endregion

        #region ThirdFunctionValue
        private ICollection<XYValueModel> _ThirdFunctionValue;

        public ICollection<XYValueModel> ThirdFunctionValue { get => _ThirdFunctionValue; set => Set(ref _ThirdFunctionValue, value); }

        #endregion

        #region X
        private double _X;

        public double X { get => _X; set => Set(ref _X, value); }

        #endregion

        public ICommand FunctionStartCalculation { get; }
        private bool CanFunctionStartCalculationExecute(object p)
        {
            return true;
        }
        private void OnFunctionStartCalculationExecuted(object p)
        {
            List<double> xData = new List<double> { };
            List<double> yData = new List<double> { };
            for(int i = 0; i < DataValue.Count; i++)
            {
                xData.Add(DataValue.ElementAt(i).X);
                yData.Add(DataValue.ElementAt(i).Y);
            }

            List<double> AB = LeastSquares.LinearAproximation(xData, yData);
            FirstFunctionValue = new List<XYValueModel> { };
            for (int i = 0; i < DataValue.Count; i++)
            {
                FirstFunctionValue.Add(new XYValueModel { X = xData[i], Y = AB[0] * xData[i] + AB[1]});
            }
            double mistakeFirst = 0;

            for(int i = 0; i < DataValue.Count; i++)
            {
                mistakeFirst += Math.Pow((yData[i] - FirstFunctionValue.ElementAt(i).Y), 2);
            }
            //

            List<double> ABz = LeastSquares.QuadraticAproximation(xData, yData);
            SecondFunctionValue = new List<XYValueModel> { };
            for (int i = 0; i < DataValue.Count; i++)
            {
                SecondFunctionValue.Add(new XYValueModel { X = xData[i], Y = ABz[0] + ABz[1] * xData[i] + ABz[2] * xData[i] });
            }
            double mistakeSecond = 0;
            for (int i = 0; i < DataValue.Count; i++)
            {
                mistakeSecond += Math.Pow((yData[i] - SecondFunctionValue.ElementAt(i).Y), 2);
            }
            //
            List<double> xDataZ = new List<double> { };
            List<double> yDataZ = new List<double> { };

            foreach(var el in xDataZ)
                xDataZ.Add(Math.Log(el));
            foreach (var el in yData)
                yDataZ.Add(Math.Log(el));

            List<double> AB2 = LeastSquares.LinearAproximation(xDataZ, yDataZ);

            double az = Math.Exp(AB2[0]);

            double bz = AB2[1];

            ThirdFunctionValue = new List<XYValueModel> { };
            for (int i = 0; i < DataValue.Count; i++)
            {
                ThirdFunctionValue.Add(new XYValueModel { X = xData[i], Y = az * Math.Exp(bz * xData[i]) });
            }
            double mistakeThird = 0;
            for (int i = 0; i < DataValue.Count; i++) 
            {
                mistakeThird += Math.Pow((yData[i] - ThirdFunctionValue.ElementAt(i).Y), 2);
            }





            List<ValueModel> temp1 = new List<ValueModel> { };
            temp1.Add(new ValueModel
            {
                ExternalValue = new List<MainValueClass> { new MainValueClass { MainValue = mistakeFirst } },
                ValueName = "First"
            });
            temp1.Add(new ValueModel
            {
                ExternalValue = new List<MainValueClass> { new MainValueClass { MainValue = mistakeSecond } },
                ValueName = "Second"
            });
            temp1.Add(new ValueModel
            {
                ExternalValue = new List<MainValueClass> { new MainValueClass { MainValue = mistakeThird } },
                ValueName = "Third"
            });

            MistakeValue = temp1;
        }

        public MainLab78ViewModel(MainWindowModel MainModel) 
        {
            this.MainWindow = MainWindow;

            FunctionStartCalculation = new LambdaCommand(OnFunctionStartCalculationExecuted, CanFunctionStartCalculationExecute);

            DataValue = new List<XYValueModel> { };
        }
    }
}
