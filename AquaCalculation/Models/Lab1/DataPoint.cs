using System;

namespace AquaCalculation.Models.Lab1
{
    internal class DataPoint
    {
        #region XValue : double -  Х
        private double _XValue;

        public double XValue { get => _XValue; set => _XValue = value; }
        #endregion

        #region YValue : double -  Y
        private double _YValue;
        public double YValue { get => _YValue; set => _YValue = value; }
        #endregion
    }
}
