using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Models
{
    internal class ValueModel
    {
        #region MainValue : double - Значение числового значения
        private double _MainValue;

        public double MainValue { get => _MainValue; set => _MainValue = value; }
        #endregion

        #region ValueName : String - Название елемента

        private String _ValueName;

        public String ValueName { get => _ValueName; set => _ValueName = value; }
        #endregion
    }
}
