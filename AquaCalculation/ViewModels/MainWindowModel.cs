using AquaCalculation.Models;
using AquaCalculation.Models.Lab1;
using AquaCalculation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml.Schema;

namespace AquaCalculation.ViewModels
{
    class MainWindowModel : ViewModel
    {
        /* ------------------------------------------------------------------------- */
        #region SelectedValue : ValueModel - Набор выбранного значения

        private ValueModel _SelectedValue;

        public ValueModel SelectedValue { get => _SelectedValue; set => Set(ref _SelectedValue, value); }

        #endregion

        #region DataXY : IEnumerable<DataPoint> - Набор данних заданих в таблице

        private IEnumerable<DataPoint> _DataXY;

        public IEnumerable<DataPoint> DataXY { get => _DataXY; set => Set(ref _DataXY, value); }

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
        public MainWindowModel()
        {
            var perm = new List<DataPoint> { };
            perm.Add(new DataPoint {});
            DataXY = perm;

            var perm2 = new List<ValueModel> { };
            perm2.Add(new ValueModel { MainValue = 10, ValueName = "x"});
            perm2.Add(new ValueModel { MainValue = 35, ValueName = "y" });
            perm2.Add(new ValueModel { MainValue = 67, ValueName = "z" });
            DataValue = perm2;
        }
    }
}
