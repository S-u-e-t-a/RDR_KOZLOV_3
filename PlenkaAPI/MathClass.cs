using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Math;

namespace PlenkaAPI
{
    /// <summary>
    /// Структура для отображения данных в таблице с результатами
    /// </summary>
    public struct CordTempN
    {
        /// <summary>
        /// Координата канала по X
        /// </summary>
        public double cord { get; set; }

        /// <summary>
        /// Температура
        /// </summary>
        public double temp { get; set; }

        /// <summary>
        /// Вязкость
        /// </summary>
        public double n { get; set; }
    }
    public struct CalculationParameters
    {
        public double W;
        public double H;
        public double L;
        public double p;
        public double c;
        public double T0;
        public double Vu;
        public double Tu;
        public double u0;
        public double b;
        public double Tr;
        public double n;
        public double au;
        public double step;
    }

    public struct CalculationResults
    {
        /// <summary>
        /// Таймер для времени расчета
        /// </summary>
        public Stopwatch MathTimer { get; init; } 
        /// <summary>
        /// Список с результатами расчета по координате канала
        /// </summary>
        public List<CordTempN> cordTempNs { get; init; } 
        /// <summary>
        /// Производительность канала
        /// </summary>
        public double Q { get; init; }
        /// <summary>
        /// Температура продукта
        /// </summary>
        public double T { get; init; }
        /// <summary>
        /// Вязкость продукта
        /// </summary>
        public double N { get; init; }
    }

    public class MathClass // todo как-то красиво переписать все это
    {
        public MathClass(CalculationParameters cp)
        {
            this.cp = cp;
        }

        #region Parameters

        private CalculationParameters cp;
        private double W => cp.W;
        private double H => cp.H;
        private double L => cp.L;
        private double p => cp.p;
        private double c => cp.c;
        private double T0 => cp.T0;
        private double Vu => cp.Vu;
        private double Tu => cp.Tu;
        private double u0 => cp.u0;
        private double b => cp.b;
        private double Tr => cp.Tr;
        private double n => cp.n;
        private double au => cp.au;
        private double step => cp.step;

        #endregion

        /// <summary>
        /// Результаты вычислений
        /// </summary>
        public CalculationResults Results { get; private set; }

        private int GetDecimalDigitsCount(double number)
        {
            string[] str = number.ToString(new System.Globalization.NumberFormatInfo() {NumberDecimalSeparator = "."})
                .Split('.');
            return str.Length == 2 ? str[1].Length : 0;
        }

        /// <summary>
        /// Функция производит вычисления с заданными параметрами
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
                var t = Tr + (1 / b) * Log((b * qGamma + W * au) /
                                                 (b * qAlpha) *
                                                 (1 - Exp(-((z * b * qAlpha) / (p * c * Qch)))) +
                                                 Exp(b * (T0 - Tr - (z * qAlpha) / (p * c * Qch))));
                var ni = u0 * Exp(-b * (t - Tr)) * Pow(gamma, n - 1);
                t = Round(t, 2);
                ni = Round(ni, 2);
                cordTempNs.Add(new CordTempN{cord = z,n=ni,temp =t });
            }

            var Q = Round(p * Qch * 3600,2);
            var T = cordTempNs.Last().temp;
            var N = cordTempNs.Last().n;
            sw.Stop();
            Results = new CalculationResults() { Q = Q, T = T, N = N, cordTempNs = cordTempNs, MathTimer = sw };
        }
    }
}