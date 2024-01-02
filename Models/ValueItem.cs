using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohansSimpleCalculator.Models
{
    internal class ValueItem
    {
        decimal _Value = 0;
        public void AddDigit(char value)
        {
            if (_Value == 0)
            {
                bool result = Int32.TryParse(value.ToString(), out int NumberValue);

                if (result is not false)
                {
                    _Value = NumberValue;
                }
            }
            else
            { 
                string StringNumber = _Value.ToString() + value;

                bool result = Decimal.TryParse(StringNumber, out decimal NumberValue);

                if (result is not false)
                {
                    _Value = NumberValue;
                }
            }
        }

        public void SetValue(decimal value)
        {
            _Value = value;
        }

        public string GetValueString()
        { 
            return _Value.ToString();
        }

        public decimal GetValue()
        {
            return _Value;
        }
        public void RemoveDigit()
        {

        }

    }
}
