using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohansSimpleCalculator.Models
{
    internal class ValueItem
    {
        decimal Value = 0;
        public void AddDigit(char value)
        {
            if (Value == 0)
            {
                bool result = Int32.TryParse(value.ToString(), out int NumberValue);

                if (result is not false)
                {
                    Value = NumberValue;
                }
            }
            else
            { 
                string StringNumber = Value.ToString() + value;

                bool result = Decimal.TryParse(StringNumber, out decimal NumberValue);

                if (result is not false)
                {
                    Value = NumberValue;
                }
            }
        }

        public string GetValueString()
        { 
            return Value.ToString();
        }

        public decimal GetValue()
        {
            return Value;
        }
        public void RemoveDigit()
        {

        }

    }
}
