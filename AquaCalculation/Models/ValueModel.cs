using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaCalculation.Models
{
    internal class ValueModel
    {
        #region MainValue : IEnumerable<double> - Значение для числа
        private IEnumerable<MainValueClass> _ExternalValue;

        public IEnumerable<MainValueClass> ExternalValue { get => _ExternalValue; set => _ExternalValue = value; }
        #endregion

        #region ValueName : String - Название елемента

        private String _ValueName;

        public String ValueName { get => _ValueName; set => _ValueName = value; }
        #endregion
    }
}
