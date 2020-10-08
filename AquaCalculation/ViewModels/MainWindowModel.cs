using AquaCalculation.Data.MathLabs;
using AquaCalculation.Infrastructure.Commands;
using AquaCalculation.Models;
using AquaCalculation.Models.Lab1;
using AquaCalculation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xaml.Schema;

namespace AquaCalculation.ViewModels
{
    class MainWindowModel : ViewModel
    {
        /* ------------------------------------------------------------------------- */
        #region SelectedValue : ValueModel - Какое значение выбранно из ListBox. Lab1

        private ValueModel _SelectedValue;

        public ValueModel SelectedValue { get => _SelectedValue; set => Set(ref _SelectedValue, value); }

        #endregion

        #region DataXY : IEnumerable<DataPoint> - Набор данних заданих в таблице

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
        /* ------------------------------------------------------------------------- */
        #region Command

        #region 

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

            X = 4.39;

            var answer2 = LagrangePolynom.InterpolationPolynom(valueX, valueY, X, valueX.Count);

            List<ValueModel> myModel = new List<ValueModel> { };

            myModel.Add(new ValueModel { ExternalValue = new List<MainValueClass> { new MainValueClass { MainValue = answer2 } }, 
                ValueName = "IntPolynom"});

            DataValue = myModel;
        }
        #endregion

        #endregion

        /* ------------------------------------------------------------------------- */
        public MainWindowModel()
        {

            DataXY = new ObservableCollection<DataPoint> { };

            LagrangeRun = new LambdaCommand(OnLagrangeRunExecuted, CanLagrangeRunExecute);
        }
    }
}
