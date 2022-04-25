using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PlenkaAPI;
using PlenkaAPI.Data;
using PlenkaAPI.Models;
using PlenkaWpf.Utils;


namespace PlenkaWpf.VM
{
    

    /// <summary>
    /// VM для окна исследователя
    /// </summary>
    internal class Window1VM : ViewModelBase

    {
        #region Functions

        #region Constructors

        public Window1VM()
        {
            Materials = DbContextSingleton.GetInstance().MembraneObjects.Where(o => o.Type.TypeName == "Материал")
                .ToList();

            Material = DbContextSingleton.GetInstance().MembraneObjects.First(v => v.ObName == "НашМатериал");
            Canal = DbContextSingleton.GetInstance().MembraneObjects.First(v => v.ObName == "Канал");
            MatModel = DbContextSingleton.GetInstance().MembraneObjects.First(v => v.ObName == "Стандартная модель");

            tempLineSerie = new LineSeries() {Title = "Температура, °С"};
            tempLineSerie.Fill = System.Windows.Media.Brushes.Transparent;
            TempSeries = new SeriesCollection() {tempLineSerie};

            nLineSerie = new LineSeries() {Title = "Вязкость, Па·с"};
            nLineSerie.Fill = System.Windows.Media.Brushes.Transparent;
            NSeries = new SeriesCollection() {nLineSerie};
        }

        #endregion

        /// <summary>
        /// Фукнция, получающая свойство по его названию
        /// </summary>
        /// <param name="propName">Название свойства</param>
        /// <param name="mat">Объект, свойство которого надо получить</param>
        /// <returns>Полученное свойство</returns>
        private Value GetMatValueByPropertyName(string propName, MembraneObject mat)
        {
            return mat.Values.First(v => v.Prop.PropertyName == propName);
        }

        /// <summary>
        /// Функция, обновляющая точки графика по словарю со значениями
        /// </summary>
        /// <param name="ls">Серия графика</param>
        /// <param name="points">Точки графика</param>
        private void updateLineSeriesByCordAndValue(LineSeries ls, List<double> x, List<double>y)
        {
            if (x.Count!=y.Count)
            {
                throw new ArgumentException("Количество значений x не совпадает с количеством значений y");
            }
            var newValues = new ChartValues<ObservablePoint>();
            for (int i = 0; i < x.Count; i++)
            {
                newValues.Add(new ObservablePoint(x[i], y[i]));
            }

            ls.Values = newValues;
        }

        /// <summary>
        /// Функция, создающая список со значениями для таблицы с результатами
        /// </summary>
        /// <param name="ti"> Словарь значений с температурой </param>
        /// <param name="ni"> Словарь значений с вязкостью </param>
        /// <returns></returns>
        private List<CordTempN> cordTempNsByDictionaries(Dictionary<double, double> ti, Dictionary<double, double> ni)
        {
            var l = new List<CordTempN>();
            foreach (var d in ni)
            {
                l.Add(new CordTempN() {cord = d.Key, temp = ti[d.Key], n = ni[d.Key]});
            }

            return l;
        }

        //private bool isValueBetweenBounds(double value, double leftBound, double rightBound)
        //{
        //    if (value >= leftBound && value <= rightBound)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //private double getStepByValueRange(double valueRange, int countOfSteps)
        //{
        //    var estimatedStep = valueRange / countOfSteps;
        //    var normalizedStep = estimatedStep;
        //    var p = 1;

        //    //получаем мантиссу и порядок
        //    while (normalizedStep<1)
        //    {
        //        normalizedStep *= 10;
        //        p--;
        //    }
        //    while (normalizedStep > 10)
        //    {
        //        normalizedStep /= 10;
        //        p++;
        //    }

        //    double roundRule = 0;

        //    for (int i = 0; i <= roundRuleDigits.Length-2; i++)
        //    {
        //        if (isValueBetweenBounds(normalizedStep,roundRuleDigits[i],roundRuleDigits[i+1]))
        //        {
        //            if (normalizedStep - roundRuleDigits[i] < roundRuleDigits[i + 1] - normalizedStep)
        //            {
        //                roundRule=roundRuleDigits[i];
        //            }
        //            else
        //            {
        //                roundRule = roundRuleDigits[i + 1];
        //            }
        //            break;
        //        }
        //    }

        //    return roundRule * Math.Pow(10,p);
        //}

        //private double stepByDictionary(List<double> points)
        //{
        //    var range = points.Max() - points.Min();
        //    var step = getStepByValueRange(range, 10);
        //    return step;
        //}

