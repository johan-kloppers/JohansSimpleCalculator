using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohansSimpleCalculator.Models
{
    internal class ValueItem
    {
        decimal _Value = 0;
        bool PerriodChecked = false;
        public void AddDigit(char value)
        {
            if (_Value == 0 && PerriodChecked == false)
            {
                bool result = Int32.TryParse(value.ToString(), out int NumberValue);

                if (result is not false)
                {
                    _Value = NumberValue;
                }
            }
            else
            {
                string StringNumber = _Value.ToString(CultureInfo.InvariantCulture);

                StringNumber = InsertPeriodIfNeeded(StringNumber);

                StringNumber = StringNumber + value;

                bool result = Decimal.TryParse(StringNumber, CultureInfo.InvariantCulture, out decimal NumberValue);

                if (result is not false)
                {
                    _Value = NumberValue;
                }
            }
        }

        private string InsertPeriodIfNeeded(string StringNumber)
        {
            if (PerriodChecked == true)
            {
                if (StringNumber.Contains('.') == false)
                {
                    StringNumber = StringNumber + '.';
                }
            }
            return StringNumber;
        }

        private string RemoveTrailingZero(string StringNumber)
        {
            if (StringNumber.Contains('.') == true)
            {
                string newString = "";

                var reverseString = StringNumber.Reverse();
                bool Non0Found = false;

                foreach (char c in reverseString)
                {
                    if (Non0Found == false)
                    {
                        if (c != '0')
                        {
                            Non0Found = true;
                            if (c != '.')
                            {
                                newString += c;
                            }
                        }
                    }
                    else
                    {
                        newString += c;
                    }
                }

                var CorrectOrientation = newString.Reverse();

                StringNumber = new string(CorrectOrientation.ToArray());

            }

            return StringNumber;
        }

        public void SetPeriod()
        {
            PerriodChecked = true;
        }

        public void SetValue(decimal value)
        {
            string StringNumber = value.ToString(CultureInfo.InvariantCulture);

            StringNumber = RemoveTrailingZero(StringNumber);

            if (StringNumber.Contains('.') == true)
            {
                PerriodChecked = true;
            }
            else
            {
                PerriodChecked = false;
            }

            bool result = Decimal.TryParse(StringNumber, CultureInfo.InvariantCulture, out decimal NumberValue);

            if (result is not false)
            {
                _Value = NumberValue;
            }
        }

        public string GetValueString()
        {
            return InsertPeriodIfNeeded(
                    _Value.ToString(CultureInfo.InvariantCulture)
                );
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
