using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PlenkaWpf
{
    internal class DoubleValidator : ValidationRule
    {
        public double? Min { get; set; }
        public double? Max { get; set; }

        public DoubleValidator()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double val = 0;
            if (value==null)
            {
                return new ValidationResult(false, "Значение не может быть пустым");
            }
            try
            {
                if (((string) value).Length > 0)
                {
                    val = double.Parse((String)value);
                }
                    
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Введено недопустимое значение {e.Message}");
            }

            if ((val < Min) || (val > Max))
            {
                return new ValidationResult(false,
                    $"Введите значение в диапазоне: {Min}-{Max}.");
            }
            return ValidationResult.ValidResult;
        } 
    }
}
