using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using iText.IO.Source;

namespace PlenkaWpf
{
    /// <summary>
    /// Валидатор для double значений
    /// </summary>
    internal class DoubleValidator : ValidationRule
    {
        /// <summary>
        /// Минимальное значение
        /// </summary>
        public double? Min { get; set; }

        /// <summary>
        /// Максимальное значение
        /// </summary>
        public double? Max { get; set; }

        public bool IncludingMinValue { get; set; } = false;
        public bool IncludingMaxValue { get; set; } = true;

        private string leftBound
        {
            get
            {
                var boundBracket = "";
                var boundValue = "";
                if (IncludingMinValue)
                {
                    boundBracket = "[";
                }
                else
                {
                    boundBracket = "(";
                }

                if (Min == null)
                {
                    boundBracket = "(";
                    boundValue = "-∞";
                }
                else
                {
                    boundValue = Min.ToString();
                }

                return $"{boundBracket}{boundValue}";
            }
        }

        private string rightBound
        {
            get
            {
                var boundBracket = "";
                var boundValue = "";
                if (IncludingMaxValue)
                {
                    boundBracket = "]";
                }
                else
                {
                    boundBracket = ")";
                }

                if (Max == null)
                {
                    boundBracket = ")";
                    boundValue = "∞";
                }
                else
                {
                    boundValue = Max.ToString();
                }

                return $"{boundValue}{boundBracket}";
            }
        }

        private string range
        {
            get { return $"{leftBound};{rightBound}"; }
        }

        private string enterValueInRange
        {
            get { return $"Введите значение в диапазоне: {range}"; }
        }

        public DoubleValidator()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double val = 0;
            if (value == null)
            {
                return new ValidationResult(false, "Значение не может быть пустым");
            }

            try
            {
                if (((string) value).Length > 0)
                {
                    val = double.Parse((String) value);
                }
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Введено недопустимое значение {e.Message}");
            }

            if (Min != null || Max != null)
            {
                if (Min == null && Max != null)
                {
                    if (IncludingMaxValue)
                    {
                        if (val > Max)
                        {
                            return new ValidationResult(false, enterValueInRange);
                        }
                    }
                    else
                    {
                        if (val >= Max)
                        {
                            return new ValidationResult(false, enterValueInRange);
                        }
                    }
                }

                if (Min != null && Max == null)
                {
                    if (IncludingMinValue)
                    {
                        if (val < Min)
                        {
                            return new ValidationResult(false, $"Введите значение большее чем {Min}.");
                        }
                    }
                    else
                    {
                        if (val <= Min)
                        {
                            return new ValidationResult(false, $"Введите значение большее чем {Min}.");
                        }
                    }
                }

                if (IncludingMinValue && IncludingMaxValue)
                {
                    if ((val < Min) || (val > Max))
                    {
                        return new ValidationResult(false,
                            enterValueInRange);
                    }
                }

                if (!IncludingMinValue && !IncludingMaxValue)
                {
                    if ((val <= Min) || (val >= Max))
                    {
                        return new ValidationResult(false,
                            enterValueInRange);
                    }
                }

                if (!IncludingMinValue)
                {
                    if ((val <= Min) || (val > Max))
                    {
                        return new ValidationResult(false,
                            enterValueInRange);
                    }
                }

                if (!IncludingMaxValue)
                {
                    if ((val < Min) || (val >= Max))
                    {
                        return new ValidationResult(false,
                            enterValueInRange);
                    }
                }
            }

            return ValidationResult.ValidResult;
        }
    }
}