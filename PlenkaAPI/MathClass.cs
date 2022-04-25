using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

using static System.Math;


namespace PlenkaAPI
{
    /// <summary>
    ///     Структура для отображения данных в таблице с результатами
    /// </summary>
    public struct CordTempN
    {
        /// <summary>
        ///     Координата канала по X
        /// </summary>
        public double cord { get; set; }

        /// <summary>
        ///     Температура
        /// </summary>
        public double temp { get; set; }

        /// <summary>
        ///     Вязкость
        /// </summary>
        public double n { get; set; }
    }


    public struct CalculationParameters
    {
        public string MaterialName { get; init; }
        public double W { get; init; }
        public double H { get; init; }
        public double L { get; init; }
        public double p { get; init; }
        public double c { get; init; }
        public double T0 { get; init; }
        public double Vu { get; init; }
        public double Tu { get; init; }
        public double u0 { get; init; }
        public double b { get; init; }
        public double Tr { get; init; }
        public double n { get; init; }
        public double au { get; init; }
        public double step { get; init; }
    }


    public struct CalculationResults
    {
        /// <summary>
        ///     Таймер для времени расчета
        /// </summary>
        public Stopwatch MathTimer { get; init; }

        /// <summary>
        ///     Список с результатами расчета по координате канала
        /// </summary>
        public List<CordTempN> cordTempNs { get; init; }

        /// <summary>
        ///     Производительность канала
        /// </summary>
        public double Q { get; init; }

        /// <summary>
        ///     Температура продукта
        /// </summary>
        public double T { get; init; }

        /// <summary>
        ///     Вязкость продукта
        /// </summary>
        public double N { get; init; }
    }


    public class MathClass // todo как-то красиво переписать все это
    {
        public MathClass(CalculationParameters cp)
        {
            this.cp = cp;
        }


        /// <summary>
        ///     Результаты вычислений
        /// </summary>
        public CalculationResults Results { get; private set; }

        private int GetDecimalDigitsCount(double number)
        {
            var str = number.ToString(new NumberFormatInfo
                                          {NumberDecimalSeparator = ".",})
                            .Split('.');

            return str.Length == 2 ? str[1].Length : 0;
        }

        /// <summary>
        ///     Функция производит вычисления с заданными параметрами
        /// </summary>
        public void Calculate()
        {
            var sw = new Stopwatch();
            sw.Start();
            var F = 0.125 * Pow(H / W, 2) - 0.625 * (H / W) + 1;
            var gamma = Vu / H;
            var qGamma = H * W * u0 * Pow(gamma, n + 1);
            var qAlpha = W * au * (1 / b - Tu + Tr);
            var Qch = H * W * Vu / 2 * F;
            var cordTempNs = new List<CordTempN>();
            var digitsCount = GetDecimalDigitsCount(step);

            for (double i = 0; i <= L; i += step)
            {
                var z = Round(i, digitsCount);

                var t = Tr + 1 / b * Log((b * qGamma + W * au) /
                                         (b * qAlpha) *
                                         (1 - Exp(-(z * b * qAlpha / (p * c * Qch)))) +
                                         Exp(b * (T0 - Tr - z * qAlpha / (p * c * Qch))));

                var ni = u0 * Exp(-b * (t - Tr)) * Pow(gamma, n - 1);
                t = Round(t, 2);
                ni = Round(ni, 2);
                cordTempNs.Add(new CordTempN {cord = z, n = ni, temp = t,});
            }

            var Q = Round(p * Qch * 3600, 2);
            var T = cordTempNs.Last().temp;
            var N = cordTempNs.Last().n;
            sw.Stop();

            Results = new CalculationResults
                {Q = Q, T = T, N = N, cordTempNs = cordTempNs, MathTimer = sw,};
        }


    #region Parameters

        public CalculationParameters cp { get; init; }

        private double W
        {
            get
            {
                return cp.W;
            }
        }

        private double H
        {
            get
            {
                return cp.H;
            }
        }

        private double L
        {
            get
            {
                return cp.L;
            }
        }

        private double p
        {
            get
            {
                return cp.p;
            }
        }

        private double c
        {
            get
            {
                return cp.c;
            }
        }

        private double T0
        {
            get
            {
                return cp.T0;
            }
        }

        private double Vu
        {
            get
            {
                return cp.Vu;
            }
        }

        private double Tu
        {
            get
            {
                return cp.Tu;
            }
        }

        private double u0
        {
            get
            {
                return cp.u0;
            }
        }

        private double b
        {
            get
            {
                return cp.b;
            }
        }

        private double Tr
        {
            get
            {
                return cp.Tr;
            }
        }

        private double n
        {
            get
            {
                return cp.n;
            }
        }

        private double au
        {
            get
            {
                return cp.au;
            }
        }

        private double step
        {
            get
            {
                return cp.step;
            }
        }

    #endregion
    }
}
