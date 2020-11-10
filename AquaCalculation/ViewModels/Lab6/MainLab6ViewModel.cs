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

namespace AquaCalculation.ViewModels.Lab6
{
    class MainLab6ViewModel : ViewModel
    {
        public MainWindowModel MainModel { get; }

        #region SelectedValue : ValueModel - Какое значение выбранно из ListBox. Lab1

        private ValueModel _SelectedValue;

        public ValueModel SelectedValue { get => _SelectedValue; set => Set(ref _SelectedValue, value); }

        #endregion

        #region DataValue : IEnumerable<ValueModel> - набор значения и название

        private IEnumerable<ValueModel> _DataValue;

        public IEnumerable<ValueModel> DataValue { get => _DataValue; set => Set(ref _DataValue, value); }

        #endregion

        #region XYValueForGrahp
        private ICollection<XYValueModel> _XYValueForGrahp;

        public ICollection<XYValueModel> XYValueForGrahp { get => _XYValueForGrahp; set => Set(ref _XYValueForGrahp, value); }

        #endregion

        #region XYValue
        private ICollection<XYModel> _XYValue;

        public ICollection<XYModel> XYValue { get => _XYValue; set => Set(ref _XYValue, value); }

        #endregion

        #region XYValueMod
        private ICollection<XYValueModel> _XYValueMod;

        public ICollection<XYValueModel> XYValueMod { get => _XYValueMod; set => Set(ref _XYValueMod, value); }

        #endregion

        #region X

        private double _X;

        public double X { get => _X; set => Set(ref _X, value); }

        #endregion

        #region XValueS

        private double _XValueS;

        public double XValueS { get => _XValueS; set => Set(ref _XValueS, value); }

        #endregion AZeroValue

        #region AZeroValue
        private double _AZeroValue;

        public double AZeroValue { get => _AZeroValue; set => Set(ref _AZeroValue, value); }
        #endregion

        #region NValue
        private double _NValue;

        public double NValue { get => _NValue; set => Set(ref _NValue, value); }
        #endregion

        #region ABValue - 1 - n value AB
        private ICollection<XYValueModel> _ABValue;

        public ICollection<XYValueModel> ABValue { get => _ABValue; set => Set(ref _ABValue, value); }
        #endregion
        #region
        public ICommand TrigonomicInterpolation { get; }
        private bool CanSecondTrigonomicInterpolationExecute(object p)
        {
            return true;
        }
        private void OnTrigonomicInterpolationExecuted(object p)
        {
            AZeroValue = 0;
            List<double> XValue = new List<double> { };
            XYValueMod = new List<XYValueModel> { };
            ABValue = new List<XYValueModel> { };
            foreach (var el in XYValue)
                XValue.Add(el.XValue.Contains("Pi") ? Double.Parse(el.XValue.Replace("Pi", "").Replace('.', ',')) * 3.14 : Double.Parse(el.XValue.Replace('.', ',')));

            for (int i = 0; i < XValue.Count; i++)
                XYValueMod.Add(new XYValueModel { X = XValue[i], Y = XYValue.ElementAt(i).YValue});

            foreach (var el in XYValueMod)
                AZeroValue += el.Y;
            AZeroValue *= (1 / (2 * NValue + 1));

            for(double i = 1; i <= NValue; i++)
            {
                double a = 0;
                double b = 0;

                foreach (var el in XYValueMod)
                {
                    a += (el.Y * Math.Cos(el.X * (i)));
                    b += (el.Y * Math.Sin(el.X * (i)));
                }
                a *= (2 / (NValue * 2 + 1));
                b *= (2 / (NValue * 2 + 1));
                ABValue.Add(new XYValueModel { X = a, Y = b });
            }

            double answer = AZeroValue;

            for(int i = 1; i <= NValue; i++)
            {
                answer += (ABValue.ElementAt(i - 1).X * Math.Cos(i * X) + ABValue.ElementAt(i - 1).Y * Math.Sin(i * X));
            }

            XValueS = answer;

            List<ValueModel> DataValu2e = new List<ValueModel> { };

            DataValu2e.Add(new ValueModel
            {
                ExternalValue = new List<MainValueClass> { new MainValueClass { MainValue = answer } },
                ValueName = "X"
            });

            DataValue = DataValu2e;
            //
            ABValue = new List<XYValueModel> { };

            List<XYValueModel> XYValueLocal = new List<XYValueModel> { };

            for (double f = -XValue.Max(); f <= XValue.Max(); f += 0.001)
            {
                double z = 0;

                for (double i = 1; i <= NValue; i++)
                {
                    double a = 0;
                    double b = 0;

                    foreach (var el in XYValueMod)
                    {
                        a += (el.Y * Math.Cos(el.X * (i)));
                        b += (el.Y * Math.Sin(el.X * (i)));
                    }
                    a *= (2 / (NValue * 2 + 1));
                    b *= (2 / (NValue * 2 + 1));
                    ABValue.Add(new XYValueModel { X = a, Y = b });
                }

                z = AZeroValue;

                for (int i = 1; i <= NValue; i++)
                {
                    z += (ABValue.ElementAt(i - 1).X * Math.Cos(i * f) + ABValue.ElementAt(i - 1).Y * Math.Sin(i * f));
                }

                XYValueLocal.Add(new XYValueModel { X = f, Y = z});
            }

            XYValueForGrahp = XYValueLocal;
        }
        #endregion
        public MainLab6ViewModel(MainWindowModel MainModel)
        {
            this.MainModel = MainModel;

            XYValue = new List<XYModel> { };
            TrigonomicInterpolation = new LambdaCommand(OnTrigonomicInterpolationExecuted, CanSecondTrigonomicInterpolationExecute);
        }
    }
}