        #endregion

        public List<int> ErrorList { get; set; }

        #region Properties

        private static readonly double[] roundRuleDigits = new[] {1, 2.5, 5, 10};

        /// <summary>
        /// Доступные материалы
        /// </summary>
        public List<MembraneObject> Materials { get; set; }

        #region CanalProps

        /// <summary>
        /// Текущий канал
        /// </summary>
        public MembraneObject Canal { get; set; }

        /// <summary>
        /// Длина канала
        /// </summary>
        public double? Length
        {
            get { return GetMatValueByPropertyName("Длина", Canal).Value1; }
            set
            {
                GetMatValueByPropertyName("Длина", Canal).Value1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ширина канала
        /// </summary>
        public double? Width
        {
            get { return GetMatValueByPropertyName("Ширина", Canal).Value1; }
            set
            {
                GetMatValueByPropertyName("Ширина", Canal).Value1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Глубина канала
        /// </summary>
        public double? Depth
        {
            get { return GetMatValueByPropertyName("Глубина", Canal).Value1; }
            set
            {
                GetMatValueByPropertyName("Глубина", Canal).Value1 = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region MaterialProps

        private MembraneObject material;

        /// <summary>
        /// Выбранный материал
        /// </summary>
        public MembraneObject Material
        {
            get { return material; }
            set
            {
                material = value;

                OnPropertyChanged(nameof(Density));
                OnPropertyChanged(nameof(SpecifiсHeatCapacity));
                OnPropertyChanged(nameof(MeltingTemperature));
            }
        }

        private object _null;

        public object Null // Костыль для того чтобы ErrorStr в TextBox всегда был пустым
        {
            get { return _null; }
            set
            {
                _null = null;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Плоность материала
        /// </summary>
        public double? Density
        {
            get { return GetMatValueByPropertyName("Плотность", Material).Value1; }
            set
            {
                GetMatValueByPropertyName("Плотность", Material).Value1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Удельная теплоемкость материала
        /// </summary>
        public double? SpecifiсHeatCapacity
        {
            get { return GetMatValueByPropertyName("Удельная теплоемкость", Material).Value1; }
            set
            {
                GetMatValueByPropertyName("Удельная теплоемкость", Material).Value1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Температуры плавления материала
        /// </summary>
        public double? MeltingTemperature
        {
            get { return GetMatValueByPropertyName("Температура плавления", Material).Value1; }
            set
            {
                GetMatValueByPropertyName("Температура плавления", Material).Value1 = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region VarProps

        /// <summary>
        /// Скорость крышки
        /// </summary>
        public double? CapSpeed { get; set; } = 1;

        /// <summary>
        /// Температура крашки
        /// </summary>
        public double? CapTemperature { get; set; } = 1;

        #endregion

        #region MatModelProps

        /// <summary>
        /// Текущая мат.модель
        /// </summary>
        public MembraneObject MatModel { get; set; }

        /// <summary>
        /// Коэффициент констистенции материала при температуре приведения
        /// </summary>
        public double? СonsСoef
        {
            get
            {
                return GetMatValueByPropertyName("Коэффициент констистенции материала при температуре приведения",
                    MatModel).Value1;
            }
            set
            {
                GetMatValueByPropertyName("Коэффициент констистенции материала при температуре приведения", MatModel)
                    .Value1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Температурный коэффициент вязкости материала
        /// </summary>
        public double? TempСoef
        {
            get { return GetMatValueByPropertyName("Температурный коэффициент вязкости материала", MatModel).Value1; }
            set
            {
                GetMatValueByPropertyName("Температурный коэффициент вязкости материала", MatModel).Value1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Температура приведения
        /// </summary>
        public double? RefTemp
        {
            get { return GetMatValueByPropertyName("Температура приведения", MatModel).Value1; }
            set
            {
                GetMatValueByPropertyName("Температура приведения", MatModel).Value1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Индекс течения материала
        /// </summary>
        public double? MatFlowIndex
        {
            get { return GetMatValueByPropertyName("Индекс течения материала", MatModel).Value1; }
            set
            {
                GetMatValueByPropertyName("Индекс течения материала", MatModel).Value1 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Коэффициеент теплоотдачи от крышки канала к материалу
        /// </summary>
        public double? HeatCoef
        {
            get
            {
                return GetMatValueByPropertyName("Коэффициеент теплоотдачи от крышки канала к материалу", MatModel)
                    .Value1;
            }
            set
            {
                GetMatValueByPropertyName("Коэффициеент теплоотдачи от крышки канала к материалу", MatModel).Value1 =
                    value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Graphics

        /// <summary>
        /// Список со значениями температуры и вязкости на протяжении канала
        /// </summary>
        public List<CordTempN> CordTempNs
        {
            get
            {
                return Results.cordTempNs;
            }
        }

        /// <summary>
        /// Серия точек температуры
        /// </summary>
        private LineSeries tempLineSerie { get; set; }

        public SeriesCollection TempSeries { get; set; }

        private double _tempAxisXStep;

        //public double TempAxisXStep
        //{
        //    get { return _tempAxisXStep; }
        //    set
        //    {
        //        _tempAxisXStep = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private double _tempAxisYStep;

        //public double TempAxisYStep
        //{
        //    get { return _tempAxisYStep; }
        //    set
        //    {
        //        _tempAxisYStep = value;
        //        OnPropertyChanged();
        //    }
        //}
        /// <summary>
        /// Серия точек вязкости
        /// </summary>
        private LineSeries nLineSerie { get; set; }

        public SeriesCollection NSeries { get; set; }
        private double _nAxisXStep;

        //public double NAxisXStep
        //{
        //    get { return _nAxisXStep; }
        //    set
        //    {
        //        _nAxisXStep = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private double _nAxisYStep;

        //public double NAxisYStep
        //{
        //    get { return _nAxisYStep; }
        //    set
        //    {
        //        _nAxisYStep = value;
        //        OnPropertyChanged();
        //    }
        //}

        #endregion

        #region Timers

        /// <summary>
        /// Таймер для времени расчета
        /// </summary>
        public Stopwatch MathTimer { get; set; } = new();

        #endregion

        /// <summary>
        /// Текущая занаятая память
        /// </summary>
        public long TotalMemory
        {
            get
            {
                Process currentProcess = Process.GetCurrentProcess();
                return currentProcess.WorkingSet64 / (1024 * 1024);
            }
        }

        private CalculationResults results;

        /// <summary>
        /// Полученные результаты
        /// </summary>
        public CalculationResults Results
        {
            get { return results; }
            set
            {
                results = value;
                OnPropertyChanged();

                //TempAxisXStep = stepByDictionary(results.Ni.Keys.ToList());
                //TempAxisYStep = stepByDictionary( results.Ni.Values.ToList());
                //NAxisXStep = stepByDictionary( results.Ni.Keys.ToList());
                //NAxisYStep = stepByDictionary( results.Ni.Values.ToList());
                var x = Results.cordTempNs.Select(x => x.cord).ToList();
                var n = Results.cordTempNs.Select(x => x.n).ToList();
                var t = Results.cordTempNs.Select(x => x.temp).ToList();

                updateLineSeriesByCordAndValue(tempLineSerie, x,t);
                updateLineSeriesByCordAndValue(nLineSerie, x,n);

                OnPropertyChanged(nameof(TempSeries));
                OnPropertyChanged(nameof(NSeries));

                OnPropertyChanged(nameof(CordTempNs));
                OnPropertyChanged(nameof(MathTimer));
                OnPropertyChanged(nameof(TotalMemory));
            }
        }

        /// <summary>
        /// Шаг расчета
        /// </summary>
        public double? Step { get; set; } = 0.1;

        public bool IsCalculated = false;

        #endregion

        #region Commands

        private RelayCommand _calcCommand;

        /// <summary>
        /// Команда, выполняющая расчет
        /// </summary>
        public RelayCommand CalcCommand
        {
            get
            {
                return _calcCommand ?? (_calcCommand = new RelayCommand(o =>
                {
                    IsCalculated = true;
                    MathTimer.Reset();
                    MathTimer.Start();
                    var cp = new CalculationParameters()
                    {
                        W = (double) Width,
                        H = (double) Depth,
                        L = (double) Length,
                        p = (double) Density,
                        c = (double) SpecifiсHeatCapacity,
                        T0 = (double) MeltingTemperature,
                        Vu = (double) CapSpeed,
                        Tu = (double) CapTemperature,
                        u0 = (double) СonsСoef,
                        b = (double) TempСoef,
                        Tr = (double) RefTemp,
                        n = (double) MatFlowIndex,
                        au = (double) HeatCoef,
                        step = (double) Step
                    };
                    var mc = new MathClass(cp);
                    Results = mc.calculate();
                    MathTimer.Stop();
                    OnPropertyChanged(nameof(MathTimer));
                }));
            }
        }

        #endregion
    }
}