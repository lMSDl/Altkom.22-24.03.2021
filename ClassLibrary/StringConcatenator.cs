using System;

namespace ClassLibrary
{
    public class StringConcatenator
    {
        private string _value = "";
        public const int MAX_LENGTH = 10;

        public StringConcatenator Concat(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (_value.Length + value.Length > MAX_LENGTH)
                throw new OverflowException();
            Execture(value);

            return this;
        }

        private void Execture(string value)
        {
            //if(DateTime.Now.DayOfWeek <= DayOfWeek.Friday)
            //    _value += value.ToUpper();
            //else
                _value += value.ToLower();

        }

        public override string ToString()
        {
            return _value;
        }
    }
}
